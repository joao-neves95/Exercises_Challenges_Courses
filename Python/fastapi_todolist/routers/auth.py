from datetime import timedelta
from typing import Annotated
from fastapi import APIRouter, Path, Depends
from fastapi.security import OAuth2PasswordBearer
from jwt import InvalidTokenError
from pydantic import AfterValidator

from data.entities.data_user import DataUser, DataUserCredentials
from lib.crypto import hash_password, rehash_password
from lib.HTTPException_utils import (
    invalid_credentials_exception,
    raise_if_user_has_no_permissions,
    invalid_login_credentials_exception,
)
from lib.jwt_utils import (
    JWT_EXPIRE_MINUTES,
    create_access_token,
    decode_token,
    is_user_jwt_admin,
)
from lib.ulid_validators import validate_str_ulid
from models.jwt import JwtToken, JwtTokenDataInput
from models.mapper_utils import data_user_to_model
from models.common import StatusResponse
from models.users import LoginUser, RegisterUser, User
from data.query_utils import select_user_by_email, select_user_by_ulid


api_users_router = APIRouter(prefix="/auth")

oauth2_scheme = OAuth2PasswordBearer(tokenUrl="logins")


def get_is_user_jwt_admin(token: Annotated[str, Depends(oauth2_scheme)]) -> bool:
    try:
        return is_user_jwt_admin(token)

    except InvalidTokenError:
        raise invalid_credentials_exception


async def get_jwt_data_user(token: Annotated[str, Depends(oauth2_scheme)]) -> DataUser:
    try:
        token_data = decode_token(token=token)

    except InvalidTokenError:
        raise invalid_credentials_exception

    user = await select_user_by_ulid(token_data.sub)
    if user is None:
        raise invalid_credentials_exception

    return user


async def get_jwt_user(token: Annotated[str, Depends(oauth2_scheme)]) -> User:
    data_user = await get_jwt_data_user(token)
    return data_user_to_model(data_user)


@api_users_router.post("/")
async def create_user(register_user_model: RegisterUser) -> StatusResponse:
    # TODO: check if user exists by email.
    (hash, salt) = hash_password(register_user_model.password)

    new_user = await DataUser.create()
    _ = await DataUserCredentials.create(
        user_id=new_user.id,
        email=register_user_model.email,
        password_hash=hash,
        salt=salt,
    )

    return StatusResponse(status_code=201, message=f"User {new_user.ulid} created")


@api_users_router.put("/{user_ulid}/credentials")
async def update_user_credentials(
    user_ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)],
    register_user_model: RegisterUser,
    current_data_user: Annotated[DataUser, Depends(get_jwt_data_user)],
) -> StatusResponse:
    raise_if_user_has_no_permissions(
        token_user_ulid=current_data_user.ulid, request_user_ulid=user_ulid
    )

    (hash, salt) = hash_password(register_user_model.password)
    current_data_user.credentials.password_hash = hash
    current_data_user.credentials.salt = salt
    await current_data_user.credentials.save()

    return StatusResponse(
        status_code=204, message=f"User '{user_ulid}' credentials updated"
    )


@api_users_router.post("/logins")
async def login_user(
    user_ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)],
    login_user_model: LoginUser,
) -> StatusResponse[JwtToken]:
    data_user = await select_user_by_email(login_user_model.email)

    if data_user is None:
        raise invalid_login_credentials_exception

    hashed_password = rehash_password(
        login_user_model.password, data_user.credentials.salt
    )

    if hashed_password != data_user.credentials.password_hash:
        raise invalid_login_credentials_exception

    access_token = create_access_token(
        JwtTokenDataInput(sub=user_ulid, admin=False),
        # TODO: Use dotenv.
        expires_delta=timedelta(minutes=JWT_EXPIRE_MINUTES),
    )

    return StatusResponse(
        status_code=200,
        message="Authentication successful",
        content=JwtToken(access_token=access_token, token_type="bearer"),
    )

from datetime import timedelta
from typing import Annotated
from fastapi import APIRouter, Path, Depends
from fastapi.security import OAuth2PasswordBearer, OAuth2PasswordRequestForm
from jwt import InvalidTokenError
from pydantic import AfterValidator

from data.entities.data_user import DataUser, DataUserCredentials
from lib.crypto import hash_password, verify_password
from lib.HTTPException_utils import (
    invalid_credentials_exception,
    raise_if_user_has_no_permissions,
    invalid_login_credentials_exception,
    email_already_exists_exception,
)
from lib.jwt_utils import (
    JWT_EXPIRE_MINUTES,
    create_access_token,
    decode_token,
    is_user_jwt_admin,
)
from lib.ulid_validators import validate_str_ulid
from models.jwt import JwtToken, JwtTokenDataInput
from models.mapper_utils import data_user_to_model_async
from models.common import StatusResponse
from models.users import LoginUser, LoginUserResponse, RegisterUser, User
from data.query_utils import select_user_by_email_async, select_user_by_ulid_async


api_auth_router = APIRouter(prefix="/auth")

oauth2_scheme = OAuth2PasswordBearer(tokenUrl="api/v1/auth/openapi/logins")


def get_is_user_jwt_admin(token: Annotated[str, Depends(oauth2_scheme)]) -> bool:
    try:
        return is_user_jwt_admin(token)

    except InvalidTokenError:
        raise invalid_credentials_exception


async def get_jwt_data_user_async(
    token: Annotated[str, Depends(oauth2_scheme)],
) -> DataUser:
    try:
        token_data = decode_token(token=token)

    except InvalidTokenError:
        raise invalid_credentials_exception

    user = await select_user_by_ulid_async(token_data.sub)
    if user is None:
        raise invalid_credentials_exception

    return user


async def get_jwt_user_async(token: Annotated[str, Depends(oauth2_scheme)]) -> User:
    data_user = await get_jwt_data_user_async(token)
    return await data_user_to_model_async(data_user)


@api_auth_router.post("/")
async def create_user(register_user_model: RegisterUser) -> StatusResponse:
    data_user = await select_user_by_email_async(register_user_model.email)
    if data_user is not None:
        raise email_already_exists_exception

    (hash, salt) = hash_password(register_user_model.password)

    new_user = await DataUser.create()
    new_user_credentials = await DataUserCredentials.create(
        user_id=new_user.id,
        email=register_user_model.email,
        password_hash=hash,
        salt=salt,
    )

    new_user.credentials = new_user_credentials
    await new_user.save()

    return StatusResponse(status_code=201, message=f"User {new_user.ulid} created")


@api_auth_router.put("/{user_ulid}/credentials")
async def update_user_credentials(
    user_ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)],
    register_user_model: RegisterUser,
    current_data_user: Annotated[DataUser, Depends(get_jwt_data_user_async)],
) -> StatusResponse:
    raise_if_user_has_no_permissions(
        token_user_ulid=current_data_user.ulid, request_user_ulid=user_ulid
    )

    (hash, salt) = hash_password(register_user_model.password)
    current_data_user.credentials = DataUserCredentials()
    current_data_user.credentials.password_hash = hash
    current_data_user.credentials.salt = salt
    await current_data_user.credentials.save()

    return StatusResponse(
        status_code=204, message=f"User '{user_ulid}' credentials updated"
    )


@api_auth_router.post("/logins", responses={401: {"description": "Unauthorized"}})
async def login_user(
    login_user_model: LoginUser,
) -> StatusResponse[LoginUserResponse]:
    data_user, access_token = await login_async(login_user_model.email, login_user_model.password)

    return StatusResponse(
        status_code=200,
        message="Authentication successful",
        content=LoginUserResponse(
            user=await data_user_to_model_async(data_user),
            token=JwtToken(access_token=access_token, token_type="bearer"),
        ),
    )


@api_auth_router.post(
    "/openapi/logins", responses={401: {"description": "Unauthorized"}}
)
async def login_open_api(
    login_form_data: OAuth2PasswordRequestForm = Depends(),
) -> JwtToken:
    _, access_token = await login_async(login_form_data.username, login_form_data.password)

    return JwtToken(access_token=access_token, token_type="bearer")


async def login_async(email: str, password: str) -> tuple[DataUser, str]:
    data_user = await select_user_by_email_async(email)

    if data_user is None or data_user.credentials is None:
        raise invalid_login_credentials_exception

    if not verify_password(
        data_user.credentials.password_hash,
        password,
        data_user.credentials.salt,
    ):
        raise invalid_login_credentials_exception

    return (
        data_user,
        create_access_token(
            JwtTokenDataInput(sub=data_user.ulid, admin=True),
            # TODO: Use dotenv.
            expires_delta=timedelta(minutes=JWT_EXPIRE_MINUTES),
        ),
    )

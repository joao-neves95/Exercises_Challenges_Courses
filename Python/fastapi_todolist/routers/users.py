from uuid import uuid4
from fastapi import APIRouter, HTTPException
from pydantic_extra_types.ulid import ULID as pydantic_ulid
from argon2 import PasswordHasher

from entities.data_user import DataUser
from lib.list_utils import try_get
from models.common import StatusResponse
from models.users import RegisterUser, User


api_users_router = APIRouter(prefix="/users")


# TODO: add pagination.
# TODO: add admin JWT permission.
@api_users_router.get("/")
async def get_users():
    all_users = await DataUser.all()
    all_users = [User.from_orm_data(data_user) for data_user in all_users]

    return all_users


@api_users_router.get("/{ulid}")
async def get_user(ulid: pydantic_ulid):
    data_user = try_get(await DataUser.filter(ulid=ulid), 0)

    if data_user is None:
        raise HTTPException(status_code=404, detail=f"User {ulid} not found")

    return StatusResponse(
        status_code=200,
        message=f"User '{ulid}' found",
        content=User.from_orm_data(data_user),
    )


def hash_password(password: str) -> tuple[str, str]:
    password_hasher = PasswordHasher()
    salt = str(uuid4())
    hash = password_hasher.hash(password + salt)

    return (hash, salt)


@api_users_router.post("/")
async def create_user(register_user_model: RegisterUser):
    (hash, salt) = hash_password(register_user_model.password)

    new_user = await DataUser.create(
        email=register_user_model.email, password_hash=hash, salt=salt
    )

    return StatusResponse(status_code=201, message=f"User {new_user.ulid} created")


@api_users_router.put("/{ulid}/credentials")
async def update_user_credentials(
    ulid: pydantic_ulid, register_user_model: RegisterUser
):
    data_user = try_get(await DataUser.filter(ulid=ulid), 0)

    if data_user is None:
        raise HTTPException(status_code=404, detail=f"User {ulid} not found")

    (hash, salt) = hash_password(register_user_model.password)
    data_user.password_hash = hash
    data_user.salt = salt
    await data_user.save()

    return StatusResponse(status_code=204, message=f"User '{ulid}' credentials updated")


@api_users_router.put("/{ulid}")
async def update_user(ulid: pydantic_ulid, user_model: User):
    data_user = try_get(await DataUser.filter(ulid=ulid), 0)

    if data_user is None:
        raise HTTPException(status_code=404, detail=f"User {ulid} not found")

    data_user.update_data(user_model)
    await data_user.save()
    
    return StatusResponse(status_code=204, message=f"User '{ulid}' updated")

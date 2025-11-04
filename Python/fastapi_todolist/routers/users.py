from typing import Annotated
from fastapi import APIRouter, Depends, Path
from pydantic import AfterValidator

from data.entities.data_user import DataUser
from lib.HTTPException_utils import (
    raise_if_user_has_no_permissions,
    user_has_no_permissions_exception,
)
from lib.ulid_validators import validate_str_ulid
from models.common import StatusResponse
from models.mapper_utils import data_user_to_model_async, update_data_user_from_model
from models.users import User
from routers.auth import get_is_user_jwt_admin, get_jwt_data_user_async, get_jwt_user_async

api_users_router = APIRouter(prefix="/users")

# TODO: add pagination.
@api_users_router.get("/")
async def get_users(
    is_current_user_admin: Annotated[bool, Depends(get_is_user_jwt_admin)],
):
    if not is_current_user_admin:
        raise user_has_no_permissions_exception

    all_users = await DataUser.all()
    all_users = [await data_user_to_model_async(data_user) for data_user in all_users]

    return all_users


@api_users_router.get("/{ulid}")
async def get_user(
    ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)],
    current_user: Annotated[User, Depends(get_jwt_user_async)],
):
    raise_if_user_has_no_permissions(
        token_user_ulid=current_user.ulid, request_user_ulid=current_user.ulid
    )

    return StatusResponse(
        status_code=200,
        message=f"User '{ulid}' found",
        content=current_user,
    )


@api_users_router.put("/{user_ulid}")
async def update_user(
    user_ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)],
    user_model: User,
    current_data_user: Annotated[DataUser, Depends(get_jwt_data_user_async)],
) -> StatusResponse[User]:
    raise_if_user_has_no_permissions(
        token_user_ulid=current_data_user.ulid, request_user_ulid=user_ulid
    )

    update_data_user_from_model(current_data_user, user_model)
    await current_data_user.save()

    return StatusResponse(
        status_code=204,
        message=f"User '{user_ulid}' updated",
        content=await data_user_to_model_async(current_data_user),
    )

from typing import Annotated
from fastapi import APIRouter, Depends, HTTPException, Path

from pydantic import AfterValidator
from tortoise.contrib.pydantic import pydantic_model_creator
from tortoise.transactions import in_transaction

from data.entities.data_item import DataItem
from data.entities.data_user import DataUser
from lib.HTTPException_utils import raise_if_user_has_no_permissions
from lib.list_utils import try_get
from lib.ulid_validators import validate_str_ulid
from models.mapper_utils import data_item_to_model, update_data_item_from_model
from models.common import StatusResponse
from models.items import Item, NewItem
from models.users import User
from routers.auth import get_jwt_data_user_async, get_jwt_user_async

api_user_items_router = APIRouter(prefix="/users/{user_ulid}/items")


# TODO: add pagination.
@api_user_items_router.get("/")
async def get_all_items(
    user_ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)],
    current_user: Annotated[User, Depends(get_jwt_user_async)],
) -> StatusResponse[list[Item]]:
    raise_if_user_has_no_permissions(
        token_user_ulid=current_user.ulid, request_user_ulid=user_ulid
    )

    all_user_items = await DataItem.filter(user__ulid=current_user.ulid)

    all_user_items = [
        data_item_to_model(item, str(user_ulid)) for item in all_user_items
    ]

    return StatusResponse(
        status_code=200,
        message=f"All items for user '{user_ulid}'",
        content=all_user_items,
    )


@api_user_items_router.get("/{ulid}", responses={404: {"description": "Not found"}})
async def get_item(
    user_ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)],
    ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)],
    current_user: Annotated[User, Depends(get_jwt_user_async)],
):
    raise_if_user_has_no_permissions(
        token_user_ulid=current_user.ulid, request_user_ulid=user_ulid
    )

    data_item = try_get(await DataItem.filter(ulid=ulid), 0)

    if data_item is None:
        raise HTTPException(status_code=404, detail=f"Item '{ulid} not found")

    return StatusResponse(
        status_code=200,
        message=f"Item '{data_item.ulid}' found",
        content=data_item_to_model(data_item=data_item, user_ulid=str(user_ulid)),
    )


@api_user_items_router.post("/")
async def create_item(
    user_ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)],
    item: NewItem,
    current_data_user: Annotated[DataUser, Depends(get_jwt_data_user_async)],
):
    raise_if_user_has_no_permissions(
        token_user_ulid=current_data_user.ulid, request_user_ulid=user_ulid
    )

    new_data_item = update_data_item_from_model(DataItem(), item, current_data_user)

    async with in_transaction():
        await new_data_item.save()

    return StatusResponse(
        status_code=201,
        message=f"Item '{new_data_item.ulid}' created",
        content=data_item_to_model(data_item=new_data_item, user_ulid=str(user_ulid)),
    )


@api_user_items_router.put("/{ulid}")
async def update_item(
    user_ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)],
    ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)],
    item: Item,
    current_data_user: Annotated[DataUser, Depends(get_jwt_data_user_async)],
):
    raise_if_user_has_no_permissions(
        token_user_ulid=current_data_user.ulid, request_user_ulid=user_ulid
    )

    data_item = await DataItem.get(ulid=ulid)
    update_data_item_from_model(data_item, item, current_data_user)
    await data_item.save()

    return StatusResponse(
        status_code=200,
        message=f"Item '{ulid}' updated",
        content=data_item_to_model(data_item=data_item, user_ulid=str(user_ulid)),
    )


@api_user_items_router.delete("/{ulid}")
async def delete_item(
    user_ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)],
    ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)],
    current_data_user: Annotated[DataUser, Depends(get_jwt_data_user_async)],
):
    raise_if_user_has_no_permissions(
        token_user_ulid=current_data_user.ulid, request_user_ulid=user_ulid
    )

    deleted_count = await DataItem.filter(ulid=ulid, user_id=current_data_user.id).delete()

    if not deleted_count:
        raise HTTPException(status_code=404, detail=f"Item '{ulid}' not found")

    return StatusResponse(
        status_code=204, message=f"Item '{ulid}' deleted", content=None
    )

from typing import Annotated
from fastapi import APIRouter, HTTPException, Path

from pydantic import AfterValidator
from tortoise.transactions import in_transaction

from entities.data_item import DataItem
from entities.data_user import DataUser
from lib.list_utils import try_get
from lib.ulid_validators import validate_str_ulid
from mappers import data_item_to_model, update_data_item_from_model
from models.common import StatusResponse
from models.items import Item, NewItem

api_user_items_router = APIRouter(prefix="/users/{user_ulid}/items")


# TODO: add pagination.
# TODO: only show the items of the user in session.
@api_user_items_router.get("/")
async def get_all_items(
    user_ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)],
) -> StatusResponse[list[Item]]:
    all_user_items = await DataItem.filter(user_ulid=user_ulid)

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
):
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
    user_ulid: Annotated[str, Path(), AfterValidator(validate_str_ulid)], item: NewItem
):
    user = await DataUser.get(ulid=user_ulid)

    if user is None:
        raise HTTPException(status_code=400, detail="User does not exist")

    new_data_item = update_data_item_from_model(DataItem(), item, user.id)

    async with in_transaction():
        new_data_item = await DataItem.create(**new_data_item.__dict__)

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
):
    user = await DataUser.get(ulid=str(user_ulid))

    if user is None:
        raise HTTPException(status_code=400, detail="User does not exist")

    data_item = await DataItem.get(ulid=ulid)
    update_data_item_from_model(data_item, item, user.id)
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
):
    deleted_count = await DataItem.filter(ulid=ulid, user_ulid=user_ulid).delete()

    if not deleted_count:
        raise HTTPException(status_code=404, detail=f"Item '{ulid}' not found")

    return StatusResponse(
        status_code=204, message=f"Item '{ulid}' deleted", content=None
    )

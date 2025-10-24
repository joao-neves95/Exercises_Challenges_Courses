from fastapi import APIRouter, HTTPException
from pydantic_extra_types.ulid import ULID as pydantic_ulid
from tortoise.transactions import in_transaction

from entities.data_item import DataItem
from entities.data_user import DataUser
from lib.list_utils import try_get
from models.items import Item, ItemStatusResponse, NewItem, StatusResponse

api_items_router = APIRouter(prefix="users/{user_ulid}/items")


# TODO: add pagination.
# TODO: only show the items of the user in session.
@api_items_router.get("/")
async def get_all_items(user_ulid: pydantic_ulid) -> StatusResponse[list[Item]]:
    all_user_items = await DataItem.filter(user_ulid=user_ulid)

    all_user_items = [
        Item.from_orm_data(item, str(user_ulid)) for item in all_user_items
    ]

    return StatusResponse(
        status_code=200,
        message=f"All items for user '{user_ulid}'",
        content=all_user_items,
    )


@api_items_router.get("/{ulid}", responses={404: {"description": "Not found"}})
async def get_item(user_ulid: pydantic_ulid, ulid: pydantic_ulid):
    data_item = try_get(await DataItem.filter(ulid=ulid), 0)

    if data_item is None:
        raise HTTPException(status_code=404, detail=f"Item '{ulid} not found")

    return ItemStatusResponse(
        status_code=200,
        message=f"Item '{data_item.ulid}' found",
        content=Item.from_orm_data(data_item, str(user_ulid)),
    )


@api_items_router.post("/")
async def create_item(user_ulid: pydantic_ulid, item: NewItem):
    user = await DataUser.get(ulid=item.user_ulid)

    if user is None:
        raise HTTPException(status_code=400, detail="User does not exist")

    new_data_item = DataItem.from_new_model(item, user)

    async with in_transaction():
        new_data_item = await DataItem.create(**new_data_item.__dict__)

    return ItemStatusResponse(
        status_code=201,
        message=f"Item '{new_data_item.ulid}' created",
        content=Item.from_orm_data(new_data_item, user.ulid),
    )


@api_items_router.put("/{ulid}")
async def update_item(user_ulid: pydantic_ulid, ulid: pydantic_ulid, item: Item):
    ulid_str = str(ulid)
    user = await DataUser.get(ulid=str(item.user_ulid))

    if user is None:
        raise HTTPException(status_code=400, detail="User does not exist")

    data_item = (await DataItem.get(ulid=ulid_str)).update_data(item, user)
    await data_item.save()

    return ItemStatusResponse(
        status_code=200,
        message=f"Item '{ulid_str}' updated",
        content=Item.from_orm_data(data_item, user.ulid),
    )


@api_items_router.delete("/{ulid}")
async def delete_item(user_ulid: pydantic_ulid, ulid: pydantic_ulid):
    ulid_str = str(ulid)
    deleted_count = await DataItem.filter(ulid=ulid_str).delete()

    if not deleted_count:
        raise HTTPException(status_code=404, detail=f"Item '{ulid_str}' not found")

    return StatusResponse(
        status_code=204, message=f"Item '{ulid_str}' deleted", content=None
    )

from pydantic import BaseModel
from pydantic_extra_types.ulid import ULID as pydantic_ulid

from lib.ulid_utils import pydantic_ulid_from_str
from entities.data_item import DataItem
from models.common import StatusResponse


class NewItem(BaseModel):
    user_ulid: pydantic_ulid
    title: str
    description: str
    done: bool

    @classmethod
    def from_orm_data(cls, data_item: DataItem, user_ulid: str):
        return cls(
            user_ulid=pydantic_ulid_from_str(user_ulid),
            title=data_item.title,
            description=data_item.description,
            done=data_item.done,
        )


class Item(NewItem):
    ulid: pydantic_ulid

    @classmethod
    def from_orm_data(cls, data_item: DataItem, user_ulid: str):
        item = super().from_orm_data(data_item, user_ulid)
        item.ulid = pydantic_ulid_from_str(user_ulid)

        return item


class ItemStatusResponse(StatusResponse):
    content: Item

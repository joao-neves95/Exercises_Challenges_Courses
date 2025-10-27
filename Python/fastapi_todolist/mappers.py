from entities.data_item import DataItem
from entities.data_user import DataUser
from models.items import Item, NewItem
from models.users import User


def data_item_to_model(data_item: DataItem, user_ulid: str):
    return Item(
        ulid=data_item.ulid,
        user_ulid=user_ulid,
        title=data_item.title,
        description=data_item.description,
        done=data_item.done,
    )


def update_data_item_from_model(
    data_item: DataItem, item: Item | NewItem, user_id: int
):
    data_item.user_id = user_id
    data_item.title = item.title
    data_item.description = item.description
    data_item.done = item.done


def update_data_user_from_model(data_user: DataUser, user: User):
    data_user.first_name = user.first_name
    data_user.last_name = user.last_name

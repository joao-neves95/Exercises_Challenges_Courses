from data.entities.data_item import DataItem
from data.entities.data_user import DataUser
from models.items import Item, NewItem
from models.users import User, UserCredentials


def data_item_to_model(data_item: DataItem, user_ulid: str):
    return Item(
        ulid=data_item.ulid,
        user_ulid=user_ulid,
        title=data_item.title,
        description=data_item.description,
        done=data_item.done,
    )


def update_data_item_from_model(
    data_item: DataItem, item: Item | NewItem, data_user: DataUser
) -> DataItem:
    data_item.user = data_user
    data_item.title = item.title
    data_item.description = item.description
    data_item.done = item.done
    
    return data_item


async def data_user_to_model_async(data_user: DataUser):
    data_user_credentials = (
        await data_user.credentials if data_user.credentials else None
    )

    return User(
        ulid=data_user.ulid,
        first_name=data_user.first_name,
        last_name=data_user.last_name,
        credentials=UserCredentials(email=data_user_credentials.email)
        if data_user_credentials
        else None,
    )


def update_data_user_from_model(data_user: DataUser, user: User):
    data_user.first_name = user.first_name or ""
    data_user.last_name = user.last_name or ""

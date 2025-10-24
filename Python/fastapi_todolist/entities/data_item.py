from tortoise import Model, fields

from entities.data_user import DataUser
from lib.ulid_utils import new_ulid_str
from models.items import Item, NewItem


class DataItem(Model):
    id = fields.IntField(primary_key=True)
    ulid = fields.CharField(default=new_ulid_str(), unique=True, index=True)
    user_id = fields.ForeignKeyField(
        "entities.user", related_name="items", on_delete=fields.OnDelete.CASCADE
    )
    title = fields.CharField(max_length=50)
    description = fields.CharField(max_length=200)
    done = fields.BooleanField(default=False)
    created_at = fields.DatetimeField(auto_now_add=True)
    updated_at = fields.DatetimeField(auto_now=True)

    def update_data(self, item: Item | NewItem, data_user: DataUser):
        self.user_id = data_user.id
        self.title = item.title
        self.description = item.description
        self.done = item.done

        return self

    @classmethod
    def from_new_model(cls, new_item: Item | NewItem, data_user: DataUser):
        new_data_item = cls()
        new_data_item.update_data(new_item, data_user)

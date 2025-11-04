from tortoise import Model, fields

from data.entities.data_user import DataUser
from lib.ulid_utils import new_ulid_str


class DataItem(Model):
    id = fields.IntField(primary_key=True)
    ulid = fields.CharField(
        default=new_ulid_str(), max_length=27, unique=True, index=True
    )

    user: fields.ForeignKeyRelation[DataUser] = fields.ForeignKeyField(
        "entities.DataUser",
        related_name="DataItem",
        on_delete=fields.OnDelete.CASCADE,
    )

    title = fields.CharField(max_length=50)
    description = fields.CharField(max_length=200)
    done = fields.BooleanField(default=False)
    created_at = fields.DatetimeField(auto_now_add=True)
    updated_at = fields.DatetimeField(auto_now=True)

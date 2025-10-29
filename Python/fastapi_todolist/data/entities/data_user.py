from typing import Optional
from tortoise import Model, fields

from lib.ulid_utils import new_ulid_str


class DataUserCredentials(Model):
    id = fields.IntField(primary_key=True)
    user_id = fields.ForeignKeyField(
        "data.entities.DataUser",
        related_name="DataUserCredentials",
        on_delete=fields.OnDelete.CASCADE,
    )
    email = fields.CharField(max_length=150, unique=True)
    password_hash = fields.CharField(max_length=150)
    salt = fields.CharField(max_length=150, unique=True)
    created_at = fields.DatetimeField(auto_now_add=True)
    updated_at = fields.DatetimeField(auto_now=True)


class DataUser(Model):
    id = fields.IntField(primary_key=True)
    ulid = fields.CharField(
        default=new_ulid_str(), max_length=27, unique=True, index=True
    )
    first_name = fields.CharField(max_length=20, null=True)
    last_name = fields.CharField(max_length=20, null=True)
    # profile_picture

    credentials: fields.OneToOneRelation[DataUserCredentials] = fields.OneToOneField(
        "data.entities.DataUserCredentials", on_delete=fields.CASCADE
    )

    created_at = fields.DatetimeField(auto_now_add=True)
    updated_at = fields.DatetimeField(auto_now=True)

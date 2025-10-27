from tortoise import Model, fields

from lib.ulid_utils import new_ulid_str


class DataUser(Model):
    id = fields.IntField(primary_key=True)
    ulid = fields.CharField(default=new_ulid_str(), max_length=27, unique=True, index=True)
    email = fields.CharField(max_length=150, unique=True)
    password_hash = fields.CharField(max_length=150)
    salt = fields.CharField(max_length=150, unique=True)
    first_name = fields.CharField(max_length=20, null=True)
    last_name = fields.CharField(max_length=20, null=True)
    created_at = fields.DatetimeField(auto_now_add=True)
    updated_at = fields.DatetimeField(auto_now=True)

from tortoise import Model, fields

from lib.ulid_utils import new_ulid_str
from models.users import User


class DataUser(Model):
    id = fields.IntField(primary_key=True)
    ulid = fields.CharField(default=new_ulid_str(), unique=True, index=True)
    email = fields.CharField(max_length=150, unique=True)
    password_hash = fields.CharField(max_length=150)
    salt = fields.CharField(max_length=150, unique=True)
    first_name = fields.CharField(max_length=20, null=True)
    last_name = fields.CharField(max_length=20, null=True)
    created_at = fields.DatetimeField(auto_now_add=True)
    updated_at = fields.DatetimeField(auto_now=True)

    def update_data(self, user: User):
        self.first_name = user.first_name
        self.last_name = user.last_name

        return self

    @classmethod
    def from_new_model(cls, new_user: User):
        new_data_item = cls()
        new_data_item.update_data(new_user)

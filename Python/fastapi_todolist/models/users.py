from pydantic import BaseModel
from pydantic_extra_types.ulid import ULID as pydantic_ulid

from entities.data_user import DataUser
from lib.ulid_utils import pydantic_ulid_from_str
from models.items import StatusResponse


class LoginUser(BaseModel):
    email: str
    password: str


class RegisterUser(BaseModel):
    email: str
    password: str


class User(BaseModel):
    ulid: pydantic_ulid
    first_name: str
    last_name: str

    @classmethod
    def from_orm_data(cls, data_user: DataUser):
        return cls(
            ulid=pydantic_ulid_from_str(data_user.ulid),
            first_name=data_user.first_name,
            last_name=data_user.last_name,
        )


class UserStatusResponse(StatusResponse):
    content: User

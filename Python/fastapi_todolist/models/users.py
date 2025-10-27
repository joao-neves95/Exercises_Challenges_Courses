from typing import Annotated
from pydantic import AfterValidator, BaseModel

from entities.data_user import DataUser
from lib.ulid_validators import validate_str_ulid


class LoginUser(BaseModel):
    email: str
    password: str


class RegisterUser(BaseModel):
    email: str
    password: str


class User(BaseModel):
    ulid: Annotated[str, AfterValidator(validate_str_ulid)]
    first_name: str
    last_name: str

    @classmethod
    def from_orm_data(cls, data_user: DataUser):
        return cls(
            ulid=data_user.ulid,
            first_name=data_user.first_name,
            last_name=data_user.last_name,
        )

from typing import Annotated
from pydantic import AfterValidator, BaseModel

from lib.ulid_validators import validate_str_ulid


class LoginUser(BaseModel):
    email: str
    password: str


class RegisterUser(BaseModel):
    email: str
    password: str


class UserCredentials(BaseModel):
    email: str


class User(BaseModel):
    ulid: Annotated[str, AfterValidator(validate_str_ulid)]
    first_name: str
    last_name: str
    credentials: UserCredentials

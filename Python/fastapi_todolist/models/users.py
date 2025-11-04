from typing import Annotated
from pydantic import AfterValidator, BaseModel, EmailStr

from lib.ulid_validators import validate_str_ulid
from models.jwt import JwtToken


class UserCredentials(BaseModel):
    email: str


class User(BaseModel):
    ulid: Annotated[str, AfterValidator(validate_str_ulid)]
    first_name: str | None
    last_name: str | None
    credentials: UserCredentials | None


class LoginUser(BaseModel):
    email: EmailStr
    password: str


class LoginOpenApi(BaseModel):
    username: EmailStr
    password: str


class LoginUserResponse(BaseModel):
    user: User
    token: JwtToken


class RegisterUser(BaseModel):
    email: EmailStr
    password: str

from typing import Annotated
from pydantic import AfterValidator, BaseModel

from lib.ulid_validators import validate_str_ulid


class NewItem(BaseModel):
    user_ulid: Annotated[str, AfterValidator(validate_str_ulid)]
    title: str
    description: str
    done: bool = False


class Item(NewItem):
    ulid: Annotated[str, AfterValidator(validate_str_ulid)]

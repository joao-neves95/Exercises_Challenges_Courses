from typing import Generic, TypeVar

from pydantic import BaseModel


TContent = TypeVar("TContent")


class StatusResponse(BaseModel, Generic[TContent]):
    status_code: int = 200
    message: str
    content: TContent | None = None

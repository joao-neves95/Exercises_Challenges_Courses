from datetime import datetime
from pydantic import BaseModel


class JwtToken(BaseModel):
    access_token: str
    token_type: str

class JwtTokenDataInput(BaseModel):
    sub: str
    """ The user ULID. """
    admin: bool
    """ If this user has admin permissions. """

class JwtTokenData(BaseModel):
    sub: str
    """ The user ULID. """
    exp: datetime
    admin: bool
    """ If this user has admin permissions. """

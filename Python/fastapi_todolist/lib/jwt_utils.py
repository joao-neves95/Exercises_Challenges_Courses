from datetime import timedelta

import jwt

from lib.date_utils import now_utc
from models.jwt import JwtTokenData, JwtTokenDataInput

# TODO: add dotenv
JWT_ALGORITHM = "HS512"
# openssl rand -hex 32
JWT_SECRET_KEY = "f949957bc6601afa135c8daf653c1e9b0cd242eca421105fe54afb6503033957"
JWT_EXPIRE_MINUTES = 10080  # 7 days.
DEFAULT_JWT_EXPIRE_MINUTES = 1440  # 1 day.


def create_access_token(
    jwt_token_data: JwtTokenDataInput, expires_delta: timedelta | None = None
):
    to_encode = jwt_token_data.model_dump()

    if expires_delta:
        expire = now_utc() + expires_delta
    else:
        expire = now_utc() + timedelta(minutes=DEFAULT_JWT_EXPIRE_MINUTES)
    to_encode.update({"exp": expire})

    return jwt.encode(to_encode, JWT_SECRET_KEY, algorithm=JWT_ALGORITHM)


def decode_token(token: str) -> JwtTokenData:
    payload = jwt.decode(token, JWT_SECRET_KEY, algorithms=[JWT_ALGORITHM])

    return JwtTokenData(**payload)


def is_user_jwt_admin(token: str):
    return decode_token(token=token).admin

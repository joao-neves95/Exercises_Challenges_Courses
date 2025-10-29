from argon2 import PasswordHasher
from ulid import ULID


def hash_password(password: str) -> tuple[str, str]:
    password_hasher = PasswordHasher()
    salt = str(ULID())
    hash = password_hasher.hash(password + salt)

    return (hash, salt)


def rehash_password(password: str, salt: str) -> tuple[str, str]:
    password_hasher = PasswordHasher()
    hash = password_hasher.hash(password + salt)

    return (hash, salt)

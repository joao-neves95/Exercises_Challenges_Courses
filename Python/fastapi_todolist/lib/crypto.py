from argon2 import PasswordHasher
from ulid import ULID


def hash_password(password: str) -> tuple[str, str]:
    password_hasher = PasswordHasher()
    salt = str(ULID())
    hash = password_hasher.hash(password + salt)

    return (hash, salt)


def verify_password(hash, cleartext_password: str, salt: str) -> bool:
    """

    Args:
        hash (str): The hashed password.
        password (str): The cleartext password.
        salt (str): The original application salt.

    Returns:
        tuple[str, str]: Returns `True` or `False`
    """
    password_hasher = PasswordHasher()

    try:
        return password_hasher.verify(hash, cleartext_password + salt)
    except Exception:
        return False

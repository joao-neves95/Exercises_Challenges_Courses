from ulid import ULID


def validate_str_ulid(ulid: str) -> str:
    try:
        return str(ULID.from_str(ulid))
    except Exception as e:
        raise ValueError("Invalid ULID format.") from e

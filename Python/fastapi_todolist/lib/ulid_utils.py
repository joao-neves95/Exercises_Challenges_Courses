from uuid import UUID
from pydantic_extra_types.ulid import ULID as pydantic_ulid
from ulid import ULID


def new_uuid_from_ulid() -> UUID:
    return UUID(bytes=ULID().bytes)


def uuid_from_ulid(ulid: ULID) -> UUID:
    return UUID(bytes=ulid.bytes)


def uuid_from_pydantic_ulid(ulid: pydantic_ulid) -> UUID:
    return uuid_from_ulid(ulid.ulid)

def new_ulid_str() -> str:
    return str(ULID())


def ulid_from_uuid(uuid: UUID) -> ULID:
    return ULID.from_bytes(uuid.bytes)


def pydantic_ulid_from_ulid(ulid: ULID) -> pydantic_ulid:
    return pydantic_ulid(ulid)


def pydantic_ulid_from_uuid(ulid: UUID) -> pydantic_ulid:
    return pydantic_ulid_from_ulid(ulid_from_uuid(ulid))


def pydantic_ulid_from_str(ulid_str: str) -> pydantic_ulid:
    return pydantic_ulid(ULID.from_str(ulid_str))

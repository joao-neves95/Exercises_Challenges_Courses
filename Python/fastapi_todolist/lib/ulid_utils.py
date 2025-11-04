from uuid import UUID
from ulid import ULID


def new_uuid_from_ulid() -> UUID:
    return UUID(bytes=ULID().bytes)


def uuid_from_ulid(ulid: ULID) -> UUID:
    return UUID(bytes=ulid.bytes)


def new_ulid_str() -> str:
    return str(ULID())


def ulid_from_uuid(uuid: UUID) -> ULID:
    return ULID.from_bytes(uuid.bytes)

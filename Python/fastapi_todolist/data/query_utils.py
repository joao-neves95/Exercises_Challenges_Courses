from data.entities.data_user import DataUser


async def select_user_by_ulid_async(ulid: str) -> DataUser | None:
    try:
        return await DataUser.filter(ulid=ulid).select_related("credentials").first()
    except Exception:
        return None


async def select_user_by_email_async(email: str) -> DataUser | None:
    try:
        return await DataUser.filter(credentials__email=email).select_related("credentials").first()
    except Exception:
        return None

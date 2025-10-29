from entities.data_user import DataUser


async def select_user_by_ulid(ulid: str):
    return await DataUser.filter(ulid=ulid).select_related("credentials").first()

async def select_user_by_email(email: str):
    return await DataUser.filter(email=email).select_related("credentials").first()

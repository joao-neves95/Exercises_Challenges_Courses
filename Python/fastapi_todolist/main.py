from contextlib import asynccontextmanager
from fastapi import FastAPI
from tortoise.contrib.fastapi import RegisterTortoise, tortoise_exception_handlers

from routers.user_items import api_user_items_router
from routers.users import api_users_router


@asynccontextmanager
async def lifespan(app: FastAPI):
    async with RegisterTortoise(
        app=app,
        # Use UTC.
        use_tz=True,
        modules={"fastapi_todolist": ["entities.data_item", "entities.data_user"]},
        db_url="sqlite://db.sqlite3",
        # Only for DEV.
        generate_schemas=True,
    ):
        yield


app = FastAPI(exception_handlers=tortoise_exception_handlers())
app.include_router(api_user_items_router, prefix="/api/v1")
app.include_router(api_users_router, prefix="/api/v1")

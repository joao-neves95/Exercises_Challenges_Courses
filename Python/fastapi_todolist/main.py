from contextlib import asynccontextmanager
import os
from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from tortoise.contrib.fastapi import RegisterTortoise, tortoise_exception_handlers

from routers.user_items import api_user_items_router
from routers.users import api_users_router
from routers.auth import api_auth_router


@asynccontextmanager
async def lifespan(app: FastAPI):
    async with RegisterTortoise(
        app=app,
        modules={
            "entities": [
                "data.entities.data_item",
                "data.entities.data_user",
            ]
        },
        db_url=f"sqlite:///{os.path.abspath("db.sqlite3")}",
        # Use UTC.
        use_tz=True,
        add_exception_handlers=True,
        # Only for DEV.
        generate_schemas=True,
    ):
        yield


app = FastAPI(lifespan=lifespan, exception_handlers=tortoise_exception_handlers())
app.include_router(api_auth_router, prefix="/api/v1")
app.include_router(api_users_router, prefix="/api/v1")
app.include_router(api_user_items_router, prefix="/api/v1")

origins = [
    "http://localhost",
    "http://localhost:8000",
]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

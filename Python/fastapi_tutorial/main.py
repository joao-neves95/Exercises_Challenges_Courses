from enum import Enum
import random
from typing import Any
from typing import Annotated
from fastapi import FastAPI, Path, Query
from fastapi.responses import JSONResponse
from pydantic import AfterValidator, BaseModel


class Item(BaseModel):
    name: str
    description: str | None = None
    price: float
    tax: float | None = None


class ModelName(str, Enum):
    alex_net = "alexnet"
    res_net = "resnet"
    le_net = "lenet"


app = FastAPI()


@app.get("/")
async def root():
    return {"message": "Hello, World!"}


fake_items_db = [{"item_name": "Foo"}, {"item_name": "Bar"}, {"item_name": "Baz"}]


@app.get("/items/", response_class=JSONResponse)
async def get_items(skip: int = 0, limit: int = 10):
    return fake_items_db[skip : skip + limit]


@app.get("/items/{item_id}", response_class=JSONResponse)
async def get_item(item_id: int):
    return {"item_id": item_id}


@app.post("/items/", response_class=JSONResponse)
async def create_item(item: Item):
    item_dict = item.model_dump()
    update_item_model_with_price_with_tax(item, item_dict)

    return item_dict


@app.put("/items/{item_id}")
async def update_item(
    item_id: int,
    item: Item,
    q: Annotated[
        str | None, Query(description="Query string", min_length=1, max_length=3)
    ] = None,
):
    item_dict = item.model_dump()
    update_item_model_with_price_with_tax(item, item_dict)
    result = {"item_id": item_id, **item_dict}

    if q:
        result.update({"q": q})

    return result


def update_item_model_with_price_with_tax(item: Item, item_dict: dict[str, Any]):
    if item.tax is not None:
        price_with_tax = item.price + item.tax
        item_dict.update({"price_with_tax": price_with_tax})


@app.get("/models/{model_name}", response_class=JSONResponse)
async def get_model(model_name: ModelName):
    if model_name is ModelName.alex_net:
        return get_model_response(model_name, "I want to learn Deep Learning")
    elif model_name.value == "lenet":
        return get_model_response(model_name, "I want to learn Machine Leaning")
    else:
        return get_model_response(model_name, "I want to learn AI")


def get_model_response(model_name: str, message: str):
    return {"model_name": model_name, "message": message}


@app.get("/files/{file_path:path}", response_class=JSONResponse)
async def get_file(file_path: str):
    return {"file_path": file_path}


@app.get("/other-items/{item_id}")
async def read_item(item_id: str, name: str | None = None, uppercase: bool = False):
    if name:
        return {"item_id": item_id, "name": name.upper() if uppercase else name}
    return {"item_id": item_id}


def movie_id_validator(id: str):
    if not id.startswith(("isbn-", "imdb-")):
        raise ValueError('Invalid ID format. The ID must start with "isbn-" or "imdb-"')

    return id


movie_data = {
    "isbn-9781529046137": "The Hitchhiker's Guide to the Galaxy",
    "imdb-tt0371724": "The Hitchhiker's Guide to the Galaxy, The Movie",
    "isbn-9781439512982": "Isaac Asimov: The Complete Stories, Vol. 2",
}


@app.get("/movies")
async def get_movie(
    id: Annotated[str | None, Query(), AfterValidator(movie_id_validator)] = None,
):
    return {"id": id, "name": movie_data[id if id else random.choice(list(movie_data.keys()))]}

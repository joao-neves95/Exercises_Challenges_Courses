FROM python:3.14-slim

ENV PYTHONUNBUFFERED=1

WORKDIR /app

# Install UV.
# https://docs.astral.sh/uv/guides/integration/docker/#installing-uv
# https://hub.docker.com/r/astral/uv
COPY --from=docker.io/astral/uv:latest /uv /uvx /bin/

# Activate the project virtual environment by placing its binary directory at the front of the path.
# https://docs.astral.sh/uv/guides/integration/docker/#using-the-environment
ENV PATH="/.venv/bin:$PATH"

# Compile bytecode
# https://docs.astral.sh/uv/guides/integration/docker/#compiling-bytecode
ENV UV_COMPILE_BYTECODE=1

# uv Cache
# https://docs.astral.sh/uv/guides/integration/docker/#caching
ENV UV_LINK_MODE=copy

# Install dependencies.
# https://docs.astral.sh/uv/guides/integration/docker/#intermediate-layers
RUN --mount=type=cache,target=/root/.cache/uv \
    --mount=type=bind,source=uv.lock,target=uv.lock \
    --mount=type=bind,source=pyproject.toml,target=pyproject.toml \
    uv sync --frozen --no-install-project

ENV PYTHONPATH=./

# Copy the project into the image.
COPY . ./

# Sync the project
# Ref: https://docs.astral.sh/uv/guides/integration/docker/#intermediate-layers
RUN --mount=type=cache,target=/root/.cache/uv \
    uv sync

EXPOSE 8000
CMD ["uv", "run", "uvicorn", "main:app", "--host", "0.0.0.0", "--port", "8000", "--workers", "4"]

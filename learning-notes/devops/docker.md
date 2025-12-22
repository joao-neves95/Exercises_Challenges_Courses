# Docker

---

## Introduction

- Open source platform/environment for developing, running and shipping applications.
- Separates the application (the code) from the infrastructure.
- It automates the deployment of applications, as portable and self sufficient containers.
- Good:
  - Reduce time to production.
  - Run anywhere.

---

## Concepts

- Image:
  - A static representation of the application, with its configuration and dependencies.
  - A lightweight, standalone executable package of software that includes everything that is needed to run the application.
  - Stored in a **registry**.
  - Instantiated to create a **container**.
- Container:
  - A standard unit of software (image) that packages up the code and all its dependencies, that is running on the docker host.

---

## The process of containerization

- Build the image:
  - > `build`
  - e.g.: `docker build -t todolist-app:0.1.0 .`
  - -`-no-cache` option.
  - `DOCKER_BUILDKIT=0 docker build --no-cache -t myimage .`
- Run the image to create and run the container:
  - > `run`
  - e.g.: `docker run -p 8000:8000 --name todolist-app todolist-app:0.1.0`

---

## Some commands
<https://docs.docker.com/engine/reference/commandline/docker>

- `run`
  - `-p` (publish/expose)
    - E.g. `docker run -p 127.0.0.1:80:8080/tcp nginx:alpine`
    - Binds port `8080` of the container to TCP port `80` on `127.0.0.1` of the host.
- `docker builder prune --all`
  - To delete all cache.

---

## Docker Compose

### Running

- Build the images:
  - `docker compose build`
  - `docker compose build --no-cache`
- Start the containers:
  - `docker compose up`
  - `docker compose up -d` (to run in detached/background mode)
- To stop:
  - `docker compose down`
  - `docker compose down --volumes=false` (to keep the volumes)

---

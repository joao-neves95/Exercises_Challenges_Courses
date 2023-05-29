# Discount

## Discount.Api

- C#:
  - ASP.NET Core Web API
  - Swagger Open API
  - Libs:
    - Dapper (micro-ORM)
- Architecture:
  - REST API, CRUD
  - N-Layer
    - Presentation layer - (REST API)
    - Business layer
    - Data access layer
      - Repository Pattern
- DB:
  - PostgreSQL
    - https://hub.docker.com/_/postgres
  - pgAdmin
    - https://hub.docker.com/r/dpage/pgadmin4
- DevOps:
  - Docker & Docker Compose

## Discount.Grpc
(for highly performant inter-service communication)

- C#:
  - ASP.NET Core gRPC with Protobuf messages
  - Libs:
    - Dapper (micro-ORM)
- DB:
  - PostgreSQL
- DevOps:
  - Docker & Docker Compose

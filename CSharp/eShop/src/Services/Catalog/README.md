# Catalog.Api

- C#:
  - ASP.NET Core Web API
  - Swagger Open API
- Architecture:
  - REST API, CRUD
  - N-Layer
    - Presentation layer - (REST API)
      - Can only interact with the business layer (can not consume Data directly).
    - Business layer
      - Processes user input. Transmits data to the user.
      - Consumes the data layer.
    - Data access layer
        - Does not have logic, it only adds, deletes, updates and extracts data.
        - Repository Pattern
        - Abstracts the database context.
        - More consistency. Less errors.
        - Easier to maintain.
        - Better testability.
- DB:
  - Mongo DB:
    - https://hub.docker.com/_/mongo
- DevOps:
  - Docker & Docker Compose

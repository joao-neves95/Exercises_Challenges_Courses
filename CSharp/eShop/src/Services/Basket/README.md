# Basket.Api

- C#:
  - ASP.NET Core Web API
  - Swagger Open API
  - Libs:
    - 
- Architecture:
  - REST API, CRUD
  - N-Layer
    - Presentation layer - (REST API)
    - Business layer
    - Data access layer
      - Repository Pattern
- DB:
  - Redis
    - Since the data in this service is temporary, we can use this fast in-memory storage.
- DevOps:
  - Docker & Docker Compose

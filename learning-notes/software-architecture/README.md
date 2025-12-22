# Software Architecture

- Architectures
  - Read more here: <https://learn.microsoft.com/en-us/azure/architecture/patterns>
  - N-Layer:
    - Presentation layer
      - e.g.: REST API.
      - Can only interact with the business layer (can not consume Data directly).
    - Business layer
      - Processes user input. Transmits data to the user.
      - Consumes the data layer.
    - Data access layer
      - Does not have logic, it only adds, deletes, updates and extracts data.
      - Abstracts the database context, e.g.: repository pattern.
      - More consistency. Less errors.
      - Easier to maintain.
      - Better testability.
  - Clean Architecture:
    - The 5 qualities:
      - Framework independence:
        - App is decoupled from third party frameworks.
      - Ease of testability:
        - Unit tests.
      - UI independence.
      - Persistence independence:
        - App is decoupled from the underlying data source.
      - External agency independency:
        - The business rules are isolated and know nothing about the outside world.
    - Read more:
      - <https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html>
  - [Microservices](microservices/microservices.md)
- Internal design patterns:
  - CQRS:
    - Separates the models for reading and writing data.
    - Each layer has its own data model.
    - Usually using the mediator pattern (e.g.: MediatR).
    - Queries:
      - They only return data.
      - There are no side effects.
      - Query - QueryHandler
    - Commands:
      - They change the state of the system.
      - Command - CommandHandler
  - Event Sourcing:
    - Persists the state of a business entity such an Order or a Customer as a sequence of state-changing events.
- Design principles and best practices:
  - [SOLID](./SOLID.md)
  - [DDD](./DDD.md)

---

## Further reading

- **<https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns>**
  - <https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects>
- **<https://learn.microsoft.com/en-us/azure/architecture/guide>**

---

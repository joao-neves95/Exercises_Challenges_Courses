# Microservices Architecture on .NET with applying CQRS, Clean Architecture and Event-Driven Communication

## External Links
- Udemy: https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet
- Written course: https://medium.com/aspnetrun/microservices-architecture-on-net-3b4865eea03f
- GitHub: https://github.com/aspnetrun/run-aspnetcore-microservices

## Solution Links
- src/
  - services/
    - catalog/
      - [eShop.Catalog.Api/](./src/Services/Catalog/eShop.Catalog.Api/)
    - basket/
      - [eShop.Basket.Api/](./src/Services/Basket/eShop.Basket.Api/)
    - discount/
      - [eShop.Discount.Shared/](./src/Services/Discount/eShop.Discount.Shared/)
      - [eShop.Discount.Api/](./src/Services/Discount/eShop.Discount.Api/)
      - [eShop.Discount.Grpc/](./src/Services/Discount/eShop.Discount.Grpc/)
    - ordering/
      - [eShop.Ordering.Api/](./src/Services/Ordering/eShop.Ordering.Api/)
      - [eShop.Ordering.Application/](./src/Ordering/Application/eShop.Application.Api/)
      - [eShop.Ordering.Domain/](./src/Services/Ordering/eShop.Domain.Api/)
      - [eShop.Ordering.Infrastructure/](./src/Ordering/Infrastructure/eShop.Infrastructure.Api/)
- [docker-compose.yml](./docker-compose.yml)

## Docs
- Docker & Docker Compose
  - `docker-compose -f ./docker-compose.yml -f ./docker-compose.vs.debug.yml up -d`

## Services
- Portainer Dashboard:
    - Links:
        - docker: http://127.0.0.1:9100
- pgAdmin Dashboard:
    - Links:
        - docker: http://127.0.0.1:9101
- RabbitMQ Management Dashboard:
    - Links:
        - docker: http://127.0.0.1:15673
- eshop.catalog.api:
    - Links:
        - docker: http://127.0.0.1:8000/swagger/index.html
        - kestrel: http://127.0.0.1:5000/swagger/index.html
    - Description:
        - Stores and provides the product list.
- eshop.basket.api:
    - Links:
        - docker: http://127.0.0.1:8001/swagger/index.html
        - kestrel: http://127.0.0.1:5001/swagger/index.html
    - Description:
        - Holds temporary basket data on a Redis cache.
        - On checkout, it publishes an event to RabbitMQ.
- eshop.discount.api:
    - Links:
        - docker: http://127.0.0.1:8002/swagger/index.html
        - kestrel: http://127.0.0.1:5002/swagger/index.html
    - Description:
        - Stores and provides the product discounts information list.
        - Has a gRPC API (eShop.Discount.Grpc) that can be consumed by other internal microservices.
- eshop.ordering.api:
    - Links:
        - docker: http://127.0.0.1:8004/swagger/index.html
        - kestrel: http://127.0.0.1:5004/swagger/index.html
    - Description:
        - Waits on the RabbitMQ event from the Basket, to create/finalize the order.

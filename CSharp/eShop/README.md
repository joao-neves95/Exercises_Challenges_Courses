# Microservices Architecture on .NET with applying CQRS, Clean Architecture and Event-Driven Communication

## External Links
- Course Certificate: https://www.udemy.com/certificate/UC-6f9770a4-9989-46c8-8f3d-710c2ea0d9bd
- Udemy course: https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet
- Written course: https://medium.com/aspnetrun/microservices-architecture-on-net-3b4865eea03f
- Course GitHub: https://github.com/aspnetrun/run-aspnetcore-microservices

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
  - ApiGateways/
    - [eShop.ApiGateways.Web/](./src/ApiGateways/eShop.ApiGateways.Web/)
- Docker/
  - [docker-compose.yml](./docker-compose.yml)
  - [docker-compose.vs.debug.yml](./docker-compose.vs.debug.yml)

## Docs
- Docker & Docker Compose
  - `docker-compose -f ./docker-compose.yml -f ./docker-compose.vs.debug.yml up -d`

## Services
- eshop.Web.App:
    - Links:
        - docker: http://127.0.0.1:8300
    - Description:
        - The web app.
- Portainer Dashboard:
    - Links:
        - docker: http://127.0.0.1:9100
- pgAdmin Dashboard:
    - Links:
        - docker: http://127.0.0.1:9101
- RabbitMQ Management Dashboard:
    - Links:
        - docker: http://127.0.0.1:15673
- eshop.ApiGateways.ShoppingAggregator:
    - Links:
        - docker: http://127.0.0.1:8201/swagger/index.html
    - Description:
        - A gateway to aggregate results from multiple microservices to be used through a single client request.
- eshop.ApiGateways.Web:
    - Links:
        - docker: http://127.0.0.1:8200
    - Description:
        - A gateway that re-routes all microservices endpoints to be consumed by the client application through a single host.
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

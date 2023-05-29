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
- [docker-compose.yml](./docker-compose.yml)

## Docs
- Docker & Docker Compose
  - `docker-compose -f ./docker-compose.yml -f ./docker-compose.vs.debug.yml up -d`

## Internal Links (dev)
- Portainer: http://127.0.0.1:9100
- pgAdmin: http://127.0.0.1:9101
- eshop.catalog.api: http://127.0.0.1:8000/swagger/index.html
- eshop.basket.api: http://127.0.0.1:8001/swagger/index.html
- eshop.discount.api: http://127.0.0.1:8002/swagger/index.html

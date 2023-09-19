cd ..
docker build -t eshop.catalog.api:latest -f src/Services/Catalog/eShop.Catalog.Api/Dockerfile .
docker build -t eshop.discount.api:latest -f src/Services/Discount/eShop.Discount.Api/Dockerfile .
docker build -t eshop.discount.grpc:latest -f src/Services/Discount/eShop.Discount.Grpc/Dockerfile .
docker build -t eshop.basket.api:latest -f src/Services/Basket/eShop.Basket.Api/Dockerfile .
docker build -t eshop.ordering.api:latest -f src/Services/Ordering/eShop.Ordering.Api/Dockerfile .
docker build -t eshop.apigateways.web:latest -f src/ApiGateways/eShop.ApiGateways.Web/Dockerfile .
docker build -t eshop.web.app:latest -f src/Web/AspnetRunBasics/Dockerfile .
cd scripts

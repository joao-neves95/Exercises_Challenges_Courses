#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Basket/eShop.Basket.Api/eShop.Basket.Api.csproj", "src/Services/Basket/eShop.Basket.Api/"]
COPY ["src/Shared/eShop.Shared.EventBus.Messages/eShop.Shared.EventBus.Messages.csproj", "src/Shared/eShop.Shared.EventBus.Messages/"]
RUN dotnet restore "src/Services/Basket/eShop.Basket.Api/eShop.Basket.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Basket/eShop.Basket.Api"
RUN dotnet build "eShop.Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShop.Basket.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShop.Basket.Api.dll"]

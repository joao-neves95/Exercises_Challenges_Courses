#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Ordering/eShop.Ordering.Api/eShop.Ordering.Api.csproj", "src/Services/Ordering/eShop.Ordering.Api/"]
COPY ["src/Services/Ordering/eShop.Ordering.Application/eShop.Ordering.Application.csproj", "src/Services/Ordering/eShop.Ordering.Application/"]
COPY ["src/Services/Ordering/eShop.Ordering.Domain/eShop.Ordering.Domain.csproj", "src/Services/Ordering/eShop.Ordering.Domain/"]
COPY ["src/Services/Ordering/eShop.Ordering.Infrastructure/eShop.Ordering.Infrastructure.csproj", "src/Services/Ordering/eShop.Ordering.Infrastructure/"]
COPY ["src/Shared/Shop.Shared.EventBus.Messages/eShop.Shared.EventBus.Messages.csproj", "src/Shared/Shop.Shared.EventBus.Messages/"]
RUN dotnet restore "src/Services/Ordering/eShop.Ordering.Api/eShop.Ordering.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Ordering/eShop.Ordering.Api"
RUN dotnet build "eShop.Ordering.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShop.Ordering.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShop.Ordering.Api.dll"]

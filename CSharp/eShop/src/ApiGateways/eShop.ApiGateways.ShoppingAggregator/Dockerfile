#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ApiGateways/eShop.ApiGateways.Aggregator/eShop.ApiGateways.Aggregator.csproj", "src/ApiGateways/eShop.ApiGateways.Aggregator/"]
RUN dotnet restore "src/ApiGateways/eShop.ApiGateways.Aggregator/eShop.ApiGateways.Aggregator.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/eShop.ApiGateways.Aggregator"
RUN dotnet build "eShop.ApiGateways.Aggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShop.ApiGateways.Aggregator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShop.ApiGateways.Aggregator.dll"]

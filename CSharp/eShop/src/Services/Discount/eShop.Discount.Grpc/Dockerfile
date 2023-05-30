#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Discount/eShop.Discount.Grpc/eShop.Discount.Grpc.csproj", "src/Services/Discount/eShop.Discount.Grpc/"]
COPY ["src/Services/Discount/eShop.Discount.Shared/eShop.Discount.Shared.csproj", "src/Services/Discount/eShop.Discount.Shared/"]
RUN dotnet restore "src/Services/Discount/eShop.Discount.Grpc/eShop.Discount.Grpc.csproj"
COPY . .
WORKDIR "/src/src/Services/Discount/eShop.Discount.Grpc"
RUN dotnet build "eShop.Discount.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShop.Discount.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShop.Discount.Grpc.dll"]

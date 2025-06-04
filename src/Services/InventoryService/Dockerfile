FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY InventoryService.Api/InventoryService.Api.csproj InventoryService.Api/
COPY InventoryService.Application/InventoryService.Application.csproj InventoryService.Application/
COPY InventoryService.Infrastructure/InventoryService.Infrastructure.csproj InventoryService.Infrastructure/
COPY InventoryService.Persistence/InventoryService.Persistence.csproj InventoryService.Persistence/

RUN dotnet restore InventoryService.Api/InventoryService.Api.csproj

COPY . .
WORKDIR /src/InventoryService.Api
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryService.Api.dll"]

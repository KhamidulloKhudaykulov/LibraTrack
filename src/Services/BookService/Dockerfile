FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY BookService.Api/BookService.Api.csproj BookService.Api/
COPY BookService.Application/BookService.Application.csproj BookService.Application/
COPY BookService.Infrastructure/BookService.Infrastructure.csproj BookService.Infrastructure/
COPY BookService.Persistence/BookService.Persistence.csproj BookService.Persistence/

RUN dotnet restore BookService.Api/BookService.Api.csproj

COPY . .
WORKDIR /src/BookService.Api
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookService.Api.dll"]

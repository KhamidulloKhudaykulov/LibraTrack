FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY AccountService.Api/AccountService.Api.csproj AccountService.Api/
COPY AccountService.Application/AccountService.Application.csproj AccountService.Application/
COPY AccountService.Infrastructure/AccountService.Infrastructure.csproj AccountService.Infrastructure/
COPY AccountService.Persistence/AccountService.Persistence.csproj AccountService.Persistence/

RUN dotnet restore AccountService.Api/AccountService.Api.csproj

COPY . .
WORKDIR /src/AccountService.Api
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountService.Api.dll"]

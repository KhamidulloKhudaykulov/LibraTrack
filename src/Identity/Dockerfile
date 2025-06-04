FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY IdentityService.Api/IdentityService.Api.csproj IdentityService.Api/

RUN dotnet restore IdentityService.Api/IdentityService.Api.csproj

COPY . .
WORKDIR /src/IdentityService.Api
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityService.Api.dll"]

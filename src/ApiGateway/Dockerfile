FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ApiGateway.Api/ApiGateway.Api.csproj ApiGateway.Api/

RUN dotnet restore ApiGateway.Api/ApiGateway.Api.csproj

COPY . .
WORKDIR /src/ApiGateway.Api
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.Api.dll"]

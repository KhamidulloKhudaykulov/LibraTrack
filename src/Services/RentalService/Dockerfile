FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY RentalService.Api/RentalService.Api.csproj RentalService.Api/
COPY RentalService.Application/RentalService.Application.csproj RentalService.Application/
COPY RentalService.Infrastructure/RentalService.Infrastructure.csproj RentalService.Infrastructure/
COPY RentalService.Persistence/RentalService.Persistence.csproj RentalService.Persistence/

RUN dotnet restore RentalService.Api/RentalService.Api.csproj

COPY . .
WORKDIR /src/RentalService.Api
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentalService.Api.dll"]

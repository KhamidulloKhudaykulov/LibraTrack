FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# .csproj faylni to‘g‘ri ko‘chirish
COPY AdminPanel.Api/AdminPanel.Api.csproj AdminPanel.Api/

RUN dotnet restore AdminPanel.Api/AdminPanel.Api.csproj

# Butun loyiha fayllarini ko‘chirish
COPY . .

# Ishchi papkani to‘g‘ri ko‘rsatish
WORKDIR /src/AdminPanel.Api
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "AdminPanel.Api.dll"]

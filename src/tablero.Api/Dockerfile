# Etapa 1: Imagen base para construir
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar los archivos .csproj de todas las capas del proyecto
COPY ["src/tablero.Application/tablero.Application.csproj", "src/tablero.Application/"]
COPY ["src/tablero.Common/tablero.Common.csproj", "src/tablero.Common/"]
COPY ["src/tablero.External/tablero.External.csproj", "src/tablero.External/"]
COPY ["src/tablero.Persistence/tablero.Persistence.csproj", "src/tablero.Persistence/"]
COPY ["src/tablero.Api/tablero.Api.csproj", "src/tablero.Api/"]

# Restaurar dependencias
WORKDIR /app/src/tablero.Api
RUN dotnet restore

# Copiar todo el código fuente
WORKDIR /app
COPY src/. ./src/

# Publicar la aplicación
WORKDIR /app/src/tablero.Api
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Imagen base para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 8080
ENV ASPNETCORE_ENVIRONMENT=Docker
ENTRYPOINT ["dotnet", "tablero.Api.dll"]
# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copia csproj e restaura dependências
COPY *.sln .
COPY api/*.csproj ./api/
RUN dotnet restore

# Copia todo o restante do código
COPY . .

# Publica a aplicação em modo Release
WORKDIR /app/api
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expõe a porta da API
EXPOSE 5100

# Define a entrada
ENTRYPOINT ["dotnet", "api.dll"]

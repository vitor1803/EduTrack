# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia csproj e sln e restaura dependências
COPY *.sln ./
COPY *.csproj ./
RUN dotnet restore

# Copia todo o restante do código
COPY . .

# Publica a aplicação em modo Release
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./


# Configura a porta do ASP.NET
ENV ASPNETCORE_URLS=http://+:5100
EXPOSE 5100

ENV ASPNETCORE_ENVIRONMENT=Development

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "api.dll"]

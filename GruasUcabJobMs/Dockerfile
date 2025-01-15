# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5235
EXPOSE 7100


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GruasUcabJobMs/GruasUcabJobMs.csproj", "GruasUcabJobMs/"]
COPY ["JobMs.Application/JobMs.Application.csproj", "JobMs.Application/"]
COPY ["JobMs.Core/JobMs.Core.csproj", "JobMs.Core/"]
COPY ["JobMs.Infrastructure/JobMs.Infrastructure.csproj", "JobMs.Infrastructure/"]
COPY ["JobMs.Domain/JobMs.Domain.csproj", "JobMs.Domain/"]
RUN dotnet restore "./GruasUcabJobMs/GruasUcabJobMs.csproj"
COPY . .
WORKDIR "/src/GruasUcabJobMs"
RUN dotnet build "./GruasUcabJobMs.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GruasUcabJobMs.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GruasUcabJobMs.dll"]
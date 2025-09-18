FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de solución y proyectos
COPY *.sln .
COPY AgendaSaludApp.Server/*.csproj ./AgendaSaludApp.Server/
COPY AgendaSaludApp.Application/*.csproj ./AgendaSaludApp.Application/
COPY AgendaSaludApp.Core/*.csproj ./AgendaSaludApp.Core/
COPY AgendaSaludApp.Infrastructure/*.csproj ./AgendaSaludApp.Infrastructure/

# Restaurar dependencias
RUN dotnet restore "AgendaSaludApp.Server/AgendaSaludApp.Server.csproj"

# Copiar el resto del código
COPY AgendaSaludApp.Server/ ./AgendaSaludApp.Server/
COPY AgendaSaludApp.Application/ ./AgendaSaludApp.Application/
COPY AgendaSaludApp.Core/ ./AgendaSaludApp.Core/
COPY AgendaSaludApp.Infrastructure/ ./AgendaSaludApp.Infrastructure/

# Publicar la aplicación
WORKDIR "/src/AgendaSaludApp.Server"
RUN dotnet publish "AgendaSaludApp.Server.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "AgendaSaludApp.Server.dll"]
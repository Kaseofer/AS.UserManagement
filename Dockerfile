FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia el archivo de solución
COPY ["AS.UserManagement.sln", "./"]

# Copia los archivos .csproj (SIN incluir .DB)
COPY ["AS.UserManagement.Api/*.csproj", "AS.UserManagement.Api/"]
COPY ["AS.UserManagement.Application/*.csproj", "AS.UserManagement.Application/"]
COPY ["AS.UserManagement.Core/*.csproj", "AS.UserManagement.Core/"]
COPY ["AS.UserManagement.Infrastructure/*.csproj", "AS.UserManagement.Infrastructure/"]

# Restore de dependencias
RUN dotnet restore "AS.UserManagement.Api/AS.UserManagement.Api.csproj" --disable-parallel --no-cache

# Copia todo el código fuente
COPY . .

# Build del proyecto Api
WORKDIR "/src/AS.UserManagement.Api"
RUN dotnet build "AS.UserManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AS.UserManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AS.UserManagement.Api.dll"]
# ===========================
#       BUILD STAGE
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copiar csproj y restaurar dependencias
COPY nexthappen-backend.csproj ./
RUN dotnet restore

# Copiar el resto del proyecto
COPY . .

# Compilar en modo Release
RUN dotnet publish -c Release -o /out

# ===========================
#       RUNTIME STAGE
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copiar los archivos del publish
COPY --from=build /out .

# Exponer puerto
EXPOSE 8080

# Comando de arranque
ENTRYPOINT ["dotnet", "nexthappen-backend.dll"]

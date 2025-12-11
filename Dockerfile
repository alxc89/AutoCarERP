# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files
COPY AutoCarERP.API/AutoCarERP.API.csproj AutoCarERP.API/
COPY AutoCarERP.Application/AutoCarERP.Application.csproj AutoCarERP.Application/
COPY AutoCarERP.Core/AutoCarERP.Core.csproj AutoCarERP.Core/
COPY AutoCarERP.Infra/AutoCarERP.Infra.csproj AutoCarERP.Infra/

# Restore dependencies
RUN dotnet restore AutoCarERP.API/AutoCarERP.API.csproj

# Copy everything else
COPY . .

# Build and publish
WORKDIR /src/AutoCarERP.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Create non-root user
RUN useradd -m -u 1000 appuser && chown -R appuser:appuser /app
USER appuser

# Copy published files
COPY --from=build /app/publish .

# Expose port
EXPOSE 8080

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=10s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

# Set environment
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "AutoCarERP.API.dll"]

# WhatsNext Backend - Multi-stage Docker Build

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["WhatsNext.sln", "./"]
COPY ["src/WhatsNext.API/WhatsNext.API.csproj", "src/WhatsNext.API/"]
COPY ["src/WhatsNext.Application/WhatsNext.Application.csproj", "src/WhatsNext.Application/"]
COPY ["src/WhatsNext.Domain/WhatsNext.Domain.csproj", "src/WhatsNext.Domain/"]
COPY ["src/WhatsNext.Infrastructure/WhatsNext.Infrastructure.csproj", "src/WhatsNext.Infrastructure/"]
COPY ["tests/WhatsNext.UnitTests/WhatsNext.UnitTests.csproj", "tests/WhatsNext.UnitTests/"]
COPY ["tests/WhatsNext.IntegrationTests/WhatsNext.IntegrationTests.csproj", "tests/WhatsNext.IntegrationTests/"]
COPY ["Directory.Build.props", "./"]
COPY ["stylecop.json", "./"]

# Restore dependencies
RUN dotnet restore "WhatsNext.sln"

# Copy all source files
COPY . .

# Build the API project
WORKDIR "/src/src/WhatsNext.API"
RUN dotnet build "WhatsNext.API.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "WhatsNext.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Create a non-root user
RUN addgroup --system --gid 1001 appuser && \
    adduser --system --uid 1001 --ingroup appuser appuser

# Copy published app
COPY --from=publish /app/publish .

# Set ownership
RUN chown -R appuser:appuser /app

# Switch to non-root user
USER appuser

# Expose ports
EXPOSE 8080
EXPOSE 8081

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Run the application
ENTRYPOINT ["dotnet", "WhatsNext.API.dll"]


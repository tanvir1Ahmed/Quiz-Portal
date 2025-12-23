# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the solution and project files
COPY Quiz_Portal.slnx .
COPY Quiz_Portal/Quiz_Portal.csproj Quiz_Portal/

# Restore dependencies
RUN dotnet restore Quiz_Portal/Quiz_Portal.csproj

# Copy the rest of the source code
COPY Quiz_Portal/ Quiz_Portal/

# Build and publish the application
WORKDIR /src/Quiz_Portal
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Create a non-root user for security
RUN adduser --disabled-password --gecos "" appuser

# Copy published output from build stage
COPY --from=build /app/publish .

# Create directory for SQLite database and set permissions
RUN mkdir -p /app/data && chown -R appuser:appuser /app

# Switch to non-root user
USER appuser

# Expose port (Render uses PORT environment variable)
EXPOSE 8080

# Set environment variables for production
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_RUNNING_IN_CONTAINER=true

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/health || exit 1

# Start the application
ENTRYPOINT ["dotnet", "Quiz_Portal.dll"]

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the backend folder
COPY Backend/ ./Backend/

# Set working directory to the backend folder
WORKDIR /app/Backend

# Restore and publish the specific project
RUN dotnet restore Backend.csproj
RUN dotnet publish Backend.csproj -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Backend.dll"]

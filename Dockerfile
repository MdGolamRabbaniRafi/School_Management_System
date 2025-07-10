# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy only the subfolder that contains the .csproj
COPY Backend/ ./Backend/

# Go into the folder with the .csproj
WORKDIR /app/Backend

# Restore, build, and publish
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Replace Backend.dll with your actual DLL name
ENTRYPOINT ["dotnet", "Backend.dll"]

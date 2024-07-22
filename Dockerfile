# Use the official .NET image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the csproj and restore as distinct layers
COPY HappyBookingCleanArchitectureServer/*.csproj ./HappyBookingCleanArchitectureServer/
RUN dotnet restore ./HappyBookingCleanArchitectureServer/HappyBookingCleanArchitectureServer.csproj

# Copy everything else and build the project
COPY . .
RUN dotnet publish ./HappyBookingCleanArchitectureServer/HappyBookingCleanArchitectureServer.csproj -c Release -o out

# Use the official .NET runtime image as a runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "HappyBookingCleanArchitectureServer.dll"]

# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy solution and restore as distinct layers
COPY *.sln .
COPY HappyBookingClient/*.csproj ./HappyBookingClient/
COPY HappyBookingServer/*.csproj ./HappyBookingServer/
COPY HappyBookingShare/*.csproj ./HappyBookingShare/
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish ./HappyBookingServer/HappyBookingServer.csproj -c Release -o out

# Stage 2: Serve the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HappyBookingServer.dll"]

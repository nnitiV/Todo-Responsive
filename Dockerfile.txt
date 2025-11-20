# 1. Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["FirstWebApp/FirstWebApp.csproj", "FirstWebApp/"]
RUN dotnet restore "FirstWebApp/FirstWebApp.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/FirstWebApp"

# Build and Publish the app
RUN dotnet publish "FirstWebApp.csproj" -c Release -o /app/publish

# 2. Use the Runtime image to run the app (smaller & faster)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose the port Render expects
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Start the app!
ENTRYPOINT ["dotnet", "FirstWebApp.dll"]
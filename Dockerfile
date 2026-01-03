# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the code and build 
COPY . ./
RUN dotnet publish -c Release -o /out

#///////////////////
# Runtime stage
#///////////////////
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy the published application
COPY --from=build /out ./

# Expose only port 80 (Render handles HTTPS)
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "MyWebApi.dll"]

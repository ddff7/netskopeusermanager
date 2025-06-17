# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files and restore dependencies
COPY *.sln ./
COPY NsUserManager/*.csproj ./NsUserManager/
RUN dotnet restore

# Copy the rest of the application files and build the application
COPY NsUserManager/. ./NsUserManager/
WORKDIR /app/NsUserManager
RUN dotnet publish -c Release -o /out

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Add an environment variable for appsettings
ENV APPSETTINGS__Netskope__ScimBaseUrl=
ENV APPSETTINGS__Netskope__ApiToken=
ENV APPSETTINGS__Netskope__ExternalIdPrefix=netskope-manager-

ENV APPSETTINGS__Saml__EntityId=
ENV APPSETTINGS__Saml__IdpId=
ENV APPSETTINGS__Saml__MetadataUrl=

# Expose the port your application runs on
EXPOSE 8080

# Set the entry point for the container
ENTRYPOINT ["dotnet", "NsUserManager.dll"]
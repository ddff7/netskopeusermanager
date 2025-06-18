# Netskope User Manager

A Blazor Server application for managing Netskope users through their SCIM API.

## Features

- View all Netskope users
- Create new users
- Edit existing users
- Delete users
- Add and remove users from groups
- Create new groups
- Delete groups

## Prerequisites

- .NET 8 SDK
- Netskope SCIM API credentials
- SAML configuration for authentication
- In production, the application should be run behind a reverse proxy (e.g., Nginx, Apache) to handle SSL termination and routing for SAML authentication.
- Due to limitations in Sustainsys Saml2, specificy the real URL of the application in the `PublicOrigin` setting in `appsettings.json` to ensure SAML responses are correctly signed and validated.

## Logic

The application uses the following logic to manage users and groups:
    - Fetches users and groups from the Netskope SCIM API that start with the configured `ExternalIdPrefix`.
    - Due to Netskope's SCIM API limitations, it does not support user membership, so the through the application it is not possible to see which users are in which groups.

## Setup

1. Clone the repository
2. Update the `appsettings.json` with your Netskope credentials:
   ```json
      {
        "Netskope": 
        {
            "ScimBaseUrl": "https://****.goskope.com/api/v2/scim",
            "ApiToken": "",
            "ExternalIdPrefix": "netskope-manager-"
        },
        "Saml": {
            "EntityId": "https://netskope-usermanager.test.io",
            "IdpId": "",
            "MetadataUrl": "",
            "PublicOrigin": "",
            "CertificateValidationMode": "ChainTrust",
            "RevocationMode": "Online"
        }
      }
   ```
3. Run the application:
   ```bash
   dotnet run
   ```

## Setup through docker container

1. Build the Docker image:
   ```bash
   docker build -t netskope-user-manager .
   ```
2. Run the Docker container with proper environment variable:
   ```bash
    docker run --env=APPSETTINGS__Netskope__ScimBaseUrl=https://****.goskope.com/api/v2/scim --env=APPSETTINGS__Netskope__ApiToken=**** --env=APPSETTINGS__Saml__EntityId=https://**** --env=APPSETTINGS__Saml__IdpId=https://*** --env=APPSETTINGS__Saml__MetadataUrl=https://**** --env=PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin --env=ASPNETCORE_HTTP_PORTS=8080 --env=DOTNET_RUNNING_IN_CONTAINER=true --env=DOTNET_VERSION=8.0.17 --env=ASPNET_VERSION=8.0.17 --env=APPSETTINGS__Netskope__ExternalIdPrefix=netskope-manager- --env=APPSETTINGS__Saml__PublicOrigin=https://*** --network=bridge --workdir=/app -p 8080:8080 --restart=no --runtime=runc -d netskope-user-manager
   ```

3. Access the application at `http://localhost:8080`

## Deployment through prebuilt Docker image

A prebuilt Docker image is available on GitHub Container Registry (https://github.com/ddff7/netskopeusermanager/pkgs/container/netskopeusermanager). You can pull it using the following command:
1. Pull the Docker image:
```bash
docker pull ghcr.io/ddff7/netskopeusermanager:master
```

2. Run the Docker container with proper environment variable:
```bash
docker run --env=APPSETTINGS__Netskope__ScimBaseUrl=https://****.goskope.com/api/v2/scim --env=APPSETTINGS__Netskope__ApiToken=**** --env=APPSETTINGS__Saml__EntityId=https://**** --env=APPSETTINGS__Saml__IdpId=https://*** --env=APPSETTINGS__Saml__MetadataUrl=https://**** --env=PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin --env=ASPNETCORE_HTTP_PORTS=8080 --env=DOTNET_RUNNING_IN_CONTAINER=true --env=DOTNET_VERSION=8.0.17 --env=ASPNET_VERSION=8.0.17 --env=APPSETTINGS__Netskope__ExternalIdPrefix=netskope-manager- --env=APPSETTINGS__Saml__PublicOrigin=https://*** --network=bridge --workdir=/app -p 8080:8080 --restart=no --runtime=runc -d ghcr.io/ddff7/netskopeusermanager:master
   ```

## Deployment

- Deployment as WebApp on Azure App Service (as container) is supported
- SSL certificate are managed by Azure App Service
- Just add these environment variables to the App Service:
 ```json
  {
    "name": "APPSETTINGS__Netskope__ApiToken",
    "value": "****",
    "slotSetting": false
  },
  {
    "name": "APPSETTINGS__Netskope__ScimBaseUrl",
    "value": "****",
    "slotSetting": false
  },
  {
    "name": "APPSETTINGS__Saml__EntityId",
    "value": "****",
    "slotSetting": false
  },
  {
    "name": "APPSETTINGS__Saml__IdpId",
    "value": "****",
    "slotSetting": false
  },
  {
    "name": "APPSETTINGS__Saml__MetadataUrl",
    "value": "****",
    "slotSetting": false
  },
  {
    "name": "APPSETTINGS__Saml__PublicOrigin",
    "value": "webapppubliurl",
    "slotSetting": false
  }
  {
    "name": "WEBSITES_PORT",
    "value": "8080",
    "slotSetting": false
  }
  ```

## Development

The application is built using:
- .NET 8
- Blazor Server
- Bootstrap for styling

# Netskope User Manager

A Blazor Server application for managing Netskope users through their SCIM API.

## Features

- View all Netskope users
- Create new users
- Edit existing users
- Delete users
- User status management

## Prerequisites

- .NET 8 SDK
- Netskope SCIM API credentials

## Setup

1. Clone the repository
2. Update the `appsettings.json` with your Netskope credentials:
   ```json
   {
     "Netskope": {
       "ScimBaseUrl": "YOUR_SCIM_BASE_URL",
       "ApiToken": "YOUR_API_TOKEN"
     }
   }
   ```
3. Run the application:
   ```bash
   dotnet run
   ```

## Development

The application is built using:
- .NET 8
- Blazor Server
- Bootstrap for styling

## Prerequisites
- .NET 8.0 SDK
- Docker

## Usage
To run the application, execute the following commands in the root directory of the solution:

- dotnet restore
- dotnet build
- dotnet run --project Source/Cirtuo.RetailProcurementSystem.Api/Cirtuo.RetailProcurementSystem.Api.csproj

## Docker
To run the application in a Docker container, execute the following commands in the root directory of the solution:

- cd Docker
- docker-compose up

Before running the application in a Docker container, you should consider an API version in the Docker Compose file.

## Testing
To run the unit tests, execute the following commands in the root directory of the solution:

- dotnet restore
- dotnet build
- dotnet test
```
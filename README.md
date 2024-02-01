## Prerequisites
- .NET 8.0 SDK
- dotnet-ef CLI tool
- Docker

## Usage
Before running the application, database migrations should be applied. To do this, execute the following commands in the root directory of the solution:
- dotnet tool install --global dotnet-ef
- dotnet ef database update -s .\Source\Cirtuo.RetailProcurementSystem.Api\Cirtuo.RetailProcurementSystem.Api.csproj -p .\Source\Cirtuo.RetailProcurementSystem.Persistence\Cirtuo.RetailProcurementSystem.Persistence.csproj

Ensure that the connection string in the appsettings.json file of the Cirtuo.RetailProcurementSystem.Api project is correct. The default connection string is configured to use the local Postgresql database.

To run the application, execute the following commands in the root directory of the solution:

- dotnet restore
- dotnet build
- dotnet run --project Source\Cirtuo.RetailProcurementSystem.Api\Cirtuo.RetailProcurementSystem.Api.csproj

## Docker
To run the application in a Docker container, execute the following commands in the root directory of the solution:

- cd Docker
- docker-compose up

In order to run the application via Docker, the host property in the connection string in appsettings.json file of the Cirtuo.RetailProcurementSystem.Api project should be changed to the name of the Postgresql container in the docker-compose.yml file.

## Testing
To run the unit tests, execute the following commands in the root directory of the solution:

- dotnet restore
- dotnet build
- dotnet test

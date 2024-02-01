FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./Source/Cirtuo.RetailProcurementSystem.Api/Cirtuo.RetailProcurementSystem.Api.csproj", "./Source/Cirtuo.RetailProcurementSystem.Api/"]
RUN dotnet restore "./Source/Cirtuo.RetailProcurementSystem.Api/Cirtuo.RetailProcurementSystem.Api.csproj"
COPY . .
WORKDIR "/src/Source/Cirtuo.RetailProcurementSystem.Api"
RUN dotnet build "Cirtuo.RetailProcurementSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cirtuo.RetailProcurementSystem.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cirtuo.RetailProcurementSystem.Api.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore "FinancialSystem.csproj"

RUN dotnet build "FinancialSystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FinancialSystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FinancialSystem.dll"]
# Usar la imagen base de .NET 8.0 Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Usar la imagen SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PersonalAPI.csproj", "."]
RUN dotnet restore "./PersonalAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "PersonalAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonalAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonalAPI.dll"]

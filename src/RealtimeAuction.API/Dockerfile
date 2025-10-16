FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RealtimeAuction.API/RealtimeAuction.API.csproj", "RealtimeAuction.API/"]
COPY ["RealtimeAuction.Application/RealtimeAuction.Application.csproj", "RealtimeAuction.Application/"]
COPY ["RealtimeAuction.Domain/RealtimeAuction.Domain.csproj", "RealtimeAuction.Domain/"]
COPY ["RealtimeAuction.Exceptions/RealtimeAuction.Exceptions.csproj", "RealtimeAuction.Exceptions/"]
COPY ["RealtimeAuction.Infrastructure/RealtimeAuction.Infrastructure.csproj", "RealtimeAuction.Infrastructure/"]
RUN dotnet restore "RealtimeAuction.API/RealtimeAuction.API.csproj"
COPY . .
WORKDIR "/src/RealtimeAuction.API"
RUN dotnet build "./RealtimeAuction.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RealtimeAuction.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RealtimeAuction.API.dll"]

#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Torrent.Chat.Api.Api/Torrent.Chat.Api.Api.csproj", "Torrent.Chat.Api.Api/"]
COPY ["Torrent.Chat.Api.Storage/Torrent.Chat.Api.Storage.csproj", "Torrent.Chat.Api.Storage/"]
RUN dotnet restore "./Torrent.Chat.Api.Api/Torrent.Chat.Api.Api.csproj"
COPY . .
WORKDIR "/src/Torrent.Chat.Api.Api"
RUN dotnet build "./Torrent.Chat.Api.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Torrent.Chat.Api.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Torrent.Chat.Api.Api.dll"]
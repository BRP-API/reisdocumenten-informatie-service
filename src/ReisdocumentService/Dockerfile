#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    TZ=Europe/Amsterdam \
    # Disable config change monitoring for better container performance
    # See https://github.com/Contrast-Security-OSS/agent-operator/blob/master/docs/public/08-tips.md
    DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE=false
# Install ICU libraries
RUN apk add --no-cache icu-libs
WORKDIR /app

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
WORKDIR /src

COPY ["ReisdocumentService/ReisdocumentService.csproj", "ReisdocumentService/"]

RUN dotnet restore "./ReisdocumentService/ReisdocumentService.csproj"

COPY . .

WORKDIR "/src/ReisdocumentService"
RUN dotnet build "./ReisdocumentService.csproj" -c "$BUILD_CONFIGURATION" -o /app/build --arch "$TARGETARCH"

FROM --platform=$BUILDPLATFORM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG TARGETARCH
RUN dotnet publish "./ReisdocumentService.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false --arch "$TARGETARCH"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN addgroup -g 1000 -S brp-api && \
    adduser -u 1000 -S -D -g brp-api brp-api-user && \
    chown -R brp-api-user:brp-api /app

# Set user for running the application
USER brp-api-user

ENTRYPOINT ["dotnet", "HaalCentraal.ReisdocumentService.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["API/Modulith.Commerce.API/Modulith.Commerce.API.csproj", "API/Modulith.Commerce.API/"]
COPY ["Common/Modulith.Commerce.Common.Application/Modulith.Commerce.Common.Application.csproj", "Common/Modulith.Commerce.Common.Application/"]
COPY ["Common/Modulith.Commerce.Common.Auth/Modulith.Commerce.Common.Auth.csproj", "Common/Modulith.Commerce.Common.Auth/"]
COPY ["Common/Modulith.Commerce.Common.Domain/Modulith.Commerce.Common.Domain.csproj", "Common/Modulith.Commerce.Common.Domain/"]
COPY ["Common/Modulith.Commerce.Common.Infrastructure/Modulith.Commerce.Common.Infrastructure.csproj", "Common/Modulith.Commerce.Common.Infrastructure/"]
COPY ["Contracts/Modulith.Commerce.AdminUsers.IntegrationEvents/Modulith.Commerce.AdminUsers.IntegrationEvents.csproj", "Contracts/Modulith.Commerce.AdminUsers.IntegrationEvents/"]
COPY ["Modules/AdminUser/Modulith.Commerce.AdminUser.Domain/Modulith.Commerce.AdminUser.Domain.csproj", "Modules/AdminUser/Modulith.Commerce.AdminUser.Domain/"]
COPY ["Modules/AdminUser/Modulith.Commerce.AdminUsers.Application/Modulith.Commerce.AdminUsers.Application.csproj", "Modules/AdminUser/Modulith.Commerce.AdminUsers.Application/"]
COPY ["Modules/AdminUser/Modulith.Commerce.AdminUsers.Infrastructure/Modulith.Commerce.AdminUsers.Infrastructure.csproj", "Modules/AdminUser/Modulith.Commerce.AdminUsers.Infrastructure/"]
COPY ["Modules/AdminUser/Modulith.Commerce.AdminUsers.Presentation/Modulith.Commerce.AdminUsers.Presentation.csproj", "Modules/AdminUser/Modulith.Commerce.AdminUsers.Presentation/"]
COPY ["Modules/Product/Modulith.Commerce.Products.Application/Modulith.Commerce.Products.Application.csproj", "Modules/Product/Modulith.Commerce.Products.Application/"]
COPY ["Modules/Product/Modulith.Commerce.Products.Domain/Modulith.Commerce.Products.Domain.csproj", "Modules/Product/Modulith.Commerce.Products.Domain/"]
COPY ["Modules/Product/Modulith.Commerce.Products.Infrastructure/Modulith.Commerce.Products.Infrastructure.csproj", "Modules/Product/Modulith.Commerce.Products.Infrastructure/"]
COPY ["Modules/Product/Modulith.Commerce.Products.Presentation/Modulith.Commerce.Products.Presentation.csproj", "Modules/Product/Modulith.Commerce.Products.Presentation/"]
RUN dotnet restore "./API/Modulith.Commerce.API/Modulith.Commerce.API.csproj"
COPY . .
WORKDIR "/src/API/Modulith.Commerce.API"
RUN dotnet build "./Modulith.Commerce.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./Modulith.Commerce.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
USER root
RUN apt-get update \
    && apt-get install -y --no-install-recommends curl unzip ca-certificates \
    && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /root/vsdbg \
    && apt-get purge -y --auto-remove unzip \
    && rm -rf /var/lib/apt/lists/*
COPY --from=publish /app/publish .
USER $APP_UID
ENTRYPOINT ["dotnet", "Modulith.Commerce.API.dll"]

ARG GITHIB_FEED_TOKEN
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Aguacongas.FreeTheIdServer.BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.csproj", "src/Aguacongas.FreeTheIdServer.BlazorApp/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Components/Aguacongas.FreeTheIdServer.BlazorApp.Components.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Components/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Infrastructure/Aguacongas.FreeTheIdServer.BlazorApp.Infrastructure.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Infrastructure/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages/Aguacongas.FreeTheIdServer.BlazorApp.Pages.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Api/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Api.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Api/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Apis/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Apis.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Apis/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ApiScope/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ApiScope.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ApiScope/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ApiScopes/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ApiScopes.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ApiScopes/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Client/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Client.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Client/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Clients/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Clients.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Clients/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Culture/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Culture.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Culture/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Cultures/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Cultures.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Cultures/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ExternalProvider/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ExternalProvider.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ExternalProvider/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ExternalProviders/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ExternalProviders.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ExternalProviders/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Identities/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Identities.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Identities/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Identity/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Identity.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Identity/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Import/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Import.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Import/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Keys/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Keys.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Keys/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Role/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Role.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Role/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Roles/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Roles.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Roles/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.User/Aguacongas.FreeTheIdServer.BlazorApp.Pages.User.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.User/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Users/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Users.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Users/"]
COPY ["src/IdentityServer/Aguacongas.IdentityServer.Admin.Http.Store/Aguacongas.IdentityServer.Admin.Http.Store.csproj", "src/IdentityServer/Aguacongas.IdentityServer.Admin.Http.Store/"]
COPY ["src/IdentityServer/Aguacongas.IdentityServer.Store/Aguacongas.IdentityServer.Store.csproj", "src/IdentityServer/Aguacongas.IdentityServer.Store/"]
COPY ["src/Aguacongas.FreeTheIdServer.BlazorApp/nginx.conf", "src/Aguacongas.FreeTheIdServer.BlazorApp/"]
RUN apt-get update -y && apt-get install -y python3 gcc && apt-get clean
RUN dotnet workload install wasm-tools
RUN dotnet restore "./src/Aguacongas.FreeTheIdServer.BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.csproj"
COPY . .
WORKDIR "/src/src/Aguacongas.FreeTheIdServer.BlazorApp"
RUN dotnet build "./Aguacongas.FreeTheIdServer.BlazorApp.csproj" -c "$BUILD_CONFIGURATION" -o /app/build -p:FileVersion="$FILE_VERSION" -p:SourceRevisionId="$SOURCE_VERSION"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Aguacongas.FreeTheIdServer.BlazorApp.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false -p:FileVersion="$FILE_VERSION" -p:SourceRevisionId="$SOURCE_VERSION"

FROM nginx:1.29.3-alpine3.22-slim AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY --from=build /src/src/Aguacongas.FreeTheIdServer.BlazorApp/nginx.conf /etc/nginx/nginx.conf

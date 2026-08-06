#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["nuget.config", "."]
COPY ["src/Aguacongas.FreeTheIdServer.Open/Aguacongas.FreeTheIdServer.Open.csproj", "src/Aguacongas.FreeTheIdServer.Open/"]
COPY ["src/Aguacongas.FreeTheIdServer.BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.csproj", "src/Aguacongas.FreeTheIdServer.BlazorApp/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ApiScopes/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ApiScopes.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ApiScopes/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Components/Aguacongas.FreeTheIdServer.BlazorApp.Components.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Components/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Infrastructure/Aguacongas.FreeTheIdServer.BlazorApp.Infrastructure.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Infrastructure/"]
COPY ["src/IdentityServer/Aguacongas.IdentityServer.Admin.Http.Store/Aguacongas.IdentityServer.Admin.Http.Store.csproj", "src/IdentityServer/Aguacongas.IdentityServer.Admin.Http.Store/"]
COPY ["src/IdentityServer/Aguacongas.IdentityServer.Store/Aguacongas.IdentityServer.Store.csproj", "src/IdentityServer/Aguacongas.IdentityServer.Store/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ApiScope/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ApiScope.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ApiScope/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Apis/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Apis.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Apis/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Api/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Api.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Api/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Clients/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Clients.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Clients/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Client/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Client.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Client/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Cultures/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Cultures.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Cultures/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Culture/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Culture.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Culture/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ExternalProviders/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ExternalProviders.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ExternalProviders/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ExternalProvider/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ExternalProvider.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.ExternalProvider/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Identities/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Identities.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Identities/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Identity/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Identity.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Identity/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Import/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Import.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Import/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Keys/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Keys.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Keys/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.RelyingParties/Aguacongas.FreeTheIdServer.BlazorApp.Pages.RelyingParties.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.RelyingParties/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.RelyingParty/Aguacongas.FreeTheIdServer.BlazorApp.Pages.RelyingParty.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.RelyingParty/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Roles/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Roles.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Roles/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Role/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Role.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Role/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Settings/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Settings.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Settings/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Users/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Users.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.Users/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.User/Aguacongas.FreeTheIdServer.BlazorApp.Pages.User.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages.User/"]
COPY ["src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages/Aguacongas.FreeTheIdServer.BlazorApp.Pages.csproj", "src/BlazorApp/Aguacongas.FreeTheIdServer.BlazorApp.Pages/"]
COPY ["src/Aguacongas.FreeTheIdServer/Aguacongas.FreeTheIdServer.csproj", "src/Aguacongas.FreeTheIdServer/"]
COPY ["src/Aguacongas.FreeTheIdServer.Authentication/Aguacongas.FreeTheIdServer.Authentication.csproj", "src/Aguacongas.FreeTheIdServer.Authentication/"]
COPY ["src/Aguacongas.FreeTheIdServer.Identity/Aguacongas.FreeTheIdServer.Identity.csproj", "src/Aguacongas.FreeTheIdServer.Identity/"]
COPY ["src/IdentityServer/Aguacongas.IdentityServer.Admin/Aguacongas.IdentityServer.Admin.csproj", "src/IdentityServer/Aguacongas.IdentityServer.Admin/"]
COPY ["src/IdentityServer/Aguacongas.IdentityServer.KeysRotation/Aguacongas.IdentityServer.KeysRotation.csproj", "src/IdentityServer/Aguacongas.IdentityServer.KeysRotation/"]
COPY ["src/IdentityServer/Aguacongas.IdentityServer/Aguacongas.IdentityServer.csproj", "src/IdentityServer/Aguacongas.IdentityServer/"]
COPY ["src/IdentityServer/Aguacongas.IdentityServer.EntityFramework.Store/Aguacongas.IdentityServer.EntityFramework.Store.csproj", "src/IdentityServer/Aguacongas.IdentityServer.EntityFramework.Store/"]
COPY ["src/IdentityServer/Aguacongas.IdentityServer.Http.Store/Aguacongas.IdentityServer.Http.Store.csproj", "src/IdentityServer/Aguacongas.IdentityServer.Http.Store/"]
COPY ["src/IdentityServer/Aguacongas.IdentityServer.MongoDb.Store/Aguacongas.IdentityServer.MongoDb.Store.csproj", "src/IdentityServer/Aguacongas.IdentityServer.MongoDb.Store/"]
COPY ["src/IdentityServer/Aguacongas.IdentityServer.RavenDb.Store/Aguacongas.IdentityServer.RavenDb.Store.csproj", "src/IdentityServer/Aguacongas.IdentityServer.RavenDb.Store/"]
COPY ["src/IdentityServer/Aguacongas.IdentityServer.WsFederation/Aguacongas.IdentityServer.WsFederation.csproj", "src/IdentityServer/Aguacongas.IdentityServer.WsFederation/"]
COPY ["src/IdentityServer/Migrations/Aguacongas.FreeTheIdServer.Migrations.MySql/Aguacongas.FreeTheIdServer.Migrations.MySql.csproj", "src/IdentityServer/Migrations/Aguacongas.FreeTheIdServer.Migrations.MySql/"]
COPY ["src/IdentityServer/Migrations/Aguacongas.FreeTheIdServer.Migrations.Oracle/Aguacongas.FreeTheIdServer.Migrations.Oracle.csproj", "src/IdentityServer/Migrations/Aguacongas.FreeTheIdServer.Migrations.Oracle/"]
COPY ["src/IdentityServer/Migrations/Aguacongas.FreeTheIdServer.Migrations.PostgreSQL/Aguacongas.FreeTheIdServer.Migrations.PostgreSQL.csproj", "src/IdentityServer/Migrations/Aguacongas.FreeTheIdServer.Migrations.PostgreSQL/"]
COPY ["src/IdentityServer/Migrations/Aguacongas.FreeTheIdServer.Migrations.Sqlite/Aguacongas.FreeTheIdServer.Migrations.Sqlite.csproj", "src/IdentityServer/Migrations/Aguacongas.FreeTheIdServer.Migrations.Sqlite/"]
COPY ["src/IdentityServer/Migrations/Aguacongas.FreeTheIdServer.Migrations.SqlServer/Aguacongas.FreeTheIdServer.Migrations.SqlServer.csproj", "src/IdentityServer/Migrations/Aguacongas.FreeTheIdServer.Migrations.SqlServer/"]
COPY ["src/IdentityServer/Open/Aguacongas.IdentityServer.Admin.Open/Aguacongas.IdentityServer.Admin.Open.csproj", "src/IdentityServer/Open/Aguacongas.IdentityServer.Admin.Open/"]
COPY ["src/IdentityServer/Open/Aguacongas.IdentityServer.Open/Aguacongas.IdentityServer.Open.csproj", "src/IdentityServer/Open/Aguacongas.IdentityServer.Open/"]
COPY ["src/IdentityServer/Open/Aguacongas.IdentityServer.KeysRotation.Open/Aguacongas.IdentityServer.KeysRotation.Open.csproj", "src/IdentityServer/Open/Aguacongas.IdentityServer.KeysRotation.Open/"]
COPY ["src/IdentityServer/Open/Aguacongas.IdentityServer.EntityFramework.Store.Open/Aguacongas.IdentityServer.EntityFramework.Store.Open.csproj", "src/IdentityServer/Open/Aguacongas.IdentityServer.EntityFramework.Store.Open/"]
COPY ["src/IdentityServer/Open/Aguacongas.IdentityServer.MongoDb.Store.Open/Aguacongas.IdentityServer.MongoDb.Store.Open.csproj", "src/IdentityServer/Open/Aguacongas.IdentityServer.MongoDb.Store.Open/"]
COPY ["src/IdentityServer/Open/Aguacongas.IdentityServer.RavenDb.Store.Open/Aguacongas.IdentityServer.RavenDb.Store.Open.csproj", "src/IdentityServer/Open/Aguacongas.IdentityServer.RavenDb.Store.Open/"]
COPY ["src/IdentityServer/Open/Aguacongas.IdentityServer.Saml2p.Open/Aguacongas.IdentityServer.Saml2p.Open.csproj", "src/IdentityServer/Open/Aguacongas.IdentityServer.Saml2p.Open/"]
COPY ["src/IdentityServer/Open/Aguacongas.IdentityServer.WsFederation.Open/Aguacongas.IdentityServer.WsFederation.Open.csproj", "src/IdentityServer/Open/Aguacongas.IdentityServer.WsFederation.Open/"]
COPY ["src/Identity/Aguacongas.FreeTheIdServer.Identity.Argon2PasswordHasher/Aguacongas.FreeTheIdServer.Identity.Argon2PasswordHasher.csproj", "src/Identity/Aguacongas.FreeTheIdServer.Identity.Argon2PasswordHasher/"]
COPY ["src/Identity/Aguacongas.FreeTheIdServer.Identity.BcryptPasswordHasher/Aguacongas.FreeTheIdServer.Identity.BcryptPasswordHasher.csproj", "src/Identity/Aguacongas.FreeTheIdServer.Identity.BcryptPasswordHasher/"]
COPY ["src/Identity/Aguacongas.FreeTheIdServer.Identity.ScryptPasswordHasher/Aguacongas.FreeTheIdServer.Identity.ScryptPasswordHasher.csproj", "src/Identity/Aguacongas.FreeTheIdServer.Identity.ScryptPasswordHasher/"]
COPY ["src/Identity/Aguacongas.FreeTheIdServer.Identity.UpgradePasswordHasher/Aguacongas.FreeTheIdServer.Identity.UpgradePasswordHasher.csproj", "src/Identity/Aguacongas.FreeTheIdServer.Identity.UpgradePasswordHasher/"]

RUN apt-get update -y && apt-get install -y python3 gcc && apt-get clean
RUN dotnet workload install wasm-tools
RUN dotnet restore "./src/Aguacongas.FreeTheIdServer.Open/./Aguacongas.FreeTheIdServer.Open.csproj"
COPY . .
WORKDIR "/src/src/Aguacongas.FreeTheIdServer.Open"
RUN dotnet build "./Aguacongas.FreeTheIdServer.Open.csproj" -c "$BUILD_CONFIGURATION" -o /app/build -p:FileVersion="$FILE_VERSION" -p:SourceRevisionId="$SOURCE_VERSION"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Aguacongas.FreeTheIdServer.Open.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false -p:FileVersion="$FILE_VERSION" -p:SourceRevisionId="$SOURCE_VERSION"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set permissions for wwwroot and keys folders
USER root
RUN chown -R app:app /app/wwwroot
# back to non-root user
USER app

ENTRYPOINT [ "sh", "-c", "\
if [ -n \"$APPSETTINGS_JSON\" ]; then \
  echo \"$APPSETTINGS_JSON\" > /app/wwwroot/appsettings.json && \
  echo \"Created appsettings.json\"; \
else \
  echo \"APPSETTINGS_JSON is empty, skipping file creation\"; \
fi && \
dotnet Aguacongas.FreeTheIdServer.Open.dll" ]



# FreeTheIdServer

**FreeTheIdServer** is a clone of [TheIdServer](https://github.com/Aguafrommars/TheIdServer) but using [Open.IdentityServer](https://github.com/RockSolidKnowledge/Open.IdentityServer) instead of *Duende IdentityServer* so it's free.

[OpenID/Connect](https://openid.net/connect/), [OAuth2](https://oauth.net/2/), [WS-Federation](https://docs.oasis-open.org/wsfed/federation/v1.2/os/ws-federation-1.2-spec-os.html) and [SAML 2.0](http://docs.oasis-open.org/security/saml/v2.0/sstc-saml-approved-errata-2.0.html) server based on [Open.IdentityServer](https://github.com/RockSolidKnowledge/Open.IdentityServer) and [ITfoxtec Identity SAML 2.0](https://www.itfoxtec.com/IdentitySaml2).

> [OpenID/Connect](https://openid.net/connect/), [OAuth2](https://oauth.net/2/), [WS-Federation](https://docs.oasis-open.org/wsfed/federation/v1.2/os/ws-federation-1.2-spec-os.html) and [SAML 2.0](http://docs.oasis-open.org/security/saml/v2.0/sstc-saml-approved-errata-2.0.html) are protocols that enable secure authentication and authorization of users and applications on the web. They allow users to sign in with their existing credentials from an identity provider (such as Google, Facebook, Microsoft, Twitter ans so-on) and grant access to their data and resources on different platforms and services. These protocols also enable developers to create applications that can interact with various APIs and resources without exposing the user's credentials or compromising their privacy. Some examples of applications that use these protocols are web browsers, mobile apps, web APIs, and single-page applications.

> As *Duende IdentityServer*, [Open.IdentityServer](https://github.com/RockSolidKnowledge/Open.IdentityServer) is a framework that implements OpenID Connect and OAuth 2.0 protocols for ASP.NET Core applications. It allows you to create your own identity and access management solution that can integrate with various identity providers and APIs.

> [ITfoxtec Identity SAML 2.0](https://www.itfoxtec.com/IdentitySaml2) is a framework that implements SAML-P for both Identity Provider (IdP) and Relying Party (RP).

> FreeTheIdServer implements all Open.IdentityServer features, a SAML 2.0 Identity Provider and comes with an admin UI.

[![Quality gate](https://sonarcloud.io/api/project_badges/quality_gate?project=aguacongas_FreeTheIdServer)](https://sonarcloud.io/dashboard?id=aguacongas_FreeTheIdServer)

[![Build status](https://ci.appveyor.com/api/projects/status/hutfs4sy38fy9ca7?svg=true)](https://ci.appveyor.com/project/aguacongas/FreeTheIdServer) [![Docker](https://github.com/Aguafrommars/FreeTheIdServer/actions/workflows/docker.yml/badge.svg)](https://github.com/Aguafrommars/FreeTheIdServer/actions/workflows/docker.yml) [![Artifact HUB](https://img.shields.io/endpoint?url=https://artifacthub.io/badge/repository/aguafrommars)](https://artifacthub.io/packages/search?repo=aguafrommars)
[![libs.tech recommends](https://libs.tech/project/206938663/badge.svg)](https://libs.tech/project/206938663/FreeTheIdServer)

## ⚠️ Azure Key Vault Update

FreeTheIdServer uses the modern **Azure.Security.KeyVault.Keys** SDK. The old `Microsoft.Azure.KeyVault` SDK is obsolete.

**Key changes:**
- `AzureKeyVaultTenantId` is now **required** when using Service Principal authentication
- New **DefaultAzureCredential** support (recommended) - works with Managed Identity and Azure CLI
- Existing encrypted data remains **100% compatible**

See [Data Protection](doc/DATA_PROTECTION.md#azure-key-vault) and [Keys Rotation](doc/KEYS_ROTATION.md#azure-key-vault) documentation for migration details.

### Documentation

Thanks [@ldeluigi](https://github.com/ldeluigi) and its [markdown-docs GitHub action](https://github.com/ldeluigi/markdown-docs). All markdown files are deployed in html [here](https://aguafrommars.github.io/FreeTheIdServer/).

### Try it now at [https://FreeTheIdServer-Open.herokuapp.com/](https://FreeTheIdServer-Open.herokuapp.com/)

**login**: alice  
**pwd**: Pass123$

An in-memory database version is available on [Heroku](https://www.heroku.com/).

### Give a Star! :star:

If you like or are using this project to learn or start your solution, please give it a star. Thanks!

Or if you're feeling really generous, we support sponsorships.

Choose your favorite:

* [issuehunts](https://issuehunt.io/r/Aguafrommars/FreeTheIdServer/issues/170)
* [github sponsor](https://github.com/sponsors/aguacongas),
* [liberapay](https://liberapay.com/aguacongas)

## Main features

### Admin app
![home](https://raw.githubusercontent.com/Aguafrommars/FreeTheIdServer/master/doc/assets/home.png)

* [Users management](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/USER.md)
* [Roles management](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/ROLE.md)
* [Clients management](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/CLIENT.md)
* [Apis management](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/API.md)
* [Api Scopes management](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/SCOPE.md)
* [Identities management](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/IDENTITY.md)
* [Relying parties management](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/RELYING-PARTY.md)
* [External providers management](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/PROVIDER.md)
* [Localizable](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/LOCALIZATION.md)
* [Export/import configuration](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/EXPORT_IMPORT.md)
* [Keys management](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/KEYS.md)
* [Server settings](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/SETTINGS.md)

### Server

* [OpenID/Connect](https://openid.net/connect/), [OAuth2](https://oauth.net/2/), [WS-Federation](https://docs.oasis-open.org/wsfed/federation/v1.2/os/ws-federation-1.2-spec-os.html) and [Saml2P](http://docs.oasis-open.org/security/saml/v2.0/sstc-saml-approved-errata-2.0.html) server
* [Large choice of database](https://github.com/Aguafrommars/FreeTheIdServer/blob/master/doc/SERVER.md#using-entity-framework-core)
* [Dynamic external provider configuration](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/SERVER.md#configure-the-provider-hub)
* [Public / Private installation](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/SERVER.md#using-the-api)
* [Docker support](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/SERVER.md#from-docker)
* [Claims providers](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/CLAIMS_PROVIDER.md)
* [External claims mapping](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/EXTERNAL_CLAIMS_MAPPING.md)
* [Localizable](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/LOCALIZATION.md)
* [OpenID Connect Dynamic Client Registration](https://openid.net/specs/openid-connect-registration-1_0.html)
* [Auto remove expired tokens](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/SERVER.md#configure-token-cleaner)
* [Keys rotation](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/KEYS_ROTATION.md)
* [Create Personal Access Token](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/PAT.md)
* [Duende CIBA integration](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/CIBA.md)
* [Token exchange](https://datatracker.ietf.org/doc/html/rfc8693)([RFC 8693](https://datatracker.ietf.org/doc/html/rfc8693))
* [Health checks](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/SERVER.md#health-checks)
* [OpenTelemety](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/OPEN_TELEMETRY.md)
* [Server side session](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/SERVER_SIDE_SESSIONS.md)
* [Passwor hashing configuration](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/doc/SERVER.md#configure-password-hashers-options)
  
  
## Setup

* Read the [FreeTheIdServer Web Server](doc/SERVER.md) to configure the Open.IdentityServer.  
* Read the [FreeTheIdServer Admin Application](doc/ADMINAPP.md) for application configuration.  

## Build from source

You can build the solution with Visual Studio or use the `dotnet build` command.  
To build docker images launch at solution root: 

```bash
docker build -t aguacongas/FreeTheIdServer.Open:dev -f "./src/Aguacongas.FreeTheIdServer.Open/Dockerfile" .
docker build -t aguacongas/FreeTheIdServerapp:dev -f "./src/Aguacongas.FreeTheIdServer.BlazorApp/Dockerfile" .
```

## Contribute

We warmly welcome contributions. You can contribute by opening an issue, suggest new a feature, or submit a pull request.

Read [How to contribute](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/CONTRIBUTING.md) and [Contributor Covenant Code of Conduct](https://github.com/Aguafrommars/FreeTheIdServer/tree/master/CODE_OF_CONDUCT.md) for more information.

## OIDC Certification test result

The server pass the [oidcc-basic-certification-test-plan](
https://www.certification.openid.net/plan-detail.html?plan=ZKco5LJhicIlT&public=true) with some warnings. It is anticipated that it will pass the certification process, but we need your assistance. Please sponsor this project to help us pay the required [certification fee](https://openid.net/certification/fees/).

Choose your favorite:

* [github sponsor](https://github.com/sponsors/aguacongas/sponsorships?sponsor=aguacongas&tier_id=151490)
* [issuehunts](https://issuehunt.io/r/Aguafrommars/FreeTheIdServer/issues/170)
* [liberapay](https://liberapay.com/aguacongas)


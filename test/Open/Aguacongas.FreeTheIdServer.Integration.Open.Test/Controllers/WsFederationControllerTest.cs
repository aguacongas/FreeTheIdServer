// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.Data;
using Aguacongas.FreeTheIdServer.IntegrationTest.BlazorApp;
using Aguacongas.FreeTheIdServer.UI;
using Aguacongas.Open.IdentityServer.EntityFramework.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Open.IdentityServer;
using Open.IdentityServer.Services;
using Open.IdentityServer.WsFederation;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml;
using Xunit;
using ISModels = Open.IdentityServer.Models;

namespace Aguacongas.FreeTheIdServer.Integration.Open.Test.Controllers;

[Collection(BlazorAppCollection.Name)]
public class WsFederationControllerTest(FreeTheIdServerFactory factory)
{
    private WebApplicationFactory<AccountController> _factory = factory;

    [Fact]
    public async Task Metadata_should_return_metadata_document_with_key_rotation()
    {
        using var client = _factory.CreateClient();
        using var response = await client.GetAsync("/wsfederation/metadata");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        using var reader = XmlReader.Create(await response.Content.ReadAsStreamAsync());
        var serializer = new Microsoft.IdentityModel.Protocols.WsFederation.WsFederationMetadataSerializer();
        var metadata = serializer.ReadMetadata(reader);

        Assert.NotNull(metadata);
    }

    [Fact]
    public async Task Index_should_return_bad_request_when_request_is_bad()
    {
        using var client = _factory.CreateClient();
        using var response = await client.GetAsync("/wsfederation");
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Index_should_return_bad_request_when_realm_is_not_found()
    {
        using var client = _factory.CreateClient();
        using var response = await client.GetAsync("/wsfederation?wtrealm=notfound&wa=wsignin1.0");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Index_should_return_bad_request_when_realm_is_not_wsfed_client()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        var clientId = $"urn:{Guid.NewGuid()}";
        await context.Clients.AddAsync(new Client
        {
            Id = clientId,
            Enabled = true,
            ProtocolType = "oidc"
        });
        await context.SaveChangesAsync();

        using var client = _factory.CreateClient();
        using var response = await client.GetAsync($"/wsfederation?wtrealm={clientId}&wa=wsignin1.0");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Index_should_return_bad_request_when_relyparty_is_not_found()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        var clientId = $"urn:{Guid.NewGuid()}";
        await context.Clients.AddAsync(new Client
        {
            Id = clientId,
            Enabled = true,
            ProtocolType = IdentityServerConstants.ProtocolTypes.WsFederation
        });
        await context.SaveChangesAsync();

        using var client = _factory.CreateClient();
        using var response = await client.GetAsync($"/wsfederation?wtrealm={clientId}&wa=wsignin1.0");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Index_should_wreply_nor_1st_redirect_uri_are_valid_uri()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        var clientId = $"urn:{Guid.NewGuid()}";
        await context.Clients.AddAsync(new Client
        {
            Id = clientId,
            Enabled = true,
            ProtocolType = IdentityServerConstants.ProtocolTypes.WsFederation,
            RelyingParty = new RelyingParty
            {
                Id = clientId,
                TokenType = WsFederationConstants.TokenTypes.Saml11TokenProfile11,
                DigestAlgorithm = SecurityAlgorithms.Sha256Digest,
                SignatureAlgorithm = SecurityAlgorithms.RsaSha256Signature,
                SamlNameIdentifierFormat = WsFederationConstants.SamlNameIdentifierFormats.UnspecifiedString
            }
        });
        await context.SaveChangesAsync();

        using var client = _factory.CreateClient();
        using var response = await client.GetAsync($"/wsfederation?wtrealm={clientId}&wa=wsignin1.0");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Index_should_redirect_to_login_page_when_user_not_found_in_session()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        var clientId = $"urn:{Guid.NewGuid()}";
        await context.Clients.AddAsync(new Client
        {
            Id = clientId,
            Enabled = true,
            ProtocolType = IdentityServerConstants.ProtocolTypes.WsFederation,
            RelyingParty = new RelyingParty
            {
                Id = clientId,
                TokenType = WsFederationConstants.TokenTypes.Saml11TokenProfile11,
                DigestAlgorithm = SecurityAlgorithms.Sha256Digest,
                SignatureAlgorithm = SecurityAlgorithms.RsaSha256Signature,
                SamlNameIdentifierFormat = WsFederationConstants.SamlNameIdentifierFormats.UnspecifiedString
            }
        });
        await context.SaveChangesAsync();

        using var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        using var response = await client.GetAsync($"/wsfederation?wtrealm={clientId}&wa=wsignin1.0&wreply={client.BaseAddress}");
        Assert.Equal(HttpStatusCode.Found, response.StatusCode);
        Assert.StartsWith("/Account/Login".ToUpperInvariant(), response?.Headers?.Location?.OriginalString.ToUpperInvariant());
    }

    [Fact]
    public async Task Index_should_return_signin_document_when_user_found()
    {
        var userSessionMock = new Mock<IUserSession>();
        var sub = Guid.NewGuid().ToString();
        var name = Guid.NewGuid().ToString();
        var user = new ClaimsPrincipal(
            new ClaimsIdentity(
                [
                    new Claim("name", name),
                    new Claim("sub", sub),
                    new Claim("amr", Guid.NewGuid().ToString())
                ],
                "wsfed",
                "name",
                "role"));
        userSessionMock.Setup(m => m.GetUserAsync()).ReturnsAsync(user);

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        var clientId = $"urn:{Guid.NewGuid()}";
        await context.Clients.AddAsync(new Client
        {
            Id = clientId,
            Enabled = true,
            ProtocolType = IdentityServerConstants.ProtocolTypes.WsFederation,
            AllowedScopes =
            [
                new ClientScope
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = clientId,
                    Scope = "openid"
                },
                new ClientScope
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = clientId,
                    Scope = "profile"
                }
            ],
            RelyingParty = new RelyingParty
            {
                Id = clientId,
                TokenType = WsFederationConstants.TokenTypes.Saml11TokenProfile11,
                DigestAlgorithm = SecurityAlgorithms.Sha256Digest,
                SignatureAlgorithm = SecurityAlgorithms.RsaSha256Signature,
                SamlNameIdentifierFormat = WsFederationConstants.SamlNameIdentifierFormats.UnspecifiedString,
                ClaimMappings =
                [
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.Name,
                        ToClaimType = ClaimTypes.Name
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.Subject,
                        ToClaimType = ClaimTypes.NameIdentifier
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.Email,
                        ToClaimType = ClaimTypes.Email
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.GivenName,
                        ToClaimType = ClaimTypes.GivenName
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.FamilyName,
                        ToClaimType = ClaimTypes.Surname
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.BirthDate,
                        ToClaimType = ClaimTypes.DateOfBirth
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.WebSite,
                        ToClaimType = ClaimTypes.Webpage
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.Gender,
                        ToClaimType = ClaimTypes.Gender
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.Role,
                        ToClaimType = ClaimTypes.Role
                    }
                ]
            }
        });
        await context.SaveChangesAsync();

        var identityContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await identityContext.Users.AddAsync(new User
        {
            Id = sub,
            UserName = name,
            NormalizedUserName = name.ToUpperInvariant(),
            SecurityStamp = Guid.NewGuid().ToString()
        });
        await identityContext.SaveChangesAsync();

        using var client = _factory.CreateClient();
        using var response = await client.GetAsync($"/wsfederation?wtrealm={clientId}&wa=wsignin1.0&wreply={client.BaseAddress}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();

        Assert.NotNull(content);
    }

    [Fact]
    public async Task Index_should_return_signin_document_with_client_claim_when_user_found()
    {
        var sub = Guid.NewGuid().ToString();
        var name = Guid.NewGuid().ToString();
        var user = new ClaimsPrincipal(
            new ClaimsIdentity(
                [
                    new Claim(IdentityModel.JwtClaimTypes.Name, name),
                    new Claim(IdentityModel.JwtClaimTypes.Subject, sub),
                    new Claim(IdentityModel.JwtClaimTypes.AuthenticationMethod, IdentityModel.OidcConstants.AuthenticationMethods.Password)
                ],
                "wsfed",
                "name",
                "role"));

        var userSessionMock = new Mock<IUserSession>();
        userSessionMock.Setup(m => m.GetUserAsync()).ReturnsAsync(user);

        var profileServiceMock = new Mock<IProfileService>();
        profileServiceMock.Setup(m => m.GetProfileDataAsync(It.IsAny<ISModels.ProfileDataRequestContext>()))
            .Callback<ISModels.ProfileDataRequestContext>((ctx) => ctx.IssuedClaims =
            [
                new Claim(IdentityModel.JwtClaimTypes.Name, name),
                new Claim(IdentityModel.JwtClaimTypes.Subject, sub),
                new Claim("http://exemple.com", Guid.NewGuid().ToString()),
            ])
            .Returns(Task.CompletedTask);

        _factory = _factory.WithWebHostBuilder(builder => builder.ConfigureServices(services =>
        {
            services.AddTransient(p => userSessionMock.Object)
                .AddTransient(p => profileServiceMock.Object);
        }));

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        var clientId = $"urn:{Guid.NewGuid()}";
        await context.Clients.AddAsync(new Client
        {
            Id = clientId,
            Enabled = true,
            ProtocolType = IdentityServerConstants.ProtocolTypes.WsFederation,
            AllowedScopes =
            [
                new ClientScope
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = clientId,
                    Scope = "openid"
                },
                new ClientScope
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = clientId,
                    Scope = "profile"
                }
            ],
            ClientClaims =
            [
                new ClientClaim
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = clientId,
                    Type = "http://myorg.com/claim",
                    Value = Guid.NewGuid().ToString()
                }
            ],
            RelyingParty = new RelyingParty
            {
                Id = clientId,
                TokenType = WsFederationConstants.TokenTypes.Saml11TokenProfile11,
                DigestAlgorithm = SecurityAlgorithms.Sha256Digest,
                SignatureAlgorithm = SecurityAlgorithms.RsaSha256Signature,
                SamlNameIdentifierFormat = WsFederationConstants.SamlNameIdentifierFormats.UnspecifiedString,
                ClaimMappings =
                [
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.Name,
                        ToClaimType = ClaimTypes.Name
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.Subject,
                        ToClaimType = ClaimTypes.NameIdentifier
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.Email,
                        ToClaimType = ClaimTypes.Email
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.GivenName,
                        ToClaimType = ClaimTypes.GivenName
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.FamilyName,
                        ToClaimType = ClaimTypes.Surname
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.BirthDate,
                        ToClaimType = ClaimTypes.DateOfBirth
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.WebSite,
                        ToClaimType = ClaimTypes.Webpage
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.Gender,
                        ToClaimType = ClaimTypes.Gender
                    },
                    new RelyingPartyClaimMapping
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelyingPartyId = clientId,
                        FromClaimType = IdentityModel.JwtClaimTypes.Role,
                        ToClaimType = ClaimTypes.Role
                    }
                ]
            }
        });
        await context.SaveChangesAsync();

        var identityContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await identityContext.Users.AddAsync(new User
        {
            Id = sub,
            UserName = name,
            NormalizedUserName = name.ToUpperInvariant(),
            SecurityStamp = Guid.NewGuid().ToString()
        });
        await identityContext.SaveChangesAsync();

        using var client = _factory.CreateClient();
        using var response = await client.GetAsync($"/wsfederation?wtrealm={clientId}&wa=wsignin1.0&wreply={client.BaseAddress}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();

        Assert.NotNull(content);
        Assert.Contains("exemple.com", content);
        Assert.Contains("myorg.com", content);
    }

    [SkipCiFact]
    public async Task Index_should_return_signin_document_for_saml2_token_type_when_user_found()
    {
        var sub = Guid.NewGuid().ToString();
        var name = Guid.NewGuid().ToString();
        var user = new ClaimsPrincipal(
            new ClaimsIdentity(
                [
                    new Claim(IdentityModel.JwtClaimTypes.Name, name),
                    new Claim(IdentityModel.JwtClaimTypes.Subject, sub),
                    new Claim(IdentityModel.JwtClaimTypes.AuthenticationMethod, IdentityModel.OidcConstants.AuthenticationMethods.Password)
                ],
                "wsfed",
                "name",
                "role"));

        var userSessionMock = new Mock<IUserSession>();
        userSessionMock.Setup(m => m.GetUserAsync()).ReturnsAsync(user);
        var profileServiceMock = new Mock<IProfileService>();
        profileServiceMock.Setup(m => m.GetProfileDataAsync(It.IsAny<ISModels.ProfileDataRequestContext>()))
            .Callback<ISModels.ProfileDataRequestContext>((ctx) => ctx.IssuedClaims =
            [
                new Claim(IdentityModel.JwtClaimTypes.Name, name),
                new Claim(IdentityModel.JwtClaimTypes.Subject, sub),
                new Claim("exemple.com", Guid.NewGuid().ToString()),
            ])
            .Returns(Task.CompletedTask);

        _factory = _factory.WithWebHostBuilder(builder => builder.ConfigureServices(services =>
        {
            services.AddTransient(p => userSessionMock.Object)
                .AddTransient(p => profileServiceMock.Object);
        }));

        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

        var clientId = $"urn:{Guid.NewGuid()}";
        await context.Clients.AddAsync(new Client
        {
            Id = clientId,
            Enabled = true,
            ProtocolType = IdentityServerConstants.ProtocolTypes.WsFederation,
            AllowedScopes =
            [
                new ClientScope
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = clientId,
                    Scope = "openid"
                },
                new ClientScope
                {
                    Id = Guid.NewGuid().ToString(),
                    ClientId = clientId,
                    Scope = "profile"
                }
            ],
            RelyingParty = new RelyingParty
            {
                Id = clientId,
                TokenType = WsFederationConstants.TokenTypes.Saml2TokenProfile11,
                DigestAlgorithm = SecurityAlgorithms.Sha256Digest,
                SignatureAlgorithm = SecurityAlgorithms.RsaSha256Signature,
                SamlNameIdentifierFormat = WsFederationConstants.SamlNameIdentifierFormats.UnspecifiedString,
                ClaimMappings = Array.Empty<RelyingPartyClaimMapping>()
            }
        });
        await context.SaveChangesAsync();

        var identityContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await identityContext.Users.AddAsync(new User
        {
            Id = sub,
            UserName = name,
            NormalizedUserName = name.ToUpperInvariant(),
            SecurityStamp = Guid.NewGuid().ToString()
        });
        await identityContext.SaveChangesAsync();

        using var client = _factory.CreateClient();
        using var response = await client.GetAsync($"/wsfederation?wtrealm={clientId}&wa=wsignin1.0&wreply={client.BaseAddress}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();

        Assert.NotNull(content);
        Assert.Contains("exemple.com", content);
    }


    [Fact]
    public async Task Index_should_redirect_to_logout_page_on_signout_message()
    {
        var clientId = $"urn:{Guid.NewGuid()}";

        using var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
        using var response = await client.GetAsync($"/wsfederation?wtrealm={clientId}&wa=wsignout1.0&wreply={client.BaseAddress}");

        Assert.Equal(HttpStatusCode.Found, response.StatusCode);
        Assert.StartsWith("/connect/endsession", response?.Headers?.Location?.OriginalString);
    }
}
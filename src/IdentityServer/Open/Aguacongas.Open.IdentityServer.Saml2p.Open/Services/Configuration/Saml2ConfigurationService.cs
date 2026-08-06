using ITfoxtec.Identity.Saml2;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Open.IdentityServer.Extensions;
using Open.IdentityServer.Stores;

namespace Aguacongas.Open.IdentityServer.Saml2p.Open.Services.Configuration;

/// <summary>
/// Saml2P configuration service
/// </summary>
/// <remarks>
/// Initialize a new instance of <see cref="Saml2ConfigurationService"/>
/// </remarks>
/// <param name="signingCredentialStore"></param>
/// <param name="contextAccessor"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="options"></param>
public class Saml2ConfigurationService(ISigningCredentialStore signingCredentialStore,
    IHttpContextAccessor contextAccessor,
    IHttpContextAccessor httpContextAccessor,
    IOptions<Saml2POptions> options) : ISaml2ConfigurationService
{

    /// <summary>
    /// Gets the configuration
    /// </summary>
    /// <returns>a <see cref="Saml2Configuration"/></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<Saml2Configuration> GetConfigurationAsync()
    {
        var request = (httpContextAccessor.HttpContext?.Request) ?? throw new InvalidOperationException("Http request cannot be null");
        var location = Location(request);
        var settings = options.Value;
        var credentials = await signingCredentialStore.GetSigningCredentialsAsync().ConfigureAwait(false);

        return new Saml2Configuration
        {
            ArtifactResolutionService = new Saml2IndexedEndpoint
            {
                Index = 1,
                Location = new Uri($"{location}/artifact")
            },
            SingleSignOnDestination = new Uri($"{location}/login"),
            SingleLogoutDestination = new Uri($"{location}/logout"),
            Issuer = contextAccessor.HttpContext.GetIdentityServerIssuerUri(),
            SignatureAlgorithm = settings.SignatureAlgorithm,
            SigningCertificate = credentials.Key.GetX509Certificate(signingCredentialStore),
            CertificateValidationMode = settings.CertificateValidationMode,
            RevocationMode = settings.RevocationMode
        };
    }

    private static string Location(HttpRequest request)
    => $"{request.Scheme}://{request.Host}/saml2p";
}

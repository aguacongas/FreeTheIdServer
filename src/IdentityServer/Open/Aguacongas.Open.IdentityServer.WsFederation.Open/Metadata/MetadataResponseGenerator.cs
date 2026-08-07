// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Xml;
using Open.IdentityServer.Extensions;
using Open.IdentityServer.Stores;
using System.Threading;
using System.Threading.Tasks;

namespace Aguacongas.Open.IdentityServer.WsFederation
{
    /// <summary>
    /// Generate Ws-Federation metadata document
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="MetadataResponseGenerator"/> class.
    /// </remarks>
    /// <param name="contextAccessor">The context accessor..</param>
    /// <param name="keys">The keys.</param>
    /// <param name="options">WS-Federation options</param>
    public class MetadataResponseGenerator(IHttpContextAccessor contextAccessor,
        ISigningCredentialStore keys,
        IOptions<WsFederationOptions> options) : IMetadataResponseGenerator
    {

        /// <summary>
        /// Generates the asynchronous.
        /// </summary>
        /// <param name="wsfedEndpoint">The wsfed endpoint.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<WsFederationConfiguration> GenerateAsync(string wsfedEndpoint, CancellationToken cancellationToken)
        {
            var credentials = await keys.GetSigningCredentialsAsync().ConfigureAwait(false);
            var key = credentials.Key;
            var keyInfo = new KeyInfo(key.GetX509Certificate(keys));
            var issuer = contextAccessor.HttpContext.GetIdentityServerIssuerUri();
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature, SecurityAlgorithms.Sha256Digest);
            var config = new WsFederationConfiguration()
            {
                Issuer = issuer,
                TokenEndpoint = wsfedEndpoint,
                SigningCredentials = signingCredentials,
            };
            config.SigningKeys.Add(key);
            config.KeyInfos.Add(keyInfo);
            var settings = options.Value;
            config.ClaimTypesOffered = settings.ClaimTypesOffered;
            config.ClaimTypesRequested = settings.ClaimTypesRequested;
            config.TokenTypesOffered = settings.TokenTypesOffered;
            return config;
        }
    }
}

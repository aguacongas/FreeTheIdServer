// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Open.IdentityServer.Models;
using Open.IdentityServer.Services;
using Open.IdentityServer.Validation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IsConfiguration = Open.IdentityServer.Configuration;

namespace Aguacongas.Open.IdentityServer.Admin.Services;

/// <summary>
/// 
/// </summary>
/// <seealso cref="JwtRequestValidator" />
/// <remarks>
/// Initializes a new instance of the <see cref="CustomJwtRequestValidator" /> class.
/// </remarks>
/// <param name="tokenValidationOptions">The token validation options.</param>
/// <param name="contextAccessor">The context accessor.</param>
/// <param name="options">The options.</param>
/// <param name="telemetry">The telemetry service.</param>
/// <param name="logger">The logger.</param>
/// <exception cref="ArgumentNullException">tokenValidationOptions</exception>
public class CustomJwtRequestValidator(IOptions<TokenValidationParameters> tokenValidationOptions,
    IsConfiguration.IdentityServerOptions options,
    IHttpContextAccessor contextAccessor,
    ITelemetryService telemetry,
    ILogger<CustomJwtRequestValidator> logger) : JwtRequestValidator(contextAccessor, options, telemetry, logger)
{
    private readonly TokenValidationParameters _tokenValidationOptions = tokenValidationOptions?.Value ?? throw new ArgumentNullException(nameof(tokenValidationOptions));

    /// <summary>
    /// Validates the JWT token
    /// </summary>
    /// <param name="jwtTokenString">The raw JWT string to validate.</param>
    /// <param name="keys">The trusted signing keys to validate the JWT signature against.</param>
    /// <param name="client">The client associated with the request, used to validate the issuer.</param>
    /// <returns>
    /// A task that resolves to the validated <see cref="JsonWebToken"/>.
    /// </returns>
    protected override async Task<JsonWebToken> ValidateJwtAsync(string jwtTokenString, IEnumerable<SecurityKey> keys, Client client)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKeys = keys,
            ValidIssuer = client.ClientId,
            ValidAudience = AudienceUri,
            ValidateIssuerSigningKey = _tokenValidationOptions.ValidateIssuerSigningKey,
            ValidateIssuer = _tokenValidationOptions.ValidateIssuer,
            ValidateAudience = _tokenValidationOptions.ValidateAudience,
            ValidateLifetime = _tokenValidationOptions.ValidateLifetime,

            RequireAudience = _tokenValidationOptions.RequireAudience,
            RequireSignedTokens = _tokenValidationOptions.RequireSignedTokens,
            RequireExpirationTime = _tokenValidationOptions.RequireExpirationTime
        };

        if (Options.StrictJarValidation)
        {
            tokenValidationParameters.ValidTypes = [JwtClaimTypes.JwtTypes.AuthorizationRequest];
        }

        var result = await Handler.ValidateTokenAsync(jwtTokenString, tokenValidationParameters).ConfigureAwait(false);
        if (!result.IsValid)
        {
            throw result.Exception;
        }

        return (JsonWebToken)result.SecurityToken;
    }
}
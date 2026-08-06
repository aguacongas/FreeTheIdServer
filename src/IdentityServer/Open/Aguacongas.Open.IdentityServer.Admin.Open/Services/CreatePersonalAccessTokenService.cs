using Aguacongas.Open.IdentityServer.Abstractions;
using Microsoft.AspNetCore.Http;
using Open.IdentityServer;
using Open.IdentityServer.Extensions;
using Open.IdentityServer.Models;
using Open.IdentityServer.Services;
using Open.IdentityServer.Stores;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aguacongas.Open.IdentityServer.Admin.Services;

/// <summary>
/// Creates personal access tokens
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="CreatePersonalAccessTokenService"/> class.
/// </remarks>
/// <param name="contextAccessor">The context accessor</param>
/// <param name="tokenService">The token service.</param>
/// <param name="clientStore">The client store.</param>
/// <param name="resourceStore">The resource store.</param>
/// <exception cref="System.ArgumentNullException">
/// tokenService
/// or
/// clientStore
/// </exception>
/// <exception cref="ArgumentNullException"></exception>
public class CreatePersonalAccessTokenService(IHttpContextAccessor contextAccessor, ITokenService tokenService, IClientStore clientStore, IResourceStore resourceStore) : ICreatePersonalAccessToken
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
    private readonly ITokenService _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    private readonly IClientStore _clientStore = clientStore ?? throw new ArgumentNullException(nameof(clientStore));
    private readonly IResourceStore _resourceStore = resourceStore ?? throw new ArgumentNullException(nameof(resourceStore));

    /// <summary>
    /// Create a personal access token for the current user and client
    /// </summary>
    /// <param name="context">The Http context</param>
    /// <param name="isRefenceToken">Creeate a reference token if true otherwise a JWT token</param>
    /// <param name="lifetimeDays">The token life time</param>
    /// <param name="apis">The list of audience</param>
    /// <param name="scopes">The list of scopes</param>
    /// <param name="claimTypes">The list of claims types</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">apis</exception>
    /// <exception cref="System.InvalidOperationException">Client id not found in user claims.
    /// or
    /// Client not found for client id '{clientId}'.
    /// or
    /// Api '{api}' not found in '{client.ClientName}' allowed scopes.
    /// or
    /// Scope '{scope}' not found in '{client.ClientName}' allowed scopes.</exception>
    public async Task<string> CreatePersonalAccessTokenAsync(HttpContext context,
        bool isRefenceToken,
        int lifetimeDays,
        IEnumerable<string> apis,
        IEnumerable<string> scopes,
        IEnumerable<string> claimTypes)
    {
        CheckParams(apis);

        scopes ??= [];
        claimTypes ??= [];

        claimTypes = claimTypes.Where(c => c != IdentityModel.JwtClaimTypes.Name &&
            c != IdentityModel.JwtClaimTypes.ClientId &&
            c != IdentityModel.JwtClaimTypes.Subject);

        var user = new ClaimsPrincipal(new ClaimsIdentity(context.User.Claims.Select(c =>
        {
            if (JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.TryGetValue(c.Type, out string newType))
            {
                return new Claim(newType, c.Value);
            }
            return c;
        }), "Bearer", IdentityModel.JwtClaimTypes.Name, IdentityModel.JwtClaimTypes.Role));

        var clientId = user.Claims.First(c => c.Type == IdentityModel.JwtClaimTypes.ClientId).Value;
        await ValidateRequestAsync(apis, scopes, user, clientId).ConfigureAwait(false);

        var issuer = _contextAccessor.HttpContext.GetIdentityServerIssuerUri();
        var sub = user.FindFirstValue(IdentityModel.JwtClaimTypes.Subject) ?? user.FindFirstValue("nameid");
        var userName = user.Identity.Name;

        return await _tokenService.CreateSecurityTokenAsync(new Token(IdentityServerConstants.TokenTypes.AccessToken)
        {
            AccessTokenType = isRefenceToken ? AccessTokenType.Reference : AccessTokenType.Jwt,
            Audiences = apis.ToArray(),
            ClientId = clientId,
            Claims = user.Claims.Where(c => claimTypes.Any(t => c.Type == t))
                .Concat(new[]
                {
                    new Claim(IdentityModel.JwtClaimTypes.Name, userName),
                    new Claim(IdentityModel.JwtClaimTypes.ClientId, clientId),
                    new Claim(IdentityModel.JwtClaimTypes.Subject, sub)
                })
                .Concat(scopes.Select(s => new Claim("scope", s)))
                .ToArray(),
            CreationTime = DateTime.UtcNow,
            Lifetime = Convert.ToInt32(TimeSpan.FromDays(lifetimeDays).TotalSeconds),
            Issuer = issuer
        });
    }

    private static void CheckParams(IEnumerable<string> apis)
    {
        ArgumentNullException.ThrowIfNull(apis);
    }

    private async Task ValidateRequestAsync(IEnumerable<string> apis, IEnumerable<string> scopes, ClaimsPrincipal user, string clientId)
    {
        var client = await _clientStore.FindEnabledClientByIdAsync(user.Claims.First(c => c.Type == "client_id").Value).ConfigureAwait(false) ?? throw new InvalidOperationException($"Client not found for client id '{clientId}'.");
        var apiList = await _resourceStore.FindApiScopesByNameAsync(apis).ConfigureAwait(false);
        var apiNotFound = apis.Where(a => !apiList.Any(s => s.Name == a));
        if (apiNotFound.Any())
        {
            throw new InvalidOperationException($"Apis '{string.Join(", ", apiNotFound)}' not found.");
        }
        var scopeNotFound = scopes.Where(s => !client.AllowedScopes.Any(ascope => ascope == s));
        if (scopeNotFound.Any())
        {
            throw new InvalidOperationException($"Scopes '{string.Join(", ", scopeNotFound)}' not found in '{clientId}' allowed scopes.");
        }
    }
}

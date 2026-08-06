using Open.IdentityServer;
using Open.IdentityServer.Models;
using Open.IdentityServer.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aguacongas.Open.IdentityServer.Open.Validators
{
    public class TokenExchangeGrantValidator : IExtensionGrantValidator
    {
        private readonly ITokenValidator _validator;

        public TokenExchangeGrantValidator(ITokenValidator validator)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            // defaults
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest);
            var customResponse = new Dictionary<string, object>
            {
                {IdentityModel.OidcConstants.TokenResponse.IssuedTokenType, IdentityModel.OidcConstants.TokenTypeIdentifiers.AccessToken}
            };

            var subjectToken = context.Request.Raw.Get(IdentityModel.OidcConstants.TokenRequest.SubjectToken);
            var subjectTokenType = context.Request.Raw.Get(IdentityModel.OidcConstants.TokenRequest.SubjectTokenType);
            var scopes = context.Request.Raw.Get(IdentityModel.OidcConstants.TokenRequest.Scope);
            // mandatory parameters
            if (string.IsNullOrWhiteSpace(subjectToken))
            {
                return;
            }

            if (!string.Equals(subjectTokenType, IdentityModel.OidcConstants.TokenTypeIdentifiers.AccessToken))
            {
                return;
            }

            var validationResult = await _validator.ValidateAccessTokenAsync(subjectToken, scopes);
            if (validationResult.IsError)
            {
                return;
            }

            var sub = validationResult.Claims.First(c => c.Type == IdentityModel.JwtClaimTypes.Subject).Value;
            var clientId = validationResult.Claims.First(c => c.Type == IdentityModel.JwtClaimTypes.ClientId).Value;

            var style = context.Request.Raw.Get("exchange_style");

            if (style == "impersonation")
            {
                // set token client_id to original id
                context.Request.ClientId = clientId;

                context.Result = new GrantValidationResult(
                    subject: sub,
                    authenticationMethod: GrantType,
                    customResponse: customResponse);
            }
            else if (style == "delegation")
            {
                // set token client_id to original id
                context.Request.ClientId = clientId;

                var actor = new
                {
                    client_id = context.Request.Client.ClientId
                };

                var actClaim = new Claim(IdentityModel.JwtClaimTypes.Actor, JsonSerializer.Serialize(actor), IdentityServerConstants.ClaimValueTypes.Json);

                context.Result = new GrantValidationResult(
                    subject: sub,
                    authenticationMethod: GrantType,
                    claims: new[] { actClaim },
                    customResponse: customResponse);
            }
            else if (style == "custom")
            {
                context.Result = new GrantValidationResult(
                    subject: sub,
                    authenticationMethod: GrantType,
                    customResponse: customResponse);
            }
        }

        public string GrantType => IdentityModel.OidcConstants.GrantTypes.TokenExchange;
    }
}

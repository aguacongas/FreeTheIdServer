// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.BlazorApp.Models;
using System.Linq;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Pages.Client.Extentions
{
    public static class ClientExtensions
    {
        public static bool IsClientCredentialOnly(this Open.IdentityServer.Store.Entity.Client client)
        {
            return client.AllowedGrantTypes.Any() && client.AllowedGrantTypes.All(g => g.GrantType == "client_credentials");
        }

        public static bool IsWebClient(this Open.IdentityServer.Store.Entity.Client client)
        {
            return client.ProtocolType == "wsfed" ||
                client.ProtocolType == "saml2p" ||
                client.AllowedGrantTypes.Any(g => g.GrantType == "authorization_code" ||
                    g.GrantType == "hybrid" ||
                    g.GrantType == "implicit" ||
                    g.GrantType == "urn:ietf:params:oauth:grant-type:device_code") ||
                client.HasCustomGrantType();
        }

        public static bool IsSpaClient(this Open.IdentityServer.Store.Entity.Client client)
        {
            return client.AllowedGrantTypes.Any(g => g.GrantType == "authorization_code" ||
                    g.GrantType == "implicit") ||
                client.HasCustomGrantType();
        }

        public static bool IsAuthorizationCodeClient(this Open.IdentityServer.Store.Entity.Client client)
        {
            return client.AllowedGrantTypes.Any(g => g.GrantType == "authorization_code") ||
                client.HasCustomGrantType();
        }

        public static bool CanHandlePostLogout(this Open.IdentityServer.Store.Entity.Client client)
        {
            return client.ProtocolType == "saml2p" || client.AllowedGrantTypes.Any(g => g.GrantType == "authorization_code" ||
                    g.GrantType == "hybrid" ||
                    g.GrantType == "implicit") ||
                client.HasCustomGrantType();
        }

        public static bool CanUseRefreshToken(this Open.IdentityServer.Store.Entity.Client client)
        {
            return (client.AllowedGrantTypes.Any(g => g.GrantType == "authorization_code" ||
                    g.GrantType == "hybrid" ||
                    g.GrantType == "password" ||
                    g.GrantType == "urn:openid:params:grant-type:ciba") ||
                client.HasCustomGrantType()) &&
                client.AllowOfflineAccess;
        }

        public static bool IsCiba(this Open.IdentityServer.Store.Entity.Client client)
        {
            return client.AllowedGrantTypes.Any(g => g.GrantType == "urn:openid:params:grant-type:ciba");
        }

        public static bool HasUser(this Open.IdentityServer.Store.Entity.Client client)
        {
            return CanHandlePostLogout(client) || client.IsCiba();
        }

        public static bool IsDevice(this Open.IdentityServer.Store.Entity.Client client)
        {
            return client.AllowedGrantTypes.Any(g => g.GrantType == "urn:ietf:params:oauth:grant-type:device_code") ||
                client.HasCustomGrantType();
        }

        public static bool HasCustomGrantType(this Open.IdentityServer.Store.Entity.Client client)
        {
            return !client.AllowedGrantTypes.Where(g => g.Id != null)
                .All(g => GrantTypes.Instance.ContainsKey(g.GrantType));
        }
    }
}

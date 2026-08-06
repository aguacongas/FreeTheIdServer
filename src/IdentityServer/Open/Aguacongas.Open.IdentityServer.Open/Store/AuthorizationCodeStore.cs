// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using Open.IdentityServer.Stores;
using Open.IdentityServer.Stores.Serialization;
using IdentityModel;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using IsModels = Open.IdentityServer.Models;

namespace Aguacongas.Open.IdentityServer.Store
{
    public class AuthorizationCodeStore : GrantStore<AuthorizationCode, IsModels.AuthorizationCode>, IAuthorizationCodeStore
    {
        public AuthorizationCodeStore(IAdminStore<AuthorizationCode> store,
            IPersistentGrantSerializer serializer) : base(store, serializer)
        {
        }

        public Task<IsModels.AuthorizationCode> GetAuthorizationCodeAsync(string code)
            => GetAsync(code);

        public Task RemoveAuthorizationCodeAsync(string code)
            => RemoveAsync(code);

        public Task<string> StoreAuthorizationCodeAsync(IsModels.AuthorizationCode code)
            => StoreAsync(code, code.CreationTime.AddSeconds(code.Lifetime));

        protected override string GetClientId(IsModels.AuthorizationCode dto)
            => dto?.ClientId;

        protected override string GetSubjectId(IsModels.AuthorizationCode dto)
        {
            var subject = dto?.Subject;
            if (subject == null)
            {
                throw new InvalidOperationException("No subject");
            }

            var idClaim = subject.FindFirst(JwtClaimTypes.Subject) ??
                          subject.FindFirst(ClaimTypes.NameIdentifier) ??
                          subject.FindFirst(JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap[ClaimTypes.NameIdentifier]) ??
                          throw new Exception("Unknown userid");

            return idClaim.Value;
        }

        protected override AuthorizationCode CreateEntity(IsModels.AuthorizationCode dto, string clientId, string subjectId, DateTime? expiration)
        {
            var entitiy = base.CreateEntity(dto, clientId, subjectId, expiration);
            entitiy.SessionId = dto.SessionId;
            return entitiy;
        }
    }
}

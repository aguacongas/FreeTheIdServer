// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using Open.IdentityServer.Models;
using Open.IdentityServer.Stores;
using Open.IdentityServer.Stores.Serialization;
using System;
using System.Threading.Tasks;

namespace Aguacongas.Open.IdentityServer.Store
{
    public class ReferenceTokenStore : GrantStore<ReferenceToken, Token>, IReferenceTokenStore
    {
        public ReferenceTokenStore(IAdminStore<ReferenceToken> store, IPersistentGrantSerializer serializer)
            : base(store, serializer)
        {
        }

        public Task<Token> GetReferenceTokenAsync(string handle)
            => GetAsync(handle);

        public Task RemoveReferenceTokenAsync(string handle)
            => RemoveAsync(handle);

        public async Task RemoveReferenceTokensAsync(string subjectId, string clientId)
        {
            await RemoveAsync(subjectId, clientId).ConfigureAwait(false);
        }


        public Task<string> StoreReferenceTokenAsync(Token token)
            => StoreAsync(token, token.CreationTime.AddSeconds(token.Lifetime));

        protected override string GetClientId(Token dto)
            => dto?.ClientId;

        protected override string GetSubjectId(Token dto)
            => dto?.SubjectId;

        protected override ReferenceToken CreateEntity(Token dto, string clientId, string subjectId, DateTime? expiration)
        {
            var entity = base.CreateEntity(dto, clientId, subjectId, expiration);
            entity.SessionId = dto.SessionId;
            return entity;
        }
    }
}

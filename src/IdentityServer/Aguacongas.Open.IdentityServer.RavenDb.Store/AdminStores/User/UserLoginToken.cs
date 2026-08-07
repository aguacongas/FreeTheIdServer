// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.AdminStores.User
{
    public class UserTokenStore : UserSubEntityStoreBase<Entity.UserToken>
    {
        public UserTokenStore(ScopedAsynDocumentcSession session, ILogger<AdminStore<Entity.UserToken>> logger) : base(session, logger)
        {
        }

        protected override ICollection<Entity.UserToken> GetCollection(Entity.User user)
        {
            if (user.UserTokens == null)
            {
                user.UserTokens = new List<Entity.UserToken>();
            }

            return user.UserTokens;
        }
    }
}

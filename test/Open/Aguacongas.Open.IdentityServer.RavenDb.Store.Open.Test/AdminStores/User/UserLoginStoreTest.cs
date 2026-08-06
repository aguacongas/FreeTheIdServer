// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.RavenDb.Store.AdminStores.User;
using Aguacongas.Open.IdentityServer.Store;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents.Session;
using System.Collections.Generic;
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Test.AdminStores.User
{
    public class UserLoginStoreTest : UserSubEntityStoreTestBase<Entity.UserLogin>
    {
        protected override IAdminStore<Entity.UserLogin> CreateSut(IAsyncDocumentSession session, ILogger<AdminStore<Entity.UserLogin>> logger)
        => new UserLoginStore(new ScopedAsynDocumentcSession(session), logger);

        protected override ICollection<Entity.UserLogin> GetCollection(Entity.User user)
        {
            if (user.UserLogins == null)
            {
                user.UserLogins = new List<Entity.UserLogin>();
            }

            return user.UserLogins;
        }
    }
}

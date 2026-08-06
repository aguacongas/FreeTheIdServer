// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.RavenDb.Store.Client;
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents.Session;
using System.Collections.Generic;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Test.AdminStores.Client
{
    public class ClientScopeStoreTest : ClientSubEntityStoreTestBase<ClientScope>
    {
        protected override IAdminStore<ClientScope> CreateSut(IAsyncDocumentSession session, ILogger<AdminStore<ClientScope>> logger)
        => new ClientScopeStore(new ScopedAsynDocumentcSession(session), logger);

        protected override ICollection<ClientScope> GetCollection(IdentityServer.Store.Entity.Client client)
        {
            if (client.AllowedScopes == null)
            {
                client.AllowedScopes = new List<ClientScope>();
            }

            return client.AllowedScopes;
        }
    }
}

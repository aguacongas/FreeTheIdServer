// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.RavenDb.Store.Client;
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents.Session;
using System.Collections.Generic;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Test.AdminStores.Client
{
    public class ClientLocalizedResourceStoreTest : ClientSubEntityStoreTestBase<ClientLocalizedResource>
    {
        protected override IAdminStore<ClientLocalizedResource> CreateSut(IAsyncDocumentSession session, ILogger<AdminStore<ClientLocalizedResource>> logger)
        => new ClientLocalizedResourceStore(new ScopedAsynDocumentcSession(session), logger);

        protected override ICollection<ClientLocalizedResource> GetCollection(IdentityServer.Store.Entity.Client client)
        {
            if (client.Resources == null)
            {
                client.Resources = new List<ClientLocalizedResource>();
            }

            return client.Resources;
        }
    }
}

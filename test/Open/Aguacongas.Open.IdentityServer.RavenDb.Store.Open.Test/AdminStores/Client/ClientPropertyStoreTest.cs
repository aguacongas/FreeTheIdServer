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
    public class ClientPropertyStoreTest : ClientSubEntityStoreTestBase<ClientProperty>
    {
        protected override IAdminStore<ClientProperty> CreateSut(IAsyncDocumentSession session, ILogger<AdminStore<ClientProperty>> logger)
        => new ClientPropertyStore(new ScopedAsynDocumentcSession(session), logger);

        protected override ICollection<ClientProperty> GetCollection(IdentityServer.Store.Entity.Client client)
        {
            if (client.Properties == null)
            {
                client.Properties = new List<ClientProperty>();
            }

            return client.Properties;
        }
    }
}

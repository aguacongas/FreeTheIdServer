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
    public class ClientGrantTypeStoreTest : ClientSubEntityStoreTestBase<ClientGrantType>
    {
        protected override IAdminStore<ClientGrantType> CreateSut(IAsyncDocumentSession session, ILogger<AdminStore<ClientGrantType>> logger)
        => new ClientGrantTypeStore(new ScopedAsynDocumentcSession(session), logger);

        protected override ICollection<ClientGrantType> GetCollection(IdentityServer.Store.Entity.Client client)
        {
            if (client.AllowedGrantTypes == null)
            {
                client.AllowedGrantTypes = new List<ClientGrantType>();
            }

            return client.AllowedGrantTypes;
        }
    }
}

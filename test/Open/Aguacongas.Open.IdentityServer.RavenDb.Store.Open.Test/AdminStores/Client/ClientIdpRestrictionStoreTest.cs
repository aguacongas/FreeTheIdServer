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
    public class ClientIdpRestrictionStoreTest : ClientSubEntityStoreTestBase<ClientIdpRestriction>
    {
        protected override IAdminStore<ClientIdpRestriction> CreateSut(IAsyncDocumentSession session, ILogger<AdminStore<ClientIdpRestriction>> logger)
        => new ClientIdpRestrictionStore(new ScopedAsynDocumentcSession(session), logger);

        protected override ICollection<ClientIdpRestriction> GetCollection(IdentityServer.Store.Entity.Client client)
        {
            if (client.IdentityProviderRestrictions == null)
            {
                client.IdentityProviderRestrictions = new List<ClientIdpRestriction>();
            }

            return client.IdentityProviderRestrictions;
        }
    }
}

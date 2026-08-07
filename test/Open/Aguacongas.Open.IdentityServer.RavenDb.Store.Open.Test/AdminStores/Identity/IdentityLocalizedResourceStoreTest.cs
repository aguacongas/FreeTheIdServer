// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.RavenDb.Store.Identity;
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents.Session;
using System.Collections.Generic;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Test.AdminStores.Identity
{
    public class IdentityLocalizedResourceStoreTest : IdentitySubEntityStoreTestBase<IdentityLocalizedResource>
    {
        protected override IAdminStore<IdentityLocalizedResource> CreateSut(IAsyncDocumentSession session, ILogger<AdminStore<IdentityLocalizedResource>> logger)
        => new IdentityLocalizedResourceStore(new ScopedAsynDocumentcSession(session), logger);

        protected override ICollection<IdentityLocalizedResource> GetCollection(IdentityResource identity)
        {
            if (identity.Resources == null)
            {
                identity.Resources = new List<IdentityLocalizedResource>();
            }

            return identity.Resources;
        }
    }
}

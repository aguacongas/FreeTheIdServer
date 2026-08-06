// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.RavenDb.Store.Api;
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents.Session;
using System.Collections.Generic;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Test.AdminStores.Api
{
    public class ApiLocalizedResourceStoreTest : ApiSubEntityStoreTestBase<ApiLocalizedResource>
    {
        protected override IAdminStore<ApiLocalizedResource> CreateSut(IAsyncDocumentSession session, ILogger<AdminStore<ApiLocalizedResource>> logger)
        => new ApiLocalizedResourceStore(new ScopedAsynDocumentcSession(session), logger);

        protected override ICollection<ApiLocalizedResource> GetCollection(ProtectResource api)
        {
            if (api.Resources == null)
            {
                api.Resources = new List<ApiLocalizedResource>();
            }
            return api.Resources;
        }
    }
}

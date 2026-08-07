// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.RavenDb.Store.Api;
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Microsoft.Extensions.Logging;
using Raven.Client.Documents.Session;
using System.Collections.Generic;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Test.AdminStores.Api
{
    public class ApiSecretStoreTest : ApiSubEntityStoreTestBase<ApiSecret>
    {
        protected override IAdminStore<ApiSecret> CreateSut(IAsyncDocumentSession session, ILogger<AdminStore<ApiSecret>> logger)
        => new ApiSecretStore(new ScopedAsynDocumentcSession(session), logger);

        protected override ICollection<ApiSecret> GetCollection(ProtectResource api)
        {
            if (api.Secrets == null)
            {
                api.Secrets = new List<ApiSecret>();
            }
            return api.Secrets;
        }
    }
}

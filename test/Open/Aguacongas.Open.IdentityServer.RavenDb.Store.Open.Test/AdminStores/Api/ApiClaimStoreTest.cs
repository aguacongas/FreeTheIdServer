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
    public class ApiClaimStoreTest : ApiSubEntityStoreTestBase<ApiClaim>
    {
        protected override IAdminStore<ApiClaim> CreateSut(IAsyncDocumentSession session, ILogger<AdminStore<ApiClaim>> logger)
        => new ApiClaimStore(new ScopedAsynDocumentcSession(session), logger);

        protected override ICollection<ApiClaim> GetCollection(ProtectResource api)
        {
            if (api.ApiClaims == null)
            {
                api.ApiClaims = new List<ApiClaim>();
            }
            return api.ApiClaims;
        }
    }
}

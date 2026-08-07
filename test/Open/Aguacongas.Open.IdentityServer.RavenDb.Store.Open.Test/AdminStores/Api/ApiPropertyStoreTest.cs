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
    public class ApiPropertyStoreTest : ApiSubEntityStoreTestBase<ApiProperty>
    {
        protected override IAdminStore<ApiProperty> CreateSut(IAsyncDocumentSession session, ILogger<AdminStore<ApiProperty>> logger)
        => new ApiPropertyStore(new ScopedAsynDocumentcSession(session), logger);

        protected override ICollection<ApiProperty> GetCollection(ProtectResource api)
        {
            if (api.Properties == null)
            {
                api.Properties = new List<ApiProperty>();
            }
            return api.Properties;
        }
    }
}

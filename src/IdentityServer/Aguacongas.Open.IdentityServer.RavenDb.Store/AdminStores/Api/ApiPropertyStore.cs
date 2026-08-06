// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Api
{
    public class ApiPropertyStore : ApiSubEntityStoreBase<ApiProperty>
    {
        public ApiPropertyStore(ScopedAsynDocumentcSession session, ILogger<AdminStore<ApiProperty>> logger) : base(session, logger)
        {
        }

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

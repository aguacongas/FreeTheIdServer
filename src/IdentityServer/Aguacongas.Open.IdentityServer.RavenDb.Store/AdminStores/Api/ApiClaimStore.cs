// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Api
{
    public class ApiClaimStore : ApiSubEntityStoreBase<ApiClaim>
    {
        public ApiClaimStore(ScopedAsynDocumentcSession session, ILogger<AdminStore<ApiClaim>> logger) : base(session, logger)
        {
        }

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

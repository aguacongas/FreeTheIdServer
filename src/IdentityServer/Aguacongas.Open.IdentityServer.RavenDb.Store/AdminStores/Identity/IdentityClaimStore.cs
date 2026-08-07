// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Identity
{
    public class IdentityClaimStore : IdentitySubEntityStoreBase<IdentityClaim>
    {
        public IdentityClaimStore(ScopedAsynDocumentcSession session, ILogger<AdminStore<IdentityClaim>> logger) : base(session, logger)
        {
        }

        protected override ICollection<IdentityClaim> GetCollection(IdentityResource identity)
        {
            if (identity.IdentityClaims == null)
            {
                identity.IdentityClaims = new List<IdentityClaim>();
            }

            return identity.IdentityClaims;
        }
    }
}

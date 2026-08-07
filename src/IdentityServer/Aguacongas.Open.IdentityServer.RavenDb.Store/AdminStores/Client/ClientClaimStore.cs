// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Client
{
    public class ClientClaimStore : ClientSubEntityStoreBase<Entity.ClientClaim>
    {
        public ClientClaimStore(ScopedAsynDocumentcSession session, ILogger<AdminStore<Entity.ClientClaim>> logger) : base(session, logger)
        {
        }

        protected override ICollection<Entity.ClientClaim> GetCollection(Entity.Client client)
        {
            if (client.ClientClaims == null)
            {
                client.ClientClaims = new List<Entity.ClientClaim>();
            }

            return client.ClientClaims;
        }
    }
}

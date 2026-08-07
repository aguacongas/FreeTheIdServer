// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Client
{
    public class ClientAllowedIdentityTokenSigningAlgorithmStore : ClientSubEntityStoreBase<Entity.ClientAllowedIdentityTokenSigningAlgorithm>
    {
        public ClientAllowedIdentityTokenSigningAlgorithmStore(ScopedAsynDocumentcSession session, 
            ILogger<AdminStore<Entity.ClientAllowedIdentityTokenSigningAlgorithm>> logger) : base(session, logger)
        {
        }

        protected override ICollection<Entity.ClientAllowedIdentityTokenSigningAlgorithm> GetCollection(Entity.Client client)
        {
            if (client.AllowedIdentityTokenSigningAlgorithms == null)
            {
                client.AllowedIdentityTokenSigningAlgorithms = new List<Entity.ClientAllowedIdentityTokenSigningAlgorithm>();
            }

            return client.AllowedIdentityTokenSigningAlgorithms;
        }
    }
}

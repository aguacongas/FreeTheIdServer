// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Client
{
    public class ClientSecretStore : ClientSubEntityStoreBase<Entity.ClientSecret>
    {
        public ClientSecretStore(ScopedAsynDocumentcSession session, ILogger<AdminStore<Entity.ClientSecret>> logger) : base(session, logger)
        {
        }

        protected override ICollection<Entity.ClientSecret> GetCollection(Entity.Client client)
        {
            if (client.ClientSecrets == null)
            {
                client.ClientSecrets = new List<Entity.ClientSecret>();
            }

            return client.ClientSecrets;
        }
    }
}

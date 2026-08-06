// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Client
{
    public class ClientLocalizedResourceStore : ClientSubEntityStoreBase<Entity.ClientLocalizedResource>
    {
        public ClientLocalizedResourceStore(ScopedAsynDocumentcSession session, ILogger<AdminStore<Entity.ClientLocalizedResource>> logger) : base(session, logger)
        {
        }

        protected override ICollection<Entity.ClientLocalizedResource> GetCollection(Entity.Client client)
        {
            if (client.Resources == null)
            {
                client.Resources = new List<Entity.ClientLocalizedResource>();
            }

            return client.Resources;
        }
    }
}

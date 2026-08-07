// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Client
{
    public class ClientScopeStore : ClientSubEntityStoreBase<Entity.ClientScope>
    {
        public ClientScopeStore(ScopedAsynDocumentcSession session, ILogger<AdminStore<Entity.ClientScope>> logger) : base(session, logger)
        {
        }

        protected override ICollection<Entity.ClientScope> GetCollection(Entity.Client client)
        {
            if (client.AllowedScopes == null)
            {
                client.AllowedScopes = new List<Entity.ClientScope>();
            }

            return client.AllowedScopes;
        }
    }
}

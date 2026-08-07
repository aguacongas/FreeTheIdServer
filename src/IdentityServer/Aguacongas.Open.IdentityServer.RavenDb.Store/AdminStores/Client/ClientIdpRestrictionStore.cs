// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Client
{
    public class ClientIdpRestrictionStore : ClientSubEntityStoreBase<Entity.ClientIdpRestriction>
    {
        public ClientIdpRestrictionStore(ScopedAsynDocumentcSession session, ILogger<AdminStore<Entity.ClientIdpRestriction>> logger) : base(session, logger)
        {
        }

        protected override ICollection<Entity.ClientIdpRestriction> GetCollection(Entity.Client client)
        {
            if (client.IdentityProviderRestrictions == null)
            {
                client.IdentityProviderRestrictions = new List<Entity.ClientIdpRestriction>();
            }

            return client.IdentityProviderRestrictions;
        }
    }
}

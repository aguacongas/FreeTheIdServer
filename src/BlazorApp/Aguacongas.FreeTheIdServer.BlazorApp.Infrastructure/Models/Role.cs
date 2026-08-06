// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store;
using System.Collections.Generic;
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Models
{
    public class Role : Entity.Role, ICloneable<Role>
    {
        public ICollection<Entity.RoleClaim> Claims { get; set; }

        public new Role Clone()
        {
            return MemberwiseClone() as Role;
        }

        public static Role FromEntity(Entity.Role role)
        {
            return new Role
            {
                Id = role.Id,
                Name = role.Name,
                Claims = role.RoleClaims
            };
        }
    }
}

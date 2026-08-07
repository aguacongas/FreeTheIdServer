// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Pages.Roles
{
    public partial class Roles
    {
        protected override string SelectProperties => $"{nameof(Entity.Role.Id)},{nameof(Entity.Role.Name)},{nameof(Entity.Role.ConcurrencyStamp)}";

        protected override string ExportExpand => nameof(Entity.Role.RoleClaims);
    }
}

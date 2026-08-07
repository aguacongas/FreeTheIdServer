// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Pages.RelyingParties
{
    public partial class RelyingParties
    {
        protected override string SelectProperties => $"{nameof(Entity.RelyingParty.Id)},{nameof(Entity.RelyingParty.Description)}";
        protected override string Expand => null;

        protected override string ExportExpand => $"{nameof(Entity.RelyingParty.ClaimMappings)}";
    }
}

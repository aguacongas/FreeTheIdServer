// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Pages.Identity.Components
{
    public partial class IdentityProperties
    {
        private IEnumerable<IdentityProperty> Properties => Collection.Where(p => p.Id == null || (p.Key != null && p.Key.Contains(HandleModificationState.FilterTerm)) || (p.Value != null && p.Value.Contains(HandleModificationState.FilterTerm)));
    }
}

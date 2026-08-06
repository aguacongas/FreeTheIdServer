// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.IdentityServer.Store.Entity;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Pages.ExternalProvider.Components
{
    public partial class ProviderSelect
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public IEnumerable<ExternalProviderKind> Kinds { get; set; }
    }
}

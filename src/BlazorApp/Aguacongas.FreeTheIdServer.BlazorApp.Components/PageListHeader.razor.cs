// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store;
using Microsoft.AspNetCore.Components;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Components
{
    public partial class PageListHeader
    {
        [Parameter]
        public string Url { get; set; }

        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public PageRequest ExportRequest { get; set; }

        [Parameter]
        public bool ExportDisabled { get; set; }

        protected override void OnInitialized()
        {
            Localizer.OnResourceReady = () => InvokeAsync(StateHasChanged);
            base.OnInitialized();
        }
    }
}

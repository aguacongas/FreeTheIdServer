// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.AspNetCore.Components;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Components
{
    public partial class PageLoading
    {
        [Parameter]
        public string Information { get; set; }

        protected override void OnInitialized()
        {
            Localizer.OnResourceReady = () => InvokeAsync(StateHasChanged);
            base.OnInitialized();
        }
    }
}

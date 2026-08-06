// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.AspNetCore.Components;
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Pages.Culture.Components
{
    public partial class LocalizedResource
    {
        [Parameter]
        public Entity.LocalizedResource Model { get; set; }

        protected override void OnInitialized()
        {
            Localizer.OnResourceReady = () => InvokeAsync(StateHasChanged);
            base.OnInitialized();
        }
    }
}

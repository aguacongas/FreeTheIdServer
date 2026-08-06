// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
namespace Aguacongas.FreeTheIdServer.BlazorApp.Components
{
    public partial class Spinner
    {
        protected override void OnInitialized()
        {
            Localizer.OnResourceReady = () => InvokeAsync(StateHasChanged);
            base.OnInitialized();
        }
    }
}

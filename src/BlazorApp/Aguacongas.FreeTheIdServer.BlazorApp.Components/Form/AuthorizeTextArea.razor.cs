// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.AspNetCore.Components;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Components.Form
{
    public partial class AuthorizeTextArea
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public int? MaxLength { get; set; }
    }
}

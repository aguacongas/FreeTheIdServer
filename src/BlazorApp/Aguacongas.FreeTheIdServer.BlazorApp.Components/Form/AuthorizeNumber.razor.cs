// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.AspNetCore.Components;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Components.Form
{
    public partial class AuthorizeNumber<T>
    {
        [Parameter]
        public string Id { get; set; }
        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public int? Max { get; set; }

        [Parameter]
        public int? Min { get; set; }
    }
}

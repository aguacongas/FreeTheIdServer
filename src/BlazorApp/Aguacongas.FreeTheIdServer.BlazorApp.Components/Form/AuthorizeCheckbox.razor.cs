// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Components.Form
{
    public partial class AuthorizeCheckbox
    {
        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public string Label { get; set; }

        private Task Togle()
        {
            Value = !Value;
            EditContext.NotifyFieldChanged(FieldIdentifier);
            return ValueChanged.InvokeAsync(Value);
        }
    }
}

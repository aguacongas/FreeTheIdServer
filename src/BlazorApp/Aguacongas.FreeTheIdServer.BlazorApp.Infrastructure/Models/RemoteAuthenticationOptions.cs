// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
namespace Aguacongas.FreeTheIdServer.BlazorApp.Models
{
    public abstract class RemoteAuthenticationOptions
    {
        public bool SaveTokens { get; set; }

        public string CallbackPath { get; set; }

        public string AccessDeniedPath { get; set; }

        public string ReturnUrlParameter { get; set; }
    }
}

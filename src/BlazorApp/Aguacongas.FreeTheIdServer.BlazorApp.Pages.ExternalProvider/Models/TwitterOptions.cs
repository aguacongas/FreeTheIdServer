// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
namespace Aguacongas.FreeTheIdServer.BlazorApp.Models
{
    public class TwitterOptions : RemoteAuthenticationOptions
    {

        public string ConsumerKey { get; set; }

        public string ConsumerSecret { get; set; }

        public bool RetrieveUserDetails { get; set; }
    }
}

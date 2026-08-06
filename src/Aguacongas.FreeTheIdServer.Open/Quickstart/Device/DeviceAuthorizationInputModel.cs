// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.UI;

namespace Aguacongas.Open.IdentityServer.UI.Device
{
    public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        public string? UserCode { get; set; }
    }
}
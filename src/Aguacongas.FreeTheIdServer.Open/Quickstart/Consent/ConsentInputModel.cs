// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using System.Collections.Generic;

namespace Aguacongas.FreeTheIdServer.UI
{
    public class ConsentInputModel
    {
        public string? Button { get; set; }
        public IEnumerable<string>? ScopesConsented { get; set; }
        public bool RememberConsent { get; set; }
        public string? ReturnUrl { get; set; }
        public string? Description { get; set; }
    }
}
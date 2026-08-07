// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using System;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Models
{
    public class Notification
    {
        public Guid Id { get; } = Guid.NewGuid();

        public bool IsError { get; set; }

        public string Header { get; set; }
        
        public string Message { get; set; }
    }
}

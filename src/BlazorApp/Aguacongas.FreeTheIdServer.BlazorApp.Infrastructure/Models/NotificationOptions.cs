// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using System;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Models
{
    public class NotificationOptions
    {
        public Guid Id { get; set; }

        public bool Animation { get; set; } = true;

        public bool Autohide { get; set; } = true;

        public int Delay { get; set; }
    }
}

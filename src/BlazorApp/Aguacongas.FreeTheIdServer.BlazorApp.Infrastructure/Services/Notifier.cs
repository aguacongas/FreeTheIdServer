// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.BlazorApp.Models;
using System;
using System.Threading.Tasks;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Services
{

    public class Notifier
    {
        public Func<Notification, Task> Show { get; set; }

        public async Task NotifyAsync(Notification notification)
        {
            if (Show != null)
            {
                await Show.Invoke(notification).ConfigureAwait(false);
            }
        }
    }
}

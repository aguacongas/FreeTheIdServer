// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.BlazorApp.Models;
using System.Collections.Generic;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Components
{
    public partial class Toaster
    {
        private readonly List<Notification> _notifications = new List<Notification>();

        protected override void OnInitialized()
        {
            _notifier.Show = notification =>
            {
                _notifications.Add(notification);
                return InvokeAsync(StateHasChanged);
            };
        }

        private void OnToastClosed(Notification notification)
        {
            _notifications.Remove(notification);
            InvokeAsync(StateHasChanged);
        }
    }
}

// Project: Aguafrommars/TheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Pages.User.Components
{
    public partial class UserLogins
    {
        private IEnumerable<UserLogin> Logins => Collection.Where(l => l.ProviderDisplayName.Contains(HandleModificationState.FilterTerm));
    }
}

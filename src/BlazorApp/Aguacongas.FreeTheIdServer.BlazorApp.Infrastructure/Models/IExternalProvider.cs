// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using System.Collections.Generic;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Models
{
    public interface IExternalProvider<TOptions> where TOptions : class
    {
        string Id { get; }
        TOptions DefaultOptions { get; }
        IEnumerable<ExternalProviderKind> Kinds { get; set; }
        TOptions Options { get; set; }
    }
}
// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Services
{
    public interface IReadOnlyLocalizedResourceStore
    {
        Task<PageResponse<LocalizedResource>> GetAsync(PageRequest pageRequest, CancellationToken cancellationToken = default);
    }
}

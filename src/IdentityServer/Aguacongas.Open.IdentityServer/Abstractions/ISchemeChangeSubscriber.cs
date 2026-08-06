// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using System.Threading;
using System.Threading.Tasks;

namespace Aguacongas.Open.IdentityServer.Abstractions
{
    public interface ISchemeChangeSubscriber
    {
        Task SubscribeAsync(CancellationToken cancellationToken);
        Task UnSubscribeAsync(CancellationToken cancellationToken);
    }
}
// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aguacongas.Open.IdentityServer.Abstractions
{
    public interface ISetupClaimsProvider
    {
        IServiceCollection SetupClaimsProvider(IServiceCollection services, IConfiguration configuration);
    }
}

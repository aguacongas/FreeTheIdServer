// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.AspNetCore.Authentication;
using Aguacongas.Open.IdentityServer.Abstractions;
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Aguacongas.FreeTheIdServer.Authentication;
using Microsoft.AspNetCore.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DynamicAuthenticationBuilderExtensions
    {
        public static DynamicAuthenticationBuilder AddNotifyChangedExternalProviderStore<TStore>(this DynamicAuthenticationBuilder builder)
            where TStore : IAdminStore<ExternalProvider>
        {
            builder.Services.AddTransient<IAdminStore<ExternalProvider>>(p =>
            {
                var store = p.GetRequiredService<TStore>();

                return new NotifyChangedExternalProviderStore<TStore>(
                    store,
                    p.GetRequiredService<IProviderClient>(),
                    new PersistentDynamicManager<SchemeDefinition>(
                        p.GetRequiredService<IAuthenticationSchemeProvider>(),
                        p.GetRequiredService<OptionsMonitorCacheWrapperFactory>(),
                        new DynamicProviderStore<SchemeDefinition>(
                            store,
                            p.GetRequiredService<IAuthenticationSchemeOptionsSerializer>()),
                        builder.HandlerTypes),
                    p.GetRequiredService<IAuthenticationSchemeOptionsSerializer>());
            });
            return builder;
        }

    }
}

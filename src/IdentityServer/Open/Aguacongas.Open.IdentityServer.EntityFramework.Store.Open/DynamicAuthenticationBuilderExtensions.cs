// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.AspNetCore.Authentication;
using Aguacongas.IdentityServer.Abstractions;
using Aguacongas.IdentityServer.EntityFramework.Store;
using Aguacongas.IdentityServer.Store;
using Aguacongas.IdentityServer.Store.Entity;
using Aguacongas.FreeTheIdServer.Authentication;
using Microsoft.AspNetCore.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DynamicAuthenticationBuilderExtensions
    {
        public static DynamicAuthenticationBuilder AddFreeTheIdServerEntityFrameworkStore(this DynamicAuthenticationBuilder builder)
        {
            return builder.AddFreeTheIdServerEntityFrameworkStore<SchemeDefinition>();
        }

        public static DynamicAuthenticationBuilder AddFreeTheIdServerEntityFrameworkStore<TSchemeDefinition>(this DynamicAuthenticationBuilder builder)
            where TSchemeDefinition : SchemeDefinitionBase, new()
        {
            return builder.AddFreeTheIdServerStore<TSchemeDefinition>()
                .AddNotifyChangedExternalProviderStore<CacheAdminStore<AdminStore<ExternalProvider, ConfigurationDbContext>, ExternalProvider>>();
        }
    }
}

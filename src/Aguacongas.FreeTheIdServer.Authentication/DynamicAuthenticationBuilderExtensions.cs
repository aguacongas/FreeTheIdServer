// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.AspNetCore.Authentication;
using Aguacongas.FreeTheIdServer.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DynamicAuthenticationBuilderExtensions
    {
        public static DynamicAuthenticationBuilder AddFreeTheIdServerStore(this DynamicAuthenticationBuilder builder)
        {
            return builder.AddFreeTheIdServerStore<SchemeDefinition>();
        }

        public static DynamicAuthenticationBuilder AddFreeTheIdServerStore<TSchemeDefinition>(this DynamicAuthenticationBuilder builder)
            where TSchemeDefinition : SchemeDefinitionBase, new()
        {
            builder.Services
                .AddTransient<IDynamicProviderStore<TSchemeDefinition>, DynamicProviderStore<TSchemeDefinition>>()
                .AddTransient<IAuthenticationSchemeOptionsSerializer, AuthenticationSchemeOptionsSerializer>();

            return builder;
        }
    }
}

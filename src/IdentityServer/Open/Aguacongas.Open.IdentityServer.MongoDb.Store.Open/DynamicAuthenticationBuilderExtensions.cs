// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.AspNetCore.Authentication;
using Aguacongas.Open.IdentityServer.MongoDb.Store;
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Aguacongas.FreeTheIdServer.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="IMvcBuilder"/> extensions
    /// </summary>
    public static class DynamicAuthenticationBuilderExtensions
    {
        /// <summary>
        /// Adds the mongo database store.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static DynamicAuthenticationBuilder AddFreeTheIdServerEntityMongoDbStore(this DynamicAuthenticationBuilder builder)
        {
            return builder.AddFreeTheIdServerEntityMongoDbStore<SchemeDefinition>();
        }

        /// <summary>
        /// Adds the identity server admin.
        /// </summary>
        /// <typeparam name="TSchemeDefinition">The type of the scheme definition.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static DynamicAuthenticationBuilder AddFreeTheIdServerEntityMongoDbStore<TSchemeDefinition>(this DynamicAuthenticationBuilder builder)
            where TSchemeDefinition : SchemeDefinitionBase, new()
        {
            return builder.AddFreeTheIdServerStore<TSchemeDefinition>()
                .AddNotifyChangedExternalProviderStore<CacheAdminStore<AdminStore<ExternalProvider>, ExternalProvider>>();
        }
    }
}

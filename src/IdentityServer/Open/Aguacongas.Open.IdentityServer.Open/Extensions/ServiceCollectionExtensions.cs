// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.Authentication;
using Aguacongas.Open.IdentityServer;
using Aguacongas.Open.IdentityServer.Abstractions;
using Aguacongas.Open.IdentityServer.Open.Validators;
using Aguacongas.Open.IdentityServer.Store;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Open.IdentityServer.Services;
using Open.IdentityServer.Stores;
using Open.IdentityServer.Stores.Serialization;
using Open.IdentityServer.Validation;
using System.Net.Http;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Service collection extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the identity provider store in DI container.
        /// </summary>
        /// <param name="services">The collection of service.</param>
        /// <returns></returns>
        public static IServiceCollection AddIdentityProviderStore(this IServiceCollection services)
        {
            return services
                .AddSingleton(p => new OAuthTokenManager(p.GetRequiredService<HttpClient>(), p.GetRequiredService<IOptions<IdentityServerOptions>>()))
                .AddTransient<OAuthDelegatingHandler>()
                .AddTransient(p => new HttpClient(p.GetRequiredService<HttpClientHandler>()));
        }

        public static IServiceCollection AddConfigurationStores(this IServiceCollection services)
        {
            services.TryAddScoped(typeof(IFlushableCache<>), typeof(FlushableCache<>));

            return services.AddTransient<ClientStore>()
                .AddTransient<IClientStore, ValidatingClientStore<ClientStore>>()
                .AddTransient<IResourceStore, ResourceStore>()
                .AddTransient<ICorsPolicyService, CorsPolicyService>()
                .AddTransient<IExternalProviderKindStore, ExternalProviderKindStore<SchemeDefinition>>();
        }

        public static IServiceCollection AddOperationalStores(this IServiceCollection services)
        {
            return services.AddTransient<AuthorizationCodeStore>()
                .AddTransient<RefreshTokenStore>()
                .AddTransient<ReferenceTokenStore>()
                .AddTransient<UserConsentStore>()
                .AddTransient<DeviceFlowStore>()
                .AddTransient<IPersistentGrantSerializer, PersistentGrantSerializer>()
                .AddTransient<IAuthorizationCodeStore>(p => p.GetRequiredService<AuthorizationCodeStore>())
                .AddTransient<IRefreshTokenStore>(p => p.GetRequiredService<RefreshTokenStore>())
                .AddTransient<IReferenceTokenStore>(p => p.GetRequiredService<ReferenceTokenStore>())
                .AddTransient<IUserConsentStore>(p => p.GetRequiredService<UserConsentStore>())
                .AddTransient<IDeviceFlowStore>(p => p.GetRequiredService<DeviceFlowStore>())
                .AddTransient<IPersistedGrantStore, PersistedGrantStore>();
        }

        public static IServiceCollection AddTokenExchange(this IServiceCollection services)
        => services.AddTransient<IExtensionGrantValidator, TokenExchangeGrantValidator>();

    }
}

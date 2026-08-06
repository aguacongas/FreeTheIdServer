// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Abstractions;
using Aguacongas.Open.IdentityServer.Admin.Services;
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.FreeTheIdServer.Authentication;
using Aguacongas.FreeTheIdServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using Raven.Client.Documents;
using System.Linq;
using System.Reflection;
using Xunit;
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;
using ISConfiguration = Open.IdentityServer.Configuration;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Test.Extensions
{
    public class ServiceCollectionExtensionsTest
    {
        [Fact]
        public void AddIdentityServer4AdminRavenDbkStores_should_add_ravendb_stores_for_each_entity()
        {
            var services = new ServiceCollection().AddLogging();
            
            var wrapper = new RavenDbTestDriverWrapper();
            services.AddFreeTheIdServerRavenDbStores()
                .AddLogging()
                .Configure<MemoryCacheOptions>(options => { })
                .Configure<ISConfiguration.IdentityServerOptions>(options => { })
                .AddTransient(p => p.GetRequiredService<IOptions<ISConfiguration.IdentityServerOptions>>().Value)
                .AddScoped(typeof(IFlushableCache<>), typeof(FlushableCache<>))
                .AddSingleton<HubConnectionFactory>()
                .AddTransient(p => new Mock<IConfiguration>().Object)
                .AddTransient<IProviderClient, ProviderClient>()
                .AddTransient(p => wrapper.GetDocumentStore());

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddFreeTheIdServerStores();

            services.AddAuthentication()
                .AddDynamic<SchemeDefinition>()
                .AddFreeTheIdServerStoreRavenDbStore()
                .AddGoogle();                

            var assembly = typeof(Entity.IEntityId).GetTypeInfo().Assembly;
            var entityTypeList = assembly.GetTypes().Where(t => 
                t.IsClass &&
                !t.IsAbstract &&
                !t.IsGenericType &&
                t.GetInterface(nameof(Entity.IEntityId)) != null);

            var provider = services.BuildServiceProvider();
            foreach(var entityType in entityTypeList)
            {
                var storeType = typeof(IAdminStore<>).MakeGenericType(entityType);
                Assert.NotNull(provider.GetService(storeType));
            }
        }

        [Fact]
        public void AddIdentityServer4AdminRavenDbkStores_should_add_ravendb_stores_for_each_entity_using_getDocumentStore_function()
        {
            var services = new ServiceCollection().AddLogging();

            var wrapper = new RavenDbTestDriverWrapper();
            services.AddFreeTheIdServerRavenDbStores(p => new RavenDbTestDriverWrapper().GetDocumentStore())
                .AddLogging()
                .Configure<MemoryCacheOptions>(options => { })
                .Configure<ISConfiguration.IdentityServerOptions>(options => { })
                .AddTransient(p => p.GetRequiredService<IOptions<ISConfiguration.IdentityServerOptions>>().Value)
                .AddScoped(typeof(IFlushableCache<>), typeof(FlushableCache<>))
                .AddSingleton<HubConnectionFactory>()
                .AddTransient(p => new Mock<IConfiguration>().Object)
                .AddTransient<IProviderClient, ProviderClient>()
                .AddTransient(p => wrapper.GetDocumentStore());

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddFreeTheIdServerStores();

            services.AddAuthentication()
                .AddDynamic<SchemeDefinition>()
                .AddFreeTheIdServerStoreRavenDbStore()
                .AddGoogle();

            var assembly = typeof(Entity.IEntityId).GetTypeInfo().Assembly;
            var entityTypeList = assembly.GetTypes().Where(t => t.IsClass &&
                !t.IsAbstract &&
                !t.IsGenericType &&
                t.GetInterface(nameof(Entity.IEntityId)) != null);

            var provider = services.BuildServiceProvider();
            foreach (var entityType in entityTypeList)
            {
                var storeType = typeof(IAdminStore<>).MakeGenericType(entityType);
                Assert.NotNull(provider.GetService(storeType));
            }
        }
    }
}

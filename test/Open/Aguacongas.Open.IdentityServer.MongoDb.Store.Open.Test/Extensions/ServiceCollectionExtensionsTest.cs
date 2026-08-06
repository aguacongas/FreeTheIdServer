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
using MongoDB.Driver;
using Moq;
using System;
using System.Linq;
using System.Reflection;
using Xunit;
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;
using ISConfiguration = Open.IdentityServer.Configuration;

namespace Aguacongas.Open.IdentityServer.MongoDb.Store.Test.Extensions
{
    public class ServiceCollectionExtensionsTest
    {
        [Fact]
        public void AddIdentityServer4AdminMongoDbkStores_with_connectionString_should_add_ravendb_stores_for_each_entity()
        {
            var services = new ServiceCollection().AddLogging();

            services.AddFreeTheIdServerMongoDbStores("mongodb://localhost/test")
                .AddLogging()
                .Configure<MemoryCacheOptions>(options => { })
                .Configure<ISConfiguration.IdentityServerOptions>(options => { })
                .AddTransient(p => p.GetRequiredService<IOptions<ISConfiguration.IdentityServerOptions>>().Value)
                .AddScoped(typeof(IFlushableCache<>), typeof(FlushableCache<>))
                .AddSingleton<HubConnectionFactory>()
                .AddTransient(p => new Mock<IConfiguration>().Object)
                .AddTransient<IProviderClient, ProviderClient>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddFreeTheIdServerStores();

            services.AddAuthentication()
                .AddDynamic<SchemeDefinition>()
                .AddFreeTheIdServerEntityMongoDbStore()
                .AddGoogle();

            var assembly = typeof(Entity.IEntityId).GetTypeInfo().Assembly;
            var entityTypeList = assembly.GetTypes().Where(t =>
                t.IsClass &&
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

        [Fact]
        public void AddIdentityServer4AdminMongoDbkStores_with_getDatabase_should_add_ravendb_stores_for_each_entity()
        {
            var services = new ServiceCollection().AddLogging();

            var connectionString = "mongodb://localhost/test";
            var uri = new Uri(connectionString);
            services.AddFreeTheIdServerMongoDbStores(p => p.GetRequiredService<IMongoDatabase>())
                .AddLogging()
                .Configure<MemoryCacheOptions>(options => { })
                .Configure<ISConfiguration.IdentityServerOptions>(options => { })
                .AddTransient(p => p.GetRequiredService<IOptions<ISConfiguration.IdentityServerOptions>>().Value)
                .AddScoped(typeof(IFlushableCache<>), typeof(FlushableCache<>))
                .AddScoped<IMongoClient>(p => new MongoClient(connectionString))
                .AddScoped(p => p.GetRequiredService<IMongoClient>().GetDatabase(uri.Segments[1]))
                .AddSingleton<HubConnectionFactory>()
                .AddTransient(p => new Mock<IConfiguration>().Object)
                .AddTransient<IProviderClient, ProviderClient>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddFreeTheIdServerStores();

            services.AddAuthentication()
                .AddDynamic<SchemeDefinition>()
                .AddFreeTheIdServerEntityMongoDbStore()
                .AddGoogle();

            var assembly = typeof(Entity.IEntityId).GetTypeInfo().Assembly;
            var entityTypeList = assembly.GetTypes().Where(t =>
                t.IsClass &&
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

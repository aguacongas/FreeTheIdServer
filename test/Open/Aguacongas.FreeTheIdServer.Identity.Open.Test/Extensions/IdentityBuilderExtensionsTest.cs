// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Xunit;

namespace Aguacongas.FreeTheIdServer.Identity.Test.Extensions
{
    public class IdentityBuilderExtensionsTest
    {
        [Fact]
        public void AddFreeTheIdServerStores_should_add_identity_stores()
        {
            var services = new ServiceCollection();
            services.AddTransient<HttpClient>()
                .AddTransient<HttpClientHandler>()
                .AddIdentityProviderStore()
                .AddAdminHttpStores(options => options.ApiUrl = "http://exemple.com")
                .AddOperationalStores()
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddFreeTheIdServerStores();

            var userStore = services.BuildServiceProvider().GetService<IUserStore<IdentityUser>>();
            Assert.NotNull(userStore);

            var roleStore = services.BuildServiceProvider().GetService<IRoleStore<IdentityRole>>();
            Assert.NotNull(roleStore);


            services = new ServiceCollection();
            services.AddTransient<HttpClient>()
                .AddTransient<HttpClientHandler>()
                .AddIdentityProviderStore()
                .AddAdminHttpStores(options => options.ApiUrl = "http://exemple.com")
                .AddOperationalStores()
                .AddIdentityCore<IdentityUser>()
                .AddFreeTheIdServerStores();


            var userOnlyStore = services.BuildServiceProvider().GetService<IUserStore<IdentityUser>>();
            Assert.NotNull(userOnlyStore);
        }
    }
}

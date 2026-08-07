// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.AspNetCore.Authentication;
using Aguacongas.Open.IdentityServer.EntityFramework.Store;
using Aguacongas.FreeTheIdServer.Data;
using Aguacongas.FreeTheIdServer.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.WsFederation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;

namespace Aguacongas.FreeTheIdServer.Authentication.IntegrationTest
{
    public class FreeTheIdServerTestFixture
    {
        /// <summary>
        /// Gets the system under test
        /// </summary>
        public TestServer Sut { get; }

        public FreeTheIdServerTestFixture()
        {
            var dbName = Guid.NewGuid().ToString();
            var factory = new WebApplicationFactory<AccountController>().WithWebHostBuilder(webHostBuilder =>
            {
                webHostBuilder.ConfigureAppConfiguration(configurationManager =>
                {
                    configurationManager.AddJsonFile(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\..\..\src\Aguacongas.FreeTheIdServer.Open\appsettings.json"));
                    configurationManager.AddJsonFile(Path.Combine(Environment.CurrentDirectory, @"appsettings.Test.json"), true);
                })
                .ConfigureTestServices(services =>
                {
                    services.AddFreeTheIdServerAdminEntityFrameworkStores(options =>
                        options.UseInMemoryDatabase(dbName))
                    .AddIdentityProviderStore()
                    .AddConfigurationEntityFrameworkStores(options =>
                        options.UseInMemoryDatabase(dbName))
                    .AddOperationalEntityFrameworkStores(options =>
                        options.UseInMemoryDatabase(dbName))
                    .AddSingleton(provider =>
                        new OptionsMonitorCacheWrapper<CookieAuthenticationOptions>
                        (
                            provider.GetRequiredService<IOptionsMonitorCache<CookieAuthenticationOptions>>(),
                            provider.GetRequiredService<IEnumerable<IPostConfigureOptions<CookieAuthenticationOptions>>>(),
                            (name, configure) =>
                            {

                            }
                        )
                    )
                    .AddSingleton(provider =>
                        new OptionsMonitorCacheWrapper<JwtBearerOptions>
                        (
                            provider.GetRequiredService<IOptionsMonitorCache<JwtBearerOptions>>(),
                            provider.GetRequiredService<IEnumerable<IPostConfigureOptions<JwtBearerOptions>>>(),
                            (name, configure) =>
                            {
                                configure.RequireHttpsMetadata = false;
                            }
                        )
                    )
                    .AddSingleton(provider =>
                        new OptionsMonitorCacheWrapper<WsFederationOptions>
                        (
                            provider.GetRequiredService<IOptionsMonitorCache<WsFederationOptions>>(),
                            provider.GetRequiredService<IEnumerable<IPostConfigureOptions<WsFederationOptions>>>(),
                            (name, configure) =>
                            {

                            }
                        )
                    );

                    services.AddSingleton<TestUserService>()
                        .AddMvc().AddApplicationPart(typeof(Config).Assembly);
                })
                .Configure((context, builder) =>
                {
                    builder.Use(async (context, next) =>
                    {
                        var testService = context.RequestServices.GetRequiredService<TestUserService>();
                        context.User = testService.User;
                        await next();
                    });
                    builder.UseFreeTheIdServer(context.HostingEnvironment, context.Configuration);
                });
            });
            Sut = factory.Server;

            using var scope = factory.Services.CreateScope();
            using var identityContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            identityContext.Database.EnsureCreated();
            using var operationalContext = scope.ServiceProvider.GetRequiredService<OperationalDbContext>();
            operationalContext.Database.EnsureCreated();
            using var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            appContext.Database.EnsureCreated();
        }
    }
}

// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.AspNetCore.Authentication;
using Aguacongas.Open.IdentityServer.Abstractions;
using Aguacongas.Open.IdentityServer.Admin.Models;
using Aguacongas.Open.IdentityServer.Admin.Services;
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.FreeTheIdServer.Api;
using Aguacongas.FreeTheIdServer.Authentication;
using Aguacongas.FreeTheIdServer.Data;
using Aguacongas.FreeTheIdServer.Models;
using Open.IdentityServer;
using Open.IdentityServer.Configuration;
using Open.IdentityServer.Events;
using Open.IdentityServer.Services;
using Open.IdentityServer.Services.KeyManagement;
using Open.IdentityServer.Stores;
using Open.IdentityServer.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;

namespace Microsoft.AspNetCore.Builder
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddFreeTheIdServerApi(this WebApplicationBuilder webApplicationBuilder)
        {
            var configuration = webApplicationBuilder.Configuration;
            var migrationsAssembly = "Aguacongas.FreeTheIdServer.Migrations.SqlServer";
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var services = webApplicationBuilder.Services;
            services.AddFreeTheIdServerAdminEntityFrameworkStores(options =>
                    options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)))
                .AddConfigurationEntityFrameworkStores(options =>
                    options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)))
                .AddOperationalEntityFrameworkStores(options =>
                    options.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));

            services.AddIdentityProviderStore()
                .AddConfigurationStores()
                .AddOperationalStores();

            services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    configuration.Bind(nameof(IdentityOptions), options);
                })
            .AddFreeTheIdServerStores()
            .AddDefaultTokenProviders();

            var signalRBuilder = services.AddSignalR(options => configuration.GetSection("SignalR:HubOptions").Bind(options));
            if (configuration.GetValue<bool>("SignalR:UseMessagePack"))
            {
                signalRBuilder.AddMessagePackProtocol();
            }

            services.Configure<SendGridOptions>(configuration)
                .AddLocalization()
                .AddControllersWithViews(options =>
                {
                    options.AddIdentityServerAdminFilters();
                })
                .AddNewtonsoftJson(options =>
                {
                    var settings = options.SerializerSettings;
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddIdentityServerAdmin<ApplicationUser, SchemeDefinition>()
                .AddFreeTheIdServerEntityFrameworkStore();

            services.AddAuthorization(options =>
                {
                    options.AddPolicy(SharedConstants.WRITERPOLICY, policy =>
                    {
                        policy.RequireAssertion(context =>
                           context.User.IsInRole(SharedConstants.WRITERPOLICY));
                    });
                    options.AddPolicy(SharedConstants.READERPOLICY, policy =>
                    {
                        policy.RequireAssertion(context =>
                           context.User.IsInRole(SharedConstants.READERPOLICY));
                    });
                })
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "https://localhost:7443";
                    options.Audience = "FreeTheIdServeradminapi";
                });


            services.AddDatabaseDeveloperPageExceptionFilter()
                .AddResponseCompression(opts =>
                {
                    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                        new[] { "application/octet-stream" });
                });

            services.AddTransient<ISchemeChangeSubscriber, SchemeChangeSubscriber>();

            services.Configure<IdentityServerOptions>(options => options.IssuerUri = "https://localhost:5443")
                .AddTransient(p => p.GetRequiredService<IOptions<IdentityServerOptions>>().Value)
                .AddTransient<IClientConfigurationValidator, EmptyClientConfigurationValidator>()
                .AddTransient<IEventService, EmptyEventService>();

            services.RemoveAll<ICreatePersonalAccessToken>();
            services.AddTransient<ICreatePersonalAccessToken, CreatePersonalAccessTokenServvice>();

            services.AddScoped<IAuthenticationSchemeOptionsSerializer, AuthenticationSchemeOptionsSerializer>();

            services.AddTransient(p =>
            {
                var handler = new HttpClientHandler();
                if (configuration.GetValue<bool>("DisableStrictSsl"))
                {
#pragma warning disable S4830 // Server certificates should be verified during SSL/TLS connections
                    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, policy) => true;
#pragma warning restore S4830 // Server certificates should be verified during SSL/TLS connections
                }
                return handler;
            });

            return webApplicationBuilder;
        }
    }
}

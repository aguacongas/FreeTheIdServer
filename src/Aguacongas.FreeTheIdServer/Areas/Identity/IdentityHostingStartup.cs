// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer;
using Aguacongas.FreeTheIdServer.Areas.Identity.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net.Http;

[assembly: HostingStartup(typeof(Aguacongas.FreeTheIdServer.Areas.Identity.IdentityHostingStartup))]
namespace Aguacongas.FreeTheIdServer.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {                
                services.Configure<EmailOptions>(context.Configuration.GetSection("EmailApiAuthentication"))
                    .AddSingleton<OAuthTokenManager<EmailOptions>>()
                    .AddTransient<IEmailSender>(p =>
                    {
                        var factory = p.GetRequiredService<IHttpClientFactory>();
                        var options = p.GetRequiredService<IOptions<EmailOptions>>();
                        return new EmailApiSender(factory.CreateClient(options.Value.HttpClientName), options);
                    })
                    .AddTransient<OAuthDelegatingHandler<EmailOptions>>()
                    .AddHttpClient(context.Configuration.GetValue<string>("EmailApiAuthentication:HttpClientName"))
                    .ConfigurePrimaryHttpMessageHandler(p => p.GetRequiredService<HttpClientHandler>())
                    .AddHttpMessageHandler<OAuthDelegatingHandler<EmailOptions>>();
            });
        }
    }
}
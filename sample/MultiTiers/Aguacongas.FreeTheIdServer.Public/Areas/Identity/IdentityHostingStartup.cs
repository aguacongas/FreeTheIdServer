// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Aguacongas.FreeTheIdServer.Areas.Identity.IdentityHostingStartup))]
namespace Aguacongas.FreeTheIdServer.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.Configure<AuthMessageSenderOptions>(context.Configuration);
                services.AddTransient<IEmailSender, EmailSender>();
            });
        }
    }
}
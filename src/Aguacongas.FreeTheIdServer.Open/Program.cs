// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer;
using Aguacongas.FreeTheIdServer.Options.OpenTelemetry;
using Open.ConformanceReport.Endpoints;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;
using MutualTlsOptions = Aguacongas.FreeTheIdServer.BlazorApp.Models.MutualTlsOptions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, configuration) =>
                        configuration.ReadFrom.Configuration(hostingContext.Configuration));

var configuration = builder.Configuration;

var services = builder.Services;

services.AddFreeTheIdServer(configuration);

var seed = args.Any(x => x == "/seed");
if (seed)
{
    args = args.Except(Config.SeedPage).ToArray();
}

services.AddOpenTelemetry(configuration.GetSection(nameof(OpenTelemetryOptions)));

var mutualTlsOptions = configuration.GetSection("IdentityServerOptions:MutualTls").Get<MutualTlsOptions>();
if (mutualTlsOptions?.Enabled == true && !seed)
{
    // when mutual TLS is enable the web host must receive client certificate
    builder.WebHost.ConfigureKestrel(kestrel =>
    {
        kestrel.ConfigureHttpsDefaults(https => https.ClientCertificateMode = ClientCertificateMode.DelayCertificate);
    });
}

var app = builder.Build();

if (seed)
{
    var config = app.Services.GetRequiredService<IConfiguration>();
    SeedData.EnsureSeedData(config, app.Services);
    return;
}

var activitySource = new ActivitySource("FreeTheIdServer");

app.Use(async (context, next) =>
{
    using var activity = activitySource.StartActivity("Request");
    await next().ConfigureAwait(false);
});
app.UseFreeTheIdServer(app.Environment, configuration);
app.MapConformanceReport();

await app.RunAsync().ConfigureAwait(false);

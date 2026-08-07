// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, configuration) =>
                        configuration.ReadFrom.Configuration(hostingContext.Configuration));

builder.AddFreeTheIdServerPrivate();

var seed = args.Any(x => x == "/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var app = builder.Build();

if (seed)
{
    SeedData.EnsureSeedData(app.Services);
    return;
}

app.UseFreeTheIdServerPrivate(app.Environment);

app.Run();

// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.AspNetCore.Builder;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, configuration) =>
                        configuration.ReadFrom.Configuration(hostingContext.Configuration));

builder.AddFreeTheIdServerApi();

var app = builder.Build();

app.UseFreeTheIdServerApi(app.Environment);

app.Run();
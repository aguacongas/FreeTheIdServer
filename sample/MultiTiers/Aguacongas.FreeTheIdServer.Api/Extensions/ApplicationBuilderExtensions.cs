// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.IdentityServer.Admin.Configuration;
using Aguacongas.FreeTheIdServer.Admin.Hubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseFreeTheIdServerApi(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                    .UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error")
                    .UseHsts();
            }

            ApiBasePath.Value = "/api";
            app.UseSerilogRequestLogging()
                .UseHttpsRedirection()
                .UseResponseCompression()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapAdminApiControllers();
                    endpoints.MapHub<ProviderHub>("/providerhub");
                });
        
            return app;
        }
    }
}

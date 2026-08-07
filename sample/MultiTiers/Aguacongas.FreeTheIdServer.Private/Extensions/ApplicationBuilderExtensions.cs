// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseFreeTheIdServerPrivate(this IApplicationBuilder app, IWebHostEnvironment environment)
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

            app.UseSerilogRequestLogging()
                .UseHttpsRedirection()
                .UseStaticFiles()
                .UseIdentityServer()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapRazorPages();
                })
                .LoadDynamicAuthenticationConfiguration<SchemeDefinition>();

            return app;
        }
    }
}

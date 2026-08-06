using Aguacongas.FreeTheIdServer.Models;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddFreeTheIdServer(this IMvcBuilder services)
        {
            return services.AddApplicationPart(typeof(SiteOptions).Assembly);
        }
    }
}

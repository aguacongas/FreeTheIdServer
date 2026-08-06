using Aguacongas.FreeTheIdServer.IntegrationTest.BlazorApp;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Aguacongas.FreeTheIdServer.Open.IntegrationTest
{
    [Collection(BlazorAppCollection.Name)]
    public class HealthCheckTest
    {
        private FreeTheIdServerFactory _factory;
        public HealthCheckTest(FreeTheIdServerFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Healthz_should_return_health_status()
        {
            using var client = _factory.CreateClient();
            using var response = await client.GetAsync("/healthz");

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Contains("Healthy", content);
        }
    }
}

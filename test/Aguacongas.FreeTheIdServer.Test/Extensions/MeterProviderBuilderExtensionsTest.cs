using Aguacongas.FreeTheIdServer.Options.OpenTelemetry;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aguacongas.FreeTheIdServer.Test.Extensions
{
    public class MeterProviderBuilderExtensionsTest
    {
        [Fact]
        public void AddFreeTheIdServerMetrics_should_add_exporters()
        {
            using var provider = Sdk.CreateMeterProviderBuilder()
                .AddFreeTheIdServerMetrics(new OpenTelemetryOptions
                {
                    Metrics = new MetricsOptions
                    {
                        Console = new ConsoleOptions(),
                        OpenTelemetryProtocol = new OtlpExporterOptions(),
                        Prometheus = new PrometheusOptions()
                    }
                }).Build();

            Assert.NotNull(provider);
        }
    }
}

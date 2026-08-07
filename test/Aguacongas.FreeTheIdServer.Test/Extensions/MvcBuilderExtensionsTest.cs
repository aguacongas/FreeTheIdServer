// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.Options.OpenTelemetry;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using System;
using Xunit;

namespace Aguacongas.FreeTheIdServer.Test.Extensions
{
    public class MvcBuilderExtensionsTest
    {
        [Fact]
        public void AddFreeTheIdServerMetrics_should_add_otlp_exporter()
        {
            using var provider = Sdk.CreateMeterProviderBuilder()
                .AddFreeTheIdServerMetrics(new OpenTelemetryOptions
                {
                    Metrics = new MetricsOptions
                    {
                        OpenTelemetryProtocol = new OtlpExporterOptions
                        {
                            Endpoint = new Uri("https://exemple.com")
                        },
                        Prometheus = new PrometheusOptions
                        {
                            HttpListenerPrefixes = new string[] { "http://localhost:9090" }
                        }
                    }
                })
                .Build();

            Assert.NotNull(provider);

        }
    }
}

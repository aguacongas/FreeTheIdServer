using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;

namespace Aguacongas.FreeTheIdServer.Options.OpenTelemetry
{
    public class ConsoleOptions : MetricReaderOptions
    {
        public ConsoleExporterOutputTargets Targets { get; set; } = ConsoleExporterOutputTargets.Console;
    }
}
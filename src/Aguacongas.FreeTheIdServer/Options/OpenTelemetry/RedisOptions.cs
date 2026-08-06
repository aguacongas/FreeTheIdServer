using OpenTelemetry.Instrumentation.StackExchangeRedis;

namespace Aguacongas.FreeTheIdServer.Options.OpenTelemetry
{
    public class RedisOptions : StackExchangeRedisInstrumentationOptions
    {
        public string ConnectionString { get; set; }
    }
}

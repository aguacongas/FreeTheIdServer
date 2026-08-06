using OpenTelemetry.Exporter;
using System.Collections;
using System.Collections.Generic;

namespace Aguacongas.FreeTheIdServer.Options.OpenTelemetry
{
    public class PrometheusOptions : PrometheusAspNetCoreOptions 
    {
        public bool Protected { get; set; }

        public IEnumerable<string> HttpListenerPrefixes { get; set; }

        public bool StartHttpListener { get; set; }
    }
}
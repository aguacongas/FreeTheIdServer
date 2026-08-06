# Configure OpenTelemetry

FreeTheIdServer can export [OpenTelemetry](https://opentelemetry.io/) data.  

## Traces

### Service

The service configuration is used to setup the source name and resource.

```json
{
  "OpenTelemetryOptions": {
    "Trace": {
      "Service": {
          "Name": "FreeTheIdServer.Open",
          "Version": "4.6.0"
      }
    }
  }
}
```

The `Service` node is deserialized in a [`ServiceOptions`](../src/Aguacongas.FreeTheIdServer/Options/OpenTelemetry/ServiceOptions.cs) instance used in : 

```c#
builder.AddSource(serviceOptions.Name)
  .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceOptions.Name,
      serviceOptions.Namespace,
      serviceOptions.Version,
      serviceOptions.AutoGenerateServiceInstanceId,
      serviceOptions.InstanceId));
```

### Sources

The sources array can be used to add sources such as `Open.IdentityServer.*` sources.

```json
{
  "OpenTelemetryOptions": {
    "Trace": {
      "Sources": [
        "Open.IdentityServer.Stores",
        "Open.IdentityServer.Cache",
        "Open.IdentityServer.Services",
        "Open.IdentityServer.Validation"
      ]
    }
  }
}
```

### Instrumentations

FreeTheIdServer enables instrumentation for:

- HttpClient
- Incoming requests
- SqlClient
- Redis

Each part can be configurd using the `Instrumentation` node.

#### HttpClient

```json
{
  "OpenTelemetryOptions": {
    "Trace": {
      "Instrumentation": {
        "HttpClient": {
          "RecordException": true,
          "SetHttpFlavor": true        
        }
      }
    }
  }
}
```

`HttpClient` is deserialized into a [`HttpClientInstrumentationOptions`](https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Instrumentation.Http/HttpClientInstrumentationOptions.cs) instance.

#### Incoming requests

```json
{
  "OpenTelemetryOptions": {
    "Trace": {
      "Instrumentation": {
        "AspNetCore": {
          "RecordException": true,
          "EnableGrpcAspNetCoreSupport": true
        }
      }
    }
  }
}
```

`AspNetCore` is deserialized into a [`AspNetCoreInstrumentationOptions`](https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Instrumentation.AspNetCore/AspNetCoreInstrumentationOptions.cs) instance.

#### SqlClient

```json
{
  "OpenTelemetryOptions": {
    "Trace": {
      "Instrumentation": {
        "SqlClient": {
          "RecordException": true,
          "EnableConnectionLevelAttributes": true,
          "SetDbStatementForText": true,
          "SetDbStatementForStoredProcedure": true
        }
      }
    }
  }
}
```

`SqlClient` is deserialized into a [`SqlClientInstrumentationOptions`](https://github.com/open-telemetry/opentelemetry-dotnet/blob/2a97920ff0a603837a1121204824955bba739c57/src/OpenTelemetry.Instrumentation.SqlClient/SqlClientInstrumentationOptions.cs) instance.


#### Redis

To enable the Redis instumentation you need to define the Redis connection string:

```json
{
  "OpenTelemetryOptions": {
    "Trace": {
      "Instrumentation": {
        "Redis": {
          "ConnectionString": "localhost",
          "FlushInterval": "0:0:10",
          "SetVerboseDatabaseStatements": true,
        }
      }
    }
  }
}
```

`Redis` is deserialized into a [`RedisOptions`](../src/Aguacongas.FreeTheIdServer/Options/OpenTelemetry/RedisOptions.cs) instance.


### Exporters

#### Console

To enable the console exporter set `ConsoleEnabled`:

```json
{
  "OpenTelemetryOptions": {
    "Trace": {
      "ConsoleEnabled": true
    }
  }
}
```

#### OTLP endpoint or Collector

To enable the [OTLP endpoint or Collector](https://opentelemetry.io/docs/collector/getting-started/) setup the `OpenTelemetryProtocol` node:

```json
{
  "OpenTelemetryOptions": {
    "Trace": {
      "OpenTelemetryProtocol": {
        "Endpoint": "https://exemple.com", //required
        "ExportProcessorType": "Batch",
        "Protocol": "HttpProtobuf" ,
        "TimeoutMilliseconds": 10000,
        "BatchExportProcessorOptions": {
          "BatchExportProcessorOptions": 2048,
          "ScheduledDelayMilliseconds": 5000,
          "ExporterTimeoutMilliseconds": 30000,
          "MaxExportBatchSize": 512
        }
      }
    }
  }  
}
```

`OpenTelemetryProtocol` is deserialized into a [`OtlpExporterOptions`](https://github.com/open-telemetry/opentelemetry-dotnet/blob/4b3ee96ffc39bc24c3b8377455b2c099bd9da6b0/src/OpenTelemetry.Exporter.OpenTelemetryProtocol/OtlpExporterOptions.cs) instance.


#### Honeycomb

To enable [Honeycomb](https://www.honeycomb.io/) exporter setup the `Honeycomb` node:

```json
{
  "OpenTelemetryOptions": {
    "Trace": {
      "Honeycomb": {
        "ApiKey": "my-haneycomb-api-key", //required
        "Dataset": "FreeTheIdServer", //required,
        "TracesApiKey": "my-haneycomb-traces-api-key",
        "MetricsApiKey": "my-haneycomb-metrics-api-key",
        "TracesDataset": "FreeTheIdServer-traces",
        "MetricsDataset": "FreeTheIdServer-metrics",
        "Endpoint": "https://api.honeycomb.io:443",
        "TracesEndpoint": "https://api.honeycomb.io:443",
        "MetricsEndpoint": "https://api.honeycomb.io:443",
        "SampleRate": 1,
        "ServiceName": "FreeTheIdServer",
        "ServiceVersion": "4.6.0",
        "InstrumentHttpClient": true,
        "InstrumentSqlClient": true,
        "InstrumentGrpcClient": true,
        "InstrumentStackExchangeRedisClient": true,
        "MeterNames": [
          "FreeTheIdServer"
        ]
      }
    }
  }
}
```

`Honeycomb` is deserialized into a [`HoneycombOptions`](https://docs.honeycomb.io/getting-data-in/opentelemetry/dotnet-distro/) instance.

## Metrics

FreeTheIdServer expose incoming requests and HttpClient metrics.

### Exporters

#### Console

To enable the console exporter set `Console` node:

```json
{
  "OpenTelemetryOptions": {
    "Metrics": {
      "Console": {
        "Targets": "Console",
        "MetricReaderType": "Cumulative",
        "MetricReaderType": "Manual",
        "PeriodicExportingMetricReaderOptions": {
          "ExportIntervalMilliseconds": 60000,
          "ExportTimeoutMilliseconds": 60000
        }
      }
    }
  }
}
```

`Console` is deserialized into a [`ConsoleOptions`](../src/Aguacongas.FreeTheIdServer/Options/OpenTelemetry/ConsoleOptions.cs) instance.

#### OTLP endpoint or Collector

To enable the [OTLP endpoint or Collector](https://opentelemetry.io/docs/collector/getting-started/) setup the `OpenTelemetryProtocol` node:

```json
{
  "OpenTelemetryOptions": {
    "Metrics": {
      "OpenTelemetryProtocol": {
        "Endpoint": "https://exemple.com", //required
        "ExportProcessorType": "Batch",
        "Protocol": "HttpProtobuf" ,
        "TimeoutMilliseconds": 10000,
        "BatchExportProcessorOptions": {
          "BatchExportProcessorOptions": 2048,
          "ScheduledDelayMilliseconds": 5000,
          "ExporterTimeoutMilliseconds": 30000,
          "MaxExportBatchSize": 512
        }
      }
    }
  }  
}
```

`OpenTelemetryProtocol` is deserialized into a [`OtlpExporterOptions`](https://github.com/open-telemetry/opentelemetry-dotnet/blob/4b3ee96ffc39bc24c3b8377455b2c099bd9da6b0/src/OpenTelemetry.Exporter.OpenTelemetryProtocol/OtlpExporterOptions.cs) instance.


#### Prometheus

To enable the [Prometheus](https://prometheus.io/) exporter set `Prometheus` node:

```json
{
  "OpenTelemetryOptions": {
    "Metrics": {
      "Prometheus": {
        "Protected": false,
        "StartHttpListener": false,
        "HttpListenerPrefixes": [
          "http://localhost:9090"
        ],
        "ScrapeEndpointPath": "/metrics",
        "ScrapeResponseCacheDurationMilliseconds": 10000
      }
    }
  }
}
```

`Prometheus` is deserialized into a [`PrometheusOptions`](../src/Aguacongas.FreeTheIdServer/Options/OpenTelemetry/PrometheusOptions.cs) instance.

You can protect the metrics endpoint if you don't want it be accessible to anonimous user. 

```json
{
  "OpenTelemetryOptions": {
    "Metrics": {
      "Prometheus": {
        "Protected": true,
      }
    }
  }
}
```

When protected the metirc endpoint can be accessed if the user has the role **Is4-Reader**. Setup a client_credential client with a claim of type = role and value = Is4-Reader you'll use in the prometheus job's oauth2 configuration.

![PROMETHEUS.png](assets/PROMETHEUS.png)

Your prometheus.yaml can look like :

```yaml
scrape_configs:
- job_name: "FreeTheIdServer"
  scheme: "https"
  oauth2:
    client_id: "prometheus"
    client_secret: "your.prometheus-client-secret"
    token_url: "https://FreeTheIdServer.myorg.com/connect/token"
    scopes: 
    - "FreeTheIdServertokenapi"
  static_configs:
  - targets:
    - "FreeTheIdServer.myorg.com"
```

#### Honeycomb

To enable [Honeycomb](https://www.honeycomb.io/) exporter setup the `Honeycomb` node:

```json
{
  "OpenTelemetryOptions": {
    "Metrics": {
      "Honeycomb": {
        "ApiKey": "my-haneycomb-api-key", //required
        "Dataset": "FreeTheIdServer", //required,
        "TracesApiKey": "my-haneycomb-traces-api-key",
        "MetricsApiKey": "my-haneycomb-metrics-api-key",
        "TracesDataset": "FreeTheIdServer-traces",
        "MetricsDataset": "FreeTheIdServer-metrics",
        "Endpoint": "https://api.honeycomb.io:443",
        "TracesEndpoint": "https://api.honeycomb.io:443",
        "MetricsEndpoint": "https://api.honeycomb.io:443",
        "SampleRate": 1,
        "ServiceName": "FreeTheIdServer",
        "ServiceVersion": "4.6.0",
        "InstrumentHttpClient": true,
        "InstrumentSqlClient": true,
        "InstrumentGrpcClient": true,
        "InstrumentStackExchangeRedisClient": true,
        "MeterNames": [
          "FreeTheIdServer"
        ]
      }
    }
  }
}
```
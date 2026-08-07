// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;

namespace Aguacongas.Open.IdentityServer.KeysRotation
{
    internal class KeyRotationOptionsSetup : IConfigureOptions<KeyRotationOptions>
    {
        private readonly ILoggerFactory _loggerFactory;

        public KeyRotationOptionsSetup()
            : this(NullLoggerFactory.Instance)
        {
        }

        public KeyRotationOptionsSetup(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public void Configure(KeyRotationOptions options)
        {
            var factories = options.AuthenticatedEncryptorFactories;
            factories.Add(new RsaEncryptorFactory(_loggerFactory));
            factories.Add(new ECDsaEncryptorFactory(_loggerFactory));
        }
    }
}

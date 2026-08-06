using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Open.IdentityServer.Models;
using Open.IdentityServer.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aguacongas.Open.IdentityServer.KeysRotation
{
    internal class ValidattionKeysStore : IValidationKeysStore
    {
        public ICacheableKeyRingProvider _keyringProvider;

        public ValidattionKeysStore(ICacheableKeyRingProvider keyringProvider)
        {
            _keyringProvider = keyringProvider ?? throw new ArgumentNullException(nameof(keyringProvider));
        }

        public Task<IEnumerable<SecurityKeyInfo>> GetValidationKeysAsync()
        {
            var keyInfos = _keyringProvider.GetAllKeys().Where(k => !k.IsRevoked);

            return Task.FromResult(keyInfos.Select(i =>
            {
                if (i.Descriptor is RsaEncryptorDescriptor rsa)
                {
                    return CreateRsaSinginKey(rsa);
                }

                return CreateEcdSingingKey(i);
            }).ToArray() as IEnumerable<SecurityKeyInfo>);
        }

        private SecurityKeyInfo CreateEcdSingingKey(IKey i)
        {
            var ecd = i.Descriptor as ECDsaEncryptorDescriptor;
            var algorythm = ecd.Configuration.SigningAlgorithm?.ToString() ?? _keyringProvider.Algorithm;
            var key = ecd.ECDsaSecurityKey;
            return new SecurityKeyInfo
            {
                Key = key,
                SigningAlgorithm = algorythm
            };
        }

        private SecurityKeyInfo CreateRsaSinginKey(RsaEncryptorDescriptor rsa)
        {
            var algorythm = rsa.Configuration.SigningAlgorithm?.ToString() ?? _keyringProvider.Algorithm;
            var key = rsa.RsaSecurityKey;
            return new SecurityKeyInfo
            {
                SigningAlgorithm = algorythm,
                Key = key
            };
        }
    }
}

// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Open.IdentityServer.Models;
using static Open.IdentityServer.IdentityServerConstants;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Aguacongas.Open.IdentityServer.KeysRotation
{
    internal class RsaEncryptor: ISigningAlgortithmEncryptor
    {
        private readonly RsaSecurityKey _keyDerivationKey;

        public RsaEncryptor(RsaSecurityKey keyDerivationKey)
        {
            _keyDerivationKey = keyDerivationKey;        
        }

        public SigningCredentials GetSigningCredentials(string signingAlgorithm)
        => GetSigningCredentials(Enum.Parse<RsaSigningAlgorithm>(signingAlgorithm));

        public SigningCredentials GetSigningCredentials(RsaSigningAlgorithm rsaSigningAlgorithm)
        => new(_keyDerivationKey, rsaSigningAlgorithm.ToString());

        public SecurityKeyInfo GetSecurityKeyInfo(string signingAlgorithm)
        => GetSecurityKeyInfo(Enum.Parse<RsaSigningAlgorithm>(signingAlgorithm));

        public SecurityKeyInfo GetSecurityKeyInfo(RsaSigningAlgorithm rsaSigningAlgorithm)
        => new()
            {
                Key = _keyDerivationKey,
                SigningAlgorithm = rsaSigningAlgorithm.ToString()
            };

        public byte[] Decrypt(ArraySegment<byte> ciphertext, ArraySegment<byte> additionalAuthenticatedData)
        {
            throw new NotImplementedException();
        }

        public byte[] Encrypt(ArraySegment<byte> plaintext, ArraySegment<byte> additionalAuthenticatedData)
        {
            throw new NotImplementedException();
        }
    }
}
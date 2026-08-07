// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Open.IdentityServer.Models;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.IdentityModel.Tokens;

namespace Aguacongas.Open.IdentityServer.KeysRotation
{
    public interface ISigningAlgortithmEncryptor : IAuthenticatedEncryptor
    {
        SecurityKeyInfo GetSecurityKeyInfo(string signingAlgorithm);
        SigningCredentials GetSigningCredentials(string signingAlgorithm);
    }
}
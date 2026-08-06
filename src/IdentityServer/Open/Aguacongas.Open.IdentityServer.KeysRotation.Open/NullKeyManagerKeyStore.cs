using Open.IdentityServer.Models;
using Open.IdentityServer.Services.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aguacongas.IdentityServer.KeysRotation.Open;

internal class NullKeyManagerKeyStore : IAutomaticKeyManagerKeyStore
{
    public Task<IReadOnlyCollection<SigningCredentials>> GetAllSigningCredentialsAsync(CancellationToken ct)
    {
        return Task.FromResult<IReadOnlyCollection<SigningCredentials>>(null);
    }

    public Task<SigningCredentials> GetSigningCredentialsAsync(CancellationToken ct)
    {
        return Task.FromResult<SigningCredentials>(null);
    }

    public Task<IReadOnlyCollection<SecurityKeyInfo>> GetValidationKeysAsync(CancellationToken ct)
    {
        return Task.FromResult<IReadOnlyCollection<SecurityKeyInfo>>(null);
    }
}

// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Open.IdentityServer.Stores;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;

namespace Aguacongas.Open.IdentityServer.KeysRotation
{
    public interface IKeyRingStore: IKeyRing, IValidationKeysStore, ISigningCredentialStore
    {
        IKey DefaultKey { get;  }
    }

    public interface IKeyRingStore<TC, TE> : IKeyRingStore
        where TC : SigningAlgorithmConfiguration
        where TE : ISigningAlgortithmEncryptor
    {
        string Algorithm { get; }
    }
}

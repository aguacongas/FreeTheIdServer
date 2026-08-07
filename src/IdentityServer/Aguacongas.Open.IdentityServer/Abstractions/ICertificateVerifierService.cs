// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Aguacongas.Open.IdentityServer.Abstractions
{
    public interface ICertificateVerifierService
    {
        Task<IEnumerable<string>> VerifyAsync(Stream certificateContent);
    }
}
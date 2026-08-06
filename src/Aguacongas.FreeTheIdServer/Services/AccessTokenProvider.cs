// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Threading.Tasks;

namespace Aguacongas.FreeTheIdServer.Services
{
    public class AccessTokenProvider : IAccessTokenProvider
    {
        public ValueTask<AccessTokenResult> RequestAccessToken()
        {
            throw new NotImplementedException();
        }

        public ValueTask<AccessTokenResult> RequestAccessToken(AccessTokenRequestOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Aguacongas.Open.IdentityServer.Admin.Http.Store.Test
{
    public class KeyStoreTest
    {
        [Fact]
        public async Task GetAsync_should_not_be_implemented()
        {
            var sut = new KeyStore<Key>(Task.FromResult(new HttpClient()), new NullLogger<KeyStore<Key>>());

            await Assert.ThrowsAsync<NotImplementedException>(() => sut.GetAsync(null));
            await Assert.ThrowsAsync<NotImplementedException>(() => sut.GetAsync(null, null));
        }
    }
}

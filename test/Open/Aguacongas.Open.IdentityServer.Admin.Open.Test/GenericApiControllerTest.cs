// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using System;
using Xunit;

namespace Aguacongas.Open.IdentityServer.Admin.Test
{
    public class GenericApiControllerTest
    {
        [Fact]
        public void Constructor_should_throw_on_args_null()
        {
            Assert.Throws<ArgumentNullException>(() => new GenericApiController<Key>(null));
        }
    }
}

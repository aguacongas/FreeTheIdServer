// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using System;

namespace Aguacongas.Open.IdentityServer.MongoDb.Store.Test.AdminStores
{
    public class AdminStoreOfUserClaimTest : AdminStoreTestBase<UserClaim>
    {
        protected override object CreateParentEntiy(Type parentType)
        {
            return new User
            {
                UserName = Guid.NewGuid().ToString(),
                Email = $"{Guid.NewGuid()}@sample.com"
            };
        }
    }
}

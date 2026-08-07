// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace Aguacongas.Open.IdentityServer.RavenDb.Store.Test.Extensions
{
    public class GetRequestExtensionsTest
    {
        [Fact]
        public void ToWhereClause_should_parse_filter_expression()
        {
            var sut = new PageRequest
            {
                Filter = "Id eq 'test' and contains(Email, 'test')"
            };

            var actual = sut.ToWhereClause<IdentityUser, string>(i => i.Id);

            Assert.Equal("where Id = 'test' and search(Email,'*test*')", actual);
        }
    }
}

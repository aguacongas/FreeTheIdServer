// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.IdentityServer.EntityFramework.Store;
using Aguacongas.IdentityServer.Store.Entity;
using System.Threading.Tasks;
using Xunit;
using ApiScopesPage = Aguacongas.FreeTheIdServer.BlazorApp.Pages.ApiScopes.ApiScopes;

namespace Aguacongas.FreeTheIdServer.IntegrationTest.BlazorApp.Pages
{
    [Collection(BlazorAppCollection.Name)]
    public class ApiScopesTest : EntitiesPageTestBase<ApiScope, ApiScopesPage>
    {
        public override string Entities => "apiscopes";
        public ApiScopesTest(FreeTheIdServerFactory factory)
            : base(factory)
        {
        }

        protected override Task PopulateList()
        {
            return DbActionAsync<ConfigurationDbContext>(context =>
            {
                context.ApiScopes.Add(new ApiScope
                {
                    Id = GenerateId(),
                    DisplayName = "filtered"
                });

                return context.SaveChangesAsync();
            });
        }
    }
}
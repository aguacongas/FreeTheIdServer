// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.EntityFramework.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using System.Threading.Tasks;
using Xunit;
using IndentiesPage = Aguacongas.FreeTheIdServer.BlazorApp.Pages.Identities.Indenties;

namespace Aguacongas.FreeTheIdServer.IntegrationTest.BlazorApp.Pages
{
    [Collection(BlazorAppCollection.Name)]
    public class IdentitiesTest : EntitiesPageTestBase<IdentityResource, IndentiesPage>
    {
        public override string Entities => "identities";
        public IdentitiesTest(FreeTheIdServerFactory factory)
            : base(factory)
        {
        }

        protected override Task PopulateList()
        {
            return DbActionAsync<ConfigurationDbContext>(context =>
            {
                context.Identities.Add(new IdentityResource
                {
                    Id = GenerateId(),
                    DisplayName = "filtered"
                });

                return context.SaveChangesAsync();
            });
        }
    }
}

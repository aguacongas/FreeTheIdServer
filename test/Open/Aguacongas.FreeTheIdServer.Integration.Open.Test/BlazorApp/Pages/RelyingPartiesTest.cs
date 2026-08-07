// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.EntityFramework.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using System.Threading.Tasks;
using Xunit;
using RelyingPartiesPage = Aguacongas.FreeTheIdServer.BlazorApp.Pages.RelyingParties.RelyingParties;

namespace Aguacongas.FreeTheIdServer.IntegrationTest.BlazorApp.Pages
{
    [Collection(BlazorAppCollection.Name)]
    public class RelyingPartiesTest : EntitiesPageTestBase<RelyingParty, RelyingPartiesPage>
    {
        public override string Entities => "relyingparties";
        public RelyingPartiesTest(FreeTheIdServerFactory factory)
            : base(factory)
        {
        }

        protected override Task PopulateList()
        {
            return DbActionAsync<ConfigurationDbContext>(context =>
            {
                context.RelyingParties.Add(new RelyingParty
                {
                    Id = GenerateId(),
                    Description = "filtered",
                    TokenType = GenerateId(),
                    DigestAlgorithm = GenerateId(),
                    SignatureAlgorithm = GenerateId()
                });

                return context.SaveChangesAsync();
            });
        }
    }
}

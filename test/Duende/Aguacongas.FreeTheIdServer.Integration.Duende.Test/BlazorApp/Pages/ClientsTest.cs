// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.IdentityServer.EntityFramework.Store;
using Aguacongas.IdentityServer.Store.Entity;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using ClientsPage = Aguacongas.FreeTheIdServer.BlazorApp.Pages.Clients.Clients;

namespace Aguacongas.FreeTheIdServer.IntegrationTest.BlazorApp.Pages
{
    [Collection(BlazorAppCollection.Name)]
    public class CientsTest : EntitiesPageTestBase<Client, ClientsPage>
    {
        public override string Entities => "clients";
        public CientsTest(FreeTheIdServerFactory factory)
            : base(factory)
        {
        }

        protected override Task PopulateList()
        {
            return DbActionAsync<ConfigurationDbContext>(context =>
            {
                context.Clients.Add(new Client
                {
                    Id = GenerateId(),
                    ProtocolType = "oidc",
                    ClientName = "filtered"
                });

                return context.SaveChangesAsync();
            });
        }
    }
}

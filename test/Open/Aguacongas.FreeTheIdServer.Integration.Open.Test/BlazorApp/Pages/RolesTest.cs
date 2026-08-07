// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using Aguacongas.FreeTheIdServer.Data;
using System.Threading.Tasks;
using Xunit;
using RolesPage = Aguacongas.FreeTheIdServer.BlazorApp.Pages.Roles.Roles;

namespace Aguacongas.FreeTheIdServer.IntegrationTest.BlazorApp.Pages
{
    [Collection(BlazorAppCollection.Name)]
    public class RolesTest : EntitiesPageTestBase<Role, RolesPage>
    {
        public override string Entities => "roles";

        public RolesTest(FreeTheIdServerFactory factory)
            : base(factory)
        {
        }

        protected override Task PopulateList()
        {
            return DbActionAsync<ApplicationDbContext>(context =>
            {
                context.Roles.Add(new Role
                {
                    Id = GenerateId(),
                    Name = "filtered",
                });

                return context.SaveChangesAsync();
            });
        }
    }
}

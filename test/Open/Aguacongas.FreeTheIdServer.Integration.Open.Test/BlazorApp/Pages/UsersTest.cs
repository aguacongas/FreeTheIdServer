// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using Aguacongas.FreeTheIdServer.Data;
using System.Threading.Tasks;
using Xunit;
using UsersPage = Aguacongas.FreeTheIdServer.BlazorApp.Pages.Users.Users;

namespace Aguacongas.FreeTheIdServer.IntegrationTest.BlazorApp.Pages
{
    [Collection(BlazorAppCollection.Name)]
    public class UsersTest : EntitiesPageTestBase<User, UsersPage>
    {
        public override string Entities => "users";

        public UsersTest(FreeTheIdServerFactory factory)
            : base(factory)
        {
        }

        protected override Task PopulateList()
        {
            return DbActionAsync<ApplicationDbContext>(context =>
            {
                context.Users.Add(new User
                {
                    Id = GenerateId(),
                    UserName = "filtered",
                });

                return context.SaveChangesAsync();
            });
        }
    }
}

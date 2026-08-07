// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.EntityFramework.Store;
using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Xunit;
using CulturePage = Aguacongas.FreeTheIdServer.BlazorApp.Pages.Culture.Culture;

namespace Aguacongas.FreeTheIdServer.IntegrationTest.BlazorApp.Pages
{
    [Collection(BlazorAppCollection.Name)]
    public class CultureTest : EntityPageTestBase<CulturePage, Culture>
    {
        public override string Entity => "culture";
        public CultureTest(FreeTheIdServerFactory factory) : base(factory)
        {
        }


        [Fact]
        public async Task OnFilterChanged_should_filter_resources()
        {
            string cultureId = await CreateCulture();

            var component = CreateComponent("Alice Smith",
                SharedConstants.WRITERPOLICY,
                cultureId);

            var filterInput = component.Find("input[placeholder=\"filter\"]");

            Assert.NotNull(filterInput);

            await filterInput.TriggerEventAsync("oninput", new ChangeEventArgs
            {
                Value = cultureId
            });

            Assert.DoesNotContain("filtered", component.Markup);
        }

        [Fact]
        public async Task WhenWriter_should_be_able_to_clone_entity()
        {
            string cultureId = await CreateCulture();

            var component = CreateComponent("Alice Smith",
                SharedConstants.WRITERPOLICY,
                cultureId,
                true);

            var input = WaitForNode(component, "input[placeholder=culture]");

            Assert.NotNull(input);
        }

        private async Task<string> CreateCulture()
        {
            var cultureId = CultureInfo.CurrentCulture.Name;
            await DbActionAsync<ConfigurationDbContext>(async context =>
            {
                if (await context.Cultures.AnyAsync(c => c.Id == cultureId))
                {
                    return;
                }

                await context.Cultures.AddAsync(new Culture
                {
                    Id = cultureId,
                    Resources = new[]
                    {
                        new LocalizedResource
                        {
                            Id = Guid.NewGuid().ToString(),
                            Key = "filtered",
                            Value = "filtered"
                        }
                    },
                });

                await context.SaveChangesAsync();
            });
            return cultureId;
        }
    }
}

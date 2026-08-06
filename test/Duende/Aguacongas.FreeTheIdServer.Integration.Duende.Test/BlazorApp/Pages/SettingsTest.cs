using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.FreeTheIdServer.BlazorApp.Services;
using Aguacongas.FreeTheIdServer.IntegrationTest.BlazorApp;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using SettingsPage = Aguacongas.FreeTheIdServer.BlazorApp.Pages.Settings.Settings;

namespace Aguacongas.FreeTheIdServer.Open.IntegrationTest.BlazorApp.Pages
{
    [Collection(BlazorAppCollection.Name)]
    public class SettingsTest : BunitContext
    {
        public FreeTheIdServerFactory Factory { get; }

        public SettingsTest(FreeTheIdServerFactory factory)
        {
            Factory = factory;
        }

        [Fact]
        public async Task SaveButonClick_should_notify()
        {
            var component = CreateComponent("Alice Smith",
                SharedConstants.WRITERPOLICY);

            var form = await component.WaitForElementAsync("form");

            var notifier = Services.GetRequiredService<Notifier>();
            notifier.Show = n =>
            {
                Assert.NotNull(n);
                return Task.CompletedTask;
            };

            await form.SubmitAsync();
        }

        private IRenderedComponent<SettingsPage> CreateComponent(string userName,
            string role)
        {
            Factory.ConfigureTestContext(userName,
               new Claim[]
               {
                    new Claim("scope", SharedConstants.ADMINSCOPE),
                    new Claim("role", SharedConstants.READERPOLICY),
                    new Claim("role", role),
                    new Claim("sub", Guid.NewGuid().ToString())
               },
               this);

            var component = Render<SettingsPage>();
            component.WaitForState(() => !component.Markup.Contains("Loading..."), TimeSpan.FromMinutes(1));
            return component;
        }
    }
}

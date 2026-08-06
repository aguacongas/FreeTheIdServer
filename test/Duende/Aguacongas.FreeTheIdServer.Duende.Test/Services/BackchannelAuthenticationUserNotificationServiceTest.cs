using Aguacongas.Open.IdentityServer.Services;
using Open.IdentityServer.Models;
using Open.IdentityServer.Services;
using IdentityModel;
using Microsoft.Extensions.Localization;
using Moq;
using RichardSzalay.MockHttp;
using System;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace Aguacongas.FreeTheIdServer.Open.Test.Services;

public class BackchannelAuthenticationUserNotificationServiceTest
{
    [Fact]
    public void Constructor_shoult_validate_arguments()
    {
        Assert.Throws<ArgumentNullException>(() => new BackchannelAuthenticationUserNotificationService(null, null, null, null));
        Assert.Throws<ArgumentNullException>(() => new BackchannelAuthenticationUserNotificationService(new Mock<IIssuerNameService>().Object, null, null, null));
        Assert.Throws<ArgumentNullException>(() => new BackchannelAuthenticationUserNotificationService(new Mock<IIssuerNameService>().Object,
            new Mock<IStringLocalizer<BackchannelAuthenticationUserNotificationService>>().Object, null, null));
        Assert.Throws<ArgumentNullException>(() => new BackchannelAuthenticationUserNotificationService(new Mock<IIssuerNameService>().Object,
            new Mock<IStringLocalizer<BackchannelAuthenticationUserNotificationService>>().Object, new HttpClient(), null));
    }

    [Theory]
    [InlineData(null, null, "https://FreeTheIdServer")]
    [InlineData("https://FreeTheIdServer", "secret", "https://FreeTheIdServer/")]
    public async Task SendLoginRequestAsync_should_call_email_service(string logouri, string bindinMessage, string issuer)
    {
        var mockHttpHandler = new MockHttpMessageHandler();
        mockHttpHandler.When(HttpMethod.Post, "https://FreeTheIdServer/email").Respond(r =>
        {
            Assert.Equal("https://FreeTheIdServer/email", r.RequestUri.ToString());
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
        });

        var client = mockHttpHandler.ToHttpClient();
        var issuerNameServiceMock = new Mock<IIssuerNameService>();
        issuerNameServiceMock.Setup(m => m.GetCurrentAsync(default)).ReturnsAsync(issuer);

        var stringLocalizerMock = new Mock<IStringLocalizer<BackchannelAuthenticationUserNotificationService>>();
        var options = Microsoft.Extensions.Options.Options.Create(new BackchannelAuthenticationUserNotificationServiceOptions
        {
            ApiUrl = "https://FreeTheIdServer/email"
        });
        var sut = new BackchannelAuthenticationUserNotificationService(issuerNameServiceMock.Object, stringLocalizerMock.Object, client, options);

        await sut.SendLoginRequestAsync(new BackchannelUserLoginRequest
        {
            Client = new Client
            {
                LogoUri = logouri,
                ClientName = Guid.NewGuid().ToString(),
            },
            Subject = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(JwtClaimTypes.Email, "aguacongas@gmail.com") })),
            InternalId = Guid.NewGuid().ToString(),
            BindingMessage = bindinMessage
        }, default);
    }
}

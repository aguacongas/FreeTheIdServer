using Aguacongas.Open.IdentityServer.Saml2p.Open.Services;
using Aguacongas.Open.IdentityServer.Saml2p.Open.Services.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ServiceModel.Security;

namespace Aguacongas.Open.IdentityServer.Saml2P.Open.Test.Extensions;
public class ServiceCollectionExtensionsTest
{
    [Fact]
    public void AddIdentityServerSaml2P_should_add_saml2p_services_in_di()
    {
        var builder = new ServiceCollection()
            .AddMvc()
            .AddIdentityServerSaml2P(new Saml2POptions
            {
                CertificateValidationMode = X509CertificateValidationMode.None
            });

        Assert.Contains(builder.Services, s => s.ServiceType == typeof(ISaml2PService));
    }
}

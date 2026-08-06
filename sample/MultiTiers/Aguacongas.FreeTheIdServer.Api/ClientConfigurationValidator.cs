using Open.IdentityServer.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace Aguacongas.FreeTheIdServer.Api;

public class EmptyClientConfigurationValidator : IClientConfigurationValidator
{
    public Task ValidateAsync(ClientConfigurationValidationContext context)
    => Task.CompletedTask;
}

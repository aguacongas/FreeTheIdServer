using Open.IdentityServer.Events;
using Open.IdentityServer.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Aguacongas.FreeTheIdServer.Api;

public class EmptyEventService : IEventService
{
    public bool CanRaiseEventType(EventTypes evtType)
    => true;

    public Task RaiseAsync(Event evt)
    => Task.CompletedTask;
}

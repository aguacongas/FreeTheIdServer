// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Open.IdentityServer.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aguacongas.IdentityServer.Abstractions;

public interface IProvideClaims
{
    Task<IEnumerable<Claim>> ProvideClaims(ClaimsPrincipal subject, IConnectedApplication application, string caller, Resource resource);
}

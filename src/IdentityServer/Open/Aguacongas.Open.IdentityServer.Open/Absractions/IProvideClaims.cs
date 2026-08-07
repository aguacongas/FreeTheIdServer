// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Open.IdentityServer.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Aguacongas.Open.IdentityServer.Abstractions;

public interface IProvideClaims
{
    Task<IEnumerable<Claim>> ProvideClaims(ClaimsPrincipal subject, Client application, string caller, Resource resource);
}

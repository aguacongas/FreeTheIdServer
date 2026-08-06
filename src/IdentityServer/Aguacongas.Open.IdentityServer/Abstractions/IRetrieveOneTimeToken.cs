// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
namespace Aguacongas.Open.IdentityServer.Abstractions
{
    public interface IRetrieveOneTimeToken
    {
        string ConsumeOneTimeToken(string id);

        string GetOneTimeToken(string id);
    }
}

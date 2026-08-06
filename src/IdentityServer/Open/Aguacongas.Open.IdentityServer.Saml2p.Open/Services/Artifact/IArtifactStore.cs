using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.Open.IdentityServer.Saml2p.Open.Services.Artifact;

/// <summary>
/// Artifact store interfact
/// </summary>
public interface IArtifactStore
{
    /// <summary>
    /// Removed a stored artifact from store
    /// </summary>
    /// <param name="artifact"></param>
    /// <returns></returns>
    Task<Entity.Saml2PArtifact> RemoveAsync(string artifact);

    /// <summary>
    /// Stores an artifact
    /// </summary>
    /// <param name="artifact"></param>
    /// <returns></returns>
    Task StoreAsync(Entity.Saml2PArtifact artifact);
}

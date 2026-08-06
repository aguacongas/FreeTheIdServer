using Microsoft.AspNetCore.Mvc;

namespace Aguacongas.Open.IdentityServer.Saml2p.Open.Services.Metadata;

/// <summary>
/// Metadata response generator interface
/// </summary>
public interface IMetadataResponseGenerator
{
    /// <summary>
    /// Generates the metadata response
    /// </summary>
    /// <returns></returns>
    Task<IActionResult> GenerateMetadataResponseAsync();
}
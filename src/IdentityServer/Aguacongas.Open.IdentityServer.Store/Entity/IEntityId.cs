// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
namespace Aguacongas.Open.IdentityServer.Store.Entity
{
    /// <summary>
    /// Entity id interface
    /// </summary>
    public interface IEntityId
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; set; }
    }
}

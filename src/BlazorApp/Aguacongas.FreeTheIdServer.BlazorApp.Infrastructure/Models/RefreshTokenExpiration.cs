// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
namespace Aguacongas.FreeTheIdServer.BlazorApp.Models
{
    /// <summary>
    /// Refresh token expiration
    /// </summary>
    public enum RefreshTokenExpiration
    {
        /// <summary>
        /// Sliding expiration
        /// </summary>
        Sliding = 0,
        /// <summary>
        /// Absolute expiration
        /// </summary>
        Absolute = 1
    }
}

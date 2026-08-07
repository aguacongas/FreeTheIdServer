// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using System.Net.Http;

namespace Aguacongas.Open.IdentityServer.Admin.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class HubHttpMessageHandlerAccessor
    {
        /// <summary>
        /// Gets or sets the handler.
        /// </summary>
        /// <value>
        /// The handler.
        /// </value>
        public HttpMessageHandler Handler { get; set; }
    }
}

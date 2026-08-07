// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.BlazorApp.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Entity = Aguacongas.Open.IdentityServer.Store.Entity;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Pages.Client.Components
{
    public partial class ClientSecrets
    {
        private IEnumerable<Entity.ClientSecret> Secrets => Collection.Where(s => s.Id == null || (s.Description != null && s.Description.Contains(HandleModificationState.FilterTerm)) || (s.Type != null && s.Type.Contains(HandleModificationState.FilterTerm)));

        private IEnumerable<string> SecretKinds => SecretTypes.Values.Where(t => ProtocolType == Client.OIDC ||
            (t != SecretTypes.SharedSecret && t != SecretTypes.JWK));
        private static void GenerateSecret(Entity.ClientSecret secret)
        {
            secret.Value = Guid.NewGuid().ToString();
        }

        [Parameter]
        public string ProtocolType { get; set; }
    }
}

// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Validators
{
    public class ClientScopeValidator : AbstractValidator<ClientScope>
    {
        public ClientScopeValidator(Client client, IStringLocalizer localizer)
        {
            RuleFor(m => m.Scope).IsUnique(client.AllowedScopes).WithMessage(localizer["Scopes must be unique."]);
        }
    }
}

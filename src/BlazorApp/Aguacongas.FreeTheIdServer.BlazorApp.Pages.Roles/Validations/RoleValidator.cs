// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.BlazorApp.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Validators
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator(Role role, IStringLocalizer localizer)
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage(localizer["The name is required."]);
            RuleForEach(m => m.Claims)
                .SetValidator(new RoleClaimValidator(role, localizer));
        }
    }
}

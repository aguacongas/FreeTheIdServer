// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Validators
{
    public class RoleClaimValidator : AbstractValidator<RoleClaim>
    {
        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Our validator constructor requires a parameter.")]
        public RoleClaimValidator(Models.Role role, IStringLocalizer localizer)
        {
            RuleFor(m => m.ClaimType).NotEmpty().WithMessage(localizer["The claim type is required."]);
            RuleFor(m => m.ClaimType).MaximumLength(250).WithMessage(localizer["The claim type cannot exceed 250 chars."]);
            RuleFor(m => m.ClaimValue).NotEmpty().WithMessage(localizer["The claim value is required."]);
        }
    }
}
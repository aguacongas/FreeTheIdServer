// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.BlazorApp.Models;
using FluentValidation;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Models = Aguacongas.FreeTheIdServer.BlazorApp.Models;
using Microsoft.Extensions.Localization;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Validators
{
    public class ExternalClaimTransformationValidator : AbstractValidator<ExternalClaimTransformation>
    {
        public ExternalClaimTransformationValidator(Models.ExternalProvider externalProvider, IStringLocalizer localizer)
        {
            RuleFor(m => m.FromClaimType).NotEmpty().WithMessage(localizer["The from claim tyoe is required."]);
            RuleFor(m => m.ToClaimType).NotEmpty().WithMessage(localizer["The to claim tyoe is required."]);
            RuleFor(m => m.FromClaimType).IsUnique(externalProvider.ClaimTransformations).WithMessage(localizer["The from claim type must be unique."]);
        }
    }
}
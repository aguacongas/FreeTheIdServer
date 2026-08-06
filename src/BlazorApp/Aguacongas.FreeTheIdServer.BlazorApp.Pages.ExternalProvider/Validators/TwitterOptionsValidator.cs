// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.BlazorApp.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Validators
{
    public class TwitterOptionsValidator : AbstractValidator<TwitterOptions>
    {
        public TwitterOptionsValidator(ExternalProvider _, IStringLocalizer localizer)
        {
            RuleFor(m => m.ConsumerKey).NotEmpty().WithMessage(localizer["Consumer Key is required."]);
            RuleFor(m => m.ConsumerSecret).NotEmpty().WithMessage(localizer["Consumer Secret is required."]);
        }
    }
}
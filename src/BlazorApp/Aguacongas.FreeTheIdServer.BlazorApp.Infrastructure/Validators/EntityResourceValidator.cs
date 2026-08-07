// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Validators
{
    public class EntityResourceValidator<T>: AbstractValidator<T>  where T: class, IEntityResource
    {
        public EntityResourceValidator(ILocalizable<T> model, EntityResourceKind kind, IStringLocalizer localizer)
        {
            RuleFor(m => m.CultureId).NotEmpty().WithMessage(localizer["The culture is required."]);
            RuleFor(m => m.CultureId).IsUnique(model.Resources.Where(r => r.ResourceKind == kind)).WithMessage(localizer["The culture must be unique."]);
        }
    }
}
using Aguacongas.FreeTheIdServer.BlazorApp.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Validators
{
    public class WindowsOptionsValidator : AbstractValidator<WindowsOptions>
    {        
        public WindowsOptionsValidator(ExternalProvider _, IStringLocalizer localizer)
        {
            When(m => !string.IsNullOrEmpty(m.MachineAccountName), () =>
            {
                RuleFor(m => m.MachineAccountPassword).NotEmpty().WithMessage(localizer["The machine account password is required."]);
            });
        }
    }
}

// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Open.IdentityServer.Services;

namespace Aguacongas.FreeTheIdServer.UI;

[SecurityHeaders]
[AllowAnonymous]
public class HomeController(IIdentityServerInteractionService interaction, IWebHostEnvironment environment) : Controller
{

    /// <summary>
    /// Shows the error page
    /// </summary>
    public async Task<IActionResult> Error(string errorId)
    {
        var vm = new ErrorViewModel();

        // retrieve error details from identityserver
        var message = await interaction.GetErrorContextAsync(errorId);
        if (message != null)
        {
            vm.Error = message;

            if (!environment.IsDevelopment())
            {
                // only show in development
                message.ErrorDescription = null;
            }
        }

        return View("Error", vm);
    }
}
// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Aguacongas.FreeTheIdServer.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
    {
        public void OnGet()
        {
            // just return the html code
        }
    }
}

using Open.IdentityServer.Models;
using Open.IdentityServer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Aguacongas.FreeTheIdServer.Areas.Identity.Pages.Account.Manage;

public class CibaModel(IBackchannelAuthenticationInteractionService backchannelAuthenticationInteractionService) : PageModel
{
    public IEnumerable<BackchannelUserLoginRequest>? Logins { get; set; }

    [BindProperty, Required]
    public string? Id { get; set; }
    [BindProperty, Required]
    public string? Button { get; set; }

    public async Task OnGet()
    {
        Logins = await backchannelAuthenticationInteractionService.GetPendingLoginRequestsForCurrentUserAsync(HttpContext.RequestAborted);
    }
}

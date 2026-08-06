using Aguacongas.IdentityServer.Store.Entity;
using Aguacongas.FreeTheIdServer.BlazorApp.Pages;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Aguacongas.FreeTheIdServer.BlazorApp.Components
{
    public partial class CloneButton
    {
        [Parameter]
        public string CssClass { get; set; }

        private Task Clone()
        {
            Navigation.NavigateTo(Navigation.GetUriWithQueryParameter(nameof(EntityModel<Client>.Clone), true));
            return Task.CompletedTask;
        }
    }
}

using Aguacongas.Open.IdentityServer.Store;
using Aguacongas.Open.IdentityServer.Store.Entity;
using Aguacongas.FreeTheIdServer.Models;
using Microsoft.AspNetCore.Identity;

namespace Aguacongas.Open.IdentityServer.Admin
{
    /// <summary>
    /// User API controller
    /// </summary>
    public class UserController : GenericApiController<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="manager"></param>
        public UserController(IAdminStore<User> store, UserManager<ApplicationUser> manager) :
            base(new CheckIdentityRulesUserStore<IAdminStore<User>>(store, manager))
        {
        }
    }
}

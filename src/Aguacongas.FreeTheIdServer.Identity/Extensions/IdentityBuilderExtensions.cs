// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Microsoft.AspNetCore.Identity;

namespace Microsoft.Extensions.DependencyInjection
{

    /// <summary>
    /// Contains extension methods to <see cref="IdentityBuilder"/> for adding FreeTheIdServer stores.
    /// </summary>
    public static class IdentityBuilderExtensions
    {                
        /// <summary>
        /// Adds the identifier server stores.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IdentityBuilder AddFreeTheIdServerStores(this IdentityBuilder builder)
        {
            builder.Services.AddFreeTheIdServerStores(builder.UserType, builder.RoleType);
            return builder;
        }
    }
}
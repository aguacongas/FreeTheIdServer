// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtentsions
    {
        public static void AddFreeTheIdServerStores(this IServiceCollection services, Type userType, Type roleType)
        {
            var userOnlyStoreType = typeof(UserOnlyStore<>).MakeGenericType(userType);

            if (roleType != null)
            {
                var userStoreType = typeof(UserStore<,>).MakeGenericType(userType, roleType);
                var roleStoreType = typeof(RoleStore<>).MakeGenericType(roleType);

                services.TryAddScoped(typeof(UserOnlyStore<>)
                    .MakeGenericType(userType),
                        provider => provider.CreateUserOnlyStore(userOnlyStoreType));
                services.TryAddScoped(typeof(IUserStore<>).MakeGenericType(userType),
                    provider => provider.CreateUserStore(userOnlyStoreType, userStoreType));
                services.TryAddScoped(typeof(IRoleStore<>).MakeGenericType(roleType),
                        provider => provider.CreateRoleStore(roleStoreType));
            }
            else
            {   // No Roles
                services.TryAddScoped(typeof(IUserStore<>)
                    .MakeGenericType(userType), provider => provider.CreateUserOnlyStore(userOnlyStoreType));
            }
        }
    }
}

// Project: aguacongas/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.Open.IdentityServer.Store.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Aguacongas.Open.IdentityServer.Admin
{
    /// <summary>
    /// Utils class
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// Gets the entity type list.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Type> GetEntityTypeList()
        {
            var assembly = typeof(IEntityId).GetTypeInfo().Assembly;
            var entyTypeList = assembly.GetTypes().Where(t => t.IsClass &&
                !t.IsAbstract &&
                t.Name != nameof(Key) &&
                t.GetInterface("IEntityId") != null);
            return entyTypeList;
        }
    }
}

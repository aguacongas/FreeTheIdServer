// Project: Aguafrommars/FreeTheIdServer
// Copyright (c) 2026 @Olivier Lefebvre
using Aguacongas.FreeTheIdServer.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Microsoft.EntityFrameworkCore
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseDatabaseFromConfiguration(this DbContextOptionsBuilder options, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var dbType = configuration.GetValue<DbTypes>("DbType");
            // drop this line when issue https://github.com/dotnet/efcore/issues/35110 is fixed
            options.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

            switch (dbType)
            {
                case DbTypes.InMemory:
                    options.UseInMemoryDatabase(connectionString);
                    break;
                case DbTypes.SqlServer:
                    options.UseSqlServer(connectionString, options => options.MigrationsAssembly("Aguacongas.FreeTheIdServer.Migrations.SqlServer"));
                    break;
                case DbTypes.Sqlite:
                    options.UseSqlite(connectionString, options => options.MigrationsAssembly("Aguacongas.FreeTheIdServer.Migrations.Sqlite"));
                    break;
                case DbTypes.MySql:
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), options => options.MigrationsAssembly("Aguacongas.FreeTheIdServer.Migrations.MySql"));
                    break;
                case DbTypes.Oracle:
                    options.UseOracle(connectionString, options => options.MigrationsAssembly("Aguacongas.FreeTheIdServer.Migrations.Oracle"));
                    break;
                case DbTypes.PostgreSQL:
                    options.UseNpgsql(connectionString, options => options.MigrationsAssembly("Aguacongas.FreeTheIdServer.Migrations.PostgreSQL"));
                    break;
            }
            return options;
        }
    }
}

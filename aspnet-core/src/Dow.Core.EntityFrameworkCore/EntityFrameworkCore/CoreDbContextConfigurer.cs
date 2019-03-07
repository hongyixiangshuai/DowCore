using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Dow.Core.EntityFrameworkCore
{
    public static class CoreDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<CoreDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<CoreDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}

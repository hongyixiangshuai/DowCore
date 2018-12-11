using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Cassius.App.EntityFrameworkCore
{
    public static class AppDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AppDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AppDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}

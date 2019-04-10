using Microsoft.EntityFrameworkCore;

namespace Dow.Core.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<CoreDbContext> dbContextOptions, 
            string connectionString
            )
        {
            /* This is the single point to configure DbContextOptions for CoreDbContext */
            dbContextOptions.UseSqlServer(connectionString);
        }
    }
}

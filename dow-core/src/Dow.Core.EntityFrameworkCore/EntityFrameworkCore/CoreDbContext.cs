using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dow.Core.EntityFrameworkCore
{
    public class CoreDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public CoreDbContext(DbContextOptions<CoreDbContext> options) 
            : base(options)
        {

        }
    }
}

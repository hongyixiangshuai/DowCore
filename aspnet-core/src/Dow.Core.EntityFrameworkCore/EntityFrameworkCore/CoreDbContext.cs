using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Dow.Core.Authorization.Roles;
using Dow.Core.Authorization.Users;
using Dow.Core.MultiTenancy;

namespace Dow.Core.EntityFrameworkCore
{
    public class CoreDbContext : AbpZeroDbContext<Tenant, Role, User, CoreDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
        }
    }
}

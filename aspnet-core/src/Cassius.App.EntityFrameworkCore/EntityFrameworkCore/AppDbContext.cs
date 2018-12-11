using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Cassius.App.Authorization.Roles;
using Cassius.App.Authorization.Users;
using Cassius.App.MultiTenancy;

namespace Cassius.App.EntityFrameworkCore
{
    public class AppDbContext : AbpZeroDbContext<Tenant, Role, User, AppDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}

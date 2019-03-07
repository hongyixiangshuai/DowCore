using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Dow.Core.Configuration;
using Dow.Core.Web;

namespace Dow.Core.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class CoreDbContextFactory : IDesignTimeDbContextFactory<CoreDbContext>
    {
        public CoreDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CoreDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            CoreDbContextConfigurer.Configure(builder, configuration.GetConnectionString(CoreConsts.ConnectionStringName));

            return new CoreDbContext(builder.Options);
        }
    }
}

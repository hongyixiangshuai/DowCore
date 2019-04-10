using Abp.EntityFrameworkCore;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Dow.Core.EntityFrameworkCore
{
    [DependsOn(
        typeof(CoreCoreModule), 
        typeof(AbpEntityFrameworkCoreModule))]
    public class CoreEntityFrameworkCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CoreEntityFrameworkCoreModule).GetAssembly());
        }
    }
}
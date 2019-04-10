using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Dow.Core
{
    [DependsOn(
        typeof(CoreCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class CoreApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CoreApplicationModule).GetAssembly());
        }
    }
}
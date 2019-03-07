using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Dow.Core.Authorization;

namespace Dow.Core
{
    [DependsOn(
        typeof(CoreCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class CoreApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<CoreAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(CoreApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}

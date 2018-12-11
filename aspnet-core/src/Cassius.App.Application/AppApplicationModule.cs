using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Cassius.App.Authorization;

namespace Cassius.App
{
    [DependsOn(
        typeof(AppCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class AppApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(AppApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}

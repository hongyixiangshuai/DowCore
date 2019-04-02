namespace Dow.Core
{
    using Abp.AutoMapper;
    using Abp.Modules;
    using Abp.Reflection.Extensions;
    using Dow.Core.Authorization;

    /// <summary>
    /// Defines the <see cref="CoreApplicationModule" />
    /// </summary>
    [DependsOn(
        typeof(CoreCoreModule),
        typeof(AbpAutoMapperModule))]
    public class CoreApplicationModule : AbpModule
    {
        /// <summary>
        /// The PreInitialize
        /// </summary>
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<CoreAuthorizationProvider>();
        }

        /// <summary>
        /// The Initialize
        /// </summary>
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

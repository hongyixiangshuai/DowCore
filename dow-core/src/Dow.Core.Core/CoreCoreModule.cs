using Abp.Modules;
using Abp.Reflection.Extensions;
using Dow.Core.Localization;

namespace Dow.Core
{
    public class CoreCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            CoreLocalizationConfigurer.Configure(Configuration.Localization);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CoreCoreModule).GetAssembly());
        }
    }
}
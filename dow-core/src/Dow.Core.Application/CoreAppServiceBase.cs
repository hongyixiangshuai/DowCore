using Abp.Application.Services;

namespace Dow.Core
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class CoreAppServiceBase : ApplicationService
    {
        protected CoreAppServiceBase()
        {
            LocalizationSourceName = CoreConsts.LocalizationSourceName;
        }
    }
}
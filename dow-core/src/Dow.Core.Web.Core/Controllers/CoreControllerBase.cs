using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Dow.Core.Controllers
{
    public abstract class CoreControllerBase: AbpController
    {
        protected CoreControllerBase()
        {
            LocalizationSourceName = CoreConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Cassius.App.Controllers
{
    public abstract class AppControllerBase: AbpController
    {
        protected AppControllerBase()
        {
            LocalizationSourceName = AppConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

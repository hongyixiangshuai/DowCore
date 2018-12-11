using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Abp.Application.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Cassius.App.Authorization.Users;
using Cassius.App.MultiTenancy;
using Microsoft.Extensions.Options;
using Cassius.App.Config;
using Abp.Dependency;
using Cassius.App.Dapper;

namespace Cassius.App
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class AppAppServiceBase : ApplicationService
    {
        public TenantManager TenantManager { get; set; }

        public UserManager UserManager { get; set; }

        public IDapper DapperService { get; private set; }

        ConnectionStringsConfig ConnectionStringsConfig { get; set; }


        protected AppAppServiceBase()
        {
            LocalizationSourceName = AppConsts.LocalizationSourceName;

            ConnectionStringsConfig = IocManager.Instance.Resolve<IOptions<ConnectionStringsConfig>>().Value;
            DapperService = new MySqlDapper(ConnectionStringsConfig.Default);
        }

        protected virtual Task<User> GetCurrentUserAsync()
        {
            var user = UserManager.FindByIdAsync(AbpSession.GetUserId().ToString());
            if (user == null)
            {
                throw new Exception("There is no current user!");
            }

            return user;
        }

        protected virtual Task<Tenant> GetCurrentTenantAsync()
        {
            return TenantManager.GetByIdAsync(AbpSession.GetTenantId());
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

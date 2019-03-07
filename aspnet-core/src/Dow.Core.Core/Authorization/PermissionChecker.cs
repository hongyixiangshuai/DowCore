using Abp.Authorization;
using Dow.Core.Authorization.Roles;
using Dow.Core.Authorization.Users;

namespace Dow.Core.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

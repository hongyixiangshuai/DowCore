using Abp.Authorization;
using Cassius.App.Authorization.Roles;
using Cassius.App.Authorization.Users;

namespace Cassius.App.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

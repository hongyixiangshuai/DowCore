using System.Threading.Tasks;
using Abp.Application.Services;
using Cassius.App.Authorization.Accounts.Dto;

namespace Cassius.App.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}

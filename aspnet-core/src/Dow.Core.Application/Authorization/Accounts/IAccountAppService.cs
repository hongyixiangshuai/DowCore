using System.Threading.Tasks;
using Abp.Application.Services;
using Dow.Core.Authorization.Accounts.Dto;

namespace Dow.Core.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}

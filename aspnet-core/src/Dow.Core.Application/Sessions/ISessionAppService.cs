using System.Threading.Tasks;
using Abp.Application.Services;
using Dow.Core.Sessions.Dto;

namespace Dow.Core.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

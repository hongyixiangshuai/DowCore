using System.Threading.Tasks;
using Abp.Application.Services;
using Cassius.App.Sessions.Dto;

namespace Cassius.App.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}

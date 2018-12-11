using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Cassius.App.MultiTenancy.Dto;

namespace Cassius.App.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

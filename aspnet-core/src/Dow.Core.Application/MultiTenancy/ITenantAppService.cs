using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Dow.Core.MultiTenancy.Dto;

namespace Dow.Core.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}


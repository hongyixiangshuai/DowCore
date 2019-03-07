using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Dow.Core.Configuration.Dto;

namespace Dow.Core.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : CoreAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}

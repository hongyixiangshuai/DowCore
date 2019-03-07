using System.Threading.Tasks;
using Dow.Core.Configuration.Dto;

namespace Dow.Core.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}

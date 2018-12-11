using System.Threading.Tasks;
using Cassius.App.Configuration.Dto;

namespace Cassius.App.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}

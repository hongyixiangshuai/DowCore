using Microsoft.AspNetCore.Antiforgery;
using Dow.Core.Controllers;

namespace Dow.Core.Web.Host.Controllers
{
    public class AntiForgeryController : CoreControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}

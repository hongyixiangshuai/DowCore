using Microsoft.AspNetCore.Antiforgery;
using Cassius.App.Controllers;

namespace Cassius.App.Web.Host.Controllers
{
    public class AntiForgeryController : AppControllerBase
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

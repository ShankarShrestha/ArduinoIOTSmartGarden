using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOTSmartGarden.Controllers
{
    [Authorize]
    public class ClaimsController : Controller
    {
        // GET: Claims
        public ActionResult Index()
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;

            ViewBag.Name = userClaims?.FindFirst("name")?.Value;

            ViewBag.Username = userClaims?.FindFirst(System.IdentityModel.Claims.ClaimTypes.Name)?.Value;

            ViewBag.Subject = userClaims?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            ViewBag.TenantId = userClaims?.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid")?.Value;

            return View();

        }
    }
}
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace Sample.Web.Areas.Account.Controllers
{
    public class LogoffController : Controller
    {

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}
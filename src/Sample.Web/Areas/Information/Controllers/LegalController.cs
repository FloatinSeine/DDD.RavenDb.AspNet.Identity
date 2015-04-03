using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Areas.Information.Controllers
{
    public class LegalController : Controller
    {
        // GET: Information/Legal
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get the Privacy Policy for the site
        /// </summary>
        /// <returns></returns>
        public ActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Get Terms and Conditions for the Site
        /// </summary>
        /// <returns></returns>
        public ActionResult Terms()
        {
            return View();
        }
    }
}
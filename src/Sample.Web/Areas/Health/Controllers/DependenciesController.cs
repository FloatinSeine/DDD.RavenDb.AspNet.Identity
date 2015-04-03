using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sample.Web.ApplicationServices.Configuration;


namespace Sample.Web.Areas.Health.Controllers
{
    public class DependenciesController : Controller
    {
        // GET: Health/Dependencies
        public ActionResult Index()
        {
            var hb = new HealthCheck();
            var b = hb.CheckHealth();
            return View(b);
        }

        public ActionResult Heartbeat()
        {
            this.Response.StatusCode = 200;
            this.Response.StatusDescription = "I'm Alive";
            this.Response.OutputStream.Close();
            return null;
        }
    }
}
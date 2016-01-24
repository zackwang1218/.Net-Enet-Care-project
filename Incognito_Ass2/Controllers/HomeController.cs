using Incognito_Ass2.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Incognito_Ass2.Controllers
{
    public class HomeController : Controller
    {
        [InitializeSimpleMembership]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to Enet care.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "NET Care is an international charity. The charity provides medications and vaccines to remote communities.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Please fill in the form below and we will get back to you within 24 hours.";

            return View();
        }
    }
}

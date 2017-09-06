using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lua.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
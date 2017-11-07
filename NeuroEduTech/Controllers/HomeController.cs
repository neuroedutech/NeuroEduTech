using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeuroEduTech.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tutorial()
        {
            return View("Tutorial");
        }

        public ActionResult About()
        {
            ViewBag.Message = "NeuroEduTech";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "neuroedutech@gmail.com";

            return View();
        }
    }
}
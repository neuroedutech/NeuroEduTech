using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeuroEduTech.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PerfilMetaCognitivo()
        {
            return View("PerfilMetaCognitivo");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NeuroEduTech.DAL;

namespace NeuroEduTech.Controllers
{
    public class RelatorioController : Controller
    {
        // GET: Relatorio
        public ActionResult Index()
        {
            return View();
        }

        public string GerarRelatorioJulgamentoDoUsuario()
        {
            string resultado = BaseDAL.DataTable_JSON_JavaSerializer(BaseDAL.GerarRelatorioJulgamentoDoUsuarioDAL(""));
            return resultado;
        }
    }
}
using NeuroEduTech.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeuroEduTech.Controllers
{

    public class PerguntasController : Controller
    {
        // GET: Perguntas
        public ActionResult Perguntas()
        {
            return View("Perguntas");
        }

        public string CriarPergunta(string pergunta, string resposta1, string resposta2, string resposta3, string resposta4)
        {

            string resultado = BaseDAL.CriarPerguntaDAO(pergunta, resposta1, resposta2, resposta3, resposta4);
            return resultado;

        }

        public string BuscarPerguntas()
        {

            string resultado = BaseDAL.DataTable_JSON_JavaSerializer(BaseDAL.BuscarPerguntasDAL());
            return resultado;

        }
    }
}
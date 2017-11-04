using NeuroEduTech.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeuroEduTech.Controllers
{

    public class QuestoesController : Controller
    {
        // GET: Perguntas
        public ActionResult Questoes()
        {
            return View("Questoes");
        }

        public string CriarQuestao(string questao, string assertiva1, string assertiva2, string assertiva3, string assertiva4)
        {

            string resultado = BaseDAL.CriarQuestaoDAO(questao, assertiva1, assertiva2, assertiva3, assertiva4);   
            return resultado;

        }

        public string BuscarQuestoes(string idQuestao)
        {

            int idQuestaoInt = int.Parse(idQuestao);
            string resultado = BaseDAL.DataTable_JSON_JavaSerializer(BaseDAL.BuscarQuestoesDAL(idQuestaoInt));
            return resultado;

        }

        public string EnviarRespostas(string[] lista, string[] listaCodRespostas)
        {

            string resultado = BaseDAL.EnviarRespostasDAL(lista, listaCodRespostas);
            return resultado;

        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace NeuroEduTech.DAL
{
    public static class BaseDAL
    {

        public static DataTable ConexaoDB(string procedure)
        {
            string connectionStringLivrariaIsrael = "server=DESKTOP-D8KC6K2;database=neuroEduTech;Trusted_Connection=true";
            SqlConnection sqlConn = new SqlConnection(connectionStringLivrariaIsrael);
            sqlConn.Open();
            SqlCommand sqlCommand = sqlConn.CreateCommand();
            sqlCommand.CommandText = procedure;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 7200;
            SqlDataAdapter dtaDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dt = new DataTable();
            dtaDataAdapter.Fill(dt);

            return dt;

        }

        public static SqlCommand ConexaoDBComParam(string procedure)
        {
            string connectionStringLivrariaIsrael = "server=DESKTOP-D8KC6K2;database=neuroEduTech;Trusted_Connection=true";
            SqlConnection sqlConn = new SqlConnection(connectionStringLivrariaIsrael);
            sqlConn.Open();
            SqlCommand sqlCommand = sqlConn.CreateCommand();
            sqlCommand.CommandText = procedure;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 7200;

            return sqlCommand;
        }

        #region QUESTÕES


        public static DataTable BuscarQuestoesDAL(int idQuestao)
        {

            SqlCommand sqlComando = ConexaoDBComParam("sp_v_questoes_e_assertivas");
            sqlComando.Parameters.Add(new SqlParameter("@idQuestao", idQuestao));
            SqlDataAdapter dtaDataAdapter = new SqlDataAdapter(sqlComando);

            DataTable dt = new DataTable();
            dtaDataAdapter.Fill(dt);

            return dt;

        }

        public static string CriarQuestaoDAO(string questao, string assertiva1, string assertiva2, string assertiva3, string assertiva4)
        {
            SqlParameter outputmsg = InstanciaOutput();

            string returnStringOutput;

            SqlCommand sqlComando = ConexaoDBComParam("sp_c_questoes_e_assertivas");

            sqlComando.Parameters.Add(new SqlParameter("@questao", questao));
            sqlComando.Parameters.Add(new SqlParameter("@assertiva1", assertiva1));
            sqlComando.Parameters.Add(new SqlParameter("@assertiva2", assertiva2));
            sqlComando.Parameters.Add(new SqlParameter("@assertiva3", assertiva3));
            sqlComando.Parameters.Add(new SqlParameter("@assertiva4", assertiva4));
            sqlComando.Parameters.Add(outputmsg);

            sqlComando.ExecuteNonQuery();

            returnStringOutput = (string)sqlComando.Parameters["@outputmsg"].Value;

            return returnStringOutput;
        }

        #endregion

        #region RESPOSTAS

        public static string EnviarRespostasDAL(string[] lista, string[] listaCodRespostas)
        {

            int[] listaInt = Array.ConvertAll(lista, int.Parse);
            int[] listaCodRespostasInt = Array.ConvertAll(listaCodRespostas, int.Parse);

            SqlParameter outputmsg = InstanciaOutput();

            string returnStringOutput;

            SqlCommand sqlComando = ConexaoDBComParam("sp_c_respostas_crencas");

            sqlComando.Parameters.Add(new SqlParameter("@codUsuario", 1));
            sqlComando.Parameters.Add(new SqlParameter("@codResposta1", listaCodRespostasInt[0]));
            sqlComando.Parameters.Add(new SqlParameter("@resposta1", listaInt[0]));
            sqlComando.Parameters.Add(new SqlParameter("@crenca1", listaInt[1]));
            sqlComando.Parameters.Add(new SqlParameter("@codResposta2", listaCodRespostasInt[1]));
            sqlComando.Parameters.Add(new SqlParameter("@resposta2", listaInt[2]));
            sqlComando.Parameters.Add(new SqlParameter("@crenca2", listaInt[3]));
            sqlComando.Parameters.Add(new SqlParameter("@codResposta3", listaCodRespostasInt[2]));
            sqlComando.Parameters.Add(new SqlParameter("@resposta3", listaInt[4]));
            sqlComando.Parameters.Add(new SqlParameter("@crenca3", listaInt[5]));
            sqlComando.Parameters.Add(new SqlParameter("@codResposta4", listaCodRespostasInt[3]));
            sqlComando.Parameters.Add(new SqlParameter("@resposta4", listaInt[6]));
            sqlComando.Parameters.Add(new SqlParameter("@crenca4", listaInt[7]));
            sqlComando.Parameters.Add(outputmsg);

            sqlComando.ExecuteNonQuery();

            returnStringOutput = (string)sqlComando.Parameters["@outputmsg"].Value;

            return returnStringOutput;

        }

        #endregion

        #region RELATÓRIO JULGAMENTO

        public static DataTable GerarRelatorioJulgamentoDoUsuarioDAL(string procedureName)
        {
            DataTable dt = new DataTable();
            dt = ConexaoDB("sp_v_gerarRelatorioJulgamentoDoUsuario");
            return dt;
        }



        #endregion

        public static SqlParameter InstanciaOutput()
        {
            SqlParameter outputmsg = new SqlParameter();
            outputmsg.ParameterName = "@outputmsg";
            outputmsg.Direction = ParameterDirection.Output;
            outputmsg.DbType = DbType.String;
            outputmsg.Size = 2000;
            return outputmsg;
        }

        public static string DataTable_JSON_JavaSerializer(DataTable tbParam)
        {
            DataTable tabela = new DataTable();
            tabela = tbParam;

            try
            {
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
                Dictionary<string, object> childRow;

                foreach (DataRow row in tabela.Rows)
                {
                    childRow = new Dictionary<string, object>();
                    foreach (DataColumn col in tabela.Columns)
                    {
                        childRow.Add(col.ColumnName, row[col]);
                    }
                    parentRow.Add(childRow);
                }
                return jsSerializer.Serialize(parentRow);
            }
            catch
            {

                throw;
            }

        }



    }
}
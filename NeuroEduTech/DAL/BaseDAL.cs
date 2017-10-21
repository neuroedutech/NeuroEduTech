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

        public static DataTable BuscarPerguntasDAL()
        {

            DataTable dt = new DataTable();
            dt = ConexaoDB("sp_v_perguntas_e_respostas");
            return dt;

        }

        public static string CriarPerguntaDAO(string pergunta, string resposta1, string resposta2, string resposta3, string resposta4)
        {
            SqlParameter outputmsg = InstanciaOutput();

            string returnStringOutput;

            SqlCommand sqlComando = ConexaoDBComParam("sp_c_perguntas_e_respostas");

            sqlComando.Parameters.Add(new SqlParameter("@pergunta", pergunta));
            sqlComando.Parameters.Add(new SqlParameter("@resposta1", resposta1));
            sqlComando.Parameters.Add(new SqlParameter("@resposta2", resposta2));
            sqlComando.Parameters.Add(new SqlParameter("@resposta3", resposta3));
            sqlComando.Parameters.Add(new SqlParameter("@resposta4", resposta4));
            sqlComando.Parameters.Add(outputmsg);

            sqlComando.ExecuteNonQuery();

            returnStringOutput = (string)sqlComando.Parameters["@outputmsg"].Value;

            return returnStringOutput;
        }


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
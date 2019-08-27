using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso a banco de dados
    /// </summary>
    public class USU_T135LANDataAccess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();
        /// <summary>
        /// Pesquisa relacionamento de embarque entre filiais
        /// </summary>
        /// <param name="numAneFilial">Número Filial</param>
        /// <param name="analiseEmbaqueFilial">Analise de embarque</param>
        public void PesquisaRelacionamentoAneEmbarqueEntreFilial(long numAneFilial, out long analiseEmbaqueFilial)
        {
            try
            {
                string sql = "select usu_aneori from SAPIENS.usu_t135lan where usu_numane = " + numAneFilial;

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                analiseEmbaqueFilial = 99999;
                while (dr.Read())
                {
                    analiseEmbaqueFilial = dr.GetInt64(0);
                }

                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

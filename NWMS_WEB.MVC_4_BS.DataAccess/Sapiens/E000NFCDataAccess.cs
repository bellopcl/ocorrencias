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
    /// Classe de conexão com o banco de dados
    /// </summary>
    public class E000NFCDataAccess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();

        /// <summary>
        /// Recebe o número do CNPJ e retorna uma lista de números de notas fiscais
        /// </summary>
        /// <param name="cnpj">CNPJ do Cliente</param>
        /// <returns>NUMNFC</returns>

         public List<int> ValidarNotaClienteE440NFC(string cnpj)
        {
             
            var CNPJ = cnpj.Replace("/", "").Replace("-", "").Replace(".", "");

            string sql = @"  SELECT E00.NUMNFC," +
                          "         E00.CHVNEL," +
                          "         SDE_NFE.SITNFE" +
                          "    FROM SAPIENS.E000NFC E00" +
                          "   INNER JOIN SAPIENS.E095FOR E095" +
                          "      ON (E095.CGCCPF = E00.CGCFOR)" +
                          "   INNER JOIN SDE.NW130NFE SDE_NFE" +
                          "      ON SDE_NFE.CHVNFE = E00.CHVNEL" +
                          "     AND SDE_NFE.SITNFE IN (6)" +
                          "   WHERE CGCFOR = " + CNPJ + "" +
                          "     AND E00.STANFV NOT IN (3)" +
                          "     AND NOT EXISTS (SELECT 1" +
                          "            FROM SAPIENS.E440NFC E440" +
                          "           WHERE E440.CHVNEL = E00.CHVNEL" +
                          "             AND E440.SITNFC IN (2)" +
                          "             AND E440.CODFOR = E095.CODFOR)";
            
            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            List<int> NUMNFC = new List<int>();
            OracleDataReader dr = cmd.ExecuteReader();

            try
            {
                while (dr.Read())
                {
                    NUMNFC.Add(Convert.ToInt32(dr["NUMNFC"]));
                }

                return NUMNFC;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr.Close();
                conn.Close();
            }
        }
        public List<int> ValidarNotaCliente(string codigoNota, string codFil, string cnpj)
        {
            return ValidarNotaClienteE440NFC(cnpj);
        }
    }
}

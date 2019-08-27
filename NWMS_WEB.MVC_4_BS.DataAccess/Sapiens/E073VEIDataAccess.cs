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
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class E073VEIDataAccess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();
        /// <summary>
        /// Retorna uma lista de placas
        /// </summary>
        /// <param name="codPlaca">Placa</param>
        /// <returns>listaPlacas</returns>
        public List<E073VEIModel> PesquisarCaminhaoPorPlaca(string codPlaca)
        {
            try
            {
                string sql = "select A.CODTRA, A.PLAVEI, B.DESTIP   " +
                              "  from SAPIENS.E073VEI A, SAPIENS.E073TIP B          " +
                              "  where codtra = 1                   " +
                              "  AND A.CODTIP = B.CODTIP            ";

                if (!string.IsNullOrEmpty(codPlaca))
                {
                    sql = "select A.CODTRA, A.PLAVEI, B.DESTIP      " +
                          "  from SAPIENS.E073VEI A, SAPIENS.E073TIP B              " +
                          "  where codtra = 1                       " +
                          "  and a.plavei = " + "'" + codPlaca + "' " +
                          "  AND A.CODTIP = B.CODTIP                ";
                }

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<E073VEIModel> listaPlacas = new List<E073VEIModel>();
                E073VEIModel itemPlaca = new E073VEIModel();

                while (dr.Read())
                {
                    itemPlaca = new E073VEIModel();
                    itemPlaca.Placa = dr.GetString(1).Substring(0, 3) + "-" + dr.GetString(1).Substring(3, 4);
                    itemPlaca.TipoCaminhao = dr.GetString(2);
                    listaPlacas.Add(itemPlaca);
                }

                dr.Close();
                conn.Close();

                return listaPlacas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

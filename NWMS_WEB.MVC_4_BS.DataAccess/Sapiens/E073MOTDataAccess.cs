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
    /// Classe de conexão com o Banco de dados
    /// </summary>
    public class E073MOTDataAccess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();
        /// <summary>
        /// Retorna uma lista de Cadastro de Motoristas
        /// </summary>
        /// <param name="codigoMotorista">Código do Motorista</param>
        /// <returns>listaMotoristas</returns>
        public List<E073MOTModel> PesquisasMotoristas(long? codigoMotorista)
        {
            try
            {
                string sql = string.Empty;
                sql = "select CODMTR AS Codigo, NOMMOT AS Nome, CGCCPF AS Cpf, ENDMOT ||' '|| CPLEND AS Endereco, CIDMOT AS Cidade, SIGUFS AS Endereco from SAPIENS.E073MOT where CODTRA = 1 AND SITMOT = 'D' order by CODMTR";
                if (codigoMotorista != null)
                {
                    sql = "select CODMTR AS Codigo, NOMMOT AS Nome, CGCCPF AS Cpf, ENDMOT ||' '|| CPLEND AS Endereco, CIDMOT AS Cidade, SIGUFS AS Endereco from SAPIENS.E073MOT where CODTRA = 1 AND SITMOT = 'D' and CODMTR = " + codigoMotorista;
                }

                OracleConnection conn = new OracleConnection(this.OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<E073MOTModel> listaMotoristas = new List<E073MOTModel>();
                E073MOTModel itemMotorista = new E073MOTModel();

                while (dr.Read())
                {
                    itemMotorista = new E073MOTModel();
                    itemMotorista.CodigoMotorista = dr.GetInt64(0);
                    itemMotorista.Nome = dr.GetString(1);
                    itemMotorista.Cpf = dr.GetInt64(2).ToString(@"000\.000\.000\-00");
                    itemMotorista.Endereco = dr.GetString(3);
                    itemMotorista.Cidade = dr.GetString(4);
                    itemMotorista.Estado = dr.GetString(5);
                    listaMotoristas.Add(itemMotorista);
                }

                dr.Close();
                conn.Close();
                return listaMotoristas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

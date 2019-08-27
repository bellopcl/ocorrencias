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
    /// Classe de Acesso ao Banco de dados
    /// </summary>
    public class E099USUDataAccess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();
        /// <summary>
        /// Retorna uma lista de usuários
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <returns>listaUsuarios</returns>
        public List<E099USUModel> PesquisarUsuariosSapiens(long? codigoUsuario)
        {
            try
            {
                string sql = "select B.CODUSU, A.NOMCOM, B.INTNET            " +
                             "  from SAPIENS.e099usu B                               " +
                             " inner join r910usu A on B.CODUSU = A.CODENT   " +
                             " WHERE B.SITUSU = 'A'                          " +
                             "   and B.INTNET <> ' '                         " +
                             "   and A.CONHAB = 1                            " +
                             " ORDER BY B.CODUSU                             ";

                if (codigoUsuario != null)
                {
                    sql = "select B.CODUSU, A.NOMCOM, B.INTNET            " +
                          "  from SAPIENS.e099usu B                               " +
                          " inner join r910usu A on B.CODUSU = A.CODENT   " +
                          " WHERE B.SITUSU = 'A'                          " +
                          "   and B.CODUSU = " + codigoUsuario +
                          "   and A.CONHAB = 1                            " +
                          "   and B.INTNET <> ' '                         ";
                }

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<E099USUModel> listaUsuarios = new List<E099USUModel>();
                E099USUModel itemUsuario = new E099USUModel();

                while (dr.Read())
                {
                    itemUsuario = new E099USUModel();
                    itemUsuario.CodigoUsuario = dr.GetInt32(0);
                    itemUsuario.NomeUsuario = dr.GetString(1);
                    itemUsuario.EmailUsuario = dr.GetString(2);
                    listaUsuarios.Add(itemUsuario);
                }

                dr.Close();
                conn.Close();
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

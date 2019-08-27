using System;
using System.Collections.Generic;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.Data;
using System.Data.OracleClient;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N0204DORIDataAcess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();
        /// <summary>
        /// Verifica se o usuário logado tem permissão para o Dashboard
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <returns>listaPermissaoDashBoard</returns>
        public List<N0204DORI> PesquisarPermissaoDashBoard(long codigoUsuario)
        {
            try
            {
                string sql = "  SELECT A.CODORI, A.DESCORI, NVL(B.CODUSU, 0) AS CODUSU, NVL(B.CODORI, 0) AS CODDORI" +
                             "     FROM NWMS_PRODUCAO.N0204ORI A" +
                             "    LEFT JOIN NWMS_PRODUCAO.N0204DORI B" +
                             "       ON (A.CODORI = B.CODORI)" +
                             "    AND B.CODUSU = "+codigoUsuario+"";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<N0204DORI> listaPermissaoDashBoard = new List<N0204DORI>();
                N0204DORI itensOrigensPermissoes = new N0204DORI();
                while (dr.Read())
                {
                    itensOrigensPermissoes = new N0204DORI();
                    itensOrigensPermissoes.CODORI = Convert.ToInt64(dr["CODORI"]);
                    itensOrigensPermissoes.DESCORI = dr["DESCORI"].ToString();
                    itensOrigensPermissoes.CODUSU = Convert.ToInt64(dr["CODUSU"]);
                    itensOrigensPermissoes.CODDORI = Convert.ToInt64(dr["CODDORI"]);
                    listaPermissaoDashBoard.Add(itensOrigensPermissoes);
                }

                dr.Close();
                conn.Close();
                return listaPermissaoDashBoard;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Apaga as permissões do usuário para o Dashboard
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuário</param>
        public void deletarPermissaoDashUsuario(long codigoUsuario)
        {
            try
            {
                string sql = "DELETE FROM NWMS_PRODUCAO.N0204DORI WHERE CODUSU = " + codigoUsuario + "";
                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Grava as permissões para o usuário para o Dashboard
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <param name="itensCodigo">Código das permissões</param>
        public void GravarPermissaoDashUsuario(long codigoUsuario, string[] itensCodigo)
        {
            try
            {
                deletarPermissaoDashUsuario(codigoUsuario);
                if (itensCodigo[0] != "")
                {
                string sql = "INSERT ALL";
                foreach (var item in itensCodigo)
                {
                    sql += " INTO NWMS_PRODUCAO.N0204DORI VALUES (" + codigoUsuario + "," + item + ")";
                }
                sql += "SELECT * FROM dual";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

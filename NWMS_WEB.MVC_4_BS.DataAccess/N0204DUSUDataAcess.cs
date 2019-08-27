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
    public partial class N0204DUSUDataAcess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();

        /// <summary>
        /// Pesquisa a permissão para o DashBoard para o usuário
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <returns>N0204DUSU</returns>
        public List<N0204DUSU> PesquisarPermissaoDashBoard(long codigoUsuario)
        {
            try
            {
                string sql = "  SELECT T.CODTIP AS CODTIP, T.TIP, NVL(D.CODUSU,0) AS CODUSU, NVL(D.CODTIP,0) AS CODTIP" +
                             "    FROM NWMS_PRODUCAO.N0203TIPDA T" +
                             "    LEFT JOIN NWMS_PRODUCAO.N0204DUSU D" +
                             "      ON (D.CODTIP = T.CODTIP  AND D.CODUSU = " + codigoUsuario + ") ORDER BY T.CODTIP";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<N0204DUSU> ListaTiposDash = new List<N0204DUSU>();
                N0204DUSU itensTipDash = new N0204DUSU();
                while (dr.Read())
                {
                    itensTipDash = new N0204DUSU();
                    itensTipDash.CODUSU = Convert.ToInt64(dr["CODUSU"]);
                    itensTipDash.CODTIP = Convert.ToInt64(dr["CODTIP"]);
                    itensTipDash.TIP = dr["TIP"].ToString();
                    ListaTiposDash.Add(itensTipDash);
                }

                dr.Close();
                conn.Close();
                return ListaTiposDash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Verifica os níveis de acesso do usuário para os dashboard
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <returns>ListaTiposDash</returns>
        public List<N0204DUSU> PesquisarAcessoDashBoard(long codigoUsuario)
        {
            try
            {
                string sql = "  SELECT T.CODTIP AS CODTIP, T.TIP, NVL(D.CODUSU,0) AS CODUSU, NVL(D.CODTIP,0) AS CODTIP" +
                             "    FROM NWMS_PRODUCAO.N0203TIPDA T" +
                             "    LEFT JOIN NWMS_PRODUCAO.N0204DUSU D" +
                             "      ON (D.CODTIP = T.CODTIP  AND D.CODUSU = " + codigoUsuario + ") WHERE CODUSU = "+codigoUsuario+" ORDER BY T.CODTIP";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<N0204DUSU> ListaTiposDash = new List<N0204DUSU>();
                N0204DUSU itensTipDash = new N0204DUSU();
                while (dr.Read())
                {
                    itensTipDash = new N0204DUSU();
                    itensTipDash.CODUSU = Convert.ToInt64(dr["CODUSU"]);
                    itensTipDash.CODTIP = Convert.ToInt64(dr["CODTIP"]);
                    itensTipDash.TIP = dr["TIP"].ToString();
                    ListaTiposDash.Add(itensTipDash);
                }

                dr.Close();
                conn.Close();
                return ListaTiposDash;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletar as permissões do usuário ao Dashboard
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuário</param>
        public void deletarPermissaoDashUsuario(long codigoUsuario)
        {
            try
            {
                string sql = "DELETE FROM NWMS_PRODUCAO.N0204DUSU WHERE CODUSU = " + codigoUsuario + "";
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
        /// Gravar as permissões dos usuários ao DashBoard
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <param name="itensCodigo">Código do Itens</param>
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
                        sql += " INTO NWMS_PRODUCAO.N0204DUSU VALUES (" + codigoUsuario + "," + item + ")";
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

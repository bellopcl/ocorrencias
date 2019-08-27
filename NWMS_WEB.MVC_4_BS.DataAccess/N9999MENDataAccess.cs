using System;
using System.Collections.Generic;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.Data;
using System.Data.OracleClient;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N9999MENDataAccess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();

        /// <summary>
        /// Monta as permissões para os usuários
        /// </summary>
        /// <param name="CODUSU">Código do Usuário</param>
        /// <param name="CODSIS">Código do sistema</param>
        /// <returns>Lista de permissões por usuário</returns>
        public List<MenuModel> montaPermissoes(long CODUSU, int CODSIS)
        {
            try
            {
                string sql = @" SELECT MEN.DESMEN, NVL(MEN.MENPAI,0) MENPAI, NVL(MEN.ORDMEN,0) ORDMEN, MEN.CODMEN, NVL(USM.PERMEN,'N') PERMEN, NVL(USM.CODUSU,0) CODUSU
                                  FROM NWMS_PRODUCAO.N9999MEN MEN
                                  LEFT JOIN NWMS_PRODUCAO.N9999USM USM
                                    ON USM.CODMEN = MEN.CODMEN ";
                      sql += "AND USM.CODUSU = "+CODUSU+" WHERE MEN.CODSIS = 2";
                      sql += "GROUP BY DESMEN, MENPAI, ORDMEN, MEN.CODMEN, PERMEN, CODUSU, MEN.CODSIS ORDER BY MEN.CODMEN";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<MenuModel> lista = new List<MenuModel>();
                MenuModel itens = new MenuModel();

                while (dr.Read())
                {
                    itens = new MenuModel();
                    itens.DESMEN = dr["DESMEN"].ToString();
                    itens.MENPAI = Convert.ToInt64(dr["MENPAI"]);
                    itens.CODMEN = Convert.ToInt64(dr["CODMEN"]);
                    itens.ORDMEN = Convert.ToInt64(dr["ORDMEN"]);
                    itens.CODUSU = Convert.ToInt64(dr["CODUSU"]);
                    itens.PERMEN = dr["PERMEN"].ToString();
                    lista.Add(itens);
                }

                dr.Close();
                conn.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Monta a tabela de permissão para o menu
        /// </summary>
        /// <param name="codMen">Código do Menu</param>
        /// <returns>Lista de permissões</returns>
        public bool verificarFilhos(long codMen)
        {
            try
            {
                var sql = "SELECT COALESCE(MENPAI, 0) AS MENPAI FROM NWMS_PRODUCAO.N9999MEN WHERE CODMEN = "+codMen+"";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return Convert.ToInt64(dr["MENPAI"]) == 0 ? true : false;
                }
             
                dr.Close();
                conn.Close();
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// Remove permissão de acesso ao usuário
        /// </summary>
        /// <param name="codMen">Código do Menu</param>
        /// <param name="codUsuario">Código do Usuário</param>
        /// <returns>true/false</returns>
        public Boolean removerAcesso(long codMen, long codUsuario)
        {
            try
            {
                var sql = "";
                if (!verificarFilhos(codMen))
                {
                    sql += "DELETE FROM NWMS_PRODUCAO.N9999USM WHERE CODUSU = " + codUsuario + " AND CODMEN = " + codMen + "";
                }else{
                    sql += " DELETE FROM"+
                          "              NWMS_PRODUCAO.N9999USM"+
                          "          WHERE"+
                          "              CODUSU = "+codUsuario+""+
                          "          AND CODMEN IN" +
                          "              ("+
                          "                  SELECT"+
                          "                      CODMEN"+
                          "                  FROM"+
                          "                      NWMS_PRODUCAO.N9999MEN"+
                          "                  WHERE" +
                          "                      MENPAI = " + codMen + ")";
                }

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<MenuModel> lista = new List<MenuModel>();
                MenuModel itens = new MenuModel();

                dr.Close();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Monta a lista de menus de acordo com as permissões do usuário logado
        /// </summary>
        /// <param name="CODUSU">código do usuário</param>
        /// <returns>lista de menu</returns>
        public List<MenuModel> MontarMenu(long CODUSU, int CODSIS)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var listaMenu = (from m in contexto.N9999MEN
                                     join c in contexto.N9999USM on new { m.CODSIS, m.CODMEN, CODUSU } equals new { c.CODSIS, c.CODMEN, c.CODUSU }
                                     where m.CODSIS == CODSIS
                                     select new MenuModel
                                     {
                                         CODMEN = m.CODMEN,
                                         ICOMEN = m.ICOMEN,
                                         DESMEN = m.DESMEN,
                                         CODSIS = m.CODSIS,
                                         MENPAI = m.MENPAI,
                                         ORDMEN = m.ORDMEN,
                                         ENDPAG = m.ENDPAG,
                                         PERMEN = c.PERMEN,
                                         INSMEN = c.INSMEN,
                                         ALTMEN = c.ALTMEN,
                                         EXCMEN = c.EXCMEN

                                     }).ToList();

                    return listaMenu;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lista todos os itens do menu, telas e operações que o usuário pesquisado já possuí algum tipo de operação
        /// </summary>
        /// <param name="CODUSU">código do usuário</param>
        /// <returns>lista de menus</returns>
        public List<MenuModel> MontarTreeViewPermissoes(long CODUSU, int CODSIS)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var listaMenu = (from m in contexto.N9999MEN
                                     join c in contexto.N9999USM on new { m.CODSIS, m.CODMEN, CODUSU } equals new { c.CODSIS, c.CODMEN, c.CODUSU } into ps
                                     from r in ps.DefaultIfEmpty()
                                     where m.CODSIS == CODSIS
                                     select new MenuModel
                                     {
                                         CODMEN = m.CODMEN,
                                         DESMEN = m.DESMEN,
                                         CODSIS = m.CODSIS,
                                         MENPAI = m.MENPAI,
                                         ORDMEN = m.ORDMEN,
                                         ENDPAG = m.ENDPAG,
                                         PERMEN = r.PERMEN,
                                         INSMEN = r.INSMEN,
                                         ALTMEN = r.ALTMEN,
                                         EXCMEN = r.EXCMEN

                                     }).ToList();

                    return listaMenu;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    public class PERMISSAODataAccess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();

        // PERMISSOES EM TELA
        public List<RelPermissaoTela> relPermissaoTelas(string usuario, char status) {
            try
            {
                string sql = @"SELECT
                            USU.CODUSU        AS COD_USUARIO,
                            USU.LOGIN  AS USUARIO,
                            MEN.DESMEN AS MENU,
                            CASE USM.INSMEN
                                WHEN 'I'
                                THEN 'HABILITADO'
                                ELSE 'DESABILITADO'
                            END INSERIR,
                            CASE USM.ALTMEN
                                WHEN 'A'
                                THEN 'HABILITADO'
                                ELSE 'DESABILITADO'
                            END ALTERAR,
                            CASE USM.EXCMEN
                                WHEN 'E'
                                THEN 'HABILITADO'
                                ELSE 'DESABILITADO'
                            END EXCLUIR
                        FROM
                            NWMS_PRODUCAO.N9999USU USU,
                            NWMS_PRODUCAO.N9999MEN MEN,
                            NWMS_PRODUCAO.N9999USM USM
                        WHERE
                            USU.CODUSU = USM.CODUSU
                        AND MEN.CODMEN = USM.CODMEN";
                        if(usuario != null && usuario != "")
                        {
                            sql += " AND USU.LOGIN = '" + usuario + "' ";
                        }
                        if(status == 'S')
                        {
                            sql += " AND (USM.INSMEN = 'I' OR USM.ALTMEN = 'A'  OR USM.EXCMEN = 'E')";
                        }
                        if(status == 'N')
                        {
                            sql += " AND USM.INSMEN <> 'I' AND USM.ALTMEN <> 'A' AND USM.EXCMEN <> 'E'";
                        }

                sql += " ORDER BY USU.LOGIN";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                conn.Open();

                List<RelPermissaoTela> itens = new List<RelPermissaoTela>();
                RelPermissaoTela Rel = new RelPermissaoTela();

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Rel = new RelPermissaoTela();
                    Rel.CodUsuario = dr["COD_USUARIO"].ToString();
                    Rel.Usuario = dr["USUARIO"].ToString();
                    Rel.Menu = dr["MENU"].ToString();
                    Rel.Inserir = dr["INSERIR"].ToString();
                    Rel.Alterar = dr["ALTERAR"].ToString();
                    Rel.Excluir = dr["EXCLUIR"].ToString();
                    itens.Add(Rel);
                }

                dr.Close();
                conn.Close();
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //USUÁRIO APROVADOR X ORIGEM DE OCORRÊNCIA
        public List<RelPermissaoAprovadorOrigem> relPermissaoAprovadorOrigems(string usuario, char status)
        {
            try
            {
                string sql = "";
                if(status != 'N') { 
                    sql = @"SELECT
                                USU.CODUSU AS COD_USUARIO,
                                USU.LOGIN AS USUARIO,
                                CASE AP.CODATD WHEN 1 THEN 'DEVOLUÇÃO'
                                ELSE 'TROCA' END TIPO,
                                RI.DESCORI AS ORIGEM
    
                            FROM
                                N0203UAP AP,
                                N9999USU USU,
                                N0204ORI RI
                            WHERE
                                AP.CODORI  = RI.CODORI
                            AND USU.CODUSU = AP.CODUSU";
                        if (usuario != null && usuario != "")
                        {
                            sql += " AND USU.LOGIN  = '" + usuario + "'";
                        }
                
                    OracleConnection conn = new OracleConnection(OracleStringConnection);
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();
                    OracleDataReader dr = cmd.ExecuteReader();
                
                    List<RelPermissaoAprovadorOrigem> itens = new List<RelPermissaoAprovadorOrigem>();
                    RelPermissaoAprovadorOrigem Rel = new RelPermissaoAprovadorOrigem();

                

                    while (dr.Read())
                    {
                        Rel = new RelPermissaoAprovadorOrigem();
                        Rel.CodUsuario = dr["COD_USUARIO"].ToString();
                        Rel.Usuario = dr["USUARIO"].ToString();
                        Rel.Tipo = dr["TIPO"].ToString();
                        Rel.Origem = dr["ORIGEM"].ToString();
                        itens.Add(Rel);
                    }

                    dr.Close();
                    conn.Close();
                    return itens;
                }
                List<RelPermissaoAprovadorOrigem> ite = new List<RelPermissaoAprovadorOrigem>();
                return ite;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //USUÁRIO APROVADOR X OPERAÇÃO APROVAÇÃO FATURAMENTO
        public List<RelPermissaoUsuAproFaturamento> relPermissaoUsuarioAprovacaoFaturamentos(string usuario, char status)
        {
            try
            {
                string sql = "";
                if(status != 'N') { 
                    sql = @"SELECT
                            USU.CODUSU AS CODUSU,
                            USU.LOGIN  AS LOGIN,
                            CASE UOF.CODOPE
                                WHEN 1
                                THEN 'APROVAR'
                                WHEN 2
                                THEN 'CANCELAR'
                                ELSE 'REABILITAR'
                            END OPERACAO,
                            CASE UOF.CODATD
                                WHEN 1
                                THEN 'DEVOLUÇÃO'
                                ELSE 'TROCA'
                            END TIPO
                        FROM
                            N0203UOF UOF,
                            N9999USU USU
                        WHERE
                            UOF.CODUSU = USU.CODUSU";
                        if (usuario != null && usuario != "")
                        {
                            sql += " AND USU.LOGIN = '" + usuario + "'";
                        }
                
                    OracleConnection conn = new OracleConnection(OracleStringConnection);
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();

                    List<RelPermissaoUsuAproFaturamento> itens = new List<RelPermissaoUsuAproFaturamento>();
                    RelPermissaoUsuAproFaturamento Rel = new RelPermissaoUsuAproFaturamento();

                    OracleDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Rel = new RelPermissaoUsuAproFaturamento();
                        Rel.CodUsuario = dr["CODUSU"].ToString();
                        Rel.Usuario = dr["LOGIN"].ToString();
                        Rel.Operacao = dr["OPERACAO"].ToString();
                        Rel.Tipo = dr["TIPO"].ToString();
                        itens.Add(Rel);
                    }

                    dr.Close();
                    conn.Close();
                    return itens;
                }

                List<RelPermissaoUsuAproFaturamento> ite = new List<RelPermissaoUsuAproFaturamento>();
                return ite;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PermissaoDevTrocaUsu> PermissaoTrocaDevolucao(string usuario, char status)
        {
            try
            {
                string sql = "";
                if(status != 'N') { 
                    sql = @"SELECT
                                    USU.CODUSU AS COD_USUARIO,
                                    USU.LOGIN  AS USUARIO,
                                    PU.QTDDEV  AS TEMPO_DEVOLUCAO,
                                    PU.QTDTRC  AS TEMPO_TROCA
                                FROM
                                    N0204PPU PU,
                                    N9999USU USU
                                WHERE
                                    PU.CODUSU = USU.CODUSU";
                                if(usuario != null && usuario != "")
                                {
                                    sql += " AND USU.LOGIN = '" + usuario + "'";
                                }


                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                conn.Open();
                List<PermissaoDevTrocaUsu> itens = new List<PermissaoDevTrocaUsu>();
                PermissaoDevTrocaUsu Rel = new PermissaoDevTrocaUsu();

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Rel = new PermissaoDevTrocaUsu();
                    Rel.CodUsuario = dr["COD_USUARIO"].ToString();
                    Rel.Usuario = dr["USUARIO"].ToString();
                    Rel.TempoDevolucao = dr["TEMPO_DEVOLUCAO"].ToString();
                    Rel.TempoTroca = dr["TEMPO_TROCA"].ToString();
                    itens.Add(Rel);
                }

                dr.Close();
                conn.Close();
                return itens;
                }

                List<PermissaoDevTrocaUsu> ite = new List<PermissaoDevTrocaUsu>();
                return ite;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

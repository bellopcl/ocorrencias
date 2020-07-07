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
    public class E140NFVDataAccess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();
        /// <summary>
        /// Pesquisa notas fiscais de saída
        /// </summary>
        /// <param name="codigoCliente">Código do Cliente</param>
        /// <param name="codigoMotorista">Código Motorista</param>
        /// <param name="codPlaca">Placa</param>
        /// <param name="tipoAtendimento">Tipo de Atendimento</param>
        /// <param name="codUsuarioLogado">Código Usuário Logado</param>
        /// <returns>listaNotas</returns>
        public List<E140NFVModel> PesquisarNotasFiscaisSaida(int codigoCliente, int codigoMotorista, string codPlaca, Enums.TipoAtendimento tipoAtendimento, long codUsuarioLogado)
        {
            try
            {
                long qtdeDiasDev, qtdeDiasTroca = 0;
                using (Context contexto = new Context())
                {
                    // Prazo padrão
                    var padrao = contexto.N0204PPU.Where(c => !c.CODUSU.HasValue).FirstOrDefault();

                    qtdeDiasDev = padrao.QTDDEV;
                    qtdeDiasTroca = padrao.QTDTRC;

                    var exclusivo = contexto.N0204PPU.Where(c => c.CODUSU == codUsuarioLogado).FirstOrDefault();
                    if (exclusivo != null)
                    {
                        qtdeDiasDev = exclusivo.QTDDEV;
                        qtdeDiasTroca = exclusivo.QTDTRC;
                    }
                }

                var data = DateTime.Now.ToShortDateString();
                if (tipoAtendimento == Enums.TipoAtendimento.DevolucaoMercadorias)
                    data = DateTime.Now.AddDays(-qtdeDiasDev).ToShortDateString();
                else
                    data = DateTime.Now.AddDays(-qtdeDiasTroca).ToShortDateString();

                string sql = "Select A.CodEmp as CodigoEmpresa,                              " +
                             "       A.CodFil as CodigoFilial,                               " +
                             "       A.CodSnf as SerieNota,                                  " +
                             "       A.NumNfv as NumeroNota,                                 " +
                             "       A.DatEmi as DataEmissao,                                " +
                             "       TO_CHAR(A.VlrLiq, '999999990D99') as ValorLiquido,      " +
                             "       'Fechada'as SituacaoNota,                               " +
                             "       A.TipNfs as TipoNota,                                   " +
                             "       A.CodCli as CodigoCliente,                              " +
                             "       C.NOMCLI as NomeCliente,                                " +
                             "       A.TnsPro as TipoTransacao,                              " +
                             "       B.DesTns as DescricaoTipoTransacao,                     " +
                             "       A.CodTra as CodigoTransportadora,                       " +
                             "       ' '      as IndicativoConferencia,                      " +
                             " round((to_date(current_date) - A.DatEmi)) as DiasFaturamento   " +
                             "  From SAPIENS.E140NFV A, SAPIENS.E001TNS B, SAPIENS.E085CLI C                         " +
                             " Where A.CodEmp = B.CodEmp                                     " +
                             "   And A.TnsPro = B.CodTns                                     " +
                             "   And A.SitNfv = 2                                            " +
                             "   And A.CodTra = 1                                            " +
                             "   And A.DatEmi >= " + "'" + data + "'" +
                             "   And A.CodMtr = " + codigoMotorista +
                             "   And A.CodCli = " + codigoCliente +
                             "   And A.PLAVEI = " + "'" + codPlaca + "'" +
                             "   And A.CODFIL <> 101                                           " +
                             "   and A.CodCli = C.codCli order by A.DatEmi desc";

                
                // Transportadora
                if (codigoMotorista == 0)
                {
                    sql = "Select A.CodEmp as CodigoEmpresa,                              " +
                          "       A.CodFil as CodigoFilial,                               " +
                          "       A.CodSnf as SerieNota,                                  " +
                          "       A.NumNfv as NumeroNota,                                 " +
                          "       A.DatEmi as DataEmissao,                                " +
                          "       TO_CHAR(A.VlrLiq, '999999990D99') as ValorLiquido,      " +
                          "       'Fechada'as SituacaoNota,                               " +
                          "       A.TipNfs as TipoNota,                                   " +
                          "       A.CodCli as CodigoCliente,                              " +
                          "       C.NOMCLI as NomeCliente,                                " +
                          "       A.TnsPro as TipoTransacao,                              " +
                          "       B.DesTns as DescricaoTipoTransacao,                     " +
                          "       A.CodTra as CodigoTransportadora,                       " +
                          "       ' '      as IndicativoConferencia,                      " +
                          " round((to_date(current_date) - A.DatEmi)) as DiasFaturamento  " +
                          "  From SAPIENS.E140NFV A, SAPIENS.E001TNS B, SAPIENS.E085CLI C                         " +
                          " Where A.CodEmp = B.CodEmp                                     " +
                          "   And A.TnsPro = B.CodTns                                     " +
                          "   And A.SitNfv = 2                                            " +
                          "   And A.Tipnfs in(1,10)                                       " +
                          "   And A.CodTra <> 1                                           " +
                          "   And A.CODFIL <> 101                                           " +
                          "   And A.DatEmi >= " + "'" + data + "'" +
                          "   And A.CodCli = " + codigoCliente +
                          "   and A.CodCli = C.codCli order by A.DatEmi desc";
                }

                
                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<E140NFVModel> listaNotas = new List<E140NFVModel>();
                E140NFVModel itemNota = new E140NFVModel();

                while (dr.Read())
                {
                    itemNota = new E140NFVModel();
                    itemNota.CodigoEmpresa = dr.GetInt32(0);
                    itemNota.CodigoFilial = dr.GetInt32(1);
                    itemNota.SerieNota = dr.GetString(2);
                    itemNota.NumeroNota = dr.GetInt64(3);
                    itemNota.DataEmissao = dr.GetDateTime(4).ToShortDateString();
                    itemNota.ValorLiquido = dr.GetString(5);
                    itemNota.SituacaoNota = dr.GetString(6);
                    itemNota.TipoNota = dr.GetInt32(7);
                    itemNota.CodigoCliente = dr.GetInt32(8);
                    itemNota.NomeCliente = dr.GetString(9);
                    itemNota.TipoTransacao = dr.GetString(10);
                    itemNota.DescricaoTipoTransacao = dr.GetString(11);
                    itemNota.CodigoTransportadora = dr.GetInt32(12);
                    itemNota.IndicativoConferencia = dr.GetString(13);
                    itemNota.DiasFaturamento = dr.GetInt32(14);
                    listaNotas.Add(itemNota);
                }

                dr.Close();
                conn.Close();
                return listaNotas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Pesquisa coluna no banco de dados
        /// </summary>
        /// <param name="tabela">Tabela</param>
        /// <param name="coluna">Coluna</param>
        /// <param name="where">Comando de Seleção</param>
        /// <returns>Coluna</returns>
        public string consultaString(String tabela, String coluna, String where)
        {
            String sql = "SELECT " + coluna + " AS COLUNA FROM " + tabela + " WHERE " + where + "";

            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return dr["COLUNA"].ToString();
            }
            return "VAZIO";
        }
        /// <summary>
        /// Pesquisa informações da nota 
        /// </summary>
        /// <param name="codigoNota">Código da Nota Fiscal</param>
        /// <param name="codFil">Código da Filial</param>
        /// <param name="codtra">Código da Transportadora</param>
        /// <param name="tipAte">Tipo de Atendimento</param>
        /// <returns>listaDadosNota</returns>
        public List<E140NFVModel> PesquisarDadosNota(long codigoNota, long codFil, long? codtra, string tipAte)
        {
            try
            {

                string sql = "select a.NumNfv, a.codfil, a.codsnf, a.CodCli, a.codmtr, a.plavei, a.datemi, a.VlrLiq " +
                             "  from sapiens.E140NFV a " +
                          "       left join nwms_producao.n0203ipv ipv " +
                          "      on ipv.numnfv = a.numnfv " +
                          "     and ipv.codemp = a.codemp " +
                          "     and ipv.codfil = a.codfil " +
                          "     and ipv.codsnf = a.codsnf " +
                          "   left join nwms_producao.n0203reg reg " +
                          "      on reg.numreg = ipv.numreg " +
                          "     and reg.sitreg not in (5,7) " +
                          "      and reg.tipate not in (" + tipAte + ") " +
                          "  where a.codfil = " + codFil +
                          "  and a.sitnfv = 2 " +
                          "  and a.tipnfs in (1,10) " +
                          "  and a.codtra = " + codtra +
                          "  and A.NumNfv = " + codigoNota + " "+
                          "   and not exists (select 1" +
                          " from nwms_producao.n0203ipv ipv2," +
                          "      nwms_producao.n0203reg reg2 " +
                          " where ipv2.numreg = reg2.numreg" +
                          "      and ipv2.numnfv = A.numnfv" +
                          "      and ipv2.codfil = a.codfil" +
                          "      and a.codemp = ipv2.codemp" +
                          "      and a.codsnf = ipv2.codsnf" +
                          "      and reg2.sitreg not in (1,5,7)" +
                          "      and reg2.tipate in("+tipAte+"))";

                if (codtra == null)
                {
                    sql = "select a.NumNfv, a.codfil, a.codsnf, a.CodCli, a.codmtr, a.plavei, a.datemi, a.VlrLiq " +
                          "  from sapiens.E140NFV a  " +
                      "       left join nwms_producao.n0203ipv ipv " +
                          "      on ipv.numnfv = a.numnfv " +
                          "     and ipv.codemp = a.codemp " +
                          "     and ipv.codfil = a.codfil " +
                          "     and ipv.codsnf = a.codsnf " +
                          "   left join nwms_producao.n0203reg reg " +
                          "      on reg.numreg = ipv.numreg " +
                          "  where a.codfil = " + codFil +
                          "  and A.NumNfv = " + codigoNota + ""+
                             "   and not exists (select 1" +
                          " from nwms_producao.n0203ipv ipv2," +
                          "      nwms_producao.n0203reg reg2 " +
                          " where ipv2.numreg = reg2.numreg" +
                          "      and ipv2.numnfv = A.numnfv" +
                          "      and ipv2.codfil = a.codfil" +
                          "      and a.codemp = ipv2.codemp" +
                          "      and a.codsnf = ipv2.codsnf" +
                          "      and reg2.sitreg not in(1,5,7)" +
                          "      and reg2.tipate in(" + tipAte + "))";
                }

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<E140NFVModel> listaDadosNota = new List<E140NFVModel>();
                E140NFVModel itemNota = new E140NFVModel();
                
                while (dr.Read())
                {
                    itemNota = new E140NFVModel();
                    itemNota.NumeroNota = dr.GetInt64(0);
                    itemNota.CodigoFilial = dr.GetInt32(1);
                    itemNota.SerieNota = dr.GetString(2);
                    itemNota.CodigoCliente = dr.GetInt64(3);
                    itemNota.CodigoMotorista = dr.GetInt64(4);

                    var placa = dr.GetString(5).Replace(" ", "");
                    if (!string.IsNullOrEmpty(placa))
                    {
                        itemNota.PlacaVeiculo = placa.Substring(0, 3) + "-" + placa.Substring(3, 4);
                    }

                    itemNota.DataEmissao = dr.GetDateTime(6).ToShortDateString();
                    itemNota.ValorLiquido = dr.GetDecimal(7).ToString();
                    listaDadosNota.Add(itemNota);
                }

                dr.Close();
                conn.Close();
                return listaDadosNota;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa parametrização dos dados da nota
        /// </summary>
        /// <param name="codigoNota">Código da Nota</param>
        /// <param name="codFil">Código da Filial</param>
        /// <param name="codtra">Código da Transportadora</param>
        /// <returns></returns>
        public List<E140NFVModel> PesquisarDadosNotaParametrizacao(long codigoNota, long codFil, long? codtra)
        {
            try
            {
                string sql = "select a.NumNfv, a.codfil, a.codsnf, a.CodCli, a.codmtr, a.plavei, a.datemi, a.VlrLiq " +
                             "  from sapiens.E140NFV a                                                                      " +
                             " where a.codfil = " + codFil +
                             "  and a.sitnfv = 2 " +
                             "  and a.tipnfs in (1,10) " +
                             "  and a.codtra = " + codtra +
                             "  and A.NumNfv = " + codigoNota + " order by datemi desc";
                if (codtra == 0)
                {
                    sql = "select a.NumNfv, a.codfil, a.codsnf, a.CodCli, a.codmtr, a.plavei, a.datemi, a.VlrLiq " +
                          "  from sapiens.E140NFV a                                                                      " +
                          " where a.codfil = " + codFil +
                          "  and A.NumNfv = " + codigoNota;
                }
                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                List<E140NFVModel> listaDadosNota = new List<E140NFVModel>();
                E140NFVModel itemNota = new E140NFVModel();
                while (dr.Read())
                {
                    itemNota = new E140NFVModel();
                    itemNota.NumeroNota = dr.GetInt64(0);
                    itemNota.CodigoFilial = dr.GetInt32(1);
                    itemNota.SerieNota = dr.GetString(2);
                    itemNota.CodigoCliente = dr.GetInt64(3);
                    itemNota.CodigoMotorista = dr.GetInt64(4);
                    var placa = dr.GetString(5).Replace(" ", "");
                    if (!string.IsNullOrEmpty(placa))
                    {
                        itemNota.PlacaVeiculo = placa.Substring(0, 3) + "-" + placa.Substring(3, 4);
                    }
                    itemNota.DataEmissao = dr.GetDateTime(6).ToShortDateString();
                    itemNota.ValorLiquido = dr.GetDecimal(7).ToString();
                    listaDadosNota.Add(itemNota);
                }
                dr.Close();
                conn.Close();
                return listaDadosNota;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa Situação da Nota
        /// </summary>
        /// <param name="numeroNota">Número da Nota</param>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <param name="codFilial">Código da Filial</param>
        /// <returns></returns>

        public List<E140NFVModel> PesquisarSituacaoNota(long numeroNota, long codigoUsuario, string codFilial)
        {
            try
            {
                string sql = " SELECT NOTA.PLAVEI," +
                             "          MOTORISTA.NOMMOT," +
                             "         TRUNC( SYSDATE - NOTA.DATEMI, 0) as DIAS," +
                             "          CASE" +
                             "            WHEN (SELECT PPU.QTDDEV" +
                             "                    FROM NWMS_PRODUCAO.N0204PPU PPU" +
                             "                   WHERE 1 = 1" +
                             "                     AND PPU.CODUSU = " + codigoUsuario + ") > 0 THEN" +
                             "             (SELECT PPU.QTDDEV" +
                             "                FROM NWMS_PRODUCAO.N0204PPU PPU" +
                             "               WHERE 1 = 1" +
                             "                 AND PPU.CODUSU = " + codigoUsuario + ")" +
                             "            ELSE" +
                             "             (SELECT PPU.QTDDEV" +
                             "                FROM NWMS_PRODUCAO.N0204PPU PPU" +
                             "               WHERE 1 = 1" +
                             "                 AND PPU.CODUSU IS NULL)" +
                             "          END AS QTDDEV," +
                             "          CASE" +
                             "            WHEN (SELECT PPU.QTDTRC" +
                             "                    FROM NWMS_PRODUCAO.N0204PPU PPU" +
                             "                   WHERE 1 = 1" +
                             "                     AND PPU.CODUSU = " + codigoUsuario + ") > 0 THEN" +
                             "             (SELECT PPU.QTDTRC" +
                             "                FROM NWMS_PRODUCAO.N0204PPU PPU" +
                             "               WHERE 1 = 1" +
                             "                 AND PPU.CODUSU = " + codigoUsuario + ")" +
                             "            ELSE" +
                             "             (SELECT PPU.QTDTRC" +
                             "                FROM NWMS_PRODUCAO.N0204PPU PPU" +
                             "               WHERE 1 = 1" +
                             "                 AND PPU.CODUSU IS NULL)" +
                             "          END AS QTDTRC," +
                             "            TO_CHAR (NOTA.DATEMI, 'DD/MM/YYYY') AS DATEMI" +
                             "     FROM SAPIENS.E140NFV NOTA" +
                             "     LEFT JOIN SAPIENS.E073MOT MOTORISTA" +
                             "       ON (NOTA.CODMTR = MOTORISTA.CODMTR AND NOTA.CODTRA = MOTORISTA.CODTRA)" +
                             "    WHERE NOTA.NUMNFV = " + numeroNota + "" +
                             "      AND NOTA.TIPNFS IN (1, 10)" +
                             "      AND NOTA.CODFIL = " + codFilial + "" +
                             "    ORDER BY NOTA.CODSNF";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                OracleDataReader dr = cmd.ExecuteReader();

                List<E140NFVModel> listaDadosNota = new List<E140NFVModel>();
                E140NFVModel itemNota = new E140NFVModel();

                while (dr.Read())
                {
                    itemNota = new E140NFVModel();
                    var placa = (dr.GetString(0) == " " ? "SEM PLACA" : dr.GetString(0).Replace(" ", ""));
                    var qtdDev = dr.GetInt64(3);
                    var qtdTroca = dr.GetInt64(4);

                    if (!string.IsNullOrEmpty(placa))
                    {
                        if (placa != "SEM PLACA")
                        {
                            itemNota.PlacaVeiculo = placa.Substring(0, 3) + "-" + placa.Substring(3, 4);
                            itemNota.placaControle = true;
                        }
                        else
                        {
                            itemNota.PlacaVeiculo = placa;
                            itemNota.placaControle = false;
                        }
                    }

                    itemNota.nomeMotorista = dr["NOMMOT"].ToString();
                    if (itemNota.nomeMotorista == "")
                    {
                        itemNota.nomeMotorista = "SEM MOTORISTA";
                        itemNota.motoristaControle = false;
                    }
                    else
                    {
                        itemNota.motoristaControle = true;
                    }

                    itemNota.diasDataEmissao = dr.GetInt64(2);

                    if (itemNota.diasDataEmissao > qtdDev)
                    {
                        itemNota.devolucaoMercadoria = "A nota está com " + itemNota.diasDataEmissao + " dias, e excedeu a permissão de " + qtdDev + " dias.";
                        itemNota.devolucaoMercadoriaControle = false;
                    }
                    else
                    {
                        itemNota.devolucaoMercadoria = "A nota está com " + itemNota.diasDataEmissao + " dias, a permissão é de " + qtdDev + " dias.";
                        itemNota.devolucaoMercadoriaControle = true;
                    }
                    if (itemNota.diasDataEmissao > qtdTroca)
                    {
                        itemNota.trocaMercadoria = "A nota está com " + itemNota.diasDataEmissao + " dias e excedeu a permissão de " + qtdTroca + " dias.";
                        itemNota.trocaMercadoriaControle = false;
                    }
                    else
                    {
                        itemNota.trocaMercadoria = "A nota está com " + itemNota.diasDataEmissao + " dias, a permissão é de " + qtdTroca + " dias.";
                        itemNota.trocaMercadoriaControle = true;
                    }
                    itemNota.DataEmissao = dr["DATEMI"].ToString();
                    listaDadosNota.Add(itemNota);
                }

                dr.Close();
                conn.Close();
                return listaDadosNota;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa Tipo da Nota
        /// </summary>
        /// <param name="codigoNota">Número da nota</param>
        /// <param name="tipoNota">Tipo da Nota</param>
        /// <returns>true/false</returns>
        public bool PesquisarTipoNota(long codigoNota, out string tipoNota)
        {
            try
            {
                string sql = "Select B.VenTip, A.NumNfv                      " +
                             "  From SAPIENS.E140NFV A                               " +
                             " Inner Join SAPIENS.E001TNS B On B.CodEmp = A.CodEmp   " +
                             "                     And B.CodTns = A.TnsPro   " +
                             " Where A.NumNfv = " + codigoNota;

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                tipoNota = string.Empty;
                while (dr.Read())
                {
                    tipoNota = dr.GetString(0);
                }

                dr.Close();
                conn.Close();

                if (!string.IsNullOrEmpty(tipoNota))
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Pesquisa tipo de pagamento da nota
        /// </summary>
        /// <param name="codFil">Código da filial</param>
        /// <param name="codigoNota">Código da Nota</param>
        /// <returns></returns>
        public int PesquisarTipoPagamentoNota(long codFil, long codigoNota)
        {
            try
            {
                string sql = "select codfil, codfpg  " +
                            "  from SAPIENS.E140NFV          " +
                            " where codfil = " + codFil +
                            "   and numnfv = " + codigoNota;
                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();
                int tipoPagtoNota = 0;
                while (dr.Read())
                {
                    tipoPagtoNota = dr.GetInt32(1);
                }

                dr.Close();
                conn.Close();

                return tipoPagtoNota;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

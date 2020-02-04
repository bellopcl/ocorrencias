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
    /// Classe de conexão de banco de dados
    /// </summary>
    public class E140IPVDataAccess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();
        /// <summary>
        /// Retorna uma lista de itens de notas de saída
        /// </summary>
        /// <param name="filial">Código da filial</param>
        /// <param name="numeroNota">Número da Nota</param>
        /// <param name="serieNota">Série da Nota</param>
        /// <returns>listaItens</returns>
        public List<E140IPVModel> PesquisarItensNotasFiscaisSaida(int filial, long numeroNota, string serieNota)
        {
            try
            {
                string sql = "Select A.SeqIpv as Sequencia,                                  " +
                                "       A.TnsPro as Transacao,                                  " +
                                "       A.CodPro || ' ' || A.CODDER as CodigoProduto,           " +
                                "       B.DesPro || ' ' || E.DESDER as DescricaoProduto,        " +
                                "       A.QtdFat as QtdeFaturada,                               " +
                                "       0 as QtdeDevolucao,                                     " +
                                "       TO_CHAR(A.PreUni, '999999990D99999') as PrecoUnitario,  " +
                                "       'Selecione...' As MotivoDevolucao,                      " +
                                "       'Selecione...' As OrigemOcorrencia,                     " +
                                "       TO_CHAR(A.PerDsc, '999999990D99') as PercDescUnit,      " +
                                "       TO_CHAR(A.PerIpi, '999999990D99') as PercIpi,           " +
                                "       TO_CHAR(A.VlrIpi, '999999990D99') as ValorIpi,          " +
                                "       TO_CHAR(A.VlrLiq, '999999990D99') as ValorLiquido,      " +
                                "       TO_CHAR(A.vlrics, '999999990D99') as ValorSt,           " +
                                "       TO_CHAR(A.VLRDZF + A.VLRPIT + A.VLRCRT, '999999990D99') as DescontoSuframa,   " +
                                "       B.CodFam as CodigoFamilia,                              " +
                                "       A.VLRFRE                             AS ValorFrete,     " +
                                "       COALESCE((SELECT SUM(QTDDEV)                                 " +
                                "                   FROM NWMS_PRODUCAO.N0203IPV SUBIPV          " +
                                "                    INNER JOIN NWMS_PRODUCAO.N0203REG SUBREG   " +
                                "                       ON SUBIPV.NUMREG = SUBREG.NUMREG        " +
                                " WHERE SUBIPV.NUMNFV = " + numeroNota + " AND SUBREG.SITREG NOT IN (5, 7)" +
                                "                    AND SUBIPV.CODPRO = A.CodPro               " +
                                "                    AND SUBIPV.CODDER = A.CodDer),0) AS QTDDEVSALDO " +
                                "  From SAPIENS.E140IPV A, SAPIENS.E075PRO B, SAPIENS.E075DER E " +
                                " Where A.CodEmp = B.CodEmp                                     " +
                                "   And A.CodPro = B.CodPro                                     " +
                                "   AND A.CodEmp = E.CodEmp                                     " +
                                "   And A.CodPro = E.CodPro                                     " +
                                "   AND A.CODDER = E.CODDER                                     " +
                                "   And A.CodEmp = 1                                            " +
                                "   And A.CodFil = " + filial +
                                "   And A.CodSnf = " + "'" + serieNota + "'" +
                                "   And A.NumNfv = " + numeroNota;

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                
                OracleDataReader dr = cmd.ExecuteReader();

                List<E140IPVModel> listaItens = new List<E140IPVModel>();
                E140IPVModel itemNota = new E140IPVModel();

                while (dr.Read())
                {
                    itemNota = new E140IPVModel();
                    itemNota.Sequencia = dr.GetInt32(0);
                    itemNota.Transacao = dr.GetString(1);
                    itemNota.CodigoProduto = dr.GetString(2);
                    itemNota.DescricaoProduto = dr.GetString(3);
                    itemNota.QtdeFaturada = dr.GetInt32(4);
                    itemNota.QtdeDevolucao = dr.GetInt32(5);
                    itemNota.PrecoUnitario = dr.GetString(6);
                    itemNota.MotivoDevolucao = dr.GetString(7);
                    itemNota.OrigemOcorrencia = dr.GetString(8);
                    itemNota.PercDescUnit = dr.GetString(9);
                    itemNota.PercIpi = dr.GetString(10);
                    itemNota.ValorIpi = dr.GetString(11);
                    itemNota.ValorLiquido = dr.GetString(12);
                    itemNota.ValorSt = dr.GetString(13);
                    itemNota.DescontoSuframa = dr.GetString(14);
                    itemNota.CodigoFamilia = dr.GetString(15);
                    itemNota.ValorFrete = dr.GetDecimal(16);
                    itemNota.SaldoDevolucao = Convert.ToInt32(dr["QTDDEVSALDO"]);
                    string codigoDep = string.Empty;
                    string DescricaoDep = string.Empty;
                    PesquisarCentroCustoItemNoRateio(1, filial, numeroNota, serieNota, itemNota.Sequencia, out codigoDep, out DescricaoDep);

                    if (!string.IsNullOrEmpty(codigoDep))
                    {
                        itemNota.Departamento = codigoDep;
                        itemNota.DescricaoDepartamento = DescricaoDep;
                    }
                    else
                    {
                        PesquisarCentroCustoItemNoProduto(1, itemNota.CodigoProduto, out codigoDep, out DescricaoDep);

                        if (!string.IsNullOrEmpty(codigoDep))
                        {
                            itemNota.Departamento = codigoDep;
                            itemNota.DescricaoDepartamento = DescricaoDep;
                        }
                        else
                        {
                            PesquisarCentroCustoItemNaFamilia(1, itemNota.CodigoFamilia, out codigoDep, out DescricaoDep);

                            itemNota.Departamento = codigoDep;
                            itemNota.DescricaoDepartamento = DescricaoDep;
                        }
                    }

                    listaItens.Add(itemNota);
                }

                dr.Close();
                conn.Close();
                return listaItens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Pesquisa o centro de custo do item no rateio
        /// </summary>
        /// <param name="empresa">Código da Empresa</param>
        /// <param name="filial">Código da Filial</param>
        /// <param name="numeroNota">Número da Nota</param>
        /// <param name="serieNota">Série da Nota</param>
        /// <param name="seqItem">Sequencia do Item</param>
        /// <param name="codigoDep">Código do Deposito</param>
        /// <param name="DescricaoDep">Descrição do Deposito</param>
        public void PesquisarCentroCustoItemNoRateio(int empresa, int filial, long numeroNota, string serieNota, int seqItem, out string codigoDep, out string DescricaoDep)
        {
            try
            {
                string sql = "Select A.CodCcu, B.DesCcu      " +
                             "  From SAPIENS.E140RAT A, SAPIENS.E044CCU B    " +
                             " Where A.CodEmp = B.CodEmp     " +
                             "   And A.CodCcu = B.CodCcu     " +
                             "   And A.CodEmp = " + empresa +
                             "   And A.CodFil = " + filial +
                             "   And A.CodSnf = " + "'" + serieNota + "'" +
                             "   And A.NumNfv = " + numeroNota +
                             "   And A.SeqIpv = " + seqItem;

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                codigoDep = string.Empty;
                DescricaoDep = string.Empty;
                while (dr.Read())
                {
                    codigoDep = dr.GetString(0);
                    DescricaoDep = dr.GetString(1);
                }

                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Pesquisa o centro de custo do item no produto
        /// </summary>
        /// <param name="empresa">Código da Empresa</param>
        /// <param name="codigoProduto">Códgio do Produto</param>
        /// <param name="codigoDep">Códgio do Deposito</param>
        /// <param name="DescricaoDep">Descrição de Deposito</param>
        public void PesquisarCentroCustoItemNoProduto(int empresa, string codigoProduto, out string codigoDep, out string DescricaoDep)
        {
            try
            {
                string sql = "Select A.CodCcu, B.DesCcu      " +
                            "  From SAPIENS.E075RAT A, SAPIENS.E044CCU B    " +
                            " Where A.CodEmp = B.CodEmp     " +
                            "   And A.CodCcu = B.CodCcu     " +
                            "   And A.TipRsc = 'C'          " +
                            "   And A.CriRat = 1            " +
                            "   And A.CodEmp = " + empresa +
                            "   And A.CodPro = " + "'" + codigoProduto + "'";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                codigoDep = string.Empty;
                DescricaoDep = string.Empty;
                while (dr.Read())
                {
                    codigoDep = dr.GetString(0);
                    DescricaoDep = dr.GetString(1);
                }

                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Pesquisa o centro de custo do item na familia
        /// </summary>
        /// <param name="empresa">Código da Empresa</param>
        /// <param name="codigoFamilia">Código da Familia</param>
        /// <param name="codigoDep">Código de Deposito</param>
        /// <param name="DescricaoDep">Descrição do Deposito</param>
        public void PesquisarCentroCustoItemNaFamilia(int empresa, string codigoFamilia, out string codigoDep, out string DescricaoDep)
        {
            try
            {
                string sql = "Select A.CodCcu, B.DesCcu      " +
                            "  From SAPIENS.E012RAT A, SAPIENS.E044CCU B    " +
                            " Where A.CodEmp = B.CodEmp     " +
                            "   And A.CodCcu = B.CodCcu     " +
                            "   And A.TipRsc = 'C'          " +
                            "   And A.CriRat = 1            " +
                            "   And A.CodEmp = " + empresa +
                            "   And A.CodFam = " + "'" + codigoFamilia + "'";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                codigoDep = string.Empty;
                DescricaoDep = string.Empty;
                while (dr.Read())
                {
                    codigoDep = dr.GetString(0);
                    DescricaoDep = dr.GetString(1);
                }

                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Pesquisa analise de ambarque por nota
        /// </summary>
        /// <param name="numeroNota">Número da Nota</param>
        /// <param name="codFilial">Código da Filial</param>
        /// <param name="analiseEmbaque">Analise de Ambarque</param>
        public void PesquisaAnaliseEmbarquePorNota(long numeroNota, long codFilial, out long analiseEmbaque)
        {
            try
            {
                string sql = "select a.codfil, b.numane, b.numpfa " +
                             "  from SAPIENS.e140ipv a, SAPIENS.e135pes b         " +
                             " where a.codemp = b.codemp          " +
                             "   and a.filped = b.filped          " +
                             "   and a.numped = b.numped          " +
                             "   and a.seqipd = b.seqipd          " +
                             "   and a.numnfv = " + numeroNota +
                             "   and a.codfil = " + codFilial +
                             "   group by a.codfil, b.numane, b.numpfa ";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                analiseEmbaque = 99999;
                while (dr.Read())
                {
                    analiseEmbaque = dr.GetInt64(1);
                }

                dr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Pesquisa dados do item na nota
        /// </summary>
        /// <param name="codFil">Código da Filial</param>
        /// <param name="codigoNota">Código da Nota</param>
        /// <returns>listaDadosItemNota</returns>
        public List<E140IPVModel> PesquisarDadosItemNota(long codFil, long codigoNota)
        {
            try
            {
                string sql = " select codemp, codfil, numnfv, seqipv, vlrbic, pericm, vlricm, codstr, vlrics, vlrbsi " +
                             "   from SAPIENS.e140ipv                                                " +
                             "  where codemp = 1                                             " +
                             "    and codfil = " + codFil +
                             "    and numnfv = " + codigoNota;

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                var listaDadosItemNota = new List<E140IPVModel>();
                var itemNota = new E140IPVModel();

                while (dr.Read())
                {
                    itemNota = new E140IPVModel();
                    itemNota.Sequencia = dr.GetInt32(3);
                    itemNota.PerIcms = dr.GetInt64(5).ToString();
                    itemNota.CodSitTributaria = dr.GetString(7);
                    itemNota.ValorSt = dr.GetDecimal(8).ToString();
                    itemNota.ValorBaseSt = dr.GetDecimal(9).ToString();
                    listaDadosItemNota.Add(itemNota);
                }

                dr.Close();
                conn.Close();
                return listaDadosItemNota;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

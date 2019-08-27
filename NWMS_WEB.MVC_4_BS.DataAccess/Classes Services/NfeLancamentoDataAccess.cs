using System;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess.WS_NFE_DEV;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    public class NfeLancamentoDataAccess
    {
        private sapiens_Syncnutriplan_nfe_notafiscalClient NfeClient { get; set; }
        /// <param name="dadosProtocolo"></param>
        /// <param name="mensagemRetorno"></param>
        public bool EmitirLancamentoNfe(N0203REG dadosProtocolo, out string mensagemRetorno)
        {
            try
            {
                var E085CLIDataAccess = new E085CLIDataAccess();
                var E140IPVDataAccess = new E140IPVDataAccess();
                var erro = false;
                int contDadosGerais = 0;
                int contDadosGeraisProdutos = 0;
                mensagemRetorno = string.Empty;

                using (this.NfeClient = new sapiens_Syncnutriplan_nfe_notafiscalClient())
                {
                    this.NfeClient.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
                    var dadosNfe = new notafiscalGravarNFEntradaIn();

                    // Inclusão
                    dadosNfe.tipoProcessamento = "1";
                    // Não Fechar Nota Fiscal
                    dadosNfe.fecNot = "2";
                    dadosNfe.gerPar = "1";
                    // Recalcular valores
                    dadosNfe.tipCal = "0";
                    dadosNfe.prcDoc = "1";
                    
                    var grupoNotas = (from a in dadosProtocolo.N0203IPV
                                      group new { a } by new { a.NUMREG, a.CODEMP, a.CODFIL, a.NUMNFV, a.CODSNF } into grupo
                                      orderby grupo.Key.NUMREG
                                      select new
                                      {
                                          grupo.Key.NUMREG,
                                          grupo.Key.CODEMP,
                                          grupo.Key.CODFIL,
                                          grupo.Key.NUMNFV,
                                          grupo.Key.CODSNF,
                                          VLRLIQ = grupo.Sum(v => decimal.Parse((v.a.QTDDEV * v.a.PREUNI).ToString()) + (v.a.QTDDEV * (v.a.VLRIPI / v.a.QTDFAT)))
                                      }).ToList();

                    dadosNfe.dadosGerais = new notafiscalGravarNFEntradaInDadosGerais[grupoNotas.Count()];

                    foreach (var itemNota in grupoNotas)
                    {
                        dadosNfe.dadosGerais[contDadosGerais] = new notafiscalGravarNFEntradaInDadosGerais();
                        // Empresa
                        dadosNfe.dadosGerais[contDadosGerais].codEmp = itemNota.CODEMP.ToString();
                        // Filial
                        dadosNfe.dadosGerais[contDadosGerais].codFil = itemNota.CODFIL.ToString();
                        // Cliente
                        dadosNfe.dadosGerais[contDadosGerais].codFor = dadosProtocolo.CODCLI.ToString();
                        string teste = dadosProtocolo.NUMREG.ToString();
                        // Série da Nota
                        dadosNfe.dadosGerais[contDadosGerais].codSnf = itemNota.CODSNF.ToString();

                        // Devolução Nota Fiscal NUTRIPLAN
                        dadosNfe.dadosGerais[contDadosGerais].tipNfe = "3";

                        // Série fiscal da nota
                        dadosNfe.dadosGerais[contDadosGerais].codEdc = "55";
                        if (itemNota.CODFIL == 101)
                        {
                            dadosNfe.dadosGerais[contDadosGerais].codEdc = "01";
                        }

                        // Data de Entrada
                        dadosNfe.dadosGerais[contDadosGerais].datEnt = DateTime.Now.ToShortDateString();
                        // Data de Emissão
                        dadosNfe.dadosGerais[contDadosGerais].datEmi = DateTime.Now.ToShortDateString();

                        // Pesquisa estado do Cliente
                        var estadoCli = E085CLIDataAccess.PesquisasClientes(dadosProtocolo.CODCLI).FirstOrDefault().Estado;
                        var transacao = "2201A";
                        dadosNfe.dadosGerais[contDadosGerais].tnsPro = transacao;
                        if (estadoCli == "PR")
                        {
                            transacao = "1201A";
                            // Dentro do Estado ==> '1201A' Fora ==> '2201A' 
                            dadosNfe.dadosGerais[contDadosGerais].tnsPro = transacao;
                        }

                        // Qtde Dev x Preço Uni
                        var valorInf = itemNota.VLRLIQ.ToString("###,###,##0.00").Replace(",", ".");
                        dadosNfe.dadosGerais[contDadosGerais].vlrInf = valorInf;

                        // Série legal da nota
                        dadosNfe.dadosGerais[contDadosGerais].codSel = "1";
                        if (itemNota.CODFIL == 101)
                        {
                            dadosNfe.dadosGerais[contDadosGerais].codSel = "01";
                        }

                        var listaItensProd = dadosProtocolo.N0203IPV.Where(c => c.CODFIL == itemNota.CODFIL && c.NUMNFV == itemNota.NUMNFV).OrderBy(c => c.SEQIPV).ToList();

                        dadosNfe.dadosGerais[contDadosGerais].produtos = new notafiscalGravarNFEntradaInDadosGeraisProdutos[listaItensProd.Count()];

                        // Lista dados item nota
                        var listaDadosItemNota = E140IPVDataAccess.PesquisarDadosItemNota(itemNota.CODFIL, itemNota.NUMNFV).ToList();

                        foreach (var itemProduto in listaItensProd)
                        {
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos] = new notafiscalGravarNFEntradaInDadosGeraisProdutos();
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].tnsPro = transacao;
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].codPro = itemProduto.CODPRO;
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].codDer = itemProduto.CODDER;
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].uniMed = "UN";
                            // Qtde Dev
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].qtdRec = itemProduto.QTDDEV.ToString();
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].preUni = itemProduto.PREUNI.ToString().Replace(",", ".");

                            // Valor Bruto;
                            var valorBruto = decimal.Parse((itemProduto.QTDDEV * itemProduto.PREUNI).ToString());
                            var valorBrutoS = Convert.ToDouble(valorBruto.ToString()).ToString("###,###,##0.00").Replace(",", ".");
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].vlrBru = valorBrutoS;

                            // Porcentagem de IPI
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].perIpi = itemProduto.PERIPI.ToString();
                            var valorIpi = itemProduto.QTDDEV * (itemProduto.VLRIPI / itemProduto.QTDFAT);
                            var valorIpiS = valorIpi.ToString("###,###,##0.00").Replace(",", ".");
                            
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].vecIpi = valorIpiS;
                            // Valor bruto do item do IPI
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].becIpi = valorBrutoS;

                            // Valor Liquido
                            var valorLiquido = (itemProduto.QTDDEV * decimal.Parse(itemProduto.PREUNI.ToString())) + valorIpi;
                            var valorLiquidoS = Convert.ToDouble(valorLiquido.ToString()).ToString("###,###,##0.00").Replace(",", ".");
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].vlrLiq = valorLiquidoS;
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].vlrFin = valorLiquidoS;

                            // Percentagem ICMS
                            var itemlistaDadosItemNota = listaDadosItemNota.Where(c => c.Sequencia == itemProduto.SEQIPV).FirstOrDefault();
                            var perIcms = itemlistaDadosItemNota.PerIcms;
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].perIcm = perIcms;

                            // Valor ICMS                            
                            var valorIcms = valorLiquido * (decimal.Parse(perIcms) / 100);
                            var valorIcmsS = valorIcms.ToString("###,###,##0.00").Replace(",", ".");
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].vlrBic = valorLiquidoS;
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].vlrIcm = valorIcmsS;

                            // Valor ICMS Creditado efetivamente base e valor
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].becIcm = valorLiquidoS;
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].vecIcm = valorIcmsS;
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].empNfv = itemProduto.CODEMP.ToString();
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].filNfv = itemProduto.CODFIL.ToString();
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].numNfv = itemProduto.NUMNFV.ToString();
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].snfNfv = itemProduto.CODSNF;
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].seqIpv = itemProduto.SEQIPV.ToString();

                            // Nota de Transportadora ==> Estoque 13
                            dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].codDep = "13";

                            // Nota da Nutriplan ==> Estoque 02
                            if (dadosProtocolo.CODMOT != 0)
                            {
                                dadosNfe.dadosGerais[contDadosGerais].produtos[contDadosGeraisProdutos].codDep = "02";
                            }

                            contDadosGeraisProdutos = contDadosGeraisProdutos + 1;
                        }

                        contDadosGerais = contDadosGerais + 1;
                        contDadosGeraisProdutos = 0;
                    }

                    var retorno = NfeClient.GravarNFEntrada("nworkflow.web", "!nfr@t1n", 0, dadosNfe);

                    if (retorno.erroExecucao == null)
                    {
                        foreach (var item in retorno.retornosNotasEntrada)
                        {
                            if (item.retorno != "OK")
                            {
                                erro = true;
                                mensagemRetorno = mensagemRetorno + item.retorno + "<br/>";
                            }
                            else
                            {
                                var auxMsg = "Nota gerada ==> Filial: ( " + item.codFil + " ) Nº Nota: " + item.numNfc + "<br/>";
                                mensagemRetorno = mensagemRetorno.Replace(auxMsg, "");
                                mensagemRetorno = mensagemRetorno + auxMsg;
                            }
                        }
                    }
                    else
                    {
                        mensagemRetorno = "Erro execução: " + retorno.erroExecucao;
                        return false;
                    }

                    if (erro)
                    {
                        mensagemRetorno = "Erro execução: " + retorno.erroExecucao + "<br/><br/>Erro Itens:<br/><br/>" + mensagemRetorno;
                        return false;
                    }
                    else
                    {
                        mensagemRetorno = "Nota(s) gerada(s) com sucesso!<br/>" + mensagemRetorno;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

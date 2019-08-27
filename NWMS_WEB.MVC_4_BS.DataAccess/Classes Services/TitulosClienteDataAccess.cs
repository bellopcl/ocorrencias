using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess.WS_TITULOS_CLIENTE;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    public class TitulosClienteDataAccess
    {
        private sapiens_Syncnutriplan_cre_titulosClient TitulosClient { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaRegistros"></param>
        /// <returns></returns>
        public bool AlterarObsTitulo(List<DadosNotasServicoModel> listaRegistros)
        {
            try
            {
                E140NFVDataAccess E140NFVDataAccessObj = new E140NFVDataAccess();
                string tipoNota = string.Empty;

                using (this.TitulosClient = new sapiens_Syncnutriplan_cre_titulosClient())
                {
                    foreach (var item in listaRegistros)
                    {
                        if (E140NFVDataAccessObj.PesquisarTipoNota(item.NumeroNota, out tipoNota))
                        {
                            // Normal
                            if (tipoNota == "N")
                            {
                                titulosAlterarObsTituloIn dadosTitulo = new titulosAlterarObsTituloIn();
                                dadosTitulo.ACodEmp = item.CodEmpresa.ToString();
                                dadosTitulo.AFilNfv = item.CodFilialNota.ToString();
                                dadosTitulo.ACodSnf = item.SerieNota.ToString();
                                dadosTitulo.ANumNfv = item.NumeroNota.ToString();
                                var tamanhoMaximo = 250;
                                var auxObs = "Título com Ocorrência " + item.DescDepartamentoOrigem + " - Protocolo: " + item.NumeroProtocolo.ToString() + " - ";
                                var tamanhoObsAux = auxObs.Length;
                                var tamanhoObsItem = item.Observacao.Length;

                                if (tamanhoObsAux + tamanhoObsItem > tamanhoMaximo)
                                {
                                    var novaObs = auxObs + item.Observacao;
                                    dadosTitulo.AObsTcr = novaObs.Substring(0, tamanhoMaximo);
                                }
                                else
                                {
                                    var novaObs = auxObs + item.Observacao;
                                    dadosTitulo.AObsTcr = novaObs;
                                }

                                var retorno = TitulosClient.AlterarObsTitulo("nworkflow.web", "!nfr@t1n", 0, dadosTitulo);

                                if (retorno.tipoRetorno == "0")
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                // NOTA DE BONIFICAÇÃO -- NÃO POSSUÍ TITULO -- GRAVA A OBSERVAÇÃO NA NOTA;
                                ServicoNotasDataAccess ServicoNotasDataAccess = new ServicoNotasDataAccess();
                                if (!ServicoNotasDataAccess.InserirObservacaoNota(item))
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listaRegistros"></param>
        /// <returns></returns>
        public bool InserirObsMovimento(List<DadosNotasServicoModel> listaRegistros)
        {
            try
            {
                E140NFVDataAccess E140NFVDataAccessObj = new E140NFVDataAccess();
                string tipoNota = string.Empty;

                using (this.TitulosClient = new sapiens_Syncnutriplan_cre_titulosClient())
                {
                    foreach (var item in listaRegistros)
                    {
                        if (E140NFVDataAccessObj.PesquisarTipoNota(item.NumeroNota, out tipoNota))
                        {
                            // Normal
                            if (tipoNota == "N")
                            {
                                titulosInserirObsMovimentoIn dadosTitulo = new titulosInserirObsMovimentoIn();
                                dadosTitulo.ACodEmp = item.CodEmpresa.ToString();
                                dadosTitulo.AFilNfv = item.CodFilialNota.ToString();
                                dadosTitulo.ACodSnf = item.SerieNota.ToString();
                                dadosTitulo.ANumNfv = item.NumeroNota.ToString();
                                dadosTitulo.ATipIns = item.CodDepartamentoSapiens.ToString();
                                var tamanhoMaximo = 250;
                                var auxObs = "Título com Ocorrência " + item.DescDepartamentoOrigem + " - Protocolo: " + item.NumeroProtocolo.ToString() + " - ";
                                var tamanhoObsAux = auxObs.Length;
                                var tamanhoObsItem = item.Observacao.Length;

                                if (tamanhoObsAux + tamanhoObsItem > tamanhoMaximo)
                                {
                                    var novaObs = auxObs + item.Observacao;
                                    dadosTitulo.AObsTit = novaObs.Substring(0, tamanhoMaximo);
                                }
                                else
                                {
                                    dadosTitulo.AObsTit = auxObs + item.Observacao;
                                }

                                var retorno = TitulosClient.InserirObsMovimento("nworkflow.web", "!nfr@t1n", 0, dadosTitulo);

                                if (retorno.tipoRetorno == "0")
                                {
                                    return false;
                                }
                            }
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

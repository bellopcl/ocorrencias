using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess.WS_NOTAS;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    public class ServicoNotasDataAccess
    {

        private sapiens_Syncnutriplan_nfv_notafiscalClient NotasClient { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ItemRegistro"></param>
        /// <returns>IncluirObservacoes</returns>
        public bool InserirObservacaoNota(DadosNotasServicoModel ItemRegistro)
        {
            try
            {
                using (this.NotasClient = new sapiens_Syncnutriplan_nfv_notafiscalClient())
                {
                    var dadosNota = new notafiscalIncluirObservacoesIn();
                    dadosNota.ACodEmp = ItemRegistro.CodEmpresa.ToString();
                    dadosNota.ACodFil = ItemRegistro.CodFilialNota.ToString();
                    dadosNota.ACodSnf = ItemRegistro.SerieNota.ToString();
                    dadosNota.ANumNfv = ItemRegistro.NumeroNota.ToString();
                    var auxObs = "Título com Ocorrência " + ItemRegistro.DescDepartamentoOrigem + " - Protocolo: " + ItemRegistro.NumeroProtocolo.ToString() + " - ";
                    var novaObs = auxObs + ItemRegistro.Observacao;
                    dadosNota.AObsNfv = novaObs;

                    var retorno = NotasClient.IncluirObservacoes("nworkflow.web", "!nfr@t1n", 0, dadosNota);

                    if (retorno.tipoRetorno == "0")
                    {
                        return false;
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

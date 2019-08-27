using System;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess.WS_PEDIDOS;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    public class PedidosViaOcorrenciaDataAccess
    {
        private sapiens_Syncnutriplan_ven_pedidosClient PedidosClient { get; set; }

        public bool EmitirPedido(int ocorrencia, out string mensagemRetorno)
        {
            try
            {
                var erro = false;
                mensagemRetorno = string.Empty;
                using (this.PedidosClient = new sapiens_Syncnutriplan_ven_pedidosClient())
                {
                    this.PedidosClient.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
                    var dadosPedido = new pedidosPedidoViaOcorrenciaIn();

                    DebugEmail email = new DebugEmail();

                    dadosPedido.flowInstanceID = "1";

                    dadosPedido.flowName = "1";

                    dadosPedido.ocorrencia = ocorrencia;

                    dadosPedido.ocorrenciaSpecified = true;

                    var retorno = PedidosClient.PedidoViaOcorrencia("nworkflow.web", "!nfr@t1n", 0, dadosPedido);

                    if (retorno.erroExecucao == null)
                    {

                        email.Email("Webservice", retorno.mensagemRetorno);
                    }

                    mensagemRetorno = retorno.mensagemRetorno;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
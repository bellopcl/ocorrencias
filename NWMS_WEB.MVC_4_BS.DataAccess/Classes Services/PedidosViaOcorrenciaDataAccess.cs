using System;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess.WS_PEDIDOS;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    public class PedidosViaOcorrenciaDataAccess
    {
        private sapiens_Syncnutriplan_ven_pedidosClient PedidosClient { get; set; }

        public bool EmitirPedido(int ocorrencia, long Usuario, out string mensagemRetorno)
        {
            try
            {
                mensagemRetorno = string.Empty;
                using (this.PedidosClient = new sapiens_Syncnutriplan_ven_pedidosClient())
                {

                    N0203REGDataAccess reg = new N0203REGDataAccess();

                    int codTra = reg.pegaTransportadoraOcorrencia(ocorrencia);

                    this.PedidosClient.InnerChannel.OperationTimeout = new TimeSpan(0, 10, 0);
                    var dadosPedido = new pedidosPedidoViaOcorrenciaIn();

                    DebugEmail email = new DebugEmail();

                    dadosPedido.codTra = codTra;

                    dadosPedido.codTraSpecified = true;

                    dadosPedido.flowInstanceID = "1";

                    dadosPedido.flowName = "1";

                    dadosPedido.numReg = ocorrencia;

                    dadosPedido.numRegSpecified = true;

                    var retorno = PedidosClient.PedidoViaOcorrencia("nworkflow.web", "!nfr@t1n", 0, dadosPedido);

                    if (retorno.erroExecucao == null)
                    {
                        email.Email("Webservice Pedido", retorno.mensagemRetorno);
                    }

                    mensagemRetorno = retorno.mensagemRetorno;
                    if (mensagemRetorno == "OK") { 
                        reg.GravarTransacaoIndenizado(ocorrencia, Usuario);
                    }
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
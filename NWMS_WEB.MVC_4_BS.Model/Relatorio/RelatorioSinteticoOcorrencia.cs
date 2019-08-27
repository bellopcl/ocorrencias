using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class RelatorioSinteticoOcorrencia
    {
        public long codigoProtocolo { get; set; }
        public long filial { get; set; }
        public long notaFiscal { get; set; }
        public long codigoCliente { get; set; }
        public string nomeCliente { get; set; }
        public string dataGeracao { get; set; }
        public string dataSituacao { get; set; }
        public string status { get; set; }
        public long quantidadeFaturada { get; set; }
        public long quantidadeDevolvida { get; set; }
        public string valorTotal { get; set; } 
        public string embarque { get; set; }
        public string numeroPedido { get; set; }
        public string numeroNotaFiscalEntrada { get; set; }
        public string numeroNotaFiscalFaturada { get; set; }
        public decimal valorTotalDecimal { get; set; }
        public string valorTotalString { get; set; }
        public string DATAEMISSAO { get; set; }
        public string USUIMPR { get; set; }
    }
}

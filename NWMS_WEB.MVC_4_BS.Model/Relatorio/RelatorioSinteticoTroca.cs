using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class RelatorioSinteticoTroca
    {
        //public long? NumAnaliseEmb { get; set; }
        public string NumAnaliseEmb { get; set; }        
        public string CodPro { get; set; }
        public string CodDer { get; set; }
        public string DescPro { get; set; }
        public long CodMotivoDevolucao { get; set; }
        public string DescMotivoDevolucao { get; set; }
        public long QtdeDevolucao { get; set; }
        public string PlacaCaminhao { get; set; }
        public long CodMotorista { get; set; }
        public string NomeMotorista { get; set; }
        public bool ExisteProtocoloFechado { get; set; }
        public string DataFaturamento { get; set; }
        public string DATAEMISSAO { get; set; }
        public string USUIMPR { get; set; }
        public long codigoProtocolo { get; set; }
        public long filial { get; set; }
        public long notaFiscal { get; set; }
        public long codigoCliente { get; set; }
        public string dataGerecao { get; set; }
        public string status { get; set; }
        public long quantidade { get; set; }
        public long valorTotal { get; set; }

    }
}

using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class ItensSinteticoTroca
    {
        //public long? NumAnaliseEmb { get; set; }
        public string embarque { get; set; }        
        public string CodPro { get; set; }
        public string CodDer { get; set; }
        public string DescPro { get; set; }
        public long CodMotivoDevolucao { get; set; }
        public string DescMotivoDevolucao { get; set; }
        public long QtdeDevolucao { get; set; }
        public string PlacaCaminhao { get; set; }
        public long CodMotorista { get; set; }
        public string NomeMotorista { get; set; }
        public string nomeCliente { get; set; }
        public bool ExisteProtocoloFechado { get; set; }
        public string DataFaturamento { get; set; }
        public string DATAEMISSAO { get; set; }
        public string USUIMPR { get; set; }
        public long codigoProtocolo { get; set; }
        public long filial { get; set; }
        public long notaFiscal { get; set; }
        public long codigoCliente { get; set; }
        public string dataGeracao { get; set; }
        public string status { get; set; }
        public string quantidade { get; set; }
        public string valorTotal { get; set; }

    }
}

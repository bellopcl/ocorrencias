
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class E140NFVModel
    {
        public int CodigoEmpresa { get; set; }
        public int CodigoFilial { get; set; }
        public string SerieNota { get; set; }
        public long NumeroNota { get; set; }
        public string DataEmissao { get; set; }
        public string ValorLiquido { get; set; }
        public string SituacaoNota { get; set; }
        public int TipoNota { get; set; }
        public long CodigoCliente { get; set; }
        public long CodigoMotorista { get; set; }
        public string PlacaVeiculo { get; set; }
        public string NomeCliente { get; set; }
        public string TipoTransacao { get; set; }
        public string DescricaoTipoTransacao { get; set; }
        public int CodigoTransportadora { get; set; }
        public string IndicativoConferencia { get; set; }
        public int DiasFaturamento { get; set; }
        public string nomeMotorista { get; set; }
        public long diasDataEmissao { get; set; }
        public string devolucaoMercadoria { get; set; }
        public string trocaMercadoria { get; set; }
        public bool trocaMercadoriaControle { get; set; }
        public bool devolucaoMercadoriaControle { get; set; }
        public bool placaControle { get; set; }
        public bool motoristaControle { get; set; }
    }
}

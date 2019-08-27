
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class ProtocolosAprovacaoModel
    {
        public long CodigoRegistro { get; set; }
        public long CodTipoAtendimento { get; set; }
        public string DescTipoAtendimento { get; set; }
        public string ValorDevolucao { get; set; }
        public long CodOrigemOcorrencia { get; set; }
        public string DescOrigemOcorrencia { get; set; }
        public long CodCliente { get; set; }
        public string NomeCliente { get; set; }
        public string CnpjCliente { get; set; }
        public string InscricaoEstadualCliente { get; set; }
        public long CodMotorista { get; set; }
        public string NomeMotorista { get; set; }
        public string DataHrGeracao { get; set; }
        public long UsuarioGeracao { get; set; }
        public string NomeUsuarioGeracao { get; set; }
        public long CodSituacaoRegistro { get; set; }
        public string DescSituacaoRegistro { get; set; }
        public string UltimaAlteracao { get; set; }
        public long UsuarioUltimaAlteracao { get; set; }
        public string NomeUsuarioUltimaAlteracao { get; set; }
        public string DataFechamento { get; set; }
        public long? UsuarioFechamento { get; set; }
        public string NomeUsuarioFechamento { get; set; }
        public string Observacao { get; set; }
        public string CodPlaca { get; set; }
        public string DescPlaca { get; set; }
        public string CentrosCustos { get; set; }
        public string numeroNotaFiscal { get; set; }
        public string AREASAP { get; set; }
        public string AREASPAP { get; set; }
        public string valorLiquido { get; set; }
        public string descMotivo { get; set; }
        public decimal valorTotal = 0;
    }
}

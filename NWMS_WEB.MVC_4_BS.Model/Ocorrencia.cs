
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class Ocorrencia
    {
        public long CodigoRegistro { get; set; }
        public string CodTipoAtendimento { get; set; }
        public string DescTipoAtendimento { get; set; }
        public string CodOrigemOcorrencia { get; set; }
        public string DescOrigemOcorrencia { get; set; }
        public string CodCliente { get; set; }
        public string NomeCliente { get; set; }
        public string CodMotorista { get; set; }
        public string NomeMotorista { get; set; }
        public string CodPlaca { get; set; }
        public string DataHrGeracao { get; set; }
        public string DataSituacao { get; set; }
        public string UsuarioGeracao { get; set; }
        public string NomeUsuarioGeracao { get; set; }
        public string CodSituacaoRegistro { get; set; }
        public string DescSituacaoRegistro { get; set; }
        public string UltimaAlteracao { get; set; }
        public string UsuarioUltimaAlteracao { get; set; }
        public string Observacao { get; set; }
        public string Empresa { get; set; }
        public long Filial { get; set; }
        public string SerieNota { get; set; }
        public string NumeroNota { get; set; }
        public long SeqNota { get; set; }
        public string CodPro { get; set; }
        public string CodDer { get; set; }
        public string DesDer { get; set; }
        public string DescPro { get; set; }
        public string CodDepartamento { get; set; }
        public long QtdeFat { get; set; }
        public string PrecoUnitario { get; set; }
        public decimal ValorLiquido { get; set; }
        public string ValorLiquidoS { get; set; }
        public decimal ValorBruto { get; set; }
        public string ValorBrutoS { get; set; }
        public string CodOrigemOcorrenciaItemDev { get; set; }
        public string DescOrigemOcorrenciaItemDev { get; set; }
        public string DescProduto { get; set; }
        public string CodMotivoDevolucao { get; set; }
        public string DescMotivoDevolucao { get; set; }
        public string DescAgrupamento { get; set; }
        public long QtdeDevolucao { get; set; }
        public string TipoTransacao { get; set; }
        public string UsuarioUltimaAlteracaoItemDev { get; set; }
        public string NomeUsuarioUltimaAlteracao { get; set; }
        public string DataUltimaAlteracao { get; set; }
        public string PercDesconto { get; set; }
        public string PercIpi { get; set; }
        public decimal ValorIpi { get; set; }
        public string ValorIpiS { get; set; }
        public decimal ValorSt { get; set; }
        public string ValorStS { get; set; }
        public string AnaliseEmbarque { get; set; }
        public string DATAEMISSAO { get; set; }
        public string USUIMPR { get; set; }
        public decimal TotalValorLiquido { get; set; } 
        public string TotalValorLiquidoD{ get; set; }
        public string TotalValorLiquidoS { get; set; }
        public string MenorqueTrintaDias { get; set; }
        public string MaiorqueTrintaDias { get; set; }
    }
}


namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class OperacaoAprovacaoModel
    {
        public long CodigoUsuario { get; set; }
        public long CodigoOperacao { get; set; }
        public string DescricaoOperacao { get; set; }
        /// <summary>
        /// Operação de Aprovação do Modelo
        /// </summary>
        /// <param name="RelUsuAprovadorOperacaoFat">Relatório de Usuáro Aprovador por operação de faturamento</param>
        /// <returns></returns>
        public static explicit operator OperacaoAprovacaoModel(RelUsuAprovadorOperacaoFat RelUsuAprovadorOperacaoFat)
        {
            OperacaoAprovacaoModel TipoSitItem = new OperacaoAprovacaoModel();
            TipoSitItem.CodigoUsuario = RelUsuAprovadorOperacaoFat.CodigoUsuario;
            TipoSitItem.CodigoOperacao = RelUsuAprovadorOperacaoFat.CodigoOperacao;
            TipoSitItem.DescricaoOperacao = RelUsuAprovadorOperacaoFat.DescricaoOperacao;
            return TipoSitItem;
        }
    }
}

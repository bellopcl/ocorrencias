
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class TipoAtendimentoModel
    {
        /// <summary>
        /// Obtém ou define o id do Tipo de Atendimento
        /// </summary>
        public char Id { get; set; }

        /// <summary>
        /// Obtém ou define a descrição do Tipo de Atendimento
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Lista de opções de tipos de atendimento
        /// </summary>
        /// <param name="tipoAtendimento">tipo de atendimento</param>
        /// <returns>Lista de opções de tipos de atendimento</returns>
        public static explicit operator TipoAtendimentoModel(Enums.TipoAtendimento tipoAtendimento)
        {
            TipoAtendimentoModel tipoAtendimentoModelItem = new TipoAtendimentoModel();
            tipoAtendimentoModelItem.Id = (char)tipoAtendimento;
            tipoAtendimentoModelItem.Descricao = Attributes.KeyValueAttribute.GetFirst("Descricao", tipoAtendimento).GetValue<string>();
            return tipoAtendimentoModelItem;
        }
    }
}

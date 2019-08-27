
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class MotivoDevolucaoModel
    {
        /// <summary>
        /// Obtém ou define o Id da Opção do Motivo de Devolução
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Obtém ou define a descrição do Motivo de Devolução
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Lista de Motivos de Devolução
        /// </summary>
        /// <param name="motivoDevolucao">código do motivo de devolução</param>
        /// <returns>Lista de Motivos de Devolução</returns>
        public static explicit operator MotivoDevolucaoModel(Enums.MotivoDevolucao motivoDevolucao)
        {
            MotivoDevolucaoModel motivoDevolucaoModelItem = new MotivoDevolucaoModel();
            motivoDevolucaoModelItem.Id = ((int)motivoDevolucao).ToString();
            motivoDevolucaoModelItem.Descricao = Attributes.KeyValueAttribute.GetFirst("Descricao", motivoDevolucao).GetValue<string>();
            return motivoDevolucaoModelItem;
        }
    }
}

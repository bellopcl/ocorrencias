
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class OrigemOcorrenciaModel
    {
        /// <summary>
        /// Obtém ou define o Id da Origem da Ocorrência
        /// </summary>
        public char Id { get; set; }

        /// <summary>
        /// Obtém ou define a descrição da Origem da Ocorrência
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Lista de Origem da Ocorrências
        /// </summary>
        /// <param name="motivoDevolucao">código da Origem da Ocorrência</param>
        /// <returns>Lista de Origem de Ocorrências</returns>
        public static explicit operator OrigemOcorrenciaModel(Enums.OrigemOcorrencia origemOcorrencia)
        {
            OrigemOcorrenciaModel origemOcorrenciaModelItem = new OrigemOcorrenciaModel();
            origemOcorrenciaModelItem.Id = (char)origemOcorrencia;
            origemOcorrenciaModelItem.Descricao = Attributes.KeyValueAttribute.GetFirst("Descricao", origemOcorrencia).GetValue<string>();
            return origemOcorrenciaModelItem;
        }
    }
}

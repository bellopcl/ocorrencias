
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class OperacaoModel
    {
        /// <summary>
        /// Obtém ou define o Id da Opção de Operação
        /// </summary>
        public char Id { get; set; }

        /// <summary>
        /// Obtém ou define a descrição da Opção de Operação
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Lista de opções de operação
        /// </summary>
        /// <param name="operacao">operação</param>
        /// <returns>lista de opções</returns>
        public static explicit operator OperacaoModel(Enums.Operacao operacao)
        {
            Model.OperacaoModel operacaoItem = new Model.OperacaoModel();
            operacaoItem.Id = (char)operacao;
            operacaoItem.Descricao = Attributes.KeyValueAttribute.GetFirst("Descricao", operacao).GetValue<string>();
            return operacaoItem;
        }
    }
}

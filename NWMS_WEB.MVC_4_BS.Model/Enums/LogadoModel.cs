
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class LogadoModel
    {
        /// <summary>
        /// Obtém ou define o Id da Opção de Logado
        /// </summary>
        public char Id { get; set; }

        /// <summary>
        /// Obtém ou define a descrição da Opção de Logado
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Lista de opções de logado
        /// </summary>
        /// <param name="logado">logado</param>
        /// <returns>lista de opções</returns>
        public static explicit operator LogadoModel(Enums.Logado logado)
        {
            Model.LogadoModel logadoItem = new Model.LogadoModel();
            logadoItem.Id = (char)logado;
            logadoItem.Descricao = Attributes.KeyValueAttribute.GetFirst("Descricao", logado).GetValue<string>();
            return logadoItem;
        }
    }
}

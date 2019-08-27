
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class TipoPesquisaRegModel
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public static explicit operator TipoPesquisaRegModel(Enums.TipoPesquisaRegistroOcorrencia tipoPesquisa)
        {
            TipoPesquisaRegModel TipoPesquisaRegModelItem = new TipoPesquisaRegModel();
            TipoPesquisaRegModelItem.Id = (int)tipoPesquisa;
            TipoPesquisaRegModelItem.Descricao = Attributes.KeyValueAttribute.GetFirst("Descricao", tipoPesquisa).GetValue<string>();
            return TipoPesquisaRegModelItem;
        }
    }
}

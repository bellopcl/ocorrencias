
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class SitRegOcorModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public static explicit operator SitRegOcorModel(Enums.SituacaoRegistroOcorrencia tipoSit)
        {
            SitRegOcorModel TipoSitItem = new SitRegOcorModel();
            TipoSitItem.Id = (int)tipoSit;
            TipoSitItem.Descricao = Attributes.KeyValueAttribute.GetFirst("Descricao", tipoSit).GetValue<string>();
            return TipoSitItem;
        }
    }
}

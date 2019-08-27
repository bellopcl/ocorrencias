
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class RelUsuAprovadorOperacaoFat
    {
        public long CodigoUsuario { get; set; }
        public long CodigoOperacao { get; set; }
        public string DescricaoOperacao { get; set; }
        public bool Marcar { get; set; }
    }
}

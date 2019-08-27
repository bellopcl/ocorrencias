
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class RelTipoAtendUsuario
    {
        public long CodigoUsuario { get; set; }
        public long CodigoTipoAtendimento { get; set; }
        public string DescricaoTipoAtendimento { get; set; }
        public bool Marcar { get; set; }
    }
}

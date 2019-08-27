
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_CONCORRENCIA
    {
        public long CODCONC { get; set; }
        public long CODUSU { get; set; }
        public string CHAVEREG { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
    }
}

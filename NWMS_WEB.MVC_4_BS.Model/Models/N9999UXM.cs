
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N9999UXM
    {
        public long CODUSU { get; set; }
        public long CODSIS { get; set; }
        public long CODMEN { get; set; }
        public string PERMEN { get; set; }
        public string INSMEN { get; set; }
        public string ALTMEN { get; set; }
        public string EXCMEN { get; set; }
        public virtual N9999MEN N9999MEN { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
    }
}

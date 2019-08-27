
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_USRXREL
    {
        public long CODUSU { get; set; }
        public byte[] CTRLACESSOREL { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
    }
}

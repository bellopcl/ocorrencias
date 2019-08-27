
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_USRFORMXTABLE
    {
        public long CODFORM { get; set; }
        public long CODTABLE { get; set; }
        public string TIPOFORM { get; set; }
        public virtual SYS_USRFORM SYS_USRFORM { get; set; }
        public virtual SYS_USRTABLE SYS_USRTABLE { get; set; }
    }
}

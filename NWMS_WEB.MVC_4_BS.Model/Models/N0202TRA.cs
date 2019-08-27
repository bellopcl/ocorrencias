
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0202TRA
    {
        public long CODEMP { get; set; }
        public long NUMREQ { get; set; }
        public long SEQTRA { get; set; }
        public string DESTRA { get; set; }
        public long USUGER { get; set; }
        public System.DateTime DATGER { get; set; }
        public string OBSTRA { get; set; }
        public virtual N0202REQ N0202REQ { get; set; }
    }
}

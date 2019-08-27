
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0008CNV
    {
        public string UNIMED { get; set; }
        public string UNIME2 { get; set; }
        public string TIPCNV { get; set; }
        public long VLRCNV { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public virtual N0007UNI N0007UNI { get; set; }
        public virtual N0007UNI N0007UNI1 { get; set; }
    }
}

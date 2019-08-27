
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0019CPR
    {
        public long CODEMP { get; set; }
        public string CODPRO { get; set; }
        public string CODCTE { get; set; }
        public long SEQCCP { get; set; }
        public string DESLIV { get; set; }
        public string OBSLIV { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public virtual N0006PROModel N0006PRO { get; set; }
        public virtual N0019CTE N0019CTE { get; set; }
    }
}


namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0010UCT
    {
        public long CODEMP { get; set; }
        public string CODBAR { get; set; }
        public string CODPRO { get; set; }
        public string CODDER { get; set; }
        public string UNIMED { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual N0006DER N0006DER { get; set; }
        public virtual N0006PROModel N0006PRO { get; set; }
    }
}

using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0019CTE
    {
        public N0019CTE()
        {
            this.N0019CPR = new List<N0019CPR>();
        }

        public long CODEMP { get; set; }
        public string CODCTE { get; set; }
        public string DESCTE { get; set; }
        public string TEMCCP { get; set; }
        public string TIPCCP { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual ICollection<N0019CPR> N0019CPR { get; set; }
    }
}

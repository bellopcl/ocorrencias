using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0018DEP
    {
        public N0018DEP()
        {
            this.N0018LPD = new List<N0018LPD>();
        }

        public long CODEMP { get; set; }
        public string CODDEP { get; set; }
        public string DESDEP { get; set; }
        public string ABRDEP { get; set; }
        public string TIPDEP { get; set; }
        public long CODFIL { get; set; }
        public string SITDEP { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual ICollection<N0018LPD> N0018LPD { get; set; }
    }
}

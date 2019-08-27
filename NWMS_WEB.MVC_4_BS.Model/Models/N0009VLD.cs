using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0009VLD
    {
        public N0009VLD()
        {
            this.N0009ITP = new List<N0009ITP>();
        }

        public long CODEMP { get; set; }
        public string CODTPR { get; set; }
        public System.DateTime DATINI { get; set; }
        public System.DateTime DATFIM { get; set; }
        public string SITVLD { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public virtual ICollection<N0009ITP> N0009ITP { get; set; }
        public virtual N0009TAB N0009TAB { get; set; }
    }
}

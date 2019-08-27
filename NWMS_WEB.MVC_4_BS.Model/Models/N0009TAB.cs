using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0009TAB
    {
        public N0009TAB()
        {
            this.N0009VLD = new List<N0009VLD>();
        }

        public long CODEMP { get; set; }
        public string CODTPR { get; set; }
        public string DESTPR { get; set; }
        public string ABRTPR { get; set; }
        public string SITTPR { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual ICollection<N0009VLD> N0009VLD { get; set; }
    }
}

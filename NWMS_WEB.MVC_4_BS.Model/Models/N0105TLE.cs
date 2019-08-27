using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0105TLE
    {
        public N0105TLE()
        {
            this.N0107DEN = new List<N0107DEN>();
        }

        public long CODEMP { get; set; }
        public long CODTLE { get; set; }
        public string DESTLE { get; set; }
        public string ENDFLE { get; set; }
        public string SITTPE { get; set; }
        public Nullable<short> USUCAD { get; set; }
        public Nullable<System.DateTime> DATCAD { get; set; }
        public Nullable<long> USUALT { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual ICollection<N0107DEN> N0107DEN { get; set; }
    }
}

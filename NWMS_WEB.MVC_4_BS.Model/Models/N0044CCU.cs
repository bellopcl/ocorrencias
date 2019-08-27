using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0044CCU
    {
        public N0044CCU()
        {
            this.N0202REQ = new List<N0202REQ>();
        }

        public long CODEMP { get; set; }
        public string CODCCU { get; set; }
        public string DESCCU { get; set; }
        public string ABRCCU { get; set; }
        public string MSKCCU { get; set; }
        public string CLACCU { get; set; }
        public Nullable<long> NIVCCU { get; set; }
        public Nullable<long> USUCAD { get; set; }
        public Nullable<System.DateTime> DATCAD { get; set; }
        public Nullable<long> USUALT { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual ICollection<N0202REQ> N0202REQ { get; set; }
    }
}

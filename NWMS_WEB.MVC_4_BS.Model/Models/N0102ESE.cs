using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0102ESE
    {
        public N0102ESE()
        {
            this.N0106END = new List<N0106END>();
        }

        public long CODEMP { get; set; }
        public long CODESE { get; set; }
        public string DESESE { get; set; }
        public byte[] IMGESE { get; set; }
        public string SITESE { get; set; }
        public Nullable<long> USUCAD { get; set; }
        public Nullable<System.DateTime> DATCAD { get; set; }
        public Nullable<long> USUALT { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual ICollection<N0106END> N0106END { get; set; }
    }
}

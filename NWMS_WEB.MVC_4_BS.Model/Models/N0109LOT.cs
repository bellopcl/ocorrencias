using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0109LOT
    {
        public N0109LOT()
        {
            this.N0109MOV = new List<N0109MOV>();
        }

        public long CODEMP { get; set; }
        public long CODUAR { get; set; }
        public long SEQUAR { get; set; }
        public long SEQLOT { get; set; }
        public Nullable<long> NUMSER { get; set; }
        public Nullable<long> NUMLOT { get; set; }
        public Nullable<System.DateTime> DATVLD { get; set; }
        public virtual N0109IUA N0109IUA { get; set; }
        public virtual ICollection<N0109MOV> N0109MOV { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0012TRA
    {
        public N0012TRA()
        {
            this.N0012MOT = new List<N0012MOT>();
            this.N0012VEI = new List<N0012VEI>();
            this.N0110DOC = new List<N0110DOC>();
        }

        public long CODTRA { get; set; }
        public string NOMTRA { get; set; }
        public string APETRA { get; set; }
        public string TIPTRA { get; set; }
        public string INSEST { get; set; }
        public string INSMUN { get; set; }
        public Nullable<long> CGCCPF { get; set; }
        public string SITTRA { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public virtual ICollection<N0012MOT> N0012MOT { get; set; }
        public virtual ICollection<N0012VEI> N0012VEI { get; set; }
        public virtual ICollection<N0110DOC> N0110DOC { get; set; }
    }
}

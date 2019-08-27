using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0012VEI
    {
        public N0012VEI()
        {
            this.N0012LVM = new List<N0012LVM>();
        }

        public long CODTRA { get; set; }
        public string PLAVEI { get; set; }
        public Nullable<long> ANOVEI { get; set; }
        public Nullable<long> PESMAX { get; set; }
        public Nullable<long> VOLMAX { get; set; }
        public string SITVEI { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public virtual ICollection<N0012LVM> N0012LVM { get; set; }
        public virtual N0012TRA N0012TRA { get; set; }
    }
}

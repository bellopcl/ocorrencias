using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N9999MEN
    {
        public N9999MEN()
        {
            this.N9999USM = new List<N9999USM>();
            this.N9999UXM = new List<N9999UXM>();

        }

        public long CODMEN { get; set; }
        public string DESMEN { get; set; }
        public long CODSIS { get; set; }
        public string ICOMEN { get; set; }
        public Nullable<long> MENPAI { get; set; }
        public Nullable<long> ORDMEN { get; set; }
        public string ENDPAG { get; set; }
        public virtual N9999SIS N9999SIS { get; set; }
        public virtual ICollection<N9999USM> N9999USM { get; set; }
        public virtual ICollection<N9999UXM> N9999UXM { get; set; }
    }
}

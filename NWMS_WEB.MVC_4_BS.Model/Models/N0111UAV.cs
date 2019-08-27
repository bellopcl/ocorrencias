using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0111UAV
    {
        public N0111UAV()
        {
            this.N0111ITV = new List<N0111ITV>();
        }

        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public long CODARM { get; set; }
        public long CODINV { get; set; }
        public long SEQINV { get; set; }
        public Nullable<long> CODLOC { get; set; }
        public Nullable<long> CODEND { get; set; }
        public string ETQEND { get; set; }
        public Nullable<long> CODUAR { get; set; }
        public Nullable<long> CODTUA { get; set; }
        public Nullable<System.DateTime> DATULT { get; set; }
        public Nullable<long> TUANOV { get; set; }
        public string SITREG { get; set; }
        public virtual N0103TUA N0103TUA { get; set; }
        public virtual N0111INVModel N0111INV { get; set; }
        public virtual ICollection<N0111ITV> N0111ITV { get; set; }
    }
}

using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0203APR
    {
        public long NUMREG { get; set; }
        public long SEQAPR { get; set; }
        public string DESAPR { get; set; }
        public string VIAAPR { get; set; }
        public string SITAPR { get; set; }
        public System.DateTime DATGER { get; set; }
        public long USUGER { get; set; }
        public Nullable<System.DateTime> DATAPR { get; set; }
        public Nullable<long> USUAPR { get; set; }
        public string NIVAPR { get; set; }
        public Nullable<long> USUCAN { get; set; }
        public Nullable<System.DateTime> DATCAN { get; set; }
        public virtual N0203REG N0203REG { get; set; }
    }
}

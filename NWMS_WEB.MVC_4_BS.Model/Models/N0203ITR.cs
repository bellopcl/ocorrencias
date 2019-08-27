using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0203ITR
    {
        public long NUMREG { get; set; }
        public string CODTPR { get; set; }
        public string CODPRO { get; set; }
        public string CODDER { get; set; }
        public Nullable<long> QTDPRO { get; set; }
        public Nullable<long> VLRUNI { get; set; }
        public Nullable<long> VLRLIQ { get; set; }
        public System.DateTime DATGER { get; set; }
        public long USUGER { get; set; }
        public long SEQITR { get; set; }
        public Nullable<long> PEROFE { get; set; }
        public Nullable<long> PERIPI { get; set; }
        public Nullable<long> VLRIPI { get; set; }
        public virtual N0203REG N0203REG { get; set; }
    }
}

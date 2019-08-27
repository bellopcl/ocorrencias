using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0203TRA
    {
        public long NUMREG { get; set; }
        public long SEQTRA { get; set; }
        public string DESTRA { get; set; }
        public long USUTRA { get; set; }
        public DateTime DATTRA { get; set; }
        public string OBSTRA { get; set; }
        public long? CODORI { get; set; }
        public virtual N0203REG N0203REG { get; set; }
    }
}

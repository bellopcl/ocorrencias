using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0109HUA
    {
        public long CODEMP { get; set; }
        public long CODUAR { get; set; }
        public long SEQHUA { get; set; }
        public Nullable<long> FILORI { get; set; }
        public Nullable<long> ARMORI { get; set; }
        public Nullable<long> LOCORI { get; set; }
        public Nullable<long> ENDORI { get; set; }
        public long FILDES { get; set; }
        public long ARMDES { get; set; }
        public long LOCDES { get; set; }
        public long ENDDES { get; set; }
        public string OBSMOV { get; set; }
        public string CODTNS { get; set; }
        public System.DateTime DATHUA { get; set; }
        public long USUHUA { get; set; }
        public virtual N0005TNS N0005TNS { get; set; }
        public virtual N0106END N0106END { get; set; }
        public virtual N0106END N0106END1 { get; set; }
        public virtual N0109UAR N0109UAR { get; set; }
    }
}

using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0107DEN
    {
        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public long CODARM { get; set; }
        public long CODLOC { get; set; }
        public long CODEND { get; set; }
        public string CODPRO { get; set; }
        public string CODDER { get; set; }
        public string UNIMED { get; set; }
        public long CODTLE { get; set; }
        public string SITDEN { get; set; }
        public Nullable<System.DateTime> DATCAD { get; set; }
        public Nullable<long> USUCAD { get; set; }
        public Nullable<long> QTDPRO { get; set; }
        public virtual N0006DER N0006DER { get; set; }
        public virtual N0007UNI N0007UNI { get; set; }
        public virtual N0105TLE N0105TLE { get; set; }
        public virtual N0106END N0106END { get; set; }
    }
}

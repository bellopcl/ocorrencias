using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0009ITP
    {
        public long CODEMP { get; set; }
        public string CODTPR { get; set; }
        public System.DateTime DATINI { get; set; }
        public string CODPRO { get; set; }
        public string CODDER { get; set; }
        public long QTDMAX { get; set; }
        public long PREBAS { get; set; }
        public Nullable<long> TOLMAI { get; set; }
        public Nullable<long> TOLMEN { get; set; }
        public string SITITP { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public virtual N0006DER N0006DER { get; set; }
        public virtual N0009VLD N0009VLD { get; set; }
    }
}

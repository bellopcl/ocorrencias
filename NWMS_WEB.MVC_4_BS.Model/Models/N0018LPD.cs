using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0018LPD
    {
        public long CODEMP { get; set; }
        public string CODPRO { get; set; }
        public string CODDER { get; set; }
        public string CODDEP { get; set; }
        public Nullable<System.DateTime> DATINI { get; set; }
        public Nullable<long> SALINI { get; set; }
        public string UNIMED { get; set; }
        public string ESTNEG { get; set; }
        public Nullable<long> QTDEST { get; set; }
        public Nullable<long> QTDBLO { get; set; }
        public Nullable<long> ESTREP { get; set; }
        public Nullable<long> ESTMIN { get; set; }
        public Nullable<long> ESTMAX { get; set; }
        public Nullable<long> ESTMID { get; set; }
        public Nullable<long> ESTMAD { get; set; }
        public string SITLPD { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public virtual N0006DER N0006DER { get; set; }
        public virtual N0007UNI N0007UNI { get; set; }
        public virtual N0018DEP N0018DEP { get; set; }
    }
}

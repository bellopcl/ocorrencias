using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0112TAR
    {
        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public Nullable<long> ARMORI { get; set; }
        public Nullable<long> LOCORI { get; set; }
        public Nullable<long> ENDORI { get; set; }
        public string ETQORI { get; set; }
        public Nullable<long> ARMDES { get; set; }
        public Nullable<long> LOCDES { get; set; }
        public Nullable<long> ENDDES { get; set; }
        public string ETQDES { get; set; }
        public long NUMROM { get; set; }
        public long CODTPT { get; set; }
        public Nullable<long> TEMEST { get; set; }
        public string CODROE { get; set; }
        public Nullable<long> SEQROE { get; set; }
        public string CODPRO { get; set; }
        public string CODDER { get; set; }
        public string UNIMED { get; set; }
        public Nullable<long> CODUAR { get; set; }
        public Nullable<long> SEQIUA { get; set; }
        public Nullable<long> CODTUA { get; set; }
        public Nullable<long> QTDMOV { get; set; }
        public Nullable<long> PRITAR { get; set; }
        public string SITTAR { get; set; }
        public System.DateTime DATGER { get; set; }
        public long USUGER { get; set; }
        public Nullable<System.DateTime> DATACE { get; set; }
        public Nullable<long> USUACE { get; set; }
        public Nullable<System.DateTime> DATBAI { get; set; }
        public Nullable<long> USUBAI { get; set; }
        public string OBSTAR { get; set; }
        public long CODTAR { get; set; }
        public string REPTAR { get; set; }
        public Nullable<long> CODCLI { get; set; }
        public string INTAID { get; set; }
        public Nullable<long> USUAID { get; set; }
        public Nullable<System.DateTime> DATAID { get; set; }
        public Nullable<long> CODINV { get; set; }
        public Nullable<long> SEQINV { get; set; }
        public Nullable<long> SEQLOT { get; set; }
        public Nullable<long> CONINV { get; set; }
        public Nullable<long> NUMAID { get; set; }
        public virtual N0002FIL N0002FIL { get; set; }
        public virtual N0007UNI N0007UNI { get; set; }
        public virtual N0103TUA N0103TUA { get; set; }
        public virtual N0109UAR N0109UAR { get; set; }
        public virtual N0112TPT N0112TPT { get; set; }
    }
}

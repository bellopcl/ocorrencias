using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0106END
    {
        public N0106END()
        {
            this.N0107DEN = new List<N0107DEN>();
            this.N0109HUA = new List<N0109HUA>();
            this.N0109HUA1 = new List<N0109HUA>();
            this.N0109UAR = new List<N0109UAR>();
        }

        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public long CODARM { get; set; }
        public long CODLOC { get; set; }
        public long CODEND { get; set; }
        public string RUAEND { get; set; }
        public string ETQEND { get; set; }
        public long CODTPE { get; set; }
        public long CODESE { get; set; }
        public string SITEND { get; set; }
        public string SITSAI { get; set; }
        public string SITENT { get; set; }
        public string SITMOV { get; set; }
        public string ENDVIR { get; set; }
        public Nullable<long> PESMAX { get; set; }
        public Nullable<long> ALTEND { get; set; }
        public Nullable<long> COMEND { get; set; }
        public Nullable<long> LAREND { get; set; }
        public Nullable<long> CUBMAX { get; set; }
        public Nullable<long> PRILOC { get; set; }
        public long MAXUAR { get; set; }
        public Nullable<System.DateTime> DATULT { get; set; }
        public string MOVEST { get; set; }
        public Nullable<long> USUCAD { get; set; }
        public Nullable<System.DateTime> DATCAD { get; set; }
        public Nullable<long> USUALT { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public string RECANE { get; set; }
        public string COLEND { get; set; }
        public string DIVEND { get; set; }
        public string PROEND { get; set; }
        public string ALCEND { get; set; }
        public string PARIMP { get; set; }
        public virtual N0101LOC N0101LOC { get; set; }
        public virtual N0102ESE N0102ESE { get; set; }
        public virtual N0104TPE N0104TPE { get; set; }
        public virtual ICollection<N0107DEN> N0107DEN { get; set; }
        public virtual ICollection<N0109HUA> N0109HUA { get; set; }
        public virtual ICollection<N0109HUA> N0109HUA1 { get; set; }
        public virtual ICollection<N0109UAR> N0109UAR { get; set; }
    }
}

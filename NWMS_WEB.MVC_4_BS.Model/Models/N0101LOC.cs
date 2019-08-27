using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0101LOC
    {
        public N0101LOC()
        {
            this.N0106END = new List<N0106END>();
        }

        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public long CODARM { get; set; }
        public long CODLOC { get; set; }
        public string DESLOC { get; set; }
        public string MSKEND { get; set; }
        public string INDCRD { get; set; }
        public string TIPLOC { get; set; }
        public string SITLOC { get; set; }
        public string INDDOC { get; set; }
        public Nullable<long> USUCAD { get; set; }
        public Nullable<System.DateTime> DATCAD { get; set; }
        public Nullable<long> USUALT { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public virtual N0003ARM N0003ARM { get; set; }
        public virtual ICollection<N0106END> N0106END { get; set; }
    }
}

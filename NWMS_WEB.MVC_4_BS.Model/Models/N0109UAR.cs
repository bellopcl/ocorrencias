using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0109UAR
    {
        public N0109UAR()
        {
            this.N0109HUA = new List<N0109HUA>();
            this.N0109IUA = new List<N0109IUA>();
            this.N0112TAR = new List<N0112TAR>();
        }

        public long CODEMP { get; set; }
        public long CODUAR { get; set; }
        public long CODTUA { get; set; }
        public long FILUAR { get; set; }
        public long ARMUAR { get; set; }
        public long LOCUAR { get; set; }
        public long ENDUAR { get; set; }
        public string SITUAR { get; set; }
        public string SITENT { get; set; }
        public string SITSAI { get; set; }
        public string UARGEN { get; set; }
        public System.DateTime DATGER { get; set; }
        public long USUGER { get; set; }
        public Nullable<System.DateTime> DATULT { get; set; }
        public Nullable<long> USUULT { get; set; }
        public Nullable<long> CODCLI { get; set; }
        public virtual N0103TUA N0103TUA { get; set; }
        public virtual N0106END N0106END { get; set; }
        public virtual ICollection<N0109HUA> N0109HUA { get; set; }
        public virtual ICollection<N0109IUA> N0109IUA { get; set; }
        public virtual ICollection<N0112TAR> N0112TAR { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0002FIL
    {
        public N0002FIL()
        {
            this.N0003ARM = new List<N0003ARM>();
            this.N0110ROM = new List<N0110ROM>();
            this.N0112TAR = new List<N0112TAR>();
        }

        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public string NOMFIL { get; set; }
        public string SITFIL { get; set; }
        public Nullable<long> USUCAD { get; set; }
        public Nullable<System.DateTime> DATCAD { get; set; }
        public Nullable<long> USUALT { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public Nullable<long> DIADEV { get; set; }
        public string ABRUSU { get; set; }
        public string INDOBS { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual ICollection<N0003ARM> N0003ARM { get; set; }
        public virtual ICollection<N0110ROM> N0110ROM { get; set; }
        public virtual ICollection<N0112TAR> N0112TAR { get; set; }
    }
}

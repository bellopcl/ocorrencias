using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0110ROM
    {
        public N0110ROM()
        {
            this.N0110DOC = new List<N0110DOC>();
        }

        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public long NUMROM { get; set; }
        public string SITREG { get; set; }
        public Nullable<long> FILREC { get; set; }
        public Nullable<long> ARMREC { get; set; }
        public Nullable<long> LOCREC { get; set; }
        public Nullable<long> ENDREC { get; set; }
        public Nullable<long> PRIROM { get; set; }
        public Nullable<System.DateTime> DATINI { get; set; }
        public Nullable<System.DateTime> DATFIM { get; set; }
        public Nullable<long> FILANE { get; set; }
        public Nullable<long> NUMANE { get; set; }
        public Nullable<long> FILREL { get; set; }
        public Nullable<long> NUMREL { get; set; }
        public string TIPROM { get; set; }
        public long USUIMP { get; set; }
        public System.DateTime DATIMP { get; set; }
        public string SITROM { get; set; }
        public string TIPAGR { get; set; }
        public string INDPRO { get; set; }
        public string INDENT { get; set; }
        public string ROMAGR { get; set; }
        public string INDBLK { get; set; }
        public string ESTROM { get; set; }
        public string LIMPIC { get; set; }
        public Nullable<long> NUMREG { get; set; }
        public virtual N0002FIL N0002FIL { get; set; }
        public virtual ICollection<N0110DOC> N0110DOC { get; set; }
    }
}

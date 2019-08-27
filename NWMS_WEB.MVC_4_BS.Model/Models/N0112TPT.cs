using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0112TPT
    {
        public N0112TPT()
        {
            this.N0112TAR = new List<N0112TAR>();
        }

        public long CODEMP { get; set; }
        public long CODTPT { get; set; }
        public string DESTPT { get; set; }
        public Nullable<long> TEMEST { get; set; }
        public string TNSTPT { get; set; }
        public string SITTPT { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUULT { get; set; }
        public System.DateTime DATULT { get; set; }
        public string INDBES { get; set; }
        public string INDAID { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual N0005TNS N0005TNS { get; set; }
        public virtual ICollection<N0112TAR> N0112TAR { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0104TPE
    {
        public N0104TPE()
        {
            this.N0106END = new List<N0106END>();
        }

        public long CODEMP { get; set; }
        public long CODTPE { get; set; }
        public string DESTPE { get; set; }
        public byte[] IMGTPE { get; set; }
        public string MOVUNI { get; set; }
        public string SITTPE { get; set; }
        public Nullable<long> USUCAD { get; set; }
        public Nullable<System.DateTime> DATCAD { get; set; }
        public Nullable<long> USUALT { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual ICollection<N0106END> N0106END { get; set; }
    }
}

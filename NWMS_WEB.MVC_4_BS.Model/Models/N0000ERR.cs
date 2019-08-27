using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0000ERR
    {
        public N0000ERR()
        {
            this.N0202REQ = new List<N0202REQ>();
        }

        public long CODERR { get; set; }
        public string DESSIS { get; set; }
        public string TELSIS { get; set; }
        public string CRIERR { get; set; }
        public string OBSUSU { get; set; }
        public string SITANA { get; set; }
        public System.DateTime DATGER { get; set; }
        public long USUGER { get; set; }
        public Nullable<System.DateTime> DATPAR { get; set; }
        public Nullable<long> USUPAR { get; set; }
        public string VERSIS { get; set; }
        public string COMSQL { get; set; }
        public byte[] IMGERR { get; set; }
        public string TITMSG { get; set; }
        public string MSGERR { get; set; }
        public virtual ICollection<N0202REQ> N0202REQ { get; set; }
    }
}

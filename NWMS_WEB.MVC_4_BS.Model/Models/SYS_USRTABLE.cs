using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_USRTABLE
    {
        public SYS_USRTABLE()
        {
            this.SYS_USRCOLUMN = new List<SYS_USRCOLUMN>();
            this.SYS_USRFORM = new List<SYS_USRFORM>();
            this.SYS_USRFORMXTABLE = new List<SYS_USRFORMXTABLE>();
        }

        public long CODUSRTABLE { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public string DESCRICAO { get; set; }
        public string NOME { get; set; }
        public virtual ICollection<SYS_USRCOLUMN> SYS_USRCOLUMN { get; set; }
        public virtual ICollection<SYS_USRFORM> SYS_USRFORM { get; set; }
        public virtual ICollection<SYS_USRFORMXTABLE> SYS_USRFORMXTABLE { get; set; }
    }
}

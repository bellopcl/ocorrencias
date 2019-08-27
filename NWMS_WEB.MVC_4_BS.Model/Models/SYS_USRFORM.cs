using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_USRFORM
    {
        public SYS_USRFORM()
        {
            this.SYS_USRCOMPONENT = new List<SYS_USRCOMPONENT>();
            this.SYS_USRFORMXTABLE = new List<SYS_USRFORMXTABLE>();
        }

        public long CODFORM { get; set; }
        public long CODUSRTABLE { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public string FORMPADRAO { get; set; }
        public byte[] ICONE { get; set; }
        public string NOME { get; set; }
        public string TIPOFORM { get; set; }
        public string TITULO { get; set; }
        public virtual ICollection<SYS_USRCOMPONENT> SYS_USRCOMPONENT { get; set; }
        public virtual ICollection<SYS_USRFORMXTABLE> SYS_USRFORMXTABLE { get; set; }
        public virtual SYS_USRTABLE SYS_USRTABLE { get; set; }
    }
}

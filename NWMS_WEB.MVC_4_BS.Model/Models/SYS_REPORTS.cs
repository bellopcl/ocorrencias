using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_REPORTS
    {
        public SYS_REPORTS()
        {
            this.SYS_KPI = new List<SYS_KPI>();
        }

        public long CODREL { get; set; }
        public string DESCRICAO { get; set; }
        public string ID_DEV { get; set; }
        public string TIPO { get; set; }
        public string COMANDOSQL { get; set; }
        public byte[] DIAGRAMA { get; set; }
        public string FILTROEXTERNO { get; set; }
        public string HELP { get; set; }
        public string ESTRUTURA { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public string EDITAVEL { get; set; }
        public virtual ICollection<SYS_KPI> SYS_KPI { get; set; }
    }
}

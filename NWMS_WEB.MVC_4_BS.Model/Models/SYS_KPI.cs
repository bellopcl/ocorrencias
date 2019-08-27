using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_KPI
    {
        public long CODKPI { get; set; }
        public string NOMEFORM { get; set; }
        public string DESCRICAO { get; set; }
        public string TIPO { get; set; }
        public string USAFILTRO { get; set; }
        public string PARAMETROS { get; set; }
        public Nullable<long> CODREL { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public virtual SYS_REPORTS SYS_REPORTS { get; set; }
    }
}

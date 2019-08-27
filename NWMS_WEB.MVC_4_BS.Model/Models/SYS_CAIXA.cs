using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_CAIXA
    {
        public SYS_CAIXA()
        {
            this.SYS_MAIL = new List<SYS_MAIL>();
        }
        public long CODCAI { get; set; }
        public Nullable<long> CODCAI_1 { get; set; }
        public long CODUSU { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public string DESCRICAO { get; set; }
        public virtual ICollection<SYS_MAIL> SYS_MAIL { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
    }
}

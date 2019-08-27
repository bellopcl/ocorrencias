using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_USRLOG
    {
        public long CODUSU { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public string DESCRICAO { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
    }
}

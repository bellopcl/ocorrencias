using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_USRON
    {
        public long CODUSU { get; set; }
        public Nullable<System.DateTime> DATA { get; set; }
        public string IP { get; set; }
        public string VERSAO { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
    }
}

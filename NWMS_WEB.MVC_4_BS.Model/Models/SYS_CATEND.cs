using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_CATEND
    {
        public long CODUSU { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public string EMAIL { get; set; }
        public string NOME { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
    }
}

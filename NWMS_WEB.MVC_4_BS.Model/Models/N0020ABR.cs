using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0020ABR
    {
        public long CODUSU { get; set; }
        public Nullable<long> ABREMP { get; set; }
        public Nullable<long> ABRFIL { get; set; }
        public Nullable<long> ABRARM { get; set; }
        public Nullable<long> USUCAD { get; set; }
        public Nullable<System.DateTime> DATCAD { get; set; }
        public Nullable<long> USUALT { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
    }
}

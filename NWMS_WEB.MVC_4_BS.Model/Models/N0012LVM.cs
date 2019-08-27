using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0012LVM
    {
        public long CODTRA { get; set; }
        public string PLAVEI { get; set; }
        public long TRAMOT { get; set; }
        public long CODMOT { get; set; }
        public string SITLVM { get; set; }
        public Nullable<long> USUCAD { get; set; }
        public Nullable<System.DateTime> DATCAD { get; set; }
        public Nullable<long> USUALT { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public virtual N0012MOT N0012MOT { get; set; }
        public virtual N0012VEI N0012VEI { get; set; }
    }
}

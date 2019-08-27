using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0012MOT
    {
        public N0012MOT()
        {
            this.N0012LVM = new List<N0012LVM>();
        }

        public long TRAMOT { get; set; }
        public long CODMOT { get; set; }
        public string NOMMOT { get; set; }
        public string CGCCPF { get; set; }
        public string SITMOT { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public virtual ICollection<N0012LVM> N0012LVM { get; set; }
        public virtual N0012TRA N0012TRA { get; set; }
    }
}

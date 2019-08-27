using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0204MDV
    {
        public N0204MDV()
        {
            this.N0204MDO = new List<N0204MDO>();
        }

        public long CODMDV { get; set; }
        public string DESCMDV { get; set; }
        public string SITMDV { get; set; }
        public virtual ICollection<N0204MDO> N0204MDO { get; set; }
    }
}

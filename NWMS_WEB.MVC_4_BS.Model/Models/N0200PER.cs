using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0200PER
    {
        public N0200PER()
        {
            this.N0202REQ = new List<N0202REQ>();
        }

        public long CODPER { get; set; }
        public string DESPER { get; set; }
        public virtual ICollection<N0202REQ> N0202REQ { get; set; }
    }
}

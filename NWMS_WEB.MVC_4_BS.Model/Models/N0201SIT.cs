using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0201SIT
    {
        public N0201SIT()
        {
            this.N0202REQ = new List<N0202REQ>();
        }

        public long CODSIT { get; set; }
        public string DESSIT { get; set; }
        public virtual ICollection<N0202REQ> N0202REQ { get; set; }
    }
}

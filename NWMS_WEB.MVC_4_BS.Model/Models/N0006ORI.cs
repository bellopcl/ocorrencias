using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0006ORI
    {
        public N0006ORI()
        {
            this.N0006PRO = new List<N0006PROModel>();
            this.N0202REQ = new List<N0202REQ>();
        }

        public long CODEMP { get; set; }
        public string CODORI { get; set; }
        public string DESORI { get; set; }
        public string INDQTD { get; set; }
        public virtual ICollection<N0006PROModel> N0006PRO { get; set; }
        public virtual ICollection<N0202REQ> N0202REQ { get; set; }
    }
}

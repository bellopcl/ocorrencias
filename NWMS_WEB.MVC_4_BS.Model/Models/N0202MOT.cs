using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0202MOT
    {
        public N0202MOT()
        {
            this.N0202APR = new List<N0202APR>();
        }

        public long CODMOT { get; set; }
        public string DESMOT { get; set; }
        public virtual ICollection<N0202APR> N0202APR { get; set; }
    }
}

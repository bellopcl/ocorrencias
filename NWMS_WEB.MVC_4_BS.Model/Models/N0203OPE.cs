using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class N0203OPE
    {
        public N0203OPE()
        {
            this.N0203UOF = new List<N0203UOF>();
        }

        public long CODOPE { get; set; }
        public string DSCOPE { get; set; }
        public virtual ICollection<N0203UOF> N0203UOF { get; set; }
    }
}

using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0204ATD
    {
        public N0204ATD()
        {
            this.N0203UAP = new List<N0203UAP>();
            this.N0204AOR = new List<N0204AOR>();
            this.N0203UOF = new List<N0203UOF>();
            this.N0204AUS = new List<N0204AUS>();
        }

        public long CODATD { get; set; }
        public string DESCATD { get; set; }
        public string SITATD { get; set; }
        public virtual ICollection<N0203UAP> N0203UAP { get; set; }
        public virtual ICollection<N0204AOR> N0204AOR { get; set; }
        public virtual ICollection<N0203UOF> N0203UOF { get; set; }
        public virtual ICollection<N0204AUS> N0204AUS { get; set; }
    }
}

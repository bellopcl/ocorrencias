using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0204ORI
    {
        public N0204ORI()
        {
            this.N0203UAP = new List<N0203UAP>();
            this.N0204AOR = new List<N0204AOR>();
            this.N0204MDO = new List<N0204MDO>();
        }

        public long CODORI { get; set; }
        public string DESCORI { get; set; }
        public string SITORI { get; set; }
        public long CODORI_SAPIENS { get; set; }
        public int? QTDORI;
        public decimal QTDORIVALOR;
        public virtual ICollection<N0203UAP> N0203UAP { get; set; }
        public virtual ICollection<N0204AOR> N0204AOR { get; set; }
        public virtual ICollection<N0204MDO> N0204MDO { get; set; }
    }
}

using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0007UNI
    {
        public N0007UNI()
        {
            this.N0006PRO = new List<N0006PROModel>();
            this.N0008CNV = new List<N0008CNV>();
            this.N0008CNV1 = new List<N0008CNV>();
            this.N0018LPD = new List<N0018LPD>();
            this.N0107DEN = new List<N0107DEN>();
            this.N0109IUA = new List<N0109IUA>();
            this.N0109MOV = new List<N0109MOV>();
            this.N0110ITD = new List<N0110ITD>();
            this.N0111ITV = new List<N0111ITV>();
            this.N0112TAR = new List<N0112TAR>();
        }

        public string UNIMED { get; set; }
        public string DESMED { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public virtual ICollection<N0006PROModel> N0006PRO { get; set; }
        public virtual ICollection<N0008CNV> N0008CNV { get; set; }
        public virtual ICollection<N0008CNV> N0008CNV1 { get; set; }
        public virtual ICollection<N0018LPD> N0018LPD { get; set; }
        public virtual ICollection<N0107DEN> N0107DEN { get; set; }
        public virtual ICollection<N0109IUA> N0109IUA { get; set; }
        public virtual ICollection<N0109MOV> N0109MOV { get; set; }
        public virtual ICollection<N0110ITD> N0110ITD { get; set; }
        public virtual ICollection<N0111ITV> N0111ITV { get; set; }
        public virtual ICollection<N0112TAR> N0112TAR { get; set; }
    }
}

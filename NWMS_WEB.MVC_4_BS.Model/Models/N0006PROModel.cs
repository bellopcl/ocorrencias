using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0006PROModel
    {
        public N0006PROModel()
        {
            this.N0006DER = new List<N0006DER>();
            this.N0010UCT = new List<N0010UCT>();
            this.N0019CPR = new List<N0019CPR>();
            this.N0110ITD = new List<N0110ITD>();
            this.N0202REQ = new List<N0202REQ>();
        }

        public long CODEMP { get; set; }
        public string CODPRO { get; set; }
        public string DESPRO { get; set; }
        public string CODORI { get; set; }
        public string CODFAM { get; set; }
        public string UNIMED { get; set; }
        public string TIPPRO { get; set; }
        public string CODAGP { get; set; }
        public string CODAGE { get; set; }
        public string DEPPRD { get; set; }
        public string INDMIS { get; set; }
        public string INDORP { get; set; }
        public string EXPPAL { get; set; }
        public string FORLIN { get; set; }
        public string SITPRO { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public string BLOINV { get; set; }
        public string INDRET { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual ICollection<N0006DER> N0006DER { get; set; }
        public virtual N0006FAM N0006FAM { get; set; }
        public virtual N0006ORI N0006ORI { get; set; }
        public virtual N0007UNI N0007UNI { get; set; }
        public virtual ICollection<N0010UCT> N0010UCT { get; set; }
        public virtual ICollection<N0019CPR> N0019CPR { get; set; }
        public virtual ICollection<N0110ITD> N0110ITD { get; set; }
        public virtual ICollection<N0202REQ> N0202REQ { get; set; }
    }
}

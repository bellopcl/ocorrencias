using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0006FAM
    {
        public N0006FAM()
        {
            this.N0006PRO = new List<N0006PROModel>();
            this.N0202REQ = new List<N0202REQ>();
        }

        public long CODEMP { get; set; }
        public string CODFAM { get; set; }
        public string DESFAM { get; set; }
        public string CODORI { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual ICollection<N0006PROModel> N0006PRO { get; set; }
        public virtual ICollection<N0202REQ> N0202REQ { get; set; }
    }
}

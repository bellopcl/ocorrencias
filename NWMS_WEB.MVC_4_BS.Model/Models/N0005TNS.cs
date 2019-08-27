using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0005TNS
    {
        public N0005TNS()
        {
            this.N0109HUA = new List<N0109HUA>();
            this.N0109MOV = new List<N0109MOV>();
            this.N0110DOC = new List<N0110DOC>();
            this.N0110ITD = new List<N0110ITD>();
            this.N0112TPT = new List<N0112TPT>();
            this.N0203IPV = new List<N0203IPV>();
        }

        public long CODEMP { get; set; }
        public string CODTNS { get; set; }
        public string DESTNS { get; set; }
        public string LISMOD { get; set; }
        public string ESTEOS { get; set; }
        public string SITTNS { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual ICollection<N0109HUA> N0109HUA { get; set; }
        public virtual ICollection<N0109MOV> N0109MOV { get; set; }
        public virtual ICollection<N0110DOC> N0110DOC { get; set; }
        public virtual ICollection<N0110ITD> N0110ITD { get; set; }
        public virtual ICollection<N0112TPT> N0112TPT { get; set; }
        public virtual ICollection<N0203IPV> N0203IPV { get; set; }
    }
}

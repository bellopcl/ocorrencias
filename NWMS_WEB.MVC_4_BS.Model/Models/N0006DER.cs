using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0006DER
    {
        public N0006DER()
        {
            this.N0009ITP = new List<N0009ITP>();
            this.N0010UCT = new List<N0010UCT>();
            this.N0018LPD = new List<N0018LPD>();
            this.N0107DEN = new List<N0107DEN>();
            this.N0109IUA = new List<N0109IUA>();
            this.N0109MOV = new List<N0109MOV>();
            this.N0110ITD = new List<N0110ITD>();
            this.N0111ITV = new List<N0111ITV>();
            this.N0203IPV = new List<N0203IPV>();
        }

        public long CODEMP { get; set; }
        public string CODPRO { get; set; }
        public string CODDER { get; set; }
        public string DESDER { get; set; }
        public Nullable<long> CODBAR { get; set; }
        public string CODAGT { get; set; }
        public Nullable<long> DIAVLD { get; set; }
        public Nullable<long> PRECUS { get; set; }
        public Nullable<long> PREMED { get; set; }
        public Nullable<long> PESBRU { get; set; }
        public Nullable<long> PESLIQ { get; set; }
        public string EXPPAL { get; set; }
        public string FORLIN { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATALT { get; set; }
        public long USUALT { get; set; }
        public byte[] IMGDER { get; set; }
        public string SITDER { get; set; }
        public string BLOINV { get; set; }
        public string CURABC { get; set; }
        public virtual N0006PROModel N0006PRO { get; set; }
        public virtual ICollection<N0009ITP> N0009ITP { get; set; }
        public virtual ICollection<N0010UCT> N0010UCT { get; set; }
        public virtual ICollection<N0018LPD> N0018LPD { get; set; }
        public virtual ICollection<N0107DEN> N0107DEN { get; set; }
        public virtual ICollection<N0109IUA> N0109IUA { get; set; }
        public virtual ICollection<N0109MOV> N0109MOV { get; set; }
        public virtual ICollection<N0110ITD> N0110ITD { get; set; }
        public virtual ICollection<N0111ITV> N0111ITV { get; set; }
        public virtual ICollection<N0203IPV> N0203IPV { get; set; }
    }
}

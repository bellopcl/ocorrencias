using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0001EMPModel
    {
        public N0001EMPModel()
        {
            this.N0002FIL = new List<N0002FIL>();
            this.N0005TNS = new List<N0005TNS>();
            this.N0006FAM = new List<N0006FAM>();
            this.N0006PRO = new List<N0006PROModel>();
            this.N0009TAB = new List<N0009TAB>();
            this.N0010UCT = new List<N0010UCT>();
            this.N0018DEP = new List<N0018DEP>();
            this.N0019CTE = new List<N0019CTE>();
            this.N0044CCU = new List<N0044CCU>();
            this.N0102ESE = new List<N0102ESE>();
            this.N0103TUA = new List<N0103TUA>();
            this.N0104TPE = new List<N0104TPE>();
            this.N0105TLE = new List<N0105TLE>();
            this.N0108UCA = new List<N0108UCA>();
            this.N0112TPT = new List<N0112TPT>();
            this.N0202REQ = new List<N0202REQ>();
        }

        public long CODEMP { get; set; }
        public string RAZSOC { get; set; }
        public string NOMABR { get; set; }
        public long CGCEMP { get; set; }
        public byte[] LOGEMP { get; set; }
        public string SITEMP { get; set; }
        public Nullable<System.DateTime> DATGER { get; set; }
        public Nullable<long> USUGER { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public Nullable<long> USUALT { get; set; }
        public string USUSID { get; set; }
        public string SENSID { get; set; }
        public string ENDSID { get; set; }
        public string USUDOM { get; set; }
        public string SENDOM { get; set; }
        public string NOMDOM { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO1 { get; set; }
        public virtual ICollection<N0002FIL> N0002FIL { get; set; }
        public virtual ICollection<N0005TNS> N0005TNS { get; set; }
        public virtual ICollection<N0006FAM> N0006FAM { get; set; }
        public virtual ICollection<N0006PROModel> N0006PRO { get; set; }
        public virtual ICollection<N0009TAB> N0009TAB { get; set; }
        public virtual ICollection<N0010UCT> N0010UCT { get; set; }
        public virtual ICollection<N0018DEP> N0018DEP { get; set; }
        public virtual ICollection<N0019CTE> N0019CTE { get; set; }
        public virtual ICollection<N0044CCU> N0044CCU { get; set; }
        public virtual ICollection<N0102ESE> N0102ESE { get; set; }
        public virtual ICollection<N0103TUA> N0103TUA { get; set; }
        public virtual ICollection<N0104TPE> N0104TPE { get; set; }
        public virtual ICollection<N0105TLE> N0105TLE { get; set; }
        public virtual ICollection<N0108UCA> N0108UCA { get; set; }
        public virtual ICollection<N0112TPT> N0112TPT { get; set; }
        public virtual ICollection<N0202REQ> N0202REQ { get; set; }
    }
}

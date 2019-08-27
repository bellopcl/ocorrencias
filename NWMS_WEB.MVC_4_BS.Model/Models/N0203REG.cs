using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0203REG
    {
        public N0203REG()
        {
            this.N0203ANX = new List<N0203ANX>();
            this.N0203APR = new List<N0203APR>();
            this.N0203IPV = new List<N0203IPV>();
            this.N0203ITR = new List<N0203ITR>();
            this.N0203TRA = new List<N0203TRA>();
        }

        public long NUMREG { get; set; }
        public long SITREG { get; set; }
        public System.DateTime DATGER { get; set; }
        public long USUGER { get; set; }
        public long TIPATE { get; set; }
        public long ORIOCO { get; set; }
        public System.DateTime DATULT { get; set; }
        public long USUULT { get; set; }
        public Nullable<long> USUFEC { get; set; }
        public Nullable<System.DateTime> DATFEC { get; set; }
        public long CODCLI { get; set; }
        public long CODMOT { get; set; }
        public Nullable<long> CODUSU { get; set; }
        public string OBSREG { get; set; }
        public string PLACA { get; set; }
        public string APREAP { get; set; }
        public virtual ICollection<N0203ANX> N0203ANX { get; set; }
        public virtual ICollection<N0203APR> N0203APR { get; set; }
        public virtual ICollection<N0203IPV> N0203IPV { get; set; }
        public virtual ICollection<N0203ITR> N0203ITR { get; set; }
        public virtual ICollection<N0203TRA> N0203TRA { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0103TUA
    {
        public N0103TUA()
        {
            this.N0108UCA = new List<N0108UCA>();
            this.N0109UAR = new List<N0109UAR>();
            this.N0111UAV = new List<N0111UAV>();
            this.N0112TAR = new List<N0112TAR>();
        }

        public long CODEMP { get; set; }
        public long CODTUA { get; set; }
        public string DESTUA { get; set; }
        public byte[] IMGTUA { get; set; }
        public Nullable<long> LARTUA { get; set; }
        public Nullable<long> COMTUA { get; set; }
        public Nullable<long> ALTTUA { get; set; }
        public Nullable<long> CUBMAX { get; set; }
        public Nullable<long> DIAMAX { get; set; }
        public Nullable<long> PESMAX { get; set; }
        public string SITTUA { get; set; }
        public string OBRTUA { get; set; }
        public Nullable<long> USUCAD { get; set; }
        public Nullable<System.DateTime> DATCAD { get; set; }
        public Nullable<long> USUALT { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual ICollection<N0108UCA> N0108UCA { get; set; }
        public virtual ICollection<N0109UAR> N0109UAR { get; set; }
        public virtual ICollection<N0111UAV> N0111UAV { get; set; }
        public virtual ICollection<N0112TAR> N0112TAR { get; set; }
    }
}

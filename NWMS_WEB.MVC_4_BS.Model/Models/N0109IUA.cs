using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0109IUA
    {
        public N0109IUA()
        {
            this.N0109LOT = new List<N0109LOT>();
        }

        public long CODEMP { get; set; }
        public long CODUAR { get; set; }
        public long SEQUAR { get; set; }
        public string CODPRO { get; set; }
        public string CODDER { get; set; }
        public string UNIMED { get; set; }
        public long QTDORI { get; set; }
        public long QTDDSP { get; set; }
        public Nullable<long> QTDRET { get; set; }
        public Nullable<long> QTDRES { get; set; }
        public System.DateTime DATULT { get; set; }
        public long USUULT { get; set; }
        public string SITIUA { get; set; }
        public Nullable<long> QTDPAD { get; set; }
        public virtual N0006DER N0006DER { get; set; }
        public virtual N0007UNI N0007UNI { get; set; }
        public virtual N0109UAR N0109UAR { get; set; }
        public virtual ICollection<N0109LOT> N0109LOT { get; set; }
    }
}

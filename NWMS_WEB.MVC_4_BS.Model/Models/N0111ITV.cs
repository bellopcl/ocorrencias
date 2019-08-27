using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0111ITV
    {
        public N0111ITV()
        {
            this.N0111LOT = new List<N0111LOT>();
        }

        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public long CODARM { get; set; }
        public long CODINV { get; set; }
        public long SEQINV { get; set; }
        public long SEQIUA { get; set; }
        public string CODPRO { get; set; }
        public string CODDER { get; set; }
        public string UNIMED { get; set; }
        public long QTDDSP { get; set; }
        public long QTDRES { get; set; }
        public long QTDORI { get; set; }
        public long QTDPAD { get; set; }
        public Nullable<long> QTDAC1 { get; set; }
        public Nullable<long> USUAC1 { get; set; }
        public Nullable<long> QTDAC2 { get; set; }
        public Nullable<long> USUAC2 { get; set; }
        public Nullable<long> QTDAC3 { get; set; }
        public Nullable<long> USUAC3 { get; set; }
        public Nullable<long> QTDAC4 { get; set; }
        public Nullable<long> USUAC4 { get; set; }
        public Nullable<long> QTDAPL { get; set; }
        public string PRONOV { get; set; }
        public string DERNOV { get; set; }
        public string UNINOV { get; set; }
        public virtual N0006DER N0006DER { get; set; }
        public virtual N0007UNI N0007UNI { get; set; }
        public virtual N0111UAV N0111UAV { get; set; }
        public virtual ICollection<N0111LOT> N0111LOT { get; set; }
    }
}

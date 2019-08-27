using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0111LOT
    {
        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public long CODARM { get; set; }
        public long CODINV { get; set; }
        public long SEQIUA { get; set; }
        public long SEQLOT { get; set; }
        public Nullable<long> NUMSER { get; set; }
        public Nullable<long> NUMLOT { get; set; }
        public Nullable<System.DateTime> DATVLD { get; set; }
        public long SEQINV { get; set; }
        public virtual N0111ITV N0111ITV { get; set; }
    }
}

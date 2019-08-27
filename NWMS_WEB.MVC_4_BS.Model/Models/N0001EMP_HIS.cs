using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0001EMP_HIS
    {
        public long SEQHIS { get; set; }
        public System.DateTime DATHIS { get; set; }
        public string TIPHIS { get; set; }
        public long CODEMP { get; set; }
        public string RAZSOC { get; set; }
        public string NOMABR { get; set; }
        public Nullable<long> CGCEMP { get; set; }
        public byte[] LOGEMP { get; set; }
        public string SITEMP { get; set; }
        public long USUHIS { get; set; }
    }
}

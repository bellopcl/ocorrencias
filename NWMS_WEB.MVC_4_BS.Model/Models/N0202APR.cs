using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0202APR
    {
        public long SEQAPR { get; set; }
        public long CODEMP { get; set; }
        public long NUMREQ { get; set; }
        public long CODROT { get; set; }
        public long NIVAPR { get; set; }
        public string DESNIV { get; set; }
        public Nullable<long> USUAPR { get; set; }
        public Nullable<System.DateTime> DATAPR { get; set; }
        public string OBSAPR { get; set; }
        public Nullable<long> USUREJ { get; set; }
        public Nullable<System.DateTime> DATREJ { get; set; }
        public string SITAPR { get; set; }
        public Nullable<long> CODMOT { get; set; }
        public string VIAAPR { get; set; }
        public virtual N0202MOT N0202MOT { get; set; }
        public virtual N0202REQ N0202REQ { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO1 { get; set; }
    }
}

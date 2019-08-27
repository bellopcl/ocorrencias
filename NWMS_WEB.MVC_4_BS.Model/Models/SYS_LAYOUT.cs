using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_LAYOUT
    {
        public long ID { get; set; }
        public Nullable<long> COD { get; set; }
        public string NOME { get; set; }
        public string IMPEXP { get; set; }
        public string DIRPADRAO { get; set; }
        public Nullable<long> IDUNICO { get; set; }
        public byte[] REGRAS { get; set; }
        public string LAYOUT { get; set; }
        public string EXTENSAO { get; set; }
        public string FILTRO { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
    }
}

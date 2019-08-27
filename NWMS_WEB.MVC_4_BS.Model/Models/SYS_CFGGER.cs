using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_CFGGER
    {
        public byte[] CHAVE { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public byte[] BRADLL { get; set; }
        public byte[] BRDLL { get; set; }
        public byte[] BRCLASSESDLL { get; set; }
        public string LOCKDBNAME { get; set; }
        public string LOCKDBACTION { get; set; }
        public Nullable<System.DateTime> DATADLL { get; set; }
        public string VERSAO { get; set; }
        public byte[] IMGPRINC { get; set; }
        public string ULTIMOACESSO { get; set; }
    }
}

using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_USRCOMPONENT
    {
        public long CODCOMPONENT { get; set; }
        public long CODFORM { get; set; }
        public Nullable<long> CODUSRCOLUMN { get; set; }
        public string CONFCOMPONENT { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public string DATAOBJECT { get; set; }
        public string DESCRICAO { get; set; }
        public string NOME { get; set; }
        public string PARENT { get; set; }
        public string TIPO { get; set; }
        public virtual SYS_USRCOLUMN SYS_USRCOLUMN { get; set; }
        public virtual SYS_USRFORM SYS_USRFORM { get; set; }
    }
}

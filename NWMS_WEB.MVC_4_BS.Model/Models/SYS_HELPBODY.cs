using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_HELPBODY
    {
        public long CODHELP { get; set; }
        public string CONTEUDOHTML_ENG { get; set; }
        public string CONTEUDOHTML_ESP { get; set; }
        public string CONTEUDOHTML_PTB { get; set; }
        public string CONTEUDOTXT_ENG { get; set; }
        public string CONTEUDOTXT_ESP { get; set; }
        public string CONTEUDOTXT_PTB { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public virtual SYS_HELP SYS_HELP { get; set; }
    }
}

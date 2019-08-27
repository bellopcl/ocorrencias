using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_MAIL
    {
        public string ASSUNTO { get; set; }
        public long CODCAI { get; set; }
        public long CODMAI { get; set; }
        public long CODUSU { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public Nullable<System.DateTime> DATCRIA { get; set; }
        public Nullable<System.DateTime> DATSENT { get; set; }
        public Nullable<System.DateTime> DATVIEW { get; set; }
        public string DESTCC { get; set; }
        public string DESTPARA { get; set; }
        public string EMAILLIDO { get; set; }
        public string MESSAGEID { get; set; }
        public string REMETENTE { get; set; }
        public string SOURCE { get; set; }
        public Nullable<long> TAMANHO { get; set; }
        public string TEMANEXOS { get; set; }
        public virtual SYS_CAIXA SYS_CAIXA { get; set; }
    }
}

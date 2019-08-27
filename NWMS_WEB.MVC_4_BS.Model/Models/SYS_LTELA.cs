using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_LTELA
    {
        public long CODLTELA { get; set; }
        public Nullable<long> CODUSU { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public string NOMECOMPONENTE { get; set; }
        public string NOMEFORM { get; set; }
        public byte[] PROPCOMPONENTE { get; set; }
        public string CATEGORIA_FT { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
    }
}

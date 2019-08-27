using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_CONSULTA
    {
        public long CODCONSULTA { get; set; }
        public Nullable<long> CODPRO { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public string DESCRICAO { get; set; }
        public byte[] ESTRDIAGRAMA { get; set; }
        public string TIPODIAGR { get; set; }
    }
}

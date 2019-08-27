using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0108UCA
    {
        public long CODEMP { get; set; }
        public string CODBAR { get; set; }
        public Nullable<long> QTDLAS { get; set; }
        public Nullable<long> QTDPLA { get; set; }
        public long QTDMAX { get; set; }
        public string OBSTUA { get; set; }
        public string SITUCA { get; set; }
        public long CODTUA { get; set; }
        public string INDMAX { get; set; }
        public long USUCAD { get; set; }
        public System.DateTime DATCAD { get; set; }
        public long USUALT { get; set; }
        public System.DateTime DATALT { get; set; }
        public string INDMPU { get; set; }
        public string CODPRO { get; set; }
        public string CODDER { get; set; }
        public string UNIMED { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual N0103TUA N0103TUA { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO1 { get; set; }
    }
}

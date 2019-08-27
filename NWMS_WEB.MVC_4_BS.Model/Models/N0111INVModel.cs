using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0111INVModel
    {
        public N0111INVModel()
        {
            this.N0111UAV = new List<N0111UAV>();
        }

        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public long CODARM { get; set; }
        public long CODINV { get; set; }
        public System.DateTime DATINI { get; set; }
        public Nullable<System.DateTime> DATFIM { get; set; }
        public string SITINV { get; set; }
        public string MODINV { get; set; }
        public string TIPCON { get; set; }
        public long QTDCON { get; set; }
        public Nullable<long> QTDEMB { get; set; }
        public Nullable<long> QTDUNI { get; set; }
        public Nullable<long> QTDPAL { get; set; }
        public Nullable<long> ESTVEN { get; set; }
        public Nullable<long> ESTCUS { get; set; }
        public System.DateTime DATGER { get; set; }
        public long USUGER { get; set; }
        public Nullable<long> EMBATU { get; set; }
        public Nullable<long> UNIATU { get; set; }
        public Nullable<long> PALATU { get; set; }
        public Nullable<long> VENATU { get; set; }
        public Nullable<long> VLRCUS { get; set; }
        public Nullable<long> SEQCON { get; set; }
        public string OBRCTP { get; set; }
        public string OBRALL { get; set; }
        public Nullable<long> CODTPE { get; set; }
        public string INDAID { get; set; }
        public Nullable<System.DateTime> DATINV { get; set; }
        public string TIPINV { get; set; }
        public virtual N0003ARM N0003ARM { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
        public virtual ICollection<N0111UAV> N0111UAV { get; set; }
    }
}

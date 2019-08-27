using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0003ARM
    {
        public N0003ARM()
        {
            this.N0101LOC = new List<N0101LOC>();
            this.N0109MOV = new List<N0109MOV>();
            this.N0111INV = new List<N0111INVModel>();
        }

        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public long CODARM { get; set; }
        public string DESARM { get; set; }
        public string MOVEST { get; set; }
        public string INDBOP { get; set; }
        public string DEPERP { get; set; }
        public string EMAREQ { get; set; }
        public string SITARM { get; set; }
        public Nullable<long> ARMENT { get; set; }
        public Nullable<long> LOCENT { get; set; }
        public Nullable<long> ENDENT { get; set; }
        public string LIMPIC { get; set; }
        public string INDEND { get; set; }
        public Nullable<long> TMPACE { get; set; }
        public string INDMPK { get; set; }
        public string TNSEXT { get; set; }
        public string TNSEIN { get; set; }
        public string TNSSIN { get; set; }
        public string TNSTRF { get; set; }
        public string INDBLC { get; set; }
        public string INDMDC { get; set; }
        public Nullable<long> USUCAD { get; set; }
        public Nullable<System.DateTime> DATCAD { get; set; }
        public Nullable<long> USUALT { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public string INDBEP { get; set; }
        public string GERLOG { get; set; }
        public string ARQLOG { get; set; }
        public string INDBLK { get; set; }
        public string TNSEND { get; set; }
        public string TNSIUA { get; set; }
        public string TNSIUE { get; set; }
        public string INDBAS { get; set; }
        public string INDBAR { get; set; }
        public string INDINV { get; set; }
        public string DEPDEV { get; set; }
        public string INDPRO { get; set; }
        public string TNSBPR { get; set; }
        public string TNSBPT { get; set; }
        public string INDPCC { get; set; }
        public string INDPRV { get; set; }
        public string INDBLP { get; set; }
        public string INDTFD { get; set; }
        public string INDTRD { get; set; }
        public string TNSTRI { get; set; }
        public string INDQTD { get; set; }
        public string ENTAID { get; set; }
        public string INDPIC { get; set; }
        public string INDFOR { get; set; }
        public virtual N0002FIL N0002FIL { get; set; }
        public virtual ICollection<N0101LOC> N0101LOC { get; set; }
        public virtual ICollection<N0109MOV> N0109MOV { get; set; }
        public virtual ICollection<N0111INVModel> N0111INV { get; set; }
    }
}

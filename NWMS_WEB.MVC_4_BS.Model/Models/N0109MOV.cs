using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0109MOV
    {
        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public long CODARM { get; set; }
        public string CODPRO { get; set; }
        public string CODDER { get; set; }
        public string UNIMED { get; set; }
        public System.DateTime DATMOV { get; set; }
        public long SEQMOV { get; set; }
        public string CODTNS { get; set; }
        public long CODUAR { get; set; }
        public long SEQUAR { get; set; }
        public Nullable<long> SEQLOT { get; set; }
        public Nullable<long> LOCORI { get; set; }
        public Nullable<long> ENDORI { get; set; }
        public long FILDES { get; set; }
        public long ARMDES { get; set; }
        public long LOCDES { get; set; }
        public long ENDDES { get; set; }
        public long QTDEST { get; set; }
        public long QTDMOV { get; set; }
        public Nullable<long> NUMROM { get; set; }
        public Nullable<long> DOCROM { get; set; }
        public Nullable<long> ITPROM { get; set; }
        public string CODROE { get; set; }
        public Nullable<long> SEQROE { get; set; }
        public System.DateTime HORMOV { get; set; }
        public long USUMOV { get; set; }
        public Nullable<long> VLRMOV { get; set; }
        public Nullable<long> PESMOV { get; set; }
        public Nullable<long> CUBMOV { get; set; }
        public Nullable<long> CODTAR { get; set; }
        public Nullable<long> CODCLI { get; set; }
        public string OBSMOV { get; set; }
        public Nullable<long> LIQMOV { get; set; }
        public string ESTMOV { get; set; }
        public Nullable<long> USUINT { get; set; }
        public Nullable<System.DateTime> DATINT { get; set; }
        public Nullable<System.DateTime> HORINT { get; set; }
        public Nullable<long> CODLIG { get; set; }
        public Nullable<long> SEQINT { get; set; }
        public virtual N0003ARM N0003ARM { get; set; }
        public virtual N0005TNS N0005TNS { get; set; }
        public virtual N0006DER N0006DER { get; set; }
        public virtual N0007UNI N0007UNI { get; set; }
        public virtual N0109LOT N0109LOT { get; set; }
    }
}

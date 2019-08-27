
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0203IPV
    {
        public long NUMREG { get; set; }
        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public string CODSNF { get; set; }
        public long NUMNFV { get; set; }
        public long SEQIPV { get; set; }
        public string CODPRO { get; set; }
        public string CODDER { get; set; }
        public string CPLIPV { get; set; }
        public string CODDEP { get; set; }
        public long QTDFAT { get; set; }
        public float PREUNI { get; set; }
        public decimal VLRST { get; set; }
        public decimal VLRLIQ { get; set; }
        public long ORIOCO { get; set; }
        public long CODMOT { get; set; }
        public long QTDDEV { get; set; }
        public string TNSPRO { get; set; }
        public long USUULT { get; set; }
        public System.DateTime DATULT { get; set; }
        public decimal PEROFE { get; set; }
        public decimal PERIPI { get; set; }
        public decimal VLRIPI { get; set; }
        public long NUMANE { get; set; }
        public long? NUMANE_REL { get; set; }
        public System.DateTime DATEMI { get; set; }
        public virtual N0005TNS N0005TNS { get; set; }
        public virtual N0006DER N0006DER { get; set; }
        public virtual N0203REG N0203REG { get; set; }
        
    }
}

using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0110ITD
    {
        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public long NUMROM { get; set; }
        public long NUMDOC { get; set; }
        public long SEQITD { get; set; }
        public string CODTNS { get; set; }
        public string CODPRO { get; set; }
        public string CODDER { get; set; }
        public string UNIMED { get; set; }
        public long QTDPED { get; set; }
        public Nullable<long> PREUNI { get; set; }
        public long SITITD { get; set; }
        public Nullable<long> QTDSEP { get; set; }
        public Nullable<long> CODMOT { get; set; }
        public Nullable<long> QTDCAN { get; set; }
        public Nullable<long> FILPFA { get; set; }
        public Nullable<long> NUMANE { get; set; }
        public Nullable<long> NUMPFA { get; set; }
        public Nullable<long> SEQPES { get; set; }
        public Nullable<long> NUMPED { get; set; }
        public Nullable<long> VLRPES { get; set; }
        public virtual N0005TNS N0005TNS { get; set; }
        public virtual N0006DER N0006DER { get; set; }
        public virtual N0006PROModel N0006PRO { get; set; }
        public virtual N0007UNI N0007UNI { get; set; }
        public virtual N0013MCP N0013MCP { get; set; }
        public virtual N0110DOC N0110DOC { get; set; }
    }
}

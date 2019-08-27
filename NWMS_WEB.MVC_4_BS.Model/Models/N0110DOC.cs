using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0110DOC
    {
        public N0110DOC()
        {
            this.N0110ITD = new List<N0110ITD>();
        }

        public long CODEMP { get; set; }
        public long CODFIL { get; set; }
        public long NUMROM { get; set; }
        public long NUMDOC { get; set; }
        public string CODTNS { get; set; }
        public Nullable<long> CODCLI { get; set; }
        public Nullable<long> SEQENT { get; set; }
        public string CODROE { get; set; }
        public Nullable<long> SEQROE { get; set; }
        public Nullable<long> CODREP { get; set; }
        public Nullable<long> CODTRA { get; set; }
        public string OBSPFA { get; set; }
        public Nullable<long> PESBRU { get; set; }
        public Nullable<long> PESLIQ { get; set; }
        public Nullable<long> VLRPFA { get; set; }
        public Nullable<long> VOLPFA { get; set; }
        public string PLAVEI { get; set; }
        public long SITPFA { get; set; }
        public long USUIMP { get; set; }
        public System.DateTime DATIMP { get; set; }
        public Nullable<long> FILPFA { get; set; }
        public Nullable<long> NUMANE { get; set; }
        public Nullable<long> NUMPFA { get; set; }
        public Nullable<long> CODMOT { get; set; }
        public Nullable<long> REPSUP { get; set; }
        public virtual N0005TNS N0005TNS { get; set; }
        public virtual N0012TRA N0012TRA { get; set; }
        public virtual N0110ROM N0110ROM { get; set; }
        public virtual ICollection<N0110ITD> N0110ITD { get; set; }
    }
}

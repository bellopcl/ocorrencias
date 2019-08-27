using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0202REQ
    {
        public N0202REQ()
        {
            this.N0202APR = new List<N0202APR>();
            this.N0202TRA = new List<N0202TRA>();
        }

        public long NUMREQ { get; set; }
        public long CODUSU { get; set; }
        public long CODSIT { get; set; }
        public Nullable<long> CODPER { get; set; }
        public System.DateTime DATCAD { get; set; }
        public string CODORI { get; set; }
        public string CODFAM { get; set; }
        public string CODAGE { get; set; }
        public string DESPRO { get; set; }
        public string UNIMED { get; set; }
        public string CODCCU { get; set; }
        public string NUMREF { get; set; }
        public Nullable<long> QTDCON { get; set; }
        public byte[] ANEPRO { get; set; }
        public string CODBEM { get; set; }
        public string PROEQU { get; set; }
        public string DEREQU { get; set; }
        public long CODEMP { get; set; }
        public string OBSREQ { get; set; }
        public string TIPCAD { get; set; }
        public string DESDER { get; set; }
        public Nullable<long> R_N0000ERR_CODERR { get; set; }
        public virtual N0000ERR N0000ERR { get; set; }
        public virtual N0001EMPModel N0001EMP { get; set; }
        public virtual N0006FAM N0006FAM { get; set; }
        public virtual N0006ORI N0006ORI { get; set; }
        public virtual N0006PROModel N0006PRO { get; set; }
        public virtual N0044CCU N0044CCU { get; set; }
        public virtual N0200PER N0200PER { get; set; }
        public virtual N0201SIT N0201SIT { get; set; }
        public virtual ICollection<N0202APR> N0202APR { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
        public virtual ICollection<N0202TRA> N0202TRA { get; set; }
    }
}

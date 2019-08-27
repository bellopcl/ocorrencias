using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_USRCOLUMN
    {
        public SYS_USRCOLUMN()
        {
            this.SYS_USRCOMPONENT = new List<SYS_USRCOMPONENT>();
            this.SYS_USUARIO = new List<SYS_USUARIO>();
        }

        public string CHAVE_PK { get; set; }
        public long CODUSRCOLUMN { get; set; }
        public long CODUSRTABLE { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public Nullable<long> DECIMAIS { get; set; }
        public string DESCRICAO { get; set; }
        public string NOME { get; set; }
        public string OBRIGATORIO { get; set; }
        public Nullable<long> POSICAO_PK { get; set; }
        public Nullable<long> TAMANHO { get; set; }
        public string TIPO { get; set; }
        public string VALOR_PADRAO { get; set; }
        public virtual ICollection<SYS_USRCOMPONENT> SYS_USRCOMPONENT { get; set; }
        public virtual SYS_USRTABLE SYS_USRTABLE { get; set; }
        public virtual ICollection<SYS_USUARIO> SYS_USUARIO { get; set; }
    }
}

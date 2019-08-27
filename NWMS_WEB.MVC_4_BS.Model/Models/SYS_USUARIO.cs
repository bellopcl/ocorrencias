using System;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_USUARIO
    {
        public SYS_USUARIO()
        {
            this.N0001EMP = new List<N0001EMPModel>();
            this.N0001EMP1 = new List<N0001EMPModel>();
            this.N0108UCA = new List<N0108UCA>();
            this.N0108UCA1 = new List<N0108UCA>();
            this.N0111INV = new List<N0111INVModel>();
            this.N0202APR = new List<N0202APR>();
            this.N0202APR1 = new List<N0202APR>();
            this.N0202REQ = new List<N0202REQ>();
            this.N9999UXM = new List<N9999UXM>();
            this.SYS_CAIXA = new List<SYS_CAIXA>();
            this.SYS_CATEND = new List<SYS_CATEND>();
            this.SYS_CONCORRENCIA = new List<SYS_CONCORRENCIA>();
            this.SYS_LOGMSG = new List<SYS_LOGMSG>();
            this.SYS_LOGMSG1 = new List<SYS_LOGMSG>();
            this.SYS_LTELA = new List<SYS_LTELA>();
            this.SYS_USRON = new List<SYS_USRON>();
            this.SYS_USUARIO_LISTA = new List<SYS_USUARIO>();
            this.SYS_USRCOLUMN = new List<SYS_USRCOLUMN>();
        }

        public long CODUSU { get; set; }
        public string NOME { get; set; }
        public string LOGIN { get; set; }
        public string SENHA { get; set; }
        public string MENUEDITADO { get; set; }
        public string TOOLBAREDITADO { get; set; }
        public byte[] CTRLACESSO { get; set; }
        public string TIPO { get; set; }
        public string PROLAYOUT { get; set; }
        public string LOGINWIN { get; set; }
        public Nullable<long> CODUSU_GRU { get; set; }
        public Nullable<System.DateTime> DATALT { get; set; }
        public string REMETENTE { get; set; }
        public string NOMEEXIB { get; set; }
        public string AUTENTICACAO { get; set; }
        public string LOGINMAIL { get; set; }
        public string SENHAMAIL { get; set; }
        public string SMTP { get; set; }
        public string POP { get; set; }
        public Nullable<long> PORTASMTP { get; set; }
        public Nullable<long> PORTAPOP { get; set; }
        public string CONEXSEGURA { get; set; }
        public string DEIXARCOPIAMSG { get; set; }
        public string MARCARLIDA { get; set; }
        public Nullable<System.DateTime> DATALTSENHA { get; set; }
        public Nullable<long> EXPSENHA { get; set; }
        public string TROCARSENHA { get; set; }
        public string TROCARSENHAEXP { get; set; }
        public string TODOSRELATORIOS { get; set; }
        public string CFGMAIL { get; set; }
        public string ASSINATURA { get; set; }
        public string ENTERTOTAB { get; set; }
        public string UPDATERULES { get; set; }
        public Nullable<long> TIMEROC { get; set; }
        public string GRAVARALTERACOES { get; set; }
        public string GRAVARNAVEGACAO { get; set; }
        public string GRAVARPERSONALIZACOES { get; set; }
        public string USU_SITUSU { get; set; }
        public string USU_CARUSU { get; set; }
        public string USU_DEPUSU { get; set; }
        public string USU_NUMCRX { get; set; }
        public Nullable<long> USU_USUALT { get; set; }
        public Nullable<System.DateTime> USU_DATALT { get; set; }
        public string USU_SITTAR { get; set; }
        public string RELATSIMULTANEO { get; set; }
        public string SENWEB { get; set; }
        public virtual ICollection<N0001EMPModel> N0001EMP { get; set; }
        public virtual ICollection<N0001EMPModel> N0001EMP1 { get; set; }
        public virtual N0020ABR N0020ABR { get; set; }
        public virtual ICollection<N0108UCA> N0108UCA { get; set; }
        public virtual ICollection<N0108UCA> N0108UCA1 { get; set; }
        public virtual ICollection<N0111INVModel> N0111INV { get; set; }
        public virtual ICollection<N0202APR> N0202APR { get; set; }
        public virtual ICollection<N0202APR> N0202APR1 { get; set; }
        public virtual ICollection<N0202REQ> N0202REQ { get; set; }
        public virtual ICollection<N9999UXM> N9999UXM { get; set; }
        public virtual ICollection<SYS_CAIXA> SYS_CAIXA { get; set; }
        public virtual ICollection<SYS_CATEND> SYS_CATEND { get; set; }
        public virtual ICollection<SYS_CONCORRENCIA> SYS_CONCORRENCIA { get; set; }
        public virtual ICollection<SYS_LOGMSG> SYS_LOGMSG { get; set; }
        public virtual ICollection<SYS_LOGMSG> SYS_LOGMSG1 { get; set; }
        public virtual ICollection<SYS_LTELA> SYS_LTELA { get; set; }
        public virtual SYS_USRLOG SYS_USRLOG { get; set; }
        public virtual ICollection<SYS_USRON> SYS_USRON { get; set; }
        public virtual SYS_USRXREL SYS_USRXREL { get; set; }
        public virtual ICollection<SYS_USUARIO> SYS_USUARIO_LISTA { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO_MODEL { get; set; }
        public virtual ICollection<SYS_USRCOLUMN> SYS_USRCOLUMN { get; set; }
    }
}

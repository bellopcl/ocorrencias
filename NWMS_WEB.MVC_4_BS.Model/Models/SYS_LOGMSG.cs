
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_LOGMSG
    {
        public long CODMSG { get; set; }
        public string MENSAGEM { get; set; }
        public long CODUSU_REMETENTE { get; set; }
        public long CODUSU_DESTINATARIO { get; set; }
        public string BOTAO { get; set; }
        public string RESPOSTA { get; set; }
        public string ENTREGUE { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO { get; set; }
        public virtual SYS_USUARIO SYS_USUARIO1 { get; set; }
    }
}

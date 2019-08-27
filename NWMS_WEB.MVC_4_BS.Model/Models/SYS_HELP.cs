
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class SYS_HELP
    {
        public string ALTERADO { get; set; }
        public long CODHELP { get; set; }
        public string CONTEXTO { get; set; }
        public string DESCRICAO { get; set; }
        public string PALAVRAS_CHAVE { get; set; }
        public long TOPICO_IDX { get; set; }
        public string TOPICO_PAI { get; set; }
        public virtual SYS_HELPBODY SYS_HELPBODY { get; set; }
    }
}

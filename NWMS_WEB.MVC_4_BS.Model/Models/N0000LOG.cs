
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0000LOG
    {
        public long CODAGE { get; set; }
        public long SEQAGE { get; set; }
        public string DESLOG { get; set; }
        public System.DateTime DATGER { get; set; }
        public virtual N0000AGE N0000AGE { get; set; }
    }
}

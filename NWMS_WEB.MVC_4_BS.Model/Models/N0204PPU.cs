
namespace NUTRIPLAN_WEB.MVC_4_BS.Model.Models
{
    public class N0204PPU
    {
        public long IDROW { get; set; }
        public long? CODUSU { get; set; }
        public long QTDDEV { get; set; }
        public long QTDTRC { get; set; }
        public virtual N9999USU N9999USU { get; set; }
    }
}

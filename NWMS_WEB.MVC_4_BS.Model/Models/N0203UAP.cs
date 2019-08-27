
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0203UAP
    {
        public long CODORI { get; set; }
        public long CODUSU { get; set; }
        public long CODATD { get; set; }
        public virtual N0204ORI N0204ORI { get; set; }
        public virtual N0204ATD N0204ATD { get; set; }
        public virtual N9999USU N9999USU { get; set; }
    }
}


namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0204AUS
    {
        public long CODATD { get; set; }
        public long CODUSU { get; set; }
        public long IDROW { get; set; }
        public string SITREL { get; set; }
        public virtual N0204ATD N0204ATD { get; set; }
        public virtual N9999USU N9999USU { get; set; }
    }
}

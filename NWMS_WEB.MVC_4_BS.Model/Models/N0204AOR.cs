
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0204AOR
    {
        public long CODATD { get; set; }
        public long CODORI { get; set; }
        public long IDROW { get; set; }
        public string SITREL { get; set; }
        public virtual N0204ATD N0204ATD { get; set; }
        public virtual N0204ORI N0204ORI { get; set; }
    }
}

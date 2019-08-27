
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0204MDO
    {
        public long CODMDV { get; set; }
        public long CODORI { get; set; }
        public long IDROW { get; set; }
        public string SITREL { get; set; }
        public virtual N0204MDV N0204MDV { get; set; }
        public virtual N0204ORI N0204ORI { get; set; }
    }
}

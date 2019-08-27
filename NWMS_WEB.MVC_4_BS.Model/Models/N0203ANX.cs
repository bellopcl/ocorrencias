
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0203ANX
    {
        public decimal IDROW { get; set; }
        public long NUMREG { get; set; }
        public byte[] ANEXO { get; set; }
        public string NOMANX { get; set; }
        public string EXTANX { get; set; }
        public virtual N0203REG N0203REG { get; set; }
    }
}

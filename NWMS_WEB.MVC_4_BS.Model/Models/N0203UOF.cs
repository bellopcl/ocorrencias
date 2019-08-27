
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class N0203UOF
    {
        public long CODOPE { get; set; }
        public long CODUSU { get; set; }
        public long CODATD { get; set; }
        public virtual N0203OPE N0203OPE { get; set; }
        public virtual N0204ATD N0204ATD { get; set; }
        public virtual N9999USU N9999USU { get; set; }

    }
}

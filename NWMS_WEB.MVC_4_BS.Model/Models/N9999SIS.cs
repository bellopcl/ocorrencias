using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N9999SIS
    {
        public N9999SIS()
        {
            this.N9999MEN = new List<N9999MEN>();
        }

        public long CODSIS { get; set; }
        public string DESSIS { get; set; }
        public string BASESI { get; set; }
        public string VERSAO { get; set; }
        public virtual ICollection<N9999MEN> N9999MEN { get; set; }
    }
}

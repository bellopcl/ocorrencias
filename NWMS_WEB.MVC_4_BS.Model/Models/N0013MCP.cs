using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N0013MCP
    {
        public N0013MCP()
        {
            this.N0110ITD = new List<N0110ITD>();
        }

        public long CODMCP { get; set; }
        public string DESMCP { get; set; }
        public virtual ICollection<N0110ITD> N0110ITD { get; set; }
    }
}

using NUTRIPLAN_WEB.MVC_4_BS.Model.Models;
using System.Collections.Generic;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public partial class N9999USU
    {
        public N9999USU()
        {
            this.N9999USM = new List<N9999USM>();
            this.N0203UAP = new List<N0203UAP>();
            this.N0203UOF = new List<N0203UOF>();
            this.N0204AUS = new List<N0204AUS>();
            this.N0204PPU = new List<N0204PPU>();
        }

        public long CODUSU { get; set; }
        public string LOGIN { get; set; }
        public virtual ICollection<N9999USM> N9999USM { get; set; }
        public virtual ICollection<N0203UAP> N0203UAP { get; set; }
        public virtual ICollection<N0203UOF> N0203UOF { get; set; }
        public virtual ICollection<N0204AUS> N0204AUS { get; set; }
        public virtual ICollection<N0204PPU> N0204PPU { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUTRIPLAN_WEB.MVC_4_BS
{
    public partial class RelatorioOcorrenciasReabilitadas
    {
        public long Numreg { get; set; }
        public string Cliente { get; set; }
        public String DataTransacao { get; set; }
        public string Observacao { get; set; }
        public string operacao { get; set; }
        public string nomeUsuario { get; set; }
        public DateTime Emissao { get; set; }
    }
}

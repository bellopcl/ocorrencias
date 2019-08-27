using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class RelPermissaoUsuAproFaturamento
    {
        public string CodUsuario { get; set; }
        public string Usuario { get; set; }
        public string Operacao { get; set; }
        public string Tipo { get; set; }
    }
}

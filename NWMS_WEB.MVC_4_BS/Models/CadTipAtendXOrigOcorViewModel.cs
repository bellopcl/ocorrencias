using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class CadTipAtendXOrigOcorViewModel
    {
        [Display(Name = "Tipo de Atendimento")]
        public string TipoAtendimento { get; set; }

        public List<ListaN0204ATDPesquisa> ListaTipoAtendimento { get; set; }
    }
}
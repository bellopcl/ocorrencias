using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class CadMotDevXOrigOcorViewModel
    {
        [Display(Name = "Motivo de Devolução")]
        public string MotivoDevolucao { get; set; }

        public List<ListaN0204MDVPesquisa> ListaMotivoDevolucao { get; set; }
    }
}
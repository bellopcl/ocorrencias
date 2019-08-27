using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class VincularPlacaOcorrenciaViewModel
    {
        [Display(Name = "Nova Placa")]
        public string campoPlaca { get; set; }

        [Display(Name = "Nº Ocorrência")]
        public string campoNumeroRegistro { get; set; }
    }
}
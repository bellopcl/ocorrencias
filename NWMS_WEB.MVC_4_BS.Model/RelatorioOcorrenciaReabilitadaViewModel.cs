using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class OcorrenciaReabilitadaViewModel
    {
        [Display(Name = "Nº Ocorrência")]
        public string Numreg { get; set; }

        [Display(Name = "Data Operação")]
        public DateTime DataInicial { get; set; }

        [Display(Name = "Data Final")]
        public DateTime DataFinal { get; set; }

        [Display(Name = "Operação ")]
        public string operacao { get; set; }
    }
}

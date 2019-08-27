using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class CadOrigemOcorrenciaViewModel
    {
        [Required(ErrorMessage = "A origem da ocorrência deve ser preenchida.")]
        [Display(Name = "Origem da Ocorrência")]
        public string DescOrigemOcorrencia { get; set; }

        [Required(ErrorMessage = "A origem da ocorrência deve ser preenchida.")]
        [Display(Name = "Origem da Ocorrência")]
        public string DescOrigemOcorrenciaAlteracao { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class CadMotivoDevolucaoViewModel
    {
        [Required(ErrorMessage = "O motivo de devolução deve ser preenchido.")]
        [Display(Name = "Motivo de Devolução")]
        public string DescMotivoDev { get; set; }

        [Required(ErrorMessage = "O motivo de devolução deve ser preenchido.")]
        [Display(Name = "Motivo de Devolução")]
        public string DescMotivoDevAlteracao { get; set; }
    }
}
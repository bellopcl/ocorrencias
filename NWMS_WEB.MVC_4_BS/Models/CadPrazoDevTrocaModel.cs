using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class CadPrazoDevTrocaModel
    {
        [Required(ErrorMessage = "Campo obrigatório.<br/>")]
        [Range(1, 99999, ErrorMessage = "O Prazo de Devolução não pode ser zero.<br/>")]
        [Display(Name = "Prazo de Devolução")]
        public long PrazoDevolucao { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.<br/>")]
        [Range(1, 99999, ErrorMessage = "O Prazo de Troca não pode ser zero.<br/>")]
        [Display(Name = "Prazo de Troca")]
        public long PrazoDeTroca { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.<br/>")]
        [Range(1, 99999, ErrorMessage = "O Prazo de Devolução não pode ser zero.<br/>")]
        [Display(Name = "Prazo de Devolução")]
        public long PrazoDevolucaoPop { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.<br/>")]
        [Range(1, 99999, ErrorMessage = "O Prazo de Troca não pode ser zero.<br/>")]
        [Display(Name = "Prazo de Troca")]
        public long PrazoDeTrocaPop { get; set; }
    }
}
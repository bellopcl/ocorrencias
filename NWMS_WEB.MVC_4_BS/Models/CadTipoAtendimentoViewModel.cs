using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class CadTipoAtendimentoViewModel
    {
        [Required(ErrorMessage = "O tipo de atendimento deve ser preenchido.")]
        [Display(Name = "Tipo de Atendimento")]
        public string DescTipoAtendimento { get; set; }

        [Required(ErrorMessage = "O tipo de atendimento deve ser preenchido.")]
        [Display(Name = "Tipo de Atendimento")]
        public string DescTipoAtendimentoAlteracao { get; set; }
    }
}
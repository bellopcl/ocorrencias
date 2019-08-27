using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class RelatorioTransacaoViemModel
    {
        [Display(Name = "Nº Ocorrência")]
        [Required(ErrorMessage = "Digite um Registro de Ocorrência")]
        public string campoNumeroRegistro { get; set; }
    }
}
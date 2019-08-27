using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class MudarSenhaViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string SenhaAtual { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve conter no mínimo {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NovaSenha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a Nova Senha")]
        [Compare("NovaSenha", ErrorMessage = "A Nova senha e confirmação não correspondem.")]
        public string ConfirmarSenha { get; set; }
    }
}
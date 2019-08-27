using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class UsuarioXDashboardViewModel
    {
        [Required]
        [Display(Name = "Login do Usuário")]
        public string LoginUsuario { get; set; }

        [Required(ErrorMessage = "Favor realizar a pesquisa do Usuário")]
        [Display(Name = "Nome do Usuário")]
        public string NomeUsuario { get; set; }

    }
}
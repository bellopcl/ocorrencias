using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class CadUsuarioOperFatViewModel
    {
        [Required]
        [Display(Name = "Login do Usuário")]
        public string LoginUsuario { get; set; }

        [Required(ErrorMessage = "O nome do usuário deve ser pesquisado.")]
        public string NomeUsuario { get; set; }

        [Required]
        [Display(Name = "Tipo de Atendimento")]
        [Range(1, 999, ErrorMessage = "O Tipo de Atendimento deve ser selecionado.")]
        public int TipoAtendimento { get; set; }

        public List<ListaN0204ATDPesquisa> ListaTipoAtendimento { get; set; }
    }
}
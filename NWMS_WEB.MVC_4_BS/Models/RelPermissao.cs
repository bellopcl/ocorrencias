﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class RelPermissao
    {
        [Required]
        [Display(Name = "Login do Usuário")]
        public string LoginUsuario { get; set; }

        [Required(ErrorMessage = "O nome do usuário deve ser pesquisado.")]
        public string NomeUsuario { get; set; }
        
        [Display(Name = "Habilitados?")]
        public string Status { get; set; }
    }
}
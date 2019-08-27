using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login ")]
        [StringLength(50, MinimumLength = 2)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha ")]
        [StringLength(50, MinimumLength = 1)]
        public string Password { get; set; }


      

        public string versaoSistema { get; set; }

        public string baseSistema { get; set; }

        //[Display(Name = "Remember me?")]
        //public bool RememberMe { get; set; }
    }
   
}
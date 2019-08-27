using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using NUTRIPLAN_WEB.MVC_4_BS.Model;


namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class ConsultaSituacaoNotaViewModel
    {

        [Required(ErrorMessage = "Digite um Nota Fiscal")]
        [Display(Name = "Nº Nota")]
        public string NumNota { get; set; }

        [Required]
        [Display(Name = "Filial e NF.")]
        [Range(1, 999, ErrorMessage = "Selecione uma Filial")]
        public string campoFilial { get; set; }

       

        public string placa { get; set; }

        public string devolucaoMercadoria { get; set; }

        public string trocaMercadoria { get; set; }

        public string motorista { get; set; }
    }
}
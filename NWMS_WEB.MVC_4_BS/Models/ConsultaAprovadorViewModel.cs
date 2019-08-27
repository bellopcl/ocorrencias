using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class ConsultaAprovadorViewModel
    {
        [Required(ErrorMessage = "Selecione um Setor")]
        [Display(Name = "Setor")]
        public string descOrigem { get; set; }

        public int codigoOrigem { get; set; }

        [Display(Name = "Tipo de Atendimento")]
        public int TipoAtendimento { get; set; }


        public List<N0203UAP> ListaUsuarioAprovador { get; set; }
        public List<ListaN0204ORIPesquisa> ListaOrigemOcorrencia { get; set; }
        public List<ListaN0204ATDPesquisa> ListaTipoAtendimento { get; set; }
    }
}
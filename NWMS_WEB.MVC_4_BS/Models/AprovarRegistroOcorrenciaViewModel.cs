using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class AprovarRegistroOcorrenciaViewModel
    {
        [Required(ErrorMessage = "O campo Nº de Registro deve ser preenchido.")]
        [Display(Name = "Nº Ocorrência/Nº Agrupador")]
        public string NumRegistro { get; set; }

        //[Required(ErrorMessage = "O campo de Observações deve ser preenchido.")]
        [StringLength(400, MinimumLength = 0)]
        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        [Required(ErrorMessage = "O campo de Observação da Aprovação deve ser preenchido.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Esse campo não suporta mais que 500 caracteres.")]
        [Display(Name = "Observação Aprovação")]
        public string ObservacaoAprovacao { get; set; }

        [Display(Name = "Operação")]
        [Range(1, 999, ErrorMessage = "A Operação deve ser selecionada.")]
        public int Operacao { get; set; }

        [Required]
        [Display(Name = "Tipo Nota")]
        [Range(1, 999, ErrorMessage = "O Tipo da nota deve ser selecionado.")]
        public int TipoNota { get; set; }

        [Required]
        [Display(Name = "Tipo Operação")]
        [Range(1, 999, ErrorMessage = "O Tipo da operação deve ser selecionado.")]
        public int TipoOperacao { get; set; }

        public List<OperacaoAprovacaoModel> ListaOperacoesAprovacao { get; set; }
    }
}
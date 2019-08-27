using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class RelatorioAnaliticoViewModel
    {
        [Display(Name = "Pesquisar Por:")]
        public string TipoPesquisaReg { get; set; }

        [Required(ErrorMessage = "O {0} deve ser preenchido.")]
        [Display(Name = "Nº Ocorrência")]
        public string NumRegistro { get; set; }

        [Display(Name = "Anexos")]
        public string Anexos { get; set; }

        public string CodigoFilial { get; set; }

        [Required(ErrorMessage = "O Nº da Análise de Embarque deve ser preenchido.")]
        [Display(Name = "Filial e Nº Análise Emb.")]
        public string NumAnaliseEmbarque { get; set; }

        [Required(ErrorMessage = "O código do {0} deve ser preenchido.")]
        [Display(Name = "Cliente")]
        public string CodCliente { get; set; }

        [Display(Name = "Período")]
        public string Periodo { get; set; }

        [Required(ErrorMessage = "A {0} deve ser preenchida.<br/>")]
        [Display(Name = "Data Inicial")]
        public string DataInicial { get; set; }

        [Required(ErrorMessage = "A {0} deve ser preenchida.<br/>")]
        [Display(Name = "Data Final")]
        public string DataFinal { get; set; }
          [Required(ErrorMessage = "A {0} deve ser preenchida.<br/>")]
        [Display(Name = "Situação Reg.")]
        public string SitRegOcor { get; set; }

        [Required(ErrorMessage = "A placa deve ser preenchida.")]
        [Display(Name = "Placa")]
        public string CodPlaca { get; set; }

        [Required(ErrorMessage = "A {0} deve ser preenchida.")]
        [Display(Name = "Data de Faturamento")]
        public string DataFaturamento { get; set; }

        public List<SitRegOcorModel> ListaSituacaoRegistroOcorrencia { get; set; }

    }
}
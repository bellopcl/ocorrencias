using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class AgrupamentoViewModel
    {
        [Display(Name = "Cod. Cliente")]
        [Required(ErrorMessage = "Informe um Cliente ")]
        public string campoNumeroRegistro { get; set; }

        [Display(Name = "Cod. Cliente")]
        [Required(ErrorMessage = "Informe um Cliente ")]
        public string campoCodigoCliente { get; set; }

        [Required(ErrorMessage = "Digite um Cliente")]
        [Display(Name = "Cliente")]
        public string campoCliente { get; set; }

        [Required]
        [Display(Name = "Situação")]
        [Range(1, 999, ErrorMessage = "O Tipo de Situção deve ser selecionado")]
        public string campoSituacao { get; set; }


        [Required(ErrorMessage = "Digite a Data Inicial")]
        [Display(Name = "Período OCR.")]
        public string campoDataInicial { get; set; }

        [Required(ErrorMessage = "Digite a Data Final")]
        [Display(Name = " ")]
        public string campoDataFinal { get; set; }

        [Required]
        [Display(Name = "Filial e Nº Análise Emb.")]
        [Range(1, 999, ErrorMessage = "Selecione uma Filial")]
        public string campoFilial { get; set; }

        [Required(ErrorMessage = "Digite o Nº Análise Embarque")]
        [Display(Name = "Digite o Embarque")]
        public string campoEmbarque { get; set; }

        [Required(ErrorMessage = "Digite uma Placa")]
        [Display(Name = "Placa")]
        public string campoPlaca { get; set; }

        [Required(ErrorMessage = "Digite uma Data de Faturamento")]
        [Display(Name = "Data de Faturamento")]
        public string campoDataFaturamento { get; set; }
    }
}
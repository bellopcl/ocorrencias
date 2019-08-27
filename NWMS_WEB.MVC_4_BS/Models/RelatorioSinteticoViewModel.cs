using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class RelatorioSinteticoViewModel
    {
        [Display(Name = "Pesquisar Por:")]
        public string TipoPesquisaReg { get; set; }

        public string CodigoFilial { get; set; }

        [Required(ErrorMessage = "O Nº da Análise de Embarque deve ser preenchido.")]
        [Display(Name = "Filial e Nº Análise Emb.")]
        public string NumAnaliseEmbarque { get; set; }

        [Required(ErrorMessage = "A placa deve ser preenchida.")]
        [Display(Name = "Placa")]
        public string CodPlacaDatFat { get; set; }

        [Required(ErrorMessage = "A placa deve ser preenchida.")]
        [Display(Name = "Placa")]
        public string CodPlacaDatFatTroca { get; set; }


        [Required(ErrorMessage = "A placa deve ser preenchida.")]
        [Display(Name = "Placa")]
        public string CodPlacaPer { get; set; }

        [Display(Name = "Período OCR.")]
        public string Periodo { get; set; }

        [Required(ErrorMessage = "O {0} deve ser preenchida.")]
        [Display(Name = "Período de Faturamento")]
        public string DataFaturamento { get; set; }

        [Required(ErrorMessage = "O {0} deve ser preenchida.")]
        [Display(Name = "Período de Faturamento")]
        public string DataFaturamentoTroca { get; set; }

        [Required(ErrorMessage = "A {0} deve ser preenchida.<br/>")]
        [Display(Name = "Data Inicial")]
        public string DataInicial { get; set; }

        [Required(ErrorMessage = "A {0} deve ser preenchida.<br/>")]
        [Display(Name = "Data Final")]
        public string DataFinal { get; set; }

        [Display(Name = "Nº Ocorrência")]
        [Required(ErrorMessage = "Digite um Registro de Ocorrência")]
        public string campoNumeroRegistro { get; set; }

        [Required(ErrorMessage = "Digite um Cliente")]
        [Display(Name = "Cliente")]
        public string campoCliente { get; set; }

        [Required]
        [Display(Name = "Situação")]
        [Range(1, 999, ErrorMessage = "O Tipo de Situação deve ser selecionado")]
        public string campoSituacao { get; set; }

        [Required]
        [Display(Name = "Tipo de Atendimento")]
        [Range(1, 999, ErrorMessage = "O Tipo deve ser selecionado")]
        public string campoTipo { get; set; }

        [Required]
        [Display(Name = "Motivo")]
        [Range(1, 999, ErrorMessage = "O Motivo deve ser selecionado")]
        public string campoMotivo { get; set; }

        [Required]
        [Display(Name = "Origem")]
        [Range(1, 999, ErrorMessage = "A origem deve ser selecionado")]
        public string campoOrigem { get; set; }

        [Required(ErrorMessage = "Digite a Data Inicial")]
        [Display(Name = "Período de Faturamento")]
        public string campoDataInicial { get; set; }

        [Required(ErrorMessage = "Digite a Data Final")]
        [Display(Name = " ")]
        public string campoDataFinal { get; set; }

            
        [Required(ErrorMessage = "Digite a Data Inicial")]
        [Display(Name = "Período OCR.")]
        public string campoDataInicialOCR { get; set; }

        [Required(ErrorMessage = "Digite a Data Final")]
        [Display(Name = " ")]
        public string campoDataFinalOCR { get; set; }

        [Required]
        [Display(Name = "Filial e Nº Análise Emb.")]
        [Range(1, 999, ErrorMessage = "Selecione uma Filial")]
        public string campoFilial { get; set; }

        [Display(Name = "Transportadora")]
        public string campoTransportadora { get; set; }

        [Required(ErrorMessage = "Digite o Nº Análise Embarque")]
        [Display(Name = "Digite o Embarque")]
        public string campoEmbarque { get; set; }

        [Required(ErrorMessage = "Digite uma Placa")]
        [Display(Name = "Placa")]
        public string campoPlaca { get; set; }

        [Required(ErrorMessage = "Digite uma Data de Faturamento")]
        [Display(Name = "Data de Faturamento")]
        public string campoDataFaturamento { get; set; }

        [Required(ErrorMessage = "O código do cliente deve ser preenchido.")]
        [Display(Name = "Cliente")]
        public string CodCliente { get; set; }

        [Required(ErrorMessage = "O nome do cliente deve ser pesquisado.")]
        public string NomeCliente { get; set; }

        public List<TipoPesquisaRegModel> ListaTipoPesquisaReg { get; set; }
        public List<SitRegOcorModel> ListaSituacaoRegistroOcorrencia { get; set; }

        public List<ListaN0204ATDPesquisa> listaTipoAtendimento { get; set; }
        public List<ListaN0204MDVPesquisa> listaMotivoDevolucao { get; set; }
        public List<ListaN0204ORIPesquisa> listaOrigemOcorrencia { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.ComponentModel.DataAnnotations;
namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class CadLancRegistroOcorrenciaViewModel
    {
        [Required]
        [Display(Name = "Tipo de Atendimento")]
        [Range(1, 999, ErrorMessage = "O Tipo de Atendimento deve ser selecionado.")]
        public int TipoAtendimento { get; set; }

        [Required]
        [Display(Name = "Origem da Ocorrência")]
        [Range(1, 999, ErrorMessage = "A Origem da Ocorrência deve ser selecionada.")]
        public long OrigemOcorrencia { get; set; }
        
        [Required(ErrorMessage = "O Nº da Nota deve ser preenchido.")]
        [Display(Name = "Nº Nota")]
        public string NumNotaSug { get; set; }
        
        public string DescNumNotaSug { get; set; }
        
        [Required(ErrorMessage = "O Nº do Reg. Reprovado deve ser preenchido.")]
        [Display(Name = "Reg. Reprovado")]
        public string NumRegReprovado { get; set; }
        
        public string DescNumRegReprovado { get; set; }
        
        [Required(ErrorMessage = "O código do cliente deve ser preenchido.")]
        [Display(Name = "Cliente")]
        public string CodCliente { get; set; }
        
        [Required(ErrorMessage = "O nome do cliente deve ser pesquisado.")]
        public string NomeCliente { get; set; }
        
        [Required(ErrorMessage = "O código do motorista deve ser preenchido.")]
        [Display(Name = "Motorista")]
        public string CodMotorista { get; set; }
        
        [Required(ErrorMessage = "O nome do motorista deve ser pesquisado.")]
        public string NomeMotorista { get; set; }
        
        [Required(ErrorMessage = "A placa deve ser preenchida.")]
        [Display(Name = "Placa")]
        public string CodPlaca { get; set; }
        
        [Required(ErrorMessage = "A placa do veículo deve ser pesquisada.")]
        public string DescricaoPlaca { get; set; }
        
        [Display(Name = "Empresa")]
        public string Empresa { get; set; }
        
        [Display(Name = "Filial")]
        public string Filial { get; set; }
        
        [Display(Name = "Série")]
        public string Serie { get; set; }
        
        [Display(Name = "Nº Nota")]
        public string NumNotaFiscal { get; set; }
        
        [Display(Name = "Origem da Ocorrência")]
        public string OrigemOcorrenciaFase2 { get; set; }
        
        [Required(ErrorMessage = "O Motivo de devolução deve ser selecionado.")]
        [Display(Name = "Motivo de Devolução")]
        public string MotivoDevolucao { get; set; }
        
        [Display(Name = "Qtde. Total Devolvida")]
        public string QtdeTotal { get; set; }
        
        [Display(Name = "Val. Bruto Devolvido")]
        public string ValorBruto { get; set; }
        
        [Display(Name = "Val. IPI Devolvido")]
        public string ValorIpi { get; set; }
        
        [Display(Name = "Val. ST Devolvido")]
        public string ValorSt { get; set; }

        [Display(Name = "Desc. Suframa")]
        public string DescontoSuframa { get; set; }

        [Display(Name = "Val. Líquido Devolvido")]
        public string ValorLiquido { get; set; }
        
        [Display(Name = "Quantidade Total Devolvida")]
        public string QtdeTotalDevolucao { get; set; }
        
        [Display(Name = "Valor Bruto Devolvido")]
        public string ValorBrutoDevolucao { get; set; }
        
        [Display(Name = "Valor IPI Devolvido")]
        public string ValorIpiDevolucao { get; set; }
        
        [Display(Name = "Valor Total S.T. Devolvido")]
        public string ValorStDevolucao { get; set; }
        
        [Required(ErrorMessage = "Nº da Ocorrência")]
        [Display(Name = "Nº Ocorrência")]
        public string numeroOcorrencia { get; set; }
        
        [Display(Name = "Valor Líquido Devolvido")]
        public string ValorLiquidoDevolucao { get; set; }
        
        [Display(Name = "Valor Total Notas Faturadas")]
        public string ValorTotalNotas { get; set; }
        
        [Display(Name = "Valor a Receber")]
        public string ValorReceber { get; set; }
        
        [Display(Name = "Arquivo")]
        public List<HttpPostedFileBase> AnexoArquivo { get; set; }
        
        [Required(ErrorMessage = "O campo de observações deve ser preenchido.")]
        [Display(Name = "Observações")]
        [StringLength(400, MinimumLength = 1)]
        public string Observacoes { get; set; }
        
        [Required(ErrorMessage = "A Quantidade de Devolução deve ser preenchida.<br/>")]
        [Range(1, 99999, ErrorMessage = "A Quantidade de Devolução não pode ser zero.<br/>")]
        [Display(Name = "Quantidade Devolução")]
        public int QtdeDevolucaoItemNota { get; set; }
        
        [Required]
        [Display(Name = "Motivo de Devolução")]
        [Range(1, 999, ErrorMessage = "O Motivo de Devolução deve ser selecionado.<br/>")]
        public int MotivoDevolucaoItemNota { get; set; }
        
        [Required]
        [Display(Name = "Origem da Ocorrência")]
        [Range(1, 999, ErrorMessage = "A Origem da Ocorrência deve ser selecionada.")]
        public int OrigemOcorrenciaItemNota { get; set; }

        [Display(Name ="Transportadora")]
        public int CodTra { get; set;  }

        public string NomeTra { get; set; }

        public int Aprovado { get; set; }
        
        public List<ListaN0204ATDPesquisa> ListaTipoAtendimento { get; set; }
        
        public List<ListaN0204MDVPesquisa> ListaMotivoDevolucao { get; set; }
        
        public List<ListaN0204ORIPesquisa> ListaOrigemOcorrencia { get; set; }
        
        public string ListaItensDevolucao { get; set; }
        
        public string Acao { get; set; }
        
        public string MensagemRetorno { get; set; }
    }
}
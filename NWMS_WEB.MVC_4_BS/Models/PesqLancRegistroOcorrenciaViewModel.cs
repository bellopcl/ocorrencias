using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.ComponentModel.DataAnnotations;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class PesqLancRegistroOcorrenciaViewModel
    {
        [Required(ErrorMessage = "O campo Nº de Registro deve ser preenchido.")]
        [Display(Name = "Nº Ocorrência")]
        public string NumRegistro { get; set; }

        [Required]
        [Display(Name = "Tipo de Atendimento")]
        public string TipoAtendimento { get; set; }

        [Display(Name = "Reg. Reprovado")]
        public string NumRegReprovado { get; set; }

        public string DescNumRegReprovado { get; set; }

        //[Required]
        [Display(Name = "Cliente")]
        public string CodCliente { get; set; }

        public string NomeCliente { get; set; }

        //[Required]
        [Display(Name = "Motorista")]
        public string CodMotorista { get; set; }

        public string NomeMotorista { get; set; }

        //[Required(ErrorMessage = "A placa deve ser preenchida.")]
        [Display(Name = "Placa")]
        public string CodPlaca { get; set; }

        //[Required(ErrorMessage = "A placa do veículo deve ser pesquisada.")]
        public string DescricaoPlaca { get; set; }

        [Display(Name = "Data/Hora Geração")]
        public string DataHoraGeracao { get; set; }

        [Display(Name = "Usuário Geração")]
        public string UsuarioGeracao { get; set; }

        [Display(Name = "Situação Registro")]
        public string SituacaoRegistro { get; set; }

        [Display(Name = "Última Alteração")]
        public string UltimaAlteracao { get; set; }

        [Display(Name = "Usuário")]
        public string UsuarioUltimaAlteracao { get; set; }

        [Display(Name = "Empresa")]
        public string Empresa { get; set; }

        [Display(Name = "Filial")]
        public string Filial { get; set; }

        [Display(Name = "Série")]
        public string Serie { get; set; }

        [Display(Name = "Nº Nota")]
        public string NumNotaFiscal { get; set; }

        [Display(Name = "Motivo de Devolução")]
        public string MotivoDevolucao { get; set; }
        
        [Display(Name = "Origem da Ocorrência")]
        public string OrigemOcorrencia { get; set; }

        [Display(Name = "Qtde. Total Devolvida")]
        public string QtdeTotal { get; set; }

        [Display(Name = "Val. Bruto Devolvido")]
        public string ValorBruto { get; set; }

        [Display(Name = "Val. IPI Devolvido")]
        public string ValorIpi { get; set; }

        [Display(Name = "Val. ST Devolvido")]
        public string ValorSt { get; set; }

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

        [Display(Name = "Valor Líquido Devolvido")]
        public string ValorLiquidoDevolucao { get; set; }

        [Display(Name = "Valor Total Notas Faturadas")]
        public string ValorTotalNotas { get; set; }

        [Display(Name = "Valor a Receber")]
        public string ValorReceber { get; set; }

        [Display(Name = "Arquivo")]
        public List<HttpPostedFileBase> AnexoArquivo { get; set; }

        [Display(Name = "Anexos")]
        public string Anexos { get; set; }

        [Required(ErrorMessage = "O campo de Observações deve ser preenchido.")]
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

        [Display(Name = "Transportadora")]
        public int CodTra { get; set; }

        public string NomeTra { get; set; }

        public List<ListaN0204ATDPesquisa> ListaTipoAtendimento { get; set; }

        public List<ListaN0204MDVPesquisa> ListaMotivoDevolucao { get; set; }

        public List<ListaN0204ORIPesquisa> ListaOrigemOcorrencia { get; set; }

        public string ListaItensDevolucao { get; set; }

        public string Acao { get; set; }

        public string MensagemRetorno { get; set; }
    }
}
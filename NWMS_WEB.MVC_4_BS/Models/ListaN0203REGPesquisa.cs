using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.Business;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class ListaN0203REGPesquisa
    {
        public long CodigoRegistro { get; set; }
        public long CodTipoAtendimento { get; set; }
        public string DescTipoAtendimento { get; set; }
        public long CodOrigemOcorrencia { get; set; }
        public string DescOrigemOcorrencia { get; set; }
        public long? NumRegReprovado { get; set; }
        public string DescNumRegReprovado { get; set; }
        public long CodCliente { get; set; }
        public string NomeCliente { get; set; }
        public string CnpjCliente { get; set; }
        public string InscricaoEstadualCliente { get; set; }
        public long CodMotorista { get; set; }
        public string NomeMotorista { get; set; }
        public string DataHrGeracao { get; set; }
        public long UsuarioGeracao { get; set; }
        public string NomeUsuarioGeracao { get; set; }
        public long CodSituacaoRegistro { get; set; }
        public string DescSituacaoRegistro { get; set; }
        public string UltimaAlteracao { get; set; }
        public long UsuarioUltimaAlteracao { get; set; }
        public string NomeUsuarioUltimaAlteracao { get; set; }
        public string DataFechamento { get; set; }
        public long? UsuarioFechamento { get; set; }
        public string NomeUsuarioFechamento { get; set; }
        public string Observacao { get; set; }
        public string CodPlaca { get; set; }
        public string DescPlaca { get; set; }
        public long CodTra { get; set; }

        public static explicit operator ListaN0203REGPesquisa(N0203REG N0203REG)
        {
            
            var item = new ListaN0203REGPesquisa();
            var listaClientes = new List<E085CLIModel>();
            var E085CLIBusiness = new E085CLIBusiness();
            var E073MOTBusiness = new E073MOTBusiness();
            var N0204ATDBusiness = new N0204ATDBusiness();
            var N0204ORIBusiness = new N0204ORIBusiness();
            var E073VEIBusiness = new E073VEIBusiness();
            var N0203REGBusiness = new N0203REGBusiness();
            var N9999USUBusiness = new N9999USUBusiness();
            var ActiveDirectoryBusiness = new ActiveDirectoryBusiness();
            var loginUsuario = string.Empty;

            item.CodigoRegistro = N0203REG.NUMREG;
            item.CodTipoAtendimento = N0203REG.TIPATE;
            item.DescTipoAtendimento = N0204ATDBusiness.PesquisaTipoAtendimento().Where(c => c.CODATD == N0203REG.TIPATE).FirstOrDefault().DESCATD;
            item.CodOrigemOcorrencia = N0203REG.ORIOCO;
            item.DescOrigemOcorrencia = N0204ORIBusiness.PesquisaOrigemOcorrencia().Where(c => c.CODORI == N0203REG.ORIOCO).FirstOrDefault().DESCORI;

            item.CodCliente = N0203REG.CODCLI;
            listaClientes = E085CLIBusiness.PesquisaClientes(N0203REG.CODCLI);
            item.NomeCliente = listaClientes.FirstOrDefault().NomeFantasia;
            item.CnpjCliente = listaClientes.FirstOrDefault().CnpjCpf;
            item.InscricaoEstadualCliente = listaClientes.FirstOrDefault().InscricaoEstadual;

            item.CodTra = N0203REG.TRACLI;

            item.CodMotorista = N0203REG.CODMOT;
            item.NomeMotorista = "TRANSPORTADORA";
            if (N0203REG.CODMOT != 0)
            {
                item.NomeMotorista = E073MOTBusiness.PesquisasMotoristas(N0203REG.CODMOT).FirstOrDefault().Nome;
            }

            item.DataHrGeracao = N0203REG.DATGER.ToString();
            item.UsuarioGeracao = N0203REG.USUGER;

            loginUsuario = N9999USUBusiness.ListaDadosUsuarioPorCodigo(N0203REG.USUGER).LOGIN;
            item.NomeUsuarioGeracao = ActiveDirectoryBusiness.ListaDadosUsuarioAD(loginUsuario).Nome;

            item.CodSituacaoRegistro = N0203REG.SITREG;

            if (N0203REG.SITREG == (int)Enums.SituacaoRegistroOcorrencia.Pendente)
            {
                item.DescSituacaoRegistro = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.SituacaoRegistroOcorrencia.Pendente).GetValue<string>();
            }
            else if (N0203REG.SITREG == (int)Enums.SituacaoRegistroOcorrencia.Reprovado)
            {
                item.DescSituacaoRegistro = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.SituacaoRegistroOcorrencia.Reprovado).GetValue<string>();
            }
            
            item.UltimaAlteracao = N0203REG.DATULT.ToString();
            item.UsuarioUltimaAlteracao = N0203REG.USUULT;

            loginUsuario = N9999USUBusiness.ListaDadosUsuarioPorCodigo(N0203REG.USUULT).LOGIN;
            item.NomeUsuarioUltimaAlteracao = ActiveDirectoryBusiness.ListaDadosUsuarioAD(loginUsuario).Nome;

            item.Observacao = N0203REG.OBSREG;
            item.CodPlaca = string.Empty;
            item.DescPlaca = string.Empty;

            if (!string.IsNullOrEmpty(N0203REG.PLACA))
            {
                var caminhao = E073VEIBusiness.PesquisarCaminhaoPorPlaca(N0203REG.PLACA).FirstOrDefault();
                item.CodPlaca = caminhao.Placa;
                item.DescPlaca = caminhao.TipoCaminhao;
            }

            return item;
        }
    }
}
using System.Collections.Generic;
using System.Web.Mvc;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NWORKFLOW_WEB.MVC_4_BS.Views.Util;
using NWORKFLOW_WEB.MVC_4_BS.Models;

namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    [ValidateInput(false)]
    public class BaseController : Controller
    {
        /// <summary>
        /// Obtém ou define um valor que indica se o usuário está logado.
        /// </summary>
        public string Logado
        {
            get
            {
                return (string)this.Session[SessionKeys.Logado];
            }
            set
            {
                this.Session[SessionKeys.Logado] = value;
            }
        }

        /// <summary>
        /// Obtém ou define nome do usuário logado.
        /// </summary>
        public string NomeUsuarioLogado
        {
            get
            {
                return (string)this.Session[SessionKeys.NomeUsuarioLogado];
            }

            set
            {
                this.Session[SessionKeys.NomeUsuarioLogado] = value;
            }
        }

        /// <summary>
        /// Obtém ou define login do usuário
        /// </summary>
        public string LoginUsuario
        {
            get
            {
                return (string)this.Session[SessionKeys.LoginUsuario];
            }

            set
            {
                this.Session[SessionKeys.LoginUsuario] = value;
            }
        }

        /// <summary>
        /// Obtém ou define código do usuário logado
        /// </summary>
        public string CodigoUsuarioLogado
        {
            get
            {
                return (string)this.Session[SessionKeys.CodigoUsuarioLogado];
            }

            set
            {
                this.Session[SessionKeys.CodigoUsuarioLogado] = value;
            }
        }

        /// <summary>
        /// Obtém ou define operação pesquisar para o usuário logado
        /// </summary>
        public string OperacaoPesquisar
        {
            get
            {
                return (string)this.Session[SessionKeys.OperacaoPesquisar];
            }

            set
            {
                this.Session[SessionKeys.OperacaoPesquisar] = value;
            }
        }

        /// <summary>
        /// Obtém ou define operação Cadastrar para o usuário logado
        /// </summary>
        public string OperacaoInserir
        {
            get
            {
                return (string)this.Session[SessionKeys.OperacaoInserir];
            }

            set
            {
                this.Session[SessionKeys.OperacaoInserir] = value;
            }
        }

        /// <summary>
        /// Obtém ou define operação Alterar para o usuário logado
        /// </summary>
        public string OperacaoAlterar
        {
            get
            {
                return (string)this.Session[SessionKeys.OperacaoAlterar];
            }

            set
            {
                this.Session[SessionKeys.OperacaoAlterar] = value;
            }
        }

        /// <summary>
        /// Obtém ou define operação Excluir para o usuário logado
        /// </summary>
        public string OperacaoExcluir
        {
            get
            {
                return (string)this.Session[SessionKeys.OperacaoExcluir];
            }

            set
            {
                this.Session[SessionKeys.OperacaoExcluir] = value;
            }
        }

        /// <summary>
        /// Obtém ou define Empresa
        /// </summary>
        public string Empresa
        {
            get
            {
                return (string)this.Session[SessionKeys.Empresa];
            }

            set
            {
                this.Session[SessionKeys.Empresa] = value;
            }
        }

        /// <summary>
        /// Obtém ou define Filial da Empresa
        /// </summary>
        public string EmpresaFilial
        {
            get
            {
                return (string)this.Session[SessionKeys.EmpresaFilial];
            }

            set
            {
                this.Session[SessionKeys.EmpresaFilial] = value;
            }
        }

        /// <summary>
        /// Obtém ou define Armazem da filial da empresa
        /// </summary>
        public string EmpresaFilialArmazem
        {
            get
            {
                return (string)this.Session[SessionKeys.EmpresaFilialArmazem];
            }

            set
            {
                this.Session[SessionKeys.EmpresaFilialArmazem] = value;
            }
        }

        /// <summary>
        /// Obtém ou define Nome Abreviado da Empresa
        /// </summary>
        public string NomeAbreviadoEmpresa
        {
            get
            {
                return (string)this.Session[SessionKeys.NomeAbreviadoEmpresa];
            }

            set
            {
                this.Session[SessionKeys.NomeAbreviadoEmpresa] = value;
            }
        }

        /// <summary>
        /// Obtém ou define Cnpj da empresa
        /// </summary>
        public string CnpjEmpresa
        {
            get
            {
                return (string)this.Session[SessionKeys.CnpjEmpresa];
            }

            set
            {
                this.Session[SessionKeys.CnpjEmpresa] = value;
            }
        }
        /// <summary>
        /// Obtém ou define Endereço da empresa
        /// </summary>
        public string EnderecoEmpresa
        {
            get
            {
                return (string)this.Session[SessionKeys.EnderecoEmpresa];
            }

            set
            {
                this.Session[SessionKeys.EnderecoEmpresa] = value;
            }
        }

        /// <summary>
        /// Obtém ou define Cep da Empresa
        /// </summary>
        public string CepEmpresa
        {
            get
            {
                return (string)this.Session[SessionKeys.CepEmpresa];
            }

            set
            {
                this.Session[SessionKeys.CepEmpresa] = value;
            }
        }

        /// <summary>
        /// Obtém ou define permissões de acesso (menu) para o usuário logado
        /// </summary>
        public List<ListaN0203TRAPesquisa> TramitesNotificao
        {
            get
            {
                return (List<ListaN0203TRAPesquisa>)this.Session[SessionKeys.TramitesNotofication];
            }

            set
            {
                this.Session[SessionKeys.TramitesNotofication] = value;
            }
        }
         /// <summary>
        /// Obtém ou define permissões de acesso (menu) para o usuário logado
        /// </summary>
        public List<ProtocolosAprovacaoModel> ProtocolosPendentes
        {
            get
            {
                return (List<ProtocolosAprovacaoModel>)this.Session[SessionKeys.ProtocolosPendente];
            }

            set
            {
                this.Session[SessionKeys.ProtocolosPendente] = value;
            }
        }
        
        /// <summary>
        /// Obtém ou define permissões de acesso (menu) para o usuário logado
        /// </summary>
        public List<MenuModel> PermissoesDeAcesso
        {
            get
            {
                return (List<MenuModel>)this.Session[SessionKeys.PermissoesDeAcesso];
            }

            set
            {
                this.Session[SessionKeys.PermissoesDeAcesso] = value;
            }
        }

        /// <summary>
        /// Obtém ou define permissões de acesso (tela de gerenciamento de permissões) para o usuário pesquisado
        /// </summary>
        public List<MenuModel> PermissoesDeAcessoGerenciamento
        {
            get
            {
                return (List<MenuModel>)this.Session[SessionKeys.PermissoesDeAcessoGerenciamento];
            }

            set
            {
                this.Session[SessionKeys.PermissoesDeAcessoGerenciamento] = value;
            }
        }
    }
}

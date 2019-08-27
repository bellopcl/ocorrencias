using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NWORKFLOW_WEB.MVC_4_BS.Views.Util
{
    public static class SessionKeys
    {
        /// <summary>
        /// Chave para verificar se o usuário está logado
        /// </summary>
        public const string Logado = "LOGADO";

        /// <summary>
        /// Chave para acessar o nome do usuário logado
        /// </summary>
        public const string NomeUsuarioLogado = "NOME";

        public const string LoginUsuario = "LOGIN_USUARIO";

        public const string CodigoUsuarioLogado = "CODIGO_USUARIO_LOGADO";

        public const string PermissoesDeAcesso = "PERMISSOES_DE_ACESSO";

        public const string TramitesNotofication = "TRAMITES_NOTIFICAO";
        
        public const string ProtocolosPendente = "PROTOCOLOS_PENDENTES";

        public const string PermissoesDeAcessoGerenciamento = "PERMISSOES_DE_ACESSO_GERENCIAMENTO";

        public const string OperacaoPesquisar = "OPERACAO_PESQUISAR";

        public const string OperacaoInserir = "OPERACAO_INSERIR";

        public const string OperacaoAlterar = "OPERACAO_ALTERAR";

        public const string OperacaoExcluir = "OPERACAO_EXCLUIR";

        public const string Empresa = "EMPRESA";

        public const string NomeAbreviadoEmpresa = "NOME_ABREVIADO_EMPRESA";

        public const string CnpjEmpresa = "CNPJ_EMPRESA";

        public const string EnderecoEmpresa = "ENDERECO_EMPRESA";

        public const string CepEmpresa = "CEP_EMPRESA";

        public const string EmpresaFilial = "EMPRESA_FILIAL";

        public const string EmpresaFilialArmazem = "EMPRESA_FILIAL_ARMAZEM";

    }
}
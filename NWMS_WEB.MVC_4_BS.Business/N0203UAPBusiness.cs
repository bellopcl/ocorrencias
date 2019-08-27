using System;
using System.Collections.Generic;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess;

namespace NUTRIPLAN_WEB.MVC_4_BS.Business
{
    /// <summary>
    /// Classe utilizada para chamar a classe de conexão com o banco de dados
    /// utilizar esta classe para fazer a implementação das regras de negócios
    /// </summary>
    public class N0203UAPBusiness
    {
        /// <summary>
        /// Chama a classe de Pesquisa de usuário aprovador por origem de ocorrência
        /// </summary>
        /// <param name="codUsuarioAprovador">Código do Usuário Aprovador</param>
        /// <param name="tipoAtendimento">Tipo de Atendimento</param>
        /// <returns></returns>
        public List<RelUsuAprovadorOrigemOcorrencia> PesquisaUsuarioAprovadorXOrigemOcorrencia(long codUsuarioAprovador, Enums.TipoAtendimento tipoAtendimento)
        {
            try
            {
                N0203UAPDataAccess N0203UAPDataAccess = new N0203UAPDataAccess();
                return N0203UAPDataAccess.PesquisaUsuarioAprovadorXOrigemOcorrencia(codUsuarioAprovador, tipoAtendimento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para gravar o usuário aprovador por origem de ocorrência
        /// </summary>
        /// <param name="codUsuarioAprovador">Código de Usuário Aprovador</param>
        /// <param name="listaUsuarioOrigens">Lista de origens de usuários</param>
        /// <param name="tipoAtendimento">Tipo de Atendimento</param>
        /// <returns></returns>
        public bool GravarUsuarioAprovadorXOrigemOcorrencia(long codUsuarioAprovador, List<N0203UAP> listaUsuarioOrigens, Enums.TipoAtendimento tipoAtendimento)
        {
            try
            {
                N0203UAPDataAccess N0203UAPDataAccess = new N0203UAPDataAccess();
                return N0203UAPDataAccess.GravarUsuarioAprovadorXOrigemOcorrencia(codUsuarioAprovador, listaUsuarioOrigens, tipoAtendimento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para pesquisar o usuário aprovador por origem de ocorrência
        /// </summary>
        /// <param name="listaOrigens">Lista de origens</param>
        /// <param name="tipoAtendimento">Tipo de Atendimento</param>
        /// <returns></returns>
        public string PesquisaUsuariosAprovadoresPorOrigens(List<long> listaOrigens, Enums.TipoAtendimento tipoAtendimento)
        {
            try
            {
                N0203UAPDataAccess N0203UAPDataAccess = new N0203UAPDataAccess();
                var usuarios = N0203UAPDataAccess.PesquisaUsuariosAprovadoresPorOrigens(listaOrigens, tipoAtendimento);

                // Busca emails dos usuários aprovadores
                string emails = string.Empty;

                var N9999USUDataAccess = new N9999USUDataAccess();
                var ActiveDirectoryDataAccess = new ActiveDirectoryDataAccess();

                foreach (var codUsu in usuarios)
                {
                    // Busca código do usuário
                    var loginUsuario = N9999USUDataAccess.ListaDadosUsuarioPorCodigo(codUsu).LOGIN;
                    var email = ActiveDirectoryDataAccess.ListaDadosUsuarioAD(loginUsuario).Email;

                    emails = emails + email + "&";
                }

                return emails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para pesquisar usuários aprovadores 
        /// </summary>
        /// <param name="codigoOrigem">Código de Origem </param>
        /// <param name="codigoAtedimento">Tipo de Atendimento</param>
        /// <returns></returns>
        public List<N0203UAP> PesquisarUsuariosAprovadores(long codigoOrigem, long codigoAtedimento)
        {
            N0203UAPDataAccess N0203UAPDataAccess = new N0203UAPDataAccess();
            var usuariosAprovadores = N0203UAPDataAccess.PesquisarUsuariosAprovadores(codigoOrigem, codigoAtedimento);
            return usuariosAprovadores;
        }
    }
}

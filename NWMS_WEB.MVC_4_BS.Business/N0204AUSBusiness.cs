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
    public class N0204AUSBusiness
    {
        /// <summary>
        /// Chama a classe para pesquisar o tipo de atendimetno por usuário
        /// </summary>
        /// <param name="codUsuarioAprovador">Código do Usuário Aprovador</param>
        /// <returns>PesquisarTipoAtendimentoUsuario</returns>
        public List<RelTipoAtendUsuario> PesquisarTipoAtendimentoUsuario(long codUsuarioAprovador)
        {
            try
            {
                var N0204AUSDataAccess = new N0204AUSDataAccess();
                return N0204AUSDataAccess.PesquisarTipoAtendimentoUsuario(codUsuarioAprovador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para gravar o tipo de atendimento por usuário
        /// </summary>
        /// <param name="codUsuario"></param>
        /// <param name="listaTipoAtendiUsuarios"></param>
        /// <returns>GravarTipoAtendimentoUsuario</returns>
        public bool GravarTipoAtendimentoUsuario(long codUsuario, List<N0204AUS> listaTipoAtendiUsuarios)
        {
            try
            {
                var N0204AUSDataAccess = new N0204AUSDataAccess();
                return N0204AUSDataAccess.GravarTipoAtendimentoUsuario(codUsuario, listaTipoAtendiUsuarios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

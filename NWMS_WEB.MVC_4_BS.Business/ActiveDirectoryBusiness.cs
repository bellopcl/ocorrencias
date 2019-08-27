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
    public class ActiveDirectoryBusiness
    {
        /// <summary>
        /// Lista dados do usuário por login
        /// </summary>
        /// <param name="loginUsuario">Login do Usuário</param>
        /// <returns>Lista de usuário</returns>
        public UsuarioADModel ListaDadosUsuarioAD(string loginUsuario)
        {
            try
            {
                var ActiveDirectoryDataAccess = new ActiveDirectoryDataAccess();
                return ActiveDirectoryDataAccess.ListaDadosUsuarioAD(loginUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Lista informações de todos os usuários
        /// </summary>
        /// <returns>Lista de Usuários</returns>
        public List<UsuarioADModel> ListaTodosUsuariosAD()
        {
            try
            {
                var ActiveDirectoryDataAccess = new ActiveDirectoryDataAccess();
                return ActiveDirectoryDataAccess.ListaTodosUsuariosAD();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

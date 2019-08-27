using System;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess;

namespace NUTRIPLAN_WEB.MVC_4_BS.Business
{
    /// <summary>
    /// Classe utilizada para chamar a classe de conexão com o banco de dados
    /// utilizar esta classe para fazer a implementação das regras de negócios
    /// </summary>
    public class N9999USUBusiness
    {
        /// <summary>
        /// Chama o método de classe para Pesquisa usuário por login
        /// </summary>
        /// <param name="login">Login</param>
        /// <returns>ListaDadosUsuarioPorLogin</returns>
        public N9999USU ListaDadosUsuarioPorLogin(string login)
        {
            try
            {
                var N9999USUDataAccess = new N9999USUDataAccess();
                return N9999USUDataAccess.ListaDadosUsuarioPorLogin(login);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama o método de classe para Pesquisa usuário por código
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <returns>ListaDadosUsuarioPorCodigo</returns>
        public N9999USU ListaDadosUsuarioPorCodigo(long codigoUsuario)
        {
            try
            {
                var N9999USUDataAccess = new N9999USUDataAccess();
                return N9999USUDataAccess.ListaDadosUsuarioPorCodigo(codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama o método de classe que Faz o cadastro do usuário por login
        /// </summary>
        /// <param name="login">Login</param>
        public void CadastrarUsuario(string login)
        {
            try
            {
                var N9999USUDataAccess = new N9999USUDataAccess();
                N9999USUDataAccess.CadastrarUsuario(login);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

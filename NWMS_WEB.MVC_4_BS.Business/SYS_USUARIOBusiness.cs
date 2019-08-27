using System;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess;

namespace NUTRIPLAN_WEB.MVC_4_BS.Business
{
    /// <summary>
    /// Classe utilizada para chamar a classe de conexão com o banco de dados
    /// utilizar esta classe para fazer a implementação das regras de negócios
    /// </summary>
    public class SYS_USUARIOBusiness
    {
        /// <summary>
        /// Verificar se o login e senha são válidos
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="senha">senha</param>
        /// <returns>true/false</returns>
        public bool ValidaUsuario(string login, string senha)
        {
            try
            {
                SYS_USUARIODataAccess sYS_USUARIODataAccess = new SYS_USUARIODataAccess();
                return sYS_USUARIODataAccess.ValidaUsuario(login, senha);
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }

        /// <summary>
        /// Alterar senha
        /// </summary>
        /// <param name="login">login</param>
        /// <param name="novaSenha">nova senha</param>
        /// <returns>true/false</returns>
        public bool AlterarSenha(string login, string senha, string novaSenha)
        {
            try
            {
                SYS_USUARIODataAccess sYS_USUARIODataAccess = new SYS_USUARIODataAccess();
                return sYS_USUARIODataAccess.AlterarSenha(login, senha, novaSenha);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lista informações do usuário logado
        /// </summary>
        /// <param name="login">login</param>
        /// <returns>informações do usuário</returns>
        public SYS_USUARIO ListarInformacoesUsuarioLogado(string login)
        {
            try
            {
                SYS_USUARIODataAccess sYS_USUARIODataAccess = new SYS_USUARIODataAccess();
                return sYS_USUARIODataAccess.ListarInformacoesUsuarioLogado(login);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lista informações do usuário para a tela de gerenciamento de permissões de acesso
        /// </summary>
        /// <param name="codigoUser">Código do Usuário</param>
        /// <returns>informações do usuário</returns>
        public SYS_USUARIO PesquisarUser(long codigoUser)
        {
            try
            {
                SYS_USUARIODataAccess sYS_USUARIODataAccess = new SYS_USUARIODataAccess();
                return sYS_USUARIODataAccess.PesquisarUser(codigoUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lista informações do usuário para a tela de gerenciamento de permissões de acesso
        /// </summary>
        /// <param name="codigoUser">Código do Usuário</param>
        /// <returns>informações do usuário</returns>
        public SYS_USUARIO PesquisarUser(string loginUser)
        {
            try
            {
                SYS_USUARIODataAccess sYS_USUARIODataAccess = new SYS_USUARIODataAccess();
                return sYS_USUARIODataAccess.PesquisarUser(loginUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N9999USUDataAccess
    {
        /// <summary>
        /// Busca dados do usuário por login
        /// </summary>
        /// <param name="login">Login</param>
        /// <returns>Lista de Usuários</returns>
        public N9999USU ListaDadosUsuarioPorLogin(string login)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    return contexto.N9999USU.Where(p => p.LOGIN == login).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Busca dados do usuário por código
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <returns>Lista de Usuários</returns>
        public N9999USU ListaDadosUsuarioPorCodigo(long codigoUsuario)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    return contexto.N9999USU.Where(p => p.CODUSU == codigoUsuario).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Realiza o cadastro dos usuários por código
        /// </summary>
        /// <param name="login">Login</param>
        public void CadastrarUsuario(string login)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var N9999USU = new N9999USU();

                    if (contexto.N9999USU.Count() == 0)
                    {
                        N9999USU.CODUSU = 1;
                    }
                    else
                    {
                        N9999USU.CODUSU = contexto.N9999USU.Max(p => p.CODUSU + 1);
                    }

                    N9999USU.LOGIN = login;
                    contexto.N9999USU.Add(N9999USU);
                    contexto.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

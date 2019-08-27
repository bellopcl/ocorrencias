using System;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    public class SYS_USUARIODataAccess
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
                using (Context contexto = new Context())
                {
                    var CODSIS = (long)Enums.Sistema.NWORKFLOW;
                    var resultado = (from t in contexto.SYS_USUARIO
                                     join n in contexto.N9999UXM on new { t.CODUSU, CODSIS } equals new { n.CODUSU, n.CODSIS }
                                     where t.LOGIN == login && t.SENWEB == senha
                                     select t).ToList();

                    if (resultado.Count > 0)
                    {
                        return true;
                    }

                    return false;
                }
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
        /// <param name="senha">senha</param>
        /// <param name="novaSenha">nova senha</param>
        /// <returns>true/false</returns>
        public bool AlterarSenha(string login, string senha, string novaSenha)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var resultado = contexto.SYS_USUARIO.First(p => p.LOGIN == login && p.SENWEB == senha);

                    if (resultado != null)
                    {
                        resultado.SENWEB = novaSenha;
                        contexto.SaveChanges();
                        return true;
                    }

                    return false;
                }
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
                SYS_USUARIO lista = new SYS_USUARIO();

                using (Context contexto = new Context())
                {
                    return lista = contexto.SYS_USUARIO.Where(p => p.LOGIN == login).FirstOrDefault();
                }
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
                SYS_USUARIO lista = new SYS_USUARIO();

                using (Context contexto = new Context())
                {
                    return lista = contexto.SYS_USUARIO.Where(p => p.CODUSU == codigoUser).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa usuários do sistema por login 
        /// </summary>
        /// <param name="loguin"></param>
        /// <returns></returns>
        public SYS_USUARIO PesquisarUser(string loguin)
        {
            try
            {
                SYS_USUARIO lista = new SYS_USUARIO();

                using (Context contexto = new Context())
                {
                    return lista = contexto.SYS_USUARIO.Where(p => p.LOGIN == loguin).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

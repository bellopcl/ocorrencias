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
    public class N0204DORIBusiness
    {
        /// <summary>
        /// Chama a classe para verificar a permissão por usuário para dashboard
        /// </summary>
        /// <param name="codigoUsuarioLogado">Código do Usuário Logado</param>
        /// <returns>PesquisarPermissaoDashBoard</returns>
        public List<N0204DORI> PesquisarPermissaoDashBoard(long codigoUsuarioLogado)
        {
            try
            {
                N0204DORIDataAcess N0204DORIDataAcess = new N0204DORIDataAcess();
                return N0204DORIDataAcess.PesquisarPermissaoDashBoard(codigoUsuarioLogado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// chama a classe para  Gravar as permissão de acesso de usuário por Dashboard
        /// </summary>
        /// <param name="codigoUsuarioLogado">Código do Usuário Logado</param>
        /// <param name="codigoOrigem">Código de origem</param>
        public void GravarPermissaoDashUsuario(long codigoUsuarioLogado, string[] codigoOrigem)
        {
            try
            {
                N0204DORIDataAcess N0204DORIDataAcess = new N0204DORIDataAcess();
                N0204DORIDataAcess.GravarPermissaoDashUsuario(codigoUsuarioLogado, codigoOrigem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

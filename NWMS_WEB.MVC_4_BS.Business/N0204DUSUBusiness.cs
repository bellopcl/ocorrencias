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
    public class N0204DUSUBusiness
    {
        /// <summary>
        /// Chama o metodo que pesquisa a permissão por usuário do dashboard
        /// </summary>
        /// <param name="codigoUsuarioLogado">Código do Usuário Logado</param>
        /// <returns>PesquisarPermissaoDashBoard</returns>
        public List<N0204DUSU> PesquisarPermissaoDashBoard(long codigoUsuarioLogado)
        {
            try
            {
                N0204DUSUDataAcess N0204DUSUDataAcess = new N0204DUSUDataAcess();
                return N0204DUSUDataAcess.PesquisarPermissaoDashBoard(codigoUsuarioLogado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama o metodo que pesquisa o acesso ao dashboard 
        /// </summary>
        /// <param name="codigoUsuarioLogado">Código do Usuário logado</param>
        /// <returns>PesquisarAcessoDashBoard</returns>
        public List<N0204DUSU> PesquisarAcessoDashBoard(long codigoUsuarioLogado)
        {
            try
            {
                N0204DUSUDataAcess N0204DUSUDataAcess = new N0204DUSUDataAcess();
                return N0204DUSUDataAcess.PesquisarAcessoDashBoard(codigoUsuarioLogado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Chama o metodo de classe que grava a permissão do usuário ao dashboard
        /// </summary>
        /// <param name="codigoUsuarioLogado">Código do Usuário logado</param>
        /// <param name="codigoOrigem">Código de Origem</param>
        public void GravarPermissaoDashUsuario(long codigoUsuarioLogado, string[] codigoOrigem)
        {
            try
            {
                N0204DUSUDataAcess N0204DUSUDataAcess = new N0204DUSUDataAcess();
                N0204DUSUDataAcess.GravarPermissaoDashUsuario(codigoUsuarioLogado, codigoOrigem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

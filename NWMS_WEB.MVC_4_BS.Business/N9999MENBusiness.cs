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
    public class N9999MENBusiness
    {   
        /// <summary>
        /// Chama o método de classe para Monta a lista de menus de acordo com as permissões do usuário logado
        /// </summary>
        /// <param name="codigoUser">Código do Usuário</param>
        /// <returns></returns>
        public List<MenuModel> MontarMenu(long codigoUser, int codigoSystem)
        {
            try
            {
                N9999MENDataAccess n9999MENDataAccess = new N9999MENDataAccess();
                return n9999MENDataAccess.MontarMenu(codigoUser, codigoSystem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama o metodo de classe para montar permissões de usuário
        /// </summary>
        /// <param name="codigoUser">Códgio o Usuário</param>
        /// <param name="codigoSystem">Código do Sistema</param>
        /// <returns>montaPermissoes</returns>
        public List<MenuModel> montaPermissoes(long codigoUser, int codigoSystem)
        {
            try
            {
                N9999MENDataAccess n9999MENDataAccess = new N9999MENDataAccess();
                return n9999MENDataAccess.montaPermissoes(codigoUser, codigoSystem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// chama o método de classe para remover o acesso
        /// </summary>
        /// <param name="menuPai">Código do menu principal</param>
        /// <param name="codUsuario">Código do Usuário</param>
        /// <returns>removerAcesso</returns>
        public bool removerAcesso(long menuPai, long codUsuario)
        {
            try
            {
                N9999MENDataAccess n9999MENDataAccess = new N9999MENDataAccess();
                return n9999MENDataAccess.removerAcesso(menuPai, codUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama o método de classe para adicionar acesso ao usuário
        /// </summary>
        /// <param name="codMenu">Código do Menu</param>
        /// <param name="codUsuario">Código do Usuário</param>
        /// <returns></returns>
        public bool adicionarAcesso(long codMenu, long codUsuario)
        {
            try
            {
                N9999MENDataAccess n9999MENDataAccess = new N9999MENDataAccess();
                return true;
            //    return n9999MENDataAccess.adicionarAcesso(codMenu, codUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Lista todos os itens do menu, telas e operações que o usuário pesquisado já possuí algum tipo de operação
        /// </summary>
        /// <param name="codigoUser">código do usuário</param>
        /// <returns>lista de menus</returns>
        public List<MenuModel> MontarTreeViewPermissoes(long codigoUser, int codigoSystem)
        {
            try
            {
                N9999MENDataAccess n9999MENDataAccess = new N9999MENDataAccess();
                return n9999MENDataAccess.MontarTreeViewPermissoes(codigoUser, codigoSystem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

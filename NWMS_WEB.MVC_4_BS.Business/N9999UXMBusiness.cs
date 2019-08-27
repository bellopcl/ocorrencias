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
    public class N9999UXMBusiness
    {
        /// <summary>
        /// Chama o método de classe para Grava as permissões do usuário
        /// </summary>
        /// <param name="listaMenusOperacoes">Lista de Menus</param>
        /// <param name="codUser">Código do Usuário</param>
        /// <param name="codSistema">Código do Usuário</param>
        /// <returns>GravarPermissoesUser</returns>
        public bool GravarPermissoesUser(List<N9999USM> listaMenusOperacoes, long codUser, int codSistema)
        {
            try
            {
                N9999UXMDataAccess n9999UXMDataAccess = new N9999UXMDataAccess();
                return n9999UXMDataAccess.GravarPermissoesUser(listaMenusOperacoes, codUser, codSistema);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

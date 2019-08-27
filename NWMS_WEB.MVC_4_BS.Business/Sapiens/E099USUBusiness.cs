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
    public class E099USUBusiness
    {
        /// <summary>
        /// Pesquisa usuários no Sapien
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <returns>Lista de Usuário</returns>
        public List<E099USUModel> PesquisarUsuariosSapiens(long? codigoUsuario)
        {
            try
            {
                E099USUDataAccess E099USUDataAccess = new E099USUDataAccess();
                return E099USUDataAccess.PesquisarUsuariosSapiens(codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

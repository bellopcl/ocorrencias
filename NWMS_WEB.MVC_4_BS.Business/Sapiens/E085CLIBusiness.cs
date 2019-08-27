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
    public class E085CLIBusiness
    {
        /// <summary>
        /// Pesquisa clientes por código
        /// </summary>
        /// <param name="codigoCliente">Código do Cliente</param>
        /// <returns>Lista de Clientes</returns>
        public List<E085CLIModel> PesquisaClientes(long? codigoCliente)
        {
            try
            {
                E085CLIDataAccess E085CLIDataAccess = new E085CLIDataAccess();
                return E085CLIDataAccess.PesquisasClientes(codigoCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

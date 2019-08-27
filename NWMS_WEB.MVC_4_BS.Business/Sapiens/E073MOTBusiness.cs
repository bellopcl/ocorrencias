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
    public class E073MOTBusiness
    {
        /// <summary>
        /// Pesquisa por motoristas
        /// </summary>
        /// <param name="codigoMotorista">Código do Motorista</param>
        /// <returns>Lista de Motoristas</returns>
        public List<E073MOTModel> PesquisasMotoristas(long? codigoMotorista)
        {
            try
            {
                E073MOTDataAccess E073MOTDataAccess = new E073MOTDataAccess();
                return E073MOTDataAccess.PesquisasMotoristas(codigoMotorista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

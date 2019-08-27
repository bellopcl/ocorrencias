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
    public class E073VEIBusiness
    {
        /// <summary>
        /// Pesquisa caminhão por placa
        /// </summary>
        /// <param name="codPlaca">Código da Placa</param>
        /// <returns>Lista de Caminhões</returns>
        public List<E073VEIModel> PesquisarCaminhaoPorPlaca(string codPlaca)
        {
            try
            {
                E073VEIDataAccess E073VEIDataAccess = new E073VEIDataAccess();
                return E073VEIDataAccess.PesquisarCaminhaoPorPlaca(codPlaca);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

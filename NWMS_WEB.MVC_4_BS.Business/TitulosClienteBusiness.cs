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
    public class TitulosClienteBusiness
    {
        /// <summary>
        /// Chama o método de classe para alterar observaçao dos titulos
        /// </summary>
        /// <param name="listaRegistros">Lista de Notas de Serviço</param>
        /// <returns>AlterarObsTitulo</returns>
        public bool AlterarObsTitulo(List<DadosNotasServicoModel> listaRegistros)
        {
            try
            {
                TitulosClienteDataAccess TitulosClienteDataAccess = new TitulosClienteDataAccess();
                return TitulosClienteDataAccess.AlterarObsTitulo(listaRegistros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama o método de classe para inserir observação de Movimeto
        /// </summary>
        /// <param name="listaRegistros">Lista Notas de serviço</param>
        /// <returns>InserirObsMovimento</returns>
        public bool InserirObsMovimento(List<DadosNotasServicoModel> listaRegistros)
        {
            try
            {
                TitulosClienteDataAccess TitulosClienteDataAccess = new TitulosClienteDataAccess();
                return TitulosClienteDataAccess.InserirObsMovimento(listaRegistros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

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
    public class N0204MDVBusiness
    {
        /// <summary>
        /// Chama o método de classe para inserir o motivo de devolução
        /// </summary>
        /// <param name="descricao">Descrição</param>
        /// <returns>InserirMotivoDevolucao</returns>
        public bool InserirMotivoDevolucao(string descricao)
        {
            try
            {
                N0204MDVDataAccess N0204MDVDataAccess = new N0204MDVDataAccess();
                return N0204MDVDataAccess.InserirMotivoDevolucao(descricao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama o método de classe para pesquisar o motivo da devolução
        /// </summary>
        /// <returns>PesquisaMotivoDevolucao</returns>
        public List<N0204MDV> PesquisaMotivoDevolucao()
        {
            try
            {
                N0204MDVDataAccess N0204MDVDataAccess = new N0204MDVDataAccess();
                return N0204MDVDataAccess.PesquisaMotivoDevolucao();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama o método de classe para alterar o motivo da devolução
        /// </summary>
        /// <param name="codigo">Código do Motivo de Devolução</param>
        /// <param name="descricao">Descrição da Devolução</param>
        /// <returns>AlterarMotivoDevolucao</returns>
        public bool AlterarMotivoDevolucao(int codigo, string descricao)
        {
            try
            {
                N0204MDVDataAccess N0204MDVDataAccess = new N0204MDVDataAccess();
                return N0204MDVDataAccess.AlterarMotivoDevolucao(codigo, descricao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// chama o método de classe para excluir o motivo de devolução
        /// </summary>
        /// <param name="codigo">Código do Motivo de Devolução</param>
        /// <returns>ExcluirMotivoDevolucao</returns>
        public bool ExcluirMotivoDevolucao(int codigo)
        {
            try
            {
                N0204MDVDataAccess N0204MDVDataAccess = new N0204MDVDataAccess();
                return N0204MDVDataAccess.ExcluirMotivoDevolucao(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

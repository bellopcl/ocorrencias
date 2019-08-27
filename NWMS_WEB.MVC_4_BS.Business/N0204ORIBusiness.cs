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
    public class N0204ORIBusiness
    {
        /// <summary>
        /// Chama o método de classe para inserir a origem da ocorrência
        /// </summary>
        /// <param name="descricao">Descrição da Origem da Ocorrência</param>
        /// <returns>InserirOrigemOcorrencia</returns>
        public bool InserirOrigemOcorrencia(string descricao)
        {
            try
            {
                N0204ORIDataAccess N0204ORIDataAccess = new N0204ORIDataAccess();
                return N0204ORIDataAccess.InserirOrigemOcorrencia(descricao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama o método de classe para pesquisar a origem da ocorrência
        /// </summary>
        /// <returns>PesquisaOrigemOcorrencia</returns>
        public List<N0204ORI> PesquisaOrigemOcorrencia()
        {
            try
            {
                N0204ORIDataAccess N0204ORIDataAccess = new N0204ORIDataAccess();
                return N0204ORIDataAccess.PesquisaOrigemOcorrencia();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama o método de clase para alterar a origem da ocorrência
        /// </summary>
        /// <param name="codigo">Código da Origem da Ocorrência</param>
        /// <param name="descricao">Descrição da Origem da Ocorrência</param>
        /// <returns>AlterarOrigemOcorrencia</returns>
        public bool AlterarOrigemOcorrencia(int codigo, string descricao)
        {
            try
            {
                N0204ORIDataAccess N0204ORIDataAccess = new N0204ORIDataAccess();
                return N0204ORIDataAccess.AlterarOrigemOcorrencia(codigo, descricao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama o método de classe para excluir a origem da ocorrência
        /// </summary>
        /// <param name="codigo">Código da Origem da Ocorrência</param>
        /// <returns>ExcluirOrigemOcorrencia</returns>
        public bool ExcluirOrigemOcorrencia(int codigo)
        {
            try
            {
                N0204ORIDataAccess N0204ORIDataAccess = new N0204ORIDataAccess();
                return N0204ORIDataAccess.ExcluirOrigemOcorrencia(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


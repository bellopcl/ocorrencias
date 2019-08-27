using NUTRIPLAN_WEB.MVC_4_BS.Model.Models;
using System;
using System.Collections.Generic;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess;

namespace NUTRIPLAN_WEB.MVC_4_BS.Business
{
    /// <summary>
    /// Classe utilizada para chamar a classe de conexão com o banco de dados
    /// utilizar esta classe para fazer a implementação das regras de negócios
    /// </summary>
    public class N0204PPUBusiness
    {
        /// <summary>
        /// Chama o método de classe para presquisar o prazo de devolução da ocorrência
        /// </summary>
        /// <returns>PesquisaPrazoDevolucaoTroca</returns>
        public List<N0204PPU> PesquisaPrazoDevolucaoTroca()
        {
            try
            {
                var N0204PPUDataAccess = new N0204PPUDataAccess();
                return N0204PPUDataAccess.PesquisaPrazoDevolucaoTroca();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama o método de clase para inserir o prazo de devolução da troca
        /// </summary>
        /// <param name="listaPrazos">Lista de Prazos</param>
        /// <returns>InserirPrazoDevolucaoTroca</returns>
        public bool InserirPrazoDevolucaoTroca(List<N0204PPU> listaPrazos)
        {
            try
            {
                var N0204PPUDataAccess = new N0204PPUDataAccess();
                return N0204PPUDataAccess.InserirPrazoDevolucaoTroca(listaPrazos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama o método de classe para excluir o prazo de devolução de troca
        /// </summary>
        /// <param name="idUsuario">Código do Usuário</param>
        /// <returns>ExcluirPrazoDevolucaoTroca</returns>
        public bool ExcluirPrazoDevolucaoTroca(int idUsuario)
        {
            try
            {
                var N0204PPUDataAccess = new N0204PPUDataAccess();
                return N0204PPUDataAccess.ExcluirPrazoDevolucaoTroca(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

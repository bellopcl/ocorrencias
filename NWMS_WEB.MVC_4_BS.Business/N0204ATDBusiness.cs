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
    public class N0204ATDBusiness
    {
        /// <summary>
        /// Chama a classe para inserir o tipo de atendimento
        /// </summary>
        /// <param name="descricao">Descrição</param>
        /// <returns>InserirTipoAtendimento</returns>
        public bool InserirTipoAtendimento(string descricao)
        {
            try
            {
                N0204ATDDataAccess N0204ATDDataAccess = new N0204ATDDataAccess();
                return N0204ATDDataAccess.InserirTipoAtendimento(descricao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para pesquisar o tipo de atendimento
        /// </summary>
        /// <returns>PesquisaTipoAtendimento</returns>
        public List<N0204ATD> PesquisaTipoAtendimento()
        {
            try
            {
                N0204ATDDataAccess N0204ATDDataAccess = new N0204ATDDataAccess();
                return N0204ATDDataAccess.PesquisaTipoAtendimento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe que pesquisa o tipo de atendimento por usuário
        /// </summary>
        /// <param name="idUsuario">Código do Usuário</param>
        /// <returns>PesquisaTipoAtendimentoPorUsuario</returns>
        public List<N0204ATD> PesquisaTipoAtendimentoPorUsuario(int idUsuario)
        {
            try
            {
                N0204ATDDataAccess N0204ATDDataAccess = new N0204ATDDataAccess();
                return N0204ATDDataAccess.PesquisaTipoAtendimentoPorUsuario(idUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para alterar o tipo de atendimento
        /// </summary>
        /// <param name="codigo">Código do Atendimento</param>
        /// <param name="descricao">Descrição do Atendimento</param>
        /// <returns>AlterarTipoAtendimento</returns>
        public bool AlterarTipoAtendimento(int codigo, string descricao)
        {
            try
            {
                N0204ATDDataAccess N0204ATDDataAccess = new N0204ATDDataAccess();
                return N0204ATDDataAccess.AlterarTipoAtendimento(codigo, descricao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para excluir o tipo de atendimento 
        /// </summary>
        /// <param name="codigo">Código de Atendimento</param>
        /// <returns>ExcluirTipoAtendimento</returns>
        public bool ExcluirTipoAtendimento(int codigo)
        {
            try
            {
                N0204ATDDataAccess N0204ATDDataAccess = new N0204ATDDataAccess();
                return N0204ATDDataAccess.ExcluirTipoAtendimento(codigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

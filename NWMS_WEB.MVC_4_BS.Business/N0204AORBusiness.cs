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
    public class N0204AORBusiness
    {
        /// <summary>
        /// Chama a classe para pesquisar origem por tipo de atendimento
        /// </summary>
        /// <param name="codTipAtend">Código do Tipo de atendimento</param>
        /// <returns>PesquisaOrigemPorTipoAtend</returns>
        public List<N0204AOR> PesquisaOrigemPorTipoAtend(long codTipAtend)
        {
            try
            {
                N0204AORDataAccess N0204AORDataAccess = new N0204AORDataAccess();
                return N0204AORDataAccess.PesquisaOrigemPorTipoAtend(codTipAtend);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para pesquisar tipo de atendimento por origem 
        /// </summary>
        /// <param name="codTipAtend">Código de Tipo de Atendimento</param>
        /// <returns>PesquisaTipoAtendOrigem</returns>
        public List<RelacionamentoTipoAtendOrigemModel> PesquisaTipoAtendOrigem(long codTipAtend)
        {
            try
            {
                N0204AORDataAccess N0204AORDataAccess = new N0204AORDataAccess();
                return N0204AORDataAccess.PesquisaTipoAtendOrigem(codTipAtend);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para gravar o tipo de atendimento por origem 
        /// </summary>
        /// <param name="codTipAtend">Código de Atendimento</param>
        /// <param name="listaTipoAtendOrigem">Lista de tipos de atendimento</param>
        /// <returns>GravarTipoAtendOrigem</returns>
        public bool GravarTipoAtendOrigem(long codTipAtend, List<N0204AOR> listaTipoAtendOrigem)
        {
            try
            {
                N0204AORDataAccess N0204AORDataAccess = new N0204AORDataAccess();
                return N0204AORDataAccess.GravarTipoAtendOrigem(codTipAtend, listaTipoAtendOrigem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

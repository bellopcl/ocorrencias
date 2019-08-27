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
    public class N0203ANXBusiness
    {
        /// <summary>
        /// Pesquisa os itens em anexo por ocorrência e anexo
        /// </summary>
        /// <param name="codigoRegistro">Código da Ocorrência</param>
        /// <param name="idLinhaAnexo">Código da Linha do anexo</param>
        /// <returns>Lista de Anexos</returns>
        public N0203ANX PesquisarItemAnexo(long codigoRegistro, long idLinhaAnexo)
        {
            try
            {
                N0203ANXDataAccess N0203ANXDataAccess = new N0203ANXDataAccess();
                return N0203ANXDataAccess.PesquisarItemAnexo(codigoRegistro, idLinhaAnexo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Pesquisa anexos por ocorrências
        /// </summary>
        /// <param name="codigoRegistro">Código da Ocorrência</param>
        /// <returns>Lista de anexos</returns>
        public List<N0203ANX> PesquisarAnexos(long codigoRegistro)
        {
            try
            {
                N0203ANXDataAccess N0203ANXDataAccess = new N0203ANXDataAccess();
                return N0203ANXDataAccess.PesquisarAnexos(codigoRegistro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Exclui anexo
        /// </summary>
        /// <param name="codigoRegistro">Código da Ocorrência</param>
        /// <param name="idLinhaAnexo">Código da Linha do anexo</param>
        /// <returns></returns>
        public bool ExcluirItemAnexo(long codigoRegistro, long idLinhaAnexo)
        {
            try
            {
                N0203ANXDataAccess N0203ANXDataAccess = new N0203ANXDataAccess();
                return N0203ANXDataAccess.ExcluirItemAnexo(codigoRegistro, idLinhaAnexo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

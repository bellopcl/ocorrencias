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
    public class N0204MDOBusiness
    {
        /// <summary>
        /// Chama o método de classe que pesquisa as origens por motivo
        /// </summary>
        /// <param name="codigoMotivo">Código do Motivo</param>
        /// <returns>PesquisaOrigensPorMotivo</returns>
        public List<N0204MDO> PesquisaOrigensPorMotivo(long codigoMotivo)
        {
            try
            {
                N0204MDODataAccess N0204MDODataAccess = new N0204MDODataAccess();
                return N0204MDODataAccess.PesquisaOrigensPorMotivo(codigoMotivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ListaMotivoporOrigem> PesquisaMotivoporOrigem(long codigoMotivo)
        {
            N0204MDODataAccess n0204MDODataAccess = new N0204MDODataAccess();
            var lista = n0204MDODataAccess.PesquisaMotivoporOrigem(codigoMotivo);
            return lista;
        }

        /// <summary>
        /// chama o método de classe que pesquisa o motivo por origem 
        /// </summary>
        /// <param name="codigoMotivo">Código do Motivo</param>
        /// <returns>PesquisaMotivoOrigem</returns>
        public List<RelacionamentoMotivoOrigemModel> PesquisaMotivoOrigem(long codigoMotivo)
        {
            try
            {
                N0204MDODataAccess N0204MDODataAccess = new N0204MDODataAccess();
                return N0204MDODataAccess.PesquisaMotivoOrigem(codigoMotivo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama o método de classe que grava o motivo de devolução por origem de ocorrência
        /// </summary>
        /// <param name="codigoMotivo">Código do Motivo</param>
        /// <param name="listaMotivosOrigens">Lista de Origens</param>
        /// <returns>GravarMotivoDevXOrigemOcorrencia</returns>
        public bool GravarMotivoDevXOrigemOcorrencia(long codigoMotivo, List<N0204MDO> listaMotivosOrigens)
        {
            try
            {
                N0204MDODataAccess N0204MDODataAccess = new N0204MDODataAccess();
                return N0204MDODataAccess.GravarMotivoDevXOrigemOcorrencia(codigoMotivo, listaMotivosOrigens);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess;

namespace NUTRIPLAN_WEB.MVC_4_BS.Business
{
    /// <summary>
    /// Classe para chamar as classes que fazem a conexão de banco de dados
    /// utilizar para aplicar as regras de negócios
    /// </summary>
    public class N0203TRABusiness
    {
        /// <summary>
        /// Pesquisa Tramites por código de registro
        /// </summary>
        /// <param name="codigoRegistro">Código da ocorrência</param>
        /// <returns></returns>
        public List<N0203TRA> PesquisaTramites(long codigoRegistro)
        {
            try
            {
                var N0203TRADataAccess = new N0203TRADataAccess();
                
                return N0203TRADataAccess.PesquisaTramites(codigoRegistro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Regra de negocios Relatório de Transações de Ocorrência
        /// </summary>
        /// <param name="NumReg"></param>
        /// <returns></returns>
        public List<RelatorioTransacoes> RelatorioTransacao(long NumReg)
        {
            try
            {
                var N0203TRADataAccess = new N0203TRADataAccess();
                return N0203TRADataAccess.RelatorioTransacao(NumReg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Pesquisar Tramites por codigo de usuário
        /// </summary>
        /// <param name="codigoUsuario">Codigo de Usuário</param>
        /// <returns></returns>
        public List<N0203TRA> PesquisaTramitesUsuario(long codigoUsuario)
        {
            try
            {
                var N0203TRADataAccess = new N0203TRADataAccess();
                var ActiveDirectoryBusiness = new ActiveDirectoryBusiness();
                var tramitesNotificacao = new List<N0203TRA>();
                tramitesNotificacao = N0203TRADataAccess.PesquisaTramiteUsuario(codigoUsuario);

                return tramitesNotificacao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

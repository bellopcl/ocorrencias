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
    public class N0203UOFBusiness
    {
        /// <summary>
        /// Chama a classe para pesquisar usuário aprovador por operação faturada
        /// </summary>
        /// <param name="codUsuarioAprovador">Código do usuário aprovador</param>
        /// <returns>PesquisaUsuarioAprovadorXOperacaoFat</returns>
        public List<RelUsuAprovadorOperacaoFat> PesquisaUsuarioAprovadorXOperacaoFat(long codUsuarioAprovador, long? tipoAtendimento)
        {
            try
            {
                var N0203UOFDataAccess = new N0203UOFDataAccess();
                return N0203UOFDataAccess.PesquisaUsuarioAprovadorXOperacaoFat(codUsuarioAprovador, tipoAtendimento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para fazer a pesquisa operações de aprovação por usuário
        /// </summary>
        /// <param name="codUsuario">Código do Usuário</param>
        /// <returns>PesquisaOperacoesAprovFatPorUsuario</returns>
        public List<RelUsuAprovadorOperacaoFat> PesquisaOperacoesAprovFatPorUsuario(long codUsuario)
        {
            try
            {
                var N0203UOFDataAccess = new N0203UOFDataAccess();
                return N0203UOFDataAccess.PesquisaOperacoesAprovFatPorUsuario(codUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para gravar usuário aprovador por operação faturada
        /// </summary>
        /// <param name="codUsuarioAprovador">Código do Usuário Aprovador</param>
        /// <param name="listaOperacoes">Lista de Operações</param>
        /// <returns>GravarUsuarioAprovadorXOperacaoFat</returns>
        public bool GravarUsuarioAprovadorXOperacaoFat(long codUsuarioAprovador, List<N0203UOF> listaOperacoes, long tipoAtendimento)
        {
            try
            {
                var N0203UOFDataAccess = new N0203UOFDataAccess();
                return N0203UOFDataAccess.GravarUsuarioAprovadorXOperacaoFat(codUsuarioAprovador, listaOperacoes, tipoAtendimento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

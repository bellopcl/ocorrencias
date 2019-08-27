using System;
using System.Collections.Generic;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.Data.OracleClient;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N0203UOFDataAccess
    {
        /// <summary>
        /// Pesquisa usuário aprovador por operação de faturamento
        /// </summary>
        /// <param name="codUsuarioAprovador">Código do Usuário Aprovador</param>
        /// <returns>Lista de usuários por origem</returns>
        public List<RelUsuAprovadorOperacaoFat> PesquisaUsuarioAprovadorXOperacaoFat(long codUsuarioAprovador, long? tipoAtendimento)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var listaOrigensUsuario = new List<RelUsuAprovadorOperacaoFat>();
                    var itemOrigemUsuario = new RelUsuAprovadorOperacaoFat();
                    if (tipoAtendimento == null)
                    { 
                        tipoAtendimento = (long)Enums.TipoAtendimento.DevolucaoMercadorias;
                    }
                    var listaOperacoes = contexto.N0203OPE.OrderBy(c => c.CODOPE).ToList();
                    var listaOpeUsu = contexto.N0203UOF.Where(c => c.CODUSU == codUsuarioAprovador && c.CODATD == tipoAtendimento).OrderBy(c => c.CODOPE).ToList();

                    if (listaOperacoes != null)
                    {
                        foreach (var itemOrigem in listaOperacoes)
                        {
                            itemOrigemUsuario = new RelUsuAprovadorOperacaoFat();
                            itemOrigemUsuario.CodigoOperacao = itemOrigem.CODOPE;
                            itemOrigemUsuario.DescricaoOperacao = itemOrigem.DSCOPE;

                            var marcarOrigem = listaOpeUsu.Where(c => c.CODOPE == itemOrigem.CODOPE).FirstOrDefault();
                            if (marcarOrigem != null)
                            {
                                itemOrigemUsuario.Marcar = true;
                            }

                            listaOrigensUsuario.Add(itemOrigemUsuario);
                        }
                    }

                    return listaOrigensUsuario;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa operações aprovadas por usuário
        /// </summary>
        /// <param name="codUsuario">Código do Usuário</param>
        /// <returns>Lista de operações por usuário</returns>
        public List<RelUsuAprovadorOperacaoFat> PesquisaOperacoesAprovFatPorUsuario(long codUsuario)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var tipoAtendimento = (long)Enums.TipoAtendimento.DevolucaoMercadorias;

                    var listaOpeUsu = (from a in contexto.N0203UOF
                                       join b in contexto.N0203OPE on new { a.CODOPE } equals new { b.CODOPE }
                                       where a.CODUSU == codUsuario && a.CODATD == tipoAtendimento
                                       select new RelUsuAprovadorOperacaoFat
                                       {
                                           CodigoUsuario = a.CODUSU,
                                           CodigoOperacao = a.CODOPE,
                                           DescricaoOperacao = b.DSCOPE

                                       }).ToList();

                    return listaOpeUsu;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Grava usuário aprovador por operação faturada
        /// </summary>
        /// <param name="codUsuarioAprovador">Código do Usuário Aprovador</param>
        /// <param name="listaOperacoes">Lista Operações</param>
        /// <returns>true/false</returns>
        public bool GravarUsuarioAprovadorXOperacaoFat(long codUsuarioAprovador, List<N0203UOF> listaOperacoes, long tipoAtendimento)
        {
            try
            {
                using (Context contexto = new Context())
                {

                    var listaOperUsu = contexto.N0203UOF.Where(c => c.CODUSU == codUsuarioAprovador && c.CODATD == tipoAtendimento).OrderBy(c => c.CODOPE).ToList();
                    
                    foreach (var itemUsuOpe in listaOperUsu)
                    {
                        contexto.N0203UOF.Remove(itemUsuOpe);
                        contexto.SaveChanges();
                    }

                    if (listaOperacoes != null && listaOperacoes.Count > 0)
                    {
                        foreach (var itemCad in listaOperacoes)
                        {
                            contexto.N0203UOF.Add(itemCad);
                            contexto.SaveChanges();
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

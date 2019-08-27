using System;
using System.Collections.Generic;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N0203UAPDataAccess
    {
        /// <summary>
        /// Busca o usuário aprovador x Origem de ocorrência
        /// </summary>
        /// <param name="codUsuarioAprovador">Código do Usuário Aprovador</param>
        /// <param name="tipoAtendimento">Tipo de Atendimento</param>
        /// <returns>listaOrigensUsuario</returns>
        public List<RelUsuAprovadorOrigemOcorrencia> PesquisaUsuarioAprovadorXOrigemOcorrencia(long codUsuarioAprovador, Enums.TipoAtendimento tipoAtendimento)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    List<RelUsuAprovadorOrigemOcorrencia> listaOrigensUsuario = new List<RelUsuAprovadorOrigemOcorrencia>();
                    RelUsuAprovadorOrigemOcorrencia itemOrigemUsuario = new RelUsuAprovadorOrigemOcorrencia();
                    string situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString();

                    var listaOrigens = contexto.N0204ORI.Where(c => c.SITORI == situacao).OrderBy(c => c.CODORI).ToList();
                    var listaOriUsu = contexto.N0203UAP.Where(c => c.CODUSU == codUsuarioAprovador && c.CODATD == (long)tipoAtendimento).OrderBy(c => c.CODORI).ToList();

                    if (listaOrigens != null)
                    {
                        foreach (N0204ORI itemOrigem in listaOrigens)
                        {
                            itemOrigemUsuario = new RelUsuAprovadorOrigemOcorrencia();
                            itemOrigemUsuario.CodigoOrigem = itemOrigem.CODORI;
                            itemOrigemUsuario.DescricaoOrigem = itemOrigem.DESCORI;

                            var marcarOrigem = listaOriUsu.Where(c => c.CODORI == itemOrigem.CODORI).FirstOrDefault();
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
        /// Grava usuário aprovador por origem de ocorrência
        /// </summary>
        /// <param name="codUsuarioAprovador">Código do Usuário Aprovador</param>
        /// <param name="listaUsuarioOrigens">Lista de Usuários por origem</param>
        /// <param name="tipoAtendimento">Tipo de Atendimento</param>
        /// <returns>true/false</returns>
        public bool GravarUsuarioAprovadorXOrigemOcorrencia(long codUsuarioAprovador, List<N0203UAP> listaUsuarioOrigens, Enums.TipoAtendimento tipoAtendimento)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var listaOriUsu = contexto.N0203UAP.Where(c => c.CODUSU == codUsuarioAprovador && c.CODATD == (long)tipoAtendimento).OrderBy(c => c.CODORI).ToList();

                    foreach (var itemUsuOri in listaOriUsu)
                    {
                        contexto.N0203UAP.Remove(itemUsuOri);
                        contexto.SaveChanges();
                    }

                    if (listaUsuarioOrigens != null && listaUsuarioOrigens.Count > 0)
                    {
                        foreach (var itemCad in listaUsuarioOrigens)
                        {
                            contexto.N0203UAP.Add(itemCad);
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

        /// <summary>
        /// Pesquisa usuários aprovadores por origem 
        /// </summary>
        /// <param name="listaOrigens">Lista de Origens</param>
        /// <param name="tipoAtendimento">Tipo de Atendimento</param>
        /// <returns></returns>
        public List<long> PesquisaUsuariosAprovadoresPorOrigens(List<long> listaOrigens, Enums.TipoAtendimento tipoAtendimento)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    return contexto.N0203UAP.Where(c => listaOrigens.Contains(c.CODORI) && c.CODATD == (long)tipoAtendimento).GroupBy(c => c.CODUSU).Select(c => c.Key).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Pesquisa Usuários aprovadores
        /// </summary>
        /// <param name="codigoOrigem">Código de Origem </param>
        /// <param name="codigoAtedimento">Tipo de Atendimento</param>
        /// <returns>N0203UAP</returns>
        public List<N0203UAP> PesquisarUsuariosAprovadores(long codigoOrigem, long codigoAtedimento)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    return contexto.N0203UAP.Where(c => c.CODORI == codigoOrigem && c.CODATD == codigoAtedimento).ToList();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

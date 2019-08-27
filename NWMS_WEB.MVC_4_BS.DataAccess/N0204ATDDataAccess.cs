using System;
using System.Collections.Generic;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N0204ATDDataAccess
    {
        /// <summary>
        /// Faz a inserção do tipo de atendimento
        /// </summary>
        /// <param name="descricao">Descrição</param>
        /// <returns>true/false</returns>
        public bool InserirTipoAtendimento(string descricao)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    N0204ATD N0204ATD = new N0204ATD();

                    if (contexto.N0204ATD.Count() == 0)
                    {
                        N0204ATD.CODATD = 1;
                    }
                    else
                    {
                        N0204ATD.CODATD = contexto.N0204ATD.Max(p => p.CODATD + 1);
                    }

                    N0204ATD.DESCATD = descricao;
                    N0204ATD.SITATD = ((char)Enums.SituacaoRegistro.Ativo).ToString();
                    contexto.N0204ATD.Add(N0204ATD);
                    contexto.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa todos os tipos de atendimento
        /// </summary>
        /// <returns>Lista de tipo de atendimento</returns>
        public List<N0204ATD> PesquisaTipoAtendimento()
        {
            try
            {
                using (Context contexto = new Context())
                {
                    string situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString();
                    var lista = contexto.N0204ATD.Where(c => c.SITATD == situacao).OrderBy(c => c.CODATD).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Pesquisa tipo de atendimento por usuário
        /// </summary>
        /// <param name="idUsuario">Código do Usuário</param>
        /// <returns>Lista de tipo de atendimento</returns>
        public List<N0204ATD> PesquisaTipoAtendimentoPorUsuario(int idUsuario)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    string situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString();
                    var lista = (from a in contexto.N0204ATD
                                join b in contexto.N0204AUS on a.CODATD equals b.CODATD
                                where a.SITATD == situacao && b.CODUSU == idUsuario
                                select a).ToList();

                    return lista;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Altera o tipo de atendimento
        /// </summary>
        /// <param name="codigo">Código</param>
        /// <param name="descricao">Descrição</param>
        /// <returns>true/fase</returns>
        public bool AlterarTipoAtendimento(int codigo, string descricao)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var original = contexto.N0204ATD.Where(c => c.CODATD == codigo).SingleOrDefault();

                    if (original != null)
                    {
                        original.DESCATD = descricao;
                        contexto.SaveChanges();
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Exclui o tipo de atendimento
        /// </summary>
        /// <param name="codigo">codigo</param>
        /// <returns>true/false</returns>
        public bool ExcluirTipoAtendimento(int codigo)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var original = contexto.N0204ATD.Where(c => c.CODATD == codigo).SingleOrDefault();

                    if (original != null)
                    {
                        original.SITATD = ((char)Enums.SituacaoRegistro.Inativo).ToString();
                        contexto.SaveChanges();
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

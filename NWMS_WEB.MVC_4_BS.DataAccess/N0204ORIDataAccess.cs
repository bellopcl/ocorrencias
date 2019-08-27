using System;
using System.Collections.Generic;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N0204ORIDataAccess
    {
        /// <summary>
        /// Grava a origem da Ocorrência
        /// </summary>
        /// <param name="descricao">Descrição da Ocorrencia</param>
        /// <returns>true/false</returns>
        public bool InserirOrigemOcorrencia(string descricao)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    N0204ORI N0204ORI = new N0204ORI();

                    if (contexto.N0204ORI.Count() == 0)
                    {
                        N0204ORI.CODORI = 1;
                    }
                    else
                    {
                        N0204ORI.CODORI = contexto.N0204ORI.Max(p => p.CODORI + 1);
                    }

                    N0204ORI.DESCORI = descricao;
                    N0204ORI.SITORI = ((char)Enums.SituacaoRegistro.Ativo).ToString();
                    contexto.N0204ORI.Add(N0204ORI);
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
        /// Pesquisa as origens das ocorrências
        /// </summary>
        /// <returns>N0204ORI</returns>
        public List<N0204ORI> PesquisaOrigemOcorrencia()
        {
            try
            {
                using (Context contexto = new Context())
                {
                    string situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString();
                    var lista = contexto.N0204ORI.Where(c => c.SITORI == situacao).OrderBy(c => c.CODORI).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Altera a origem das ocorrências
        /// </summary>
        /// <param name="codigo">Código de Origens da Ocorrência</param>
        /// <param name="descricao">Descrição da Ocorrência</param>
        /// <returns>true/false</returns>
        public bool AlterarOrigemOcorrencia(int codigo, string descricao)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var original = contexto.N0204ORI.Where(c => c.CODORI == codigo).SingleOrDefault();

                    if (original != null)
                    {
                        original.DESCORI = descricao;
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
        /// Faz a exclusão da origem das ocorrências
        /// </summary>
        /// <param name="codigo">Código de origem das ocorrência</param>
        /// <returns>true/false</returns>
        public bool ExcluirOrigemOcorrencia(int codigo)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var original = contexto.N0204ORI.Where(c => c.CODORI == codigo).SingleOrDefault();

                    if (original != null)
                    {
                        original.SITORI = ((char)Enums.SituacaoRegistro.Inativo).ToString();
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

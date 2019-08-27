using System;
using System.Collections.Generic;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N0204MDVDataAccess
    {
        /// <summary>
        /// Realiza a inclusão do Motivo de Devolução
        /// </summary>
        /// <param name="descricao">Descrição</param>
        /// <returns>true/false</returns>
        public bool InserirMotivoDevolucao(string descricao)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    N0204MDV N0204MDV = new N0204MDV();

                    if (contexto.N0204MDV.Count() == 0)
                    {
                        N0204MDV.CODMDV = 1;
                    }
                    else
                    {
                        N0204MDV.CODMDV = contexto.N0204MDV.Max(p => p.CODMDV + 1);
                    }

                    N0204MDV.DESCMDV = descricao;
                    N0204MDV.SITMDV = ((char)Enums.SituacaoRegistro.Ativo).ToString();
                    contexto.N0204MDV.Add(N0204MDV);
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
        /// Pesquisa os motivos de devolução
        /// </summary>
        /// <returns>Lista de Motivos</returns>
        public List<N0204MDV> PesquisaMotivoDevolucao()
        {
            try
            {
                using (Context contexto = new Context())
                {
                    string situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString();
                    var lista = contexto.N0204MDV.Where(c => c.SITMDV == situacao).OrderBy(c => c.CODMDV).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Altera motivo de Devolução
        /// </summary>
        /// <param name="codigo">Código do Motivo de Devolução</param>
        /// <param name="descricao">Descrição do Motivo de Devolução</param>
        /// <returns>true/false</returns>
        public bool AlterarMotivoDevolucao(int codigo, string descricao)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var original = contexto.N0204MDV.Where(c => c.CODMDV == codigo).SingleOrDefault();

                    if (original != null)
                    {
                        original.DESCMDV = descricao;
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
        /// Excluir motivo de devolução
        /// </summary>
        /// <param name="codigo">Código do Motivo de Devolução</param>
        /// <returns>true/false</returns>
        public bool ExcluirMotivoDevolucao(int codigo)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var original = contexto.N0204MDV.Where(c => c.CODMDV == codigo).SingleOrDefault();

                    if (original != null)
                    {
                        original.SITMDV = ((char)Enums.SituacaoRegistro.Inativo).ToString();
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

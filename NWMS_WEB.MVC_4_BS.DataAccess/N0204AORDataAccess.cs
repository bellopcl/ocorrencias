using System;
using System.Collections.Generic;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N0204AORDataAccess
    {
        /// <summary>
        /// Pesquisa as origem por tipo de atendimento
        /// </summary>
        /// <param name="codTipAtend">Tipo de Atendimento</param>
        /// <returns>Lista de tipos de atendimento</returns>
        public List<N0204AOR> PesquisaOrigemPorTipoAtend(long codTipAtend)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    string situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString();
                    var listaTipoAtend = contexto.N0204AOR.Include("N0204ORI").Where(c => c.CODATD == codTipAtend && c.SITREL == situacao).OrderBy(c => c.CODORI).ToList();
                    return listaTipoAtend;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa tipo de atendimento por origem
        /// </summary>
        /// <param name="codTipAtend">Código Tipo de Atendimento</param>
        /// <returns></returns>
        public List<RelacionamentoTipoAtendOrigemModel> PesquisaTipoAtendOrigem(long codTipAtend)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    List<RelacionamentoTipoAtendOrigemModel> listaTipoAtendOrigem = new List<RelacionamentoTipoAtendOrigemModel>();
                    RelacionamentoTipoAtendOrigemModel itemTipoAtendOrigem = new RelacionamentoTipoAtendOrigemModel();
                    string situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString();

                    var listaOrigens = contexto.N0204ORI.Where(c => c.SITORI == situacao).OrderBy(c => c.CODORI).ToList();
                    var listaTipAtendOri = contexto.N0204AOR.Where(c => c.CODATD == codTipAtend && c.SITREL == situacao).OrderBy(c => c.CODORI).ToList();

                    if (listaOrigens != null)
                    {
                        foreach (N0204ORI itemOrigem in listaOrigens)
                        {
                            itemTipoAtendOrigem = new RelacionamentoTipoAtendOrigemModel();
                            itemTipoAtendOrigem.Codigo = itemOrigem.CODORI;
                            itemTipoAtendOrigem.Descricao = itemOrigem.DESCORI;

                            var marcarOrigem = listaTipAtendOri.Where(c => c.CODORI == itemOrigem.CODORI).FirstOrDefault();

                            if (marcarOrigem != null)
                            {
                                itemTipoAtendOrigem.Marcar = true;
                            }

                            listaTipoAtendOrigem.Add(itemTipoAtendOrigem);
                        }
                    }

                    return listaTipoAtendOrigem;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Grava o tipo de atendimento por origem 
        /// </summary>
        /// <param name="codTipAtend">Código Tipo de Atendimento</param>
        /// <param name="listaTipoAtendOrigem">Lista de Tipo de Atendimento por Origem</param>
        /// <returns>true/false</returns>
        public bool GravarTipoAtendOrigem(long codTipAtend, List<N0204AOR> listaTipoAtendOrigem)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    string situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString();

                    var listaTipAtendOri = contexto.N0204AOR.Where(c => c.CODATD == codTipAtend).OrderBy(c => c.CODORI).ToList();

                    if (listaTipAtendOri != null)
                    {
                        foreach (N0204AOR itemTipoAtendOrigem in listaTipAtendOri)
                        {
                            contexto.N0204AOR.Remove(itemTipoAtendOrigem);
                            contexto.SaveChanges();
                        }
                    }

                    foreach (N0204AOR itemCad in listaTipoAtendOrigem)
                    {
                        if (contexto.N0204AOR.Count() == 0)
                        {
                            itemCad.IDROW = 1;
                        }
                        else
                        {
                            itemCad.IDROW = contexto.N0204AOR.Max(p => p.IDROW + 1);
                        }

                        contexto.N0204AOR.Add(itemCad);
                        contexto.SaveChanges();
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

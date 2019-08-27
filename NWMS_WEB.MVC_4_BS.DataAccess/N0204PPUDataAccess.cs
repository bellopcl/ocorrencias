using NUTRIPLAN_WEB.MVC_4_BS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N0204PPUDataAccess
    {
        /// <summary>
        /// Pesquisa prazo de devolução para troca
        /// </summary>
        /// <returns>Lista</returns>
        public List<N0204PPU> PesquisaPrazoDevolucaoTroca()
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var lista = contexto.N0204PPU.OrderBy(c => c.IDROW).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserir prazo de devolução para troca
        /// </summary>
        /// <param name="listaPrazos">Lista de prazos</param>
        /// <returns>true/false</returns>
        public bool InserirPrazoDevolucaoTroca(List<N0204PPU> listaPrazos)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    foreach (var item in listaPrazos)
                    {
                        if (contexto.N0204PPU.Count() == 0)
                            item.IDROW = 1;
                        else
                            item.IDROW = contexto.N0204PPU.Max(p => p.IDROW + 1);

                        if (item.CODUSU.HasValue)
                        {
                            if (contexto.N0204PPU.Where(c => c.CODUSU == item.CODUSU).FirstOrDefault() == null)
                            {
                                contexto.N0204PPU.Add(item);
                                contexto.SaveChanges();
                            }
                        }
                        else
                        {
                            var original = contexto.N0204PPU.Where(c => !c.CODUSU.HasValue).FirstOrDefault();

                            if (original != null)
                            {
                                original.QTDDEV = item.QTDDEV;
                                original.QTDTRC = item.QTDTRC;
                                contexto.SaveChanges();
                                return true;
                            }

                            return false;
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
        /// Faz a exclusão do prozo de devolução para troca
        /// </summary>
        /// <param name="idUsuario">Código do Usuário</param>
        /// <returns>true/false</returns>
        public bool ExcluirPrazoDevolucaoTroca(int idUsuario)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var original = contexto.N0204PPU.Where(c => c.CODUSU == idUsuario).FirstOrDefault();

                    if (original != null)
                    {
                        contexto.N0204PPU.Remove(original);
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

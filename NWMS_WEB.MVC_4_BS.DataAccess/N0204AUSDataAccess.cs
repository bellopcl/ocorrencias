using System;
using System.Collections.Generic;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N0204AUSDataAccess
    {
        /// <summary>
        /// Pesquisa o tipo de atendimento por usuário
        /// </summary>
        /// <param name="codUsuario">Código do Usuário</param>
        /// <returns>Lista de tipo de atendimento</returns>
        public List<RelTipoAtendUsuario> PesquisarTipoAtendimentoUsuario(long codUsuario)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    List<RelTipoAtendUsuario> listaTipoAtendiUsuarios = new List<RelTipoAtendUsuario>();

                    var listaTipoAtends = contexto.N0204ATD.OrderBy(c => c.CODATD).ToList();
                    var listaTipAtenUsu = contexto.N0204AUS.Where(c => c.CODUSU == codUsuario).OrderBy(c => c.CODATD).ToList();

                    if (listaTipoAtends != null)
                    {
                        foreach (var item in listaTipoAtends)
                        {
                            var novoItem = new RelTipoAtendUsuario();
                            novoItem.CodigoUsuario = codUsuario;
                            novoItem.CodigoTipoAtendimento = item.CODATD;
                            novoItem.DescricaoTipoAtendimento = item.DESCATD;
                            novoItem.Marcar = listaTipAtenUsu.Where(c => c.CODATD == item.CODATD).FirstOrDefault() != null;
                            listaTipoAtendiUsuarios.Add(novoItem);
                        }
                    }

                    return listaTipoAtendiUsuarios;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Grava o tipo de atendimento
        /// </summary>
        /// <param name="codUsuario">Código do Usuário</param>
        /// <param name="listaTipoAtendiUsuarios">Lista de Tipo de Atendimento por Usuário</param>
        /// <returns>true/false</returns>
        public bool GravarTipoAtendimentoUsuario(long codUsuario, List<N0204AUS> listaTipoAtendiUsuarios)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var listaAteUsu = contexto.N0204AUS.Where(c => c.CODUSU == codUsuario).OrderBy(c => c.CODUSU).ToList();

                    foreach (var item in listaAteUsu)
                    {
                        contexto.N0204AUS.Remove(item);
                        contexto.SaveChanges();
                    }

                    if (listaTipoAtendiUsuarios != null && listaTipoAtendiUsuarios.Count > 0)
                    {
                        foreach (var itemCad in listaTipoAtendiUsuarios)
                        {
                            if (contexto.N0204AUS.Count() == 0)
                                itemCad.IDROW = 1;
                            else
                                itemCad.IDROW = contexto.N0204AUS.Max(p => p.IDROW + 1);

                            contexto.N0204AUS.Add(itemCad);
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

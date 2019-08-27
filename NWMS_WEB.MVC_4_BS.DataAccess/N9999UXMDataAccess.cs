using System;
using System.Collections.Generic;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N9999UXMDataAccess
    {
        /// <summary>
        /// Lista o menu de operações
        /// Lista todos os menus e operações que o usuário já tem acesso e exclui todos para gravar posteriormente os novos
        /// </summary>
        /// <param name="listaMenusOperacoes">Lista de Menus por operações</param>
        /// <param name="codUser">Código do Usuário</param>
        /// <param name="codSistema">Código do Sistema</param>
        /// <returns>true/false</returns>
        public bool GravarPermissoesUser(List<N9999USM> listaMenusOperacoes, long codUser, int codSistema)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var menuAcesso = contexto.N9999USM.Where(p => p.CODUSU == codUser && p.CODSIS == codSistema).ToList();

                    if (menuAcesso.Count > 0)
                    {
                        foreach (var item in menuAcesso)
                        {
                            contexto.N9999USM.Remove(item);
                        }

                        contexto.SaveChanges();
                    }

                    if (listaMenusOperacoes.Count > 0)
                    {
                        // Grava os novos items
                        foreach (var item in listaMenusOperacoes)
                        {
                            contexto.N9999USM.Add(item);
                        }

                        contexto.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N0203IPVDataAccess
    {
        /// <summary>
        /// Retorna uma lista de itens devolvidos
        /// </summary>
        /// <param name="codigoRegistro"></param>
        /// <returns>lista</returns>
        public List<N0203IPV> PesquisarItensDevolucao(long codigoRegistro)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var lista = contexto.N0203IPV.Where(c => c.NUMREG == codigoRegistro).OrderBy(c => new { c.NUMNFV, c.SEQIPV }).ToList();
                    return lista;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

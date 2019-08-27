using System;
using System.Collections.Generic;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N0203ANXDataAccess
    {
        /// <summary>
        /// Retorna todos os anexos do registro de ocorrência
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        /// <param name="idLinhaAnexo">Código de Anexo</param>
        /// <returns>itemAnexo</returns>
        public N0203ANX PesquisarItemAnexo(long codigoRegistro, long idLinhaAnexo)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var itemAnexo = contexto.N0203ANX.Where(c => c.NUMREG == codigoRegistro && c.IDROW == idLinhaAnexo).FirstOrDefault();
                    return itemAnexo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Retorna uma lista de anexos registrado no processo de ocorrência;
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        /// <returns>listaAnexo</returns>

        public List<N0203ANX> PesquisarAnexos(long codigoRegistro)
        {
            try
            {

                using (Context contexto = new Context())
                {
                    var listaAnexo = contexto.N0203ANX.Where(c => c.NUMREG == codigoRegistro).ToList();
                    return listaAnexo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Exclui anexos da ocorrência
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        /// <param name="idLinhaAnexo">Código de Anexo</param>
        /// <returns>true/false</returns>
        public bool ExcluirItemAnexo(long codigoRegistro, long idLinhaAnexo)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var itemAnexo = contexto.N0203ANX.Where(c => c.NUMREG == codigoRegistro && c.IDROW == idLinhaAnexo).FirstOrDefault();

                    if (itemAnexo != null)
                    {
                        contexto.N0203ANX.Remove(itemAnexo);
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

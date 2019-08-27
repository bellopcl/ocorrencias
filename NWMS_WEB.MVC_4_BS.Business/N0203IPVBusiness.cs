using System;
using System.Collections.Generic;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess;

namespace NUTRIPLAN_WEB.MVC_4_BS.Business
{
    /// <summary>
    /// Classe utilizada para chamar a classe de conexão com o banco de dados
    /// utilizar esta classe para fazer a implementação das regras de negócios
    /// </summary>
    public class N0203IPVBusiness
    {
        /// <summary>
        /// Pesquisa itens de devolução por ocorrências
        /// </summary>
        /// <param name="codigoRegistro">Código da ocorrência</param>
        /// <returns>Itens de devoluçaõ</returns>
        public List<N0203IPV> PesquisarItensDevolucao(long codigoRegistro)
        {
            try
            {
                N0203IPVDataAccess N0203IPVDataAccess = new N0203IPVDataAccess();
                return N0203IPVDataAccess.PesquisarItensDevolucao(codigoRegistro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

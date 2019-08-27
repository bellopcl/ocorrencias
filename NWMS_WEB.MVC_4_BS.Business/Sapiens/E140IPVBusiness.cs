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
    public class E140IPVBusiness
    {
        /// <summary>
        /// Pesquisa Itens na nota Fiscal de Saída
        /// </summary>
        /// <param name="filial">Código da Filial</param>
        /// <param name="numeroNota">Número da nota</param>
        /// <param name="serieNota">Série da Nota</param>
        /// <returns>Lista de Itens</returns>
        public List<E140IPVModel> PesquisarItensNotasFiscaisSaida(int filial, long numeroNota, string serieNota)
        {
            try
            {
                E140IPVDataAccess E140IPVDataAccess = new E140IPVDataAccess();
                return E140IPVDataAccess.PesquisarItensNotasFiscaisSaida(filial, numeroNota, serieNota);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

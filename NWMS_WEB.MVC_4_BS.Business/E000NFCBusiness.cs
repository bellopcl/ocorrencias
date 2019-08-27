using System;
using System.Collections.Generic;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess;

namespace NUTRIPLAN_WEB.MVC_4_BS.Business
{
    /// <summary>
    /// Classe utilizada para chamar a classe de conexão com o banco de dados
    /// utilizar esta classe para fazer a implementação das regras de negócios
    /// </summary>
    public class E000NFCBusiness
    {
        /// <summary>
        /// Realiza a validação das notas do cliente
        /// </summary>
        /// <param name="codigoNota">Número da Nota</param>
        /// <param name="codFil">Código da Filial</param>
        /// <param name="cnpj">CNPJ do Cliente</param>
        /// <returns></returns>
        public List<int> ValidarNotaCliente(string codigoNota, string codFil, string cnpj)
        {
            try
            {
                var E000NFCDataAccess = new E000NFCDataAccess();
                return E000NFCDataAccess.ValidarNotaCliente(codigoNota, codFil, cnpj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}

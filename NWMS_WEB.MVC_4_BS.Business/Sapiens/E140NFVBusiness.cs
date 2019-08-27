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
    public class E140NFVBusiness
    {
        /// <summary>
        /// Pesquisa notas fiscais de saida
        /// </summary>
        /// <param name="codigoCliente">Código do Cliente</param>
        /// <param name="codigoMotorista">Código do Motorista</param>
        /// <param name="codPlaca">Código da Placa</param>
        /// <param name="tipoAtendimento">Tipo de Atendimento</param>
        /// <param name="codUsuarioLogado">Código do Usuário Logado</param>
        /// <returns>Lista de notas fiscais</returns>
        public List<E140NFVModel> PesquisarNotasFiscaisSaida(int codigoCliente, int codigoMotorista, string codPlaca, Enums.TipoAtendimento tipoAtendimento, long codUsuarioLogado)
        {
            try
            {
                E140NFVDataAccess E140NFVDataAccess = new E140NFVDataAccess();
                return E140NFVDataAccess.PesquisarNotasFiscaisSaida(codigoCliente, codigoMotorista, codPlaca, tipoAtendimento, codUsuarioLogado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Pesquisa Situacao das notas 
        /// </summary>
        /// <param name="numeroNotaFiscal">Número da Nota Fiscal</param>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <param name="codFilial">Códgio Filial</param>
        /// <returns>Lista de Notas</returns>
        public List<E140NFVModel> PesquisarSituacaoNota(long numeroNotaFiscal, long codigoUsuario, string codFilial)
        {
            try
            {
                E140NFVDataAccess E140NFVDataAccess = new E140NFVDataAccess();
                return E140NFVDataAccess.PesquisarSituacaoNota(numeroNotaFiscal, codigoUsuario, codFilial);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa dados das notas Parametrizadas
        /// </summary>
        /// <param name="codigoNota">Número da Nota Fiscal</param>
        /// <param name="codFil">Código da Filial</param>
        /// <param name="codtra">Código da Transação</param>
        /// <returns>Lista de Notas</returns>
        public List<E140NFVModel> PesquisarDadosNotaParametrizacao(long codigoNota, long codFil, long? codtra)
        {
            try
            {
                E140NFVDataAccess E140NFVDataAccess = new E140NFVDataAccess();
                return E140NFVDataAccess.PesquisarDadosNotaParametrizacao(codigoNota, codFil, codtra);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa dados da nota
        /// </summary>
        /// <param name="codigoNota">Número da Nota</param>
        /// <param name="codFil">Código da Filial</param>
        /// <param name="codtra">Código da Transação</param>
        /// <param name="tipAte">Tipo de Atendimento</param>
        /// <returns>Lista de Notas</returns>
        public List<E140NFVModel> PesquisarDadosNota(long codigoNota, long codFil, long? codtra, string tipAte)
        {
            try
            {
                E140NFVDataAccess E140NFVDataAccess = new E140NFVDataAccess();
                return E140NFVDataAccess.PesquisarDadosNota(codigoNota, codFil, codtra, tipAte);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa tipo de pagamento por nota
        /// </summary>
        /// <param name="codFil">Código da Filial</param>
        /// <param name="codigoNota">Número da Nota</param>
        /// <returns>Lista de Tipo de Pagamento</returns>
        public int PesquisarTipoPagamentoNota(long codFil, long codigoNota)
        {
            try
            {
                E140NFVDataAccess E140NFVDataAccess = new E140NFVDataAccess();
                return E140NFVDataAccess.PesquisarTipoPagamentoNota(codFil, codigoNota);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

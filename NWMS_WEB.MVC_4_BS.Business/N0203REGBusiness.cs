using System;
using System.Collections.Generic;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.DataAccess;
using System.Collections;
using NWORKFLOW_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.Business
{
    /// <summary>
    /// Classe utilizada para chamar a classe de conexão com o banco de dados
    /// utilizar esta classe para fazer a implementação das regras de negócios
    /// </summary>
    public class N0203REGBusiness
    {
        private List<N0203REG> listaRegistros;
        /// <summary>
        /// Emitir lançamento de notas 
        /// </summary>
        /// <param name="codRegistro">Código da ocorrência</param>
        /// <param name="dadosProtocolo">Tabela de ocorrência</param>
        /// <param name="mensagemRetorno">Mensagem de retorno</param>
        /// <returns></returns>
        public bool EmitirLancamentoNfe(long codRegistro, N0203REG dadosProtocolo, out string mensagemRetorno)
        {
            try
            {
                mensagemRetorno = string.Empty;
                var NfeLancamentoDataAccess = new NfeLancamentoDataAccess();
                return NfeLancamentoDataAccess.EmitirLancamentoNfe(dadosProtocolo, out mensagemRetorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ConsultarOrigem(int Ocorrencia)
        {
            var N0203REGDataAccess = new N0203REGDataAccess();
            bool Motivo = N0203REGDataAccess.ConsultarOrigem(Ocorrencia);
            return Motivo;
 
        }

        public bool InserirTransporteIndenizado(long Numreg, int Codtra)
        {
            N0203REGDataAccess n0203REGDataAccess = new N0203REGDataAccess();
            return n0203REGDataAccess.InserirTransporteIndenizado(Numreg, Codtra);
        }

        public listaTransportadora ConsultaTransportadora(int ocorrencia, string Tipo)
        {
            var N0203GDataAccess = new N0203REGDataAccess();
            return N0203GDataAccess.ConsultaTransportadora(ocorrencia, Tipo);
        }

        public bool verificaAprovador(long CodOri, long CodAtendimento)
        {
            var N0203GDataAccess = new N0203REGDataAccess();
            return N0203GDataAccess.verificaAprovador(CodOri, CodAtendimento);
        }

        public string OrigemOcorrencia(int NumReg)
        {
            var N0203GDataAccess = new N0203REGDataAccess();
            return N0203GDataAccess.OrigemOcorrencia(NumReg);
        }

        public bool PedidosViaOcorrencia(int ocorrencia, long Usuario, out string mensagemRetorno)
        {
            try
            {
                mensagemRetorno = string.Empty;
                var pedidosViaOcorrencia = new PedidosViaOcorrenciaDataAccess();
                return pedidosViaOcorrencia.EmitirPedido(ocorrencia, Usuario, out mensagemRetorno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa Registro de ocorrência
        /// </summary>
        /// <param name="codRegistro">Código da ocorrência</param>
        /// <param name="sitRegistro">Situação da ocorrência</param>
        /// <returns>Lista de Ocorrências</returns>
        public N0203REG PesquisaRegistroOcorrencia(long codRegistro, int sitRegistro)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.PesquisaRegistroOcorrencia(codRegistro, sitRegistro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Volta a situação do Registro de Ocorrência
        /// </summary>
        /// <param name="codigoRegistro">Código da ocorrência</param>
        public void rollbackAprovacao(string codigoRegistro)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                N0203REGDataAccess.rollbackAprovacao(codigoRegistro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para fazer a exclussão dos agrupamentos
        /// </summary>
        /// <param name="excluirAgrupamentoSelecionado">String de exclusão do agrupamento</param>
        /// <returns></returns>
        public bool excluirAgrupamento(string excluirAgrupamentoSelecionado)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.excluirAgrupamento(excluirAgrupamentoSelecionado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Chama a Classe para pesquisar os agrupamentos
        /// </summary>
        /// <param name="codigoCliente">Código do Cliente</param>
        /// <param name="filtro">Filtro</param>
        /// <returns></returns>
        public List<Agrupamento> pesquisarAgrupamento(long codigoCliente, int filtro)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.pesquisarAgrupamento(codigoCliente, filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Chama a Classe que busca as observações do SAC na Ocorrência
        /// </summary>
        /// <param name="numreg">Número da ocorrência</param>
        /// <returns></returns>
        public string pesquisarObservacaoSAC(long numreg)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.pesquisarObservacaoSAC(numreg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a class
        /// </summary>
        /// <param name="ocorrencias">Grupo de ocorrências</param>
        /// <param name="dataGeracao">Data de geração da ocorrência</param>
        /// <param name="usuarioGeracao">Usuário de Geração da ocorrência</param>
        /// <returns></returns>
        public string GravarAgrupamento(string ocorrencias, string dataGeracao, string usuarioGeracao)
        {
            var N0203REGDataAccess = new N0203REGDataAccess();
            return N0203REGDataAccess.GravarAgrupamento(ocorrencias, dataGeracao, usuarioGeracao);
        }
        /// <summary>
        /// Chama a classe para pesquisar todas ocorrências agrupadas
        /// </summary>
        /// <returns></returns>
        public List<Agrupamento> pesquisarOcorrenciaAGP()
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.pesquisarOcorrenciaAGP();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para pesquisar os agrupamentos por ocorrências
        /// </summary>
        /// <param name="numreg">Número do agrupamento da ocorrência</param>
        /// <returns></returns>
        public List<Agrupamento> pesquisarPorOcorrenciaAGP(long numreg)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.pesquisarPorOcorrenciaAGP(numreg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para fazer a gravação dos registros de ocorrências
        /// </summary>
        /// <param name="N0203REG">Tabela de ocorrência</param>
        /// <returns></returns>
        public bool GravarRegistroOcorrenciaPesquisa(N0203REG N0203REG)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.GravarRegistroOcorrenciaPesquisa(N0203REG);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string consultarPlacaPOC(string NUMREG)
        {
            string Placa;
            N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
            Placa = N0203REGDataAccess.consultarPlacaPOC(NUMREG);
            return Placa;
        }
        /// <summary>
        /// Chama a classe para fazer a confirmação do recebimento da mercadoria
        /// </summary>
        /// <param name="NUMREG">Código da ocorrência</param>
        /// <param name="PLACA">Placa</param>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <returns></returns>
        public bool confirmarRecebimento(string NUMREG, string PLACA, long codigoUsuario)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.confirmarRecebimento(NUMREG, PLACA, codigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Chama a clase para criar um novo registro de ocorrência
        /// </summary>
        /// <param name="N0203REG">Tabela de ocorrência</param>
        /// <param name="codProtocolo">Código da ocorrência</param>
        /// <returns></returns>
        public bool InserirRegistroOcorrencia(N0203REG N0203REG, out long codProtocolo)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.InserirRegistroOcorrencia(N0203REG, out codProtocolo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para inserir a ocorrência na tabela de coleta
        /// </summary>
        /// <param name="numReg">Código da ocorrência</param>
        /// <param name="codPlaca">Placa</param>
        /// <param name="usuGer">Usuário de geraçao da ocorrência</param>
        /// <param name="datVin">Data de Vinculação</param>
        /// <param name="observacao">Observação</param>
        /// <returns></returns>
        public bool Vincular(string numReg, string codPlaca, string usuGer, string datVin, string observacao)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.Vincular(numReg, usuGer, datVin, codPlaca, observacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Chama a classe que verifica se existe transportadora para esta ocorrência
        /// </summary>
        /// <param name="numReg">Código da ocorrência</param>
        /// <returns></returns>
        public bool ValidarOcorrenciaTransportadora(string numReg)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.ValidarOcorrenciaTransportadora(numReg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para gerar uma nova solicitação de coleta
        /// </summary>
        /// <param name="numReg">Código da ocorrência</param>
        /// <param name="codPlaca">Placa</param>
        /// <param name="usuGer">Usuário de geração da ocorrência</param>
        /// <param name="datVin">Data de vinculação</param>
        /// <param name="observacao">Observação</param>
        /// <returns></returns>
        public bool Revincular(string numReg, string codPlaca, string usuGer, string datVin, string observacao)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.ReVincular(numReg, usuGer, datVin, codPlaca, observacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a Classe para pesquisar totos os protocolos atrasados por usuário
        /// </summary>
        /// <param name="codUsuarioLogado">Código do Usuário logado</param>
        /// <returns>true/false</returns>
        public int PesquisaProtocolosAtrasados(long codUsuarioLogado)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.PesquisaProtocolosAtrasados(codUsuarioLogado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string descTransportadoraIndenizacao(long CodTra)
        {
            N0203REGDataAccess n0203REGDataAccess = new N0203REGDataAccess();
            return n0203REGDataAccess.descTransportadoraIndenizacao(CodTra);
        }
        /// <summary>
        /// Chama a classe para pesquisar todos os registros de ocorrência 
        /// </summary>
        /// <param name="codigoRegistro">Código da ocorrência</param>
        /// <param name="codUsuarioLogado">Código do Usuário Logado</param>
        /// <returns></returns>
        public List<N0203REG> PesquisaRegistrosOcorrencia(long? codigoRegistro, long codUsuarioLogado)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.PesquisaRegistrosOcorrencia(codigoRegistro, codUsuarioLogado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para listar todas as ocorrências da coleta
        /// </summary>
        /// <returns></returns>
        public List<N0204POC> ocorrenciasColeta()
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.ocorrenciasColeta();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Chama a classe para pesquisar todos os registros de ocorrências por situação
        /// </summary>
        /// <param name="codigoRegistro">Código da ocorrência</param>
        /// <param name="situacao">Código da Situação</param>
        /// <returns></returns>
        public N0203REG PesquisaRegistrosOcorrenciaPorSituacao(long codigoRegistro, int situacao)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.PesquisaRegistrosOcorrenciaPorSituacao(codigoRegistro, situacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para cancelar os registros de ocorrências
        /// </summary>
        /// <param name="codigoRegistro">Código da ocorrência</param>
        /// <param name="usuarioTramite">Usuário do Tramite</param>
        /// <returns></returns>
        public bool CancelarRegistrosOcorrencia(long codigoRegistro, long usuarioTramite, string motivoCancelamento)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.CancelarRegistrosOcorrencia(codigoRegistro, usuarioTramite, motivoCancelamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para verificar todos os protocolos abertos 
        /// </summary>
        /// <param name="codigoUsuario">Código Usuário</param>
        /// <param name="protocolosAbertos">Protocolos Abertos</param>
        /// <returns></returns>
        public bool VerificarProtocolosAberto(long codigoUsuario, out string protocolosAbertos)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.VerificarProtocolosAberto(codigoUsuario, out protocolosAbertos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Chama a classe para buscar as informações do relatorio analitico
        /// </summary>
        /// <param name="codigoRegistro">Código da ocorrência</param>
        /// <param name="codFilial">Código da Filial</param>
        /// <param name="analiseEmbarque">Código Analise de Embarque</param>
        /// <param name="dataInicial">Data Inicial</param>
        /// <param name="dataFinal">Data Final</param>
        /// <param name="situacaoReg">Situação da ocorrência</param>
        /// <param name="cliente">Código do Cliente</param>
        /// <param name="codPlaca">Placa</param>
        /// <param name="dataFaturamento">Data de Faturamento</param>
        /// <param name="tipoPesquisa">Tipo de pesquisa</param>
        /// <returns></returns>
        public List<RelatorioAnalitico> RelatorioAnalitico(long? codigoRegistro, long? codFilial, long? analiseEmbarque, Nullable<DateTime> dataInicial, Nullable<DateTime> dataFinal, int? situacaoReg, long? cliente, string codPlaca, Nullable<DateTime> dataFaturamento, int tipoPesquisa)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.RelatorioAnalitico(codigoRegistro, codFilial, analiseEmbarque, dataInicial, dataFinal, situacaoReg, cliente, codPlaca, dataFaturamento, tipoPesquisa);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para buscar as informações do relatório sintetico
        /// </summary>
        /// <param name="codFilial">Código da Filial</param>
        /// <param name="numAneEmbarque">Número de embarque</param>
        /// <param name="codPlaca">Placa</param>
        /// <param name="dataInicial">Data Inicial</param>
        /// <param name="dataFinal">Data Final</param>
        /// <param name="tipoPesquisa">Tipo de pesquisa</param>
        /// <param name="dataFaturamento">Data de Faturamento</param>
        /// <returns></returns>
        public List<RelatorioSintetico> RelatorioSintetico(long? codFilial, long? numAneEmbarque, string codPlaca, DateTime? dataInicial, DateTime? dataFinal, int tipoPesquisa, DateTime? dataFaturamento, long? codigoCliente)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.RelatorioSintetico(codFilial, numAneEmbarque, codPlaca, dataInicial, dataFinal, tipoPesquisa, dataFaturamento, codigoCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para buscar as informações dos itens da carga para conferencia
        /// </summary>
        /// <param name="codPlaca">Placa</param>
        /// <param name="dataFaturamento">Data de Faturamento</param>
        /// <returns></returns>
        public List<ItensSinteticoCarga> RelatorioSinteticoConferencia(string codPlaca, string dataFaturamento, long ? codigoCliente)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.ItensCargaConferencia(codPlaca, dataFaturamento, codigoCliente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para validar a placa
        /// </summary>
        /// <param name="codPlaca">Placa</param>
        /// <returns></returns>
        public bool validarPlaca(string codPlaca)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.validarPlaca(codPlaca);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// chama a classe para consultar os itens da coleta
        /// </summary>
        /// <param name="codPlaca">Placa</param>
        /// <returns></returns>
        public List<ItensColeta> ItensColeta(string codPlaca)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.ItensColeta(codPlaca);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para consultar os itens da troca
        /// </summary>
        /// <param name="codPlaca">Placa</param>
        /// <param name="dataFaturamento">Data de Faturamento</param>
        /// <returns></returns>
        public List<ItensTroca> ItensTroca(string codPlaca, string dataFaturamento)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.ItensTroca(codPlaca, dataFaturamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para consultar os itens da troca para conferencia
        /// </summary>
        /// <param name="codPlaca">Placa</param>
        /// <param name="dataFaturamento">Data de Faturamento</param>
        /// <returns></returns>
        public List<ItensSinteticoTroca> ItensTrocaConferencia(string codPlaca, string dataFaturamento, long? codCli)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.ItensTrocaConferencia(codPlaca, dataFaturamento, codCli);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para consultar os itens da coleta para conferencia
        /// </summary>
        /// <param name="codPlaca">Placa</param>
        /// <param name="dataFaturamento">Data de Faturamento</param>
        /// <returns></returns>
        public List<ItensSinteticoColeta> ItensColetaConferencia(string codPlaca, string dataFaturamento)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.ItensColetaConferencia(codPlaca, dataFaturamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para o relatório de tempo de carga
        /// </summary>
        /// <param name="filial">Código da filial</param>
        /// <param name="embarque">Código de Embarque</param>
        /// <param name="numeroNotaFiscal">Número da nota fiscal</param>
        /// <param name="codCliente">Código Cliente</param>
        /// <param name="dataInicial">Data Inicial</param>
        /// <param name="dataFinal">Data Final</param>
        /// <param name="motivo">Código do Motivo</param>
        /// <param name="situacao">Situação da ocorrência</param>
        /// <param name="origem">Origem da ocorrência</param>
        /// <param name="tipo">Tipo de Relatório</param>
        /// <param name="dataInicialOCR">Data Inicial da Ocorrência</param>
        /// <param name="dataFinalOCR">Data Final da ocorrência</param>
        /// <param name="codPlaca">Placa</param>
        /// <param name="transportadora">Transportadora</param>
        /// <returns></returns>
        public List<RelatorioTempoCarga> RelatorioTempoCarga(string filial, string embarque, string numeroNotaFiscal, string codCliente, string dataInicial, string dataFinal, string motivo, string situacao, string origem, string tipo, string dataInicialOCR, string dataFinalOCR, string codPlaca, string transportadora)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.RelatorioTempoCarga(filial, embarque, numeroNotaFiscal, codCliente, dataInicial, dataFinal, motivo, situacao, origem, tipo, dataInicialOCR, dataFinalOCR, codPlaca, transportadora);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para buscar informações para o relatório sintetico
        /// </summary>
        /// <param name="filial">Código da filial</param>
        /// <param name="embarque">Código de Embarque</param>
        /// <param name="numeroNotaFiscal">Número da nota fiscal</param>
        /// <param name="codCliente">Código Cliente</param>
        /// <param name="dataInicial">Data Inicial</param>
        /// <param name="dataFinal">Data Final</param>
        /// <param name="motivo">Código do Motivo</param>
        /// <param name="situacao">Situação da ocorrência</param>
        /// <param name="origem">Origem da ocorrência</param>
        /// <param name="tipo">Tipo de Relatório</param>
        /// <param name="dataInicialOCR">Data Inicial da Ocorrência</param>
        /// <param name="dataFinalOCR">Data Final da ocorrência</param>
        /// <param name="codPlaca">Placa</param>
        /// <param name="transportadora">Transportadora</param>
        /// <returns></returns>
        public List<RelatorioSinteticoOcorrencia> RelatorioSinteticoOcorrencia(string filial, string embarque, string numeroNotaFiscal, string codCliente, string dataInicial, string dataFinal, string motivo, string situacao, string origem, string tipo, string dataInicialOCR, string dataFinalOCR, string codPlaca, string transportadora)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.RelatorioSinteticoOcorrencia(filial, embarque, numeroNotaFiscal, codCliente, dataInicial, dataFinal, motivo, situacao, origem, tipo, dataInicialOCR, dataFinalOCR, codPlaca, transportadora);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para fazer a aprovação dos registros de ocorrência
        /// </summary>
        /// <param name="codigoRegistro">Código da ocorrência</param>
        /// <param name="usuarioTramite">Usuário de Tramite</param>
        /// <param name="observacao">Observação</param>
        /// <param name="situacaoRegistro">Situação da ocorrência</param>
        /// <param name="tipoOperacao">Tipo de operação</param>
        /// <returns></returns>
        public string AprovarRegistrosOcorrencia(long codigoRegistro, long usuarioTramite, string observacao, int situacaoRegistro, string tipoOperacao)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.AprovarRegistrosOcorrencia(codigoRegistro, usuarioTramite, observacao, situacaoRegistro, tipoOperacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para listar os protocolos de titulos dos clientes.
        /// </summary>
        /// <param name="codigoRegistro">Código da ocorrência</param>
        /// <returns></returns>
        public List<DadosNotasServicoModel> ListaProtocolosTituloCliente(long codigoRegistro)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.ListaProtocolosTituloCliente(codigoRegistro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para pesquisar protocolos pendentes de aprovação
        /// </summary>
        /// <param name="codUsuarioLogado">Código do Usuário logado</param>
        /// <returns></returns>
        public List<ProtocolosAprovacaoModel> PesquisaProtocolosPendentesAprovacaoNotificacao(long codUsuarioLogado)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaAprovacao = new List<ProtocolosAprovacaoModel>();
                var itemAprovacao = new ProtocolosAprovacaoModel();
                var listaRegistros = new List<N0203REG>();

                listaRegistros = N0203REGDataAccess.PesquisaProtocolosPendentesAprovacao(codUsuarioLogado);
                
                if (listaRegistros.Count > 0)
                {
                    var N0204ATDDataAccess = new N0204ATDDataAccess();
                    var N0204ORIDataAccess = new N0204ORIDataAccess();
                    var E073MOTDataAccess = new E073MOTDataAccess();
                    var E085CLIDataAccess = new E085CLIDataAccess();
                    var N9999USUDataAccess = new N9999USUDataAccess();
                    var ActiveDirectoryDataAccess = new ActiveDirectoryDataAccess();
                    var loginUsuario = string.Empty;
                    foreach (var item in listaRegistros)
                    {
                        itemAprovacao = new ProtocolosAprovacaoModel();
                        itemAprovacao.CodigoRegistro = item.NUMREG;
                        itemAprovacao.CodTipoAtendimento = item.TIPATE;
                        itemAprovacao.DescTipoAtendimento = N0204ATDDataAccess.PesquisaTipoAtendimento().Where(c => c.CODATD == item.TIPATE).FirstOrDefault().DESCATD;
                        itemAprovacao.CodOrigemOcorrencia = item.ORIOCO;
                        itemAprovacao.DescOrigemOcorrencia = N0204ORIDataAccess.PesquisaOrigemOcorrencia().Where(c => c.CODORI == item.ORIOCO).FirstOrDefault().DESCORI;
                        itemAprovacao.CodCliente = item.CODCLI;
                        itemAprovacao.NomeCliente = E085CLIDataAccess.PesquisasClientes(item.CODCLI).FirstOrDefault().NomeFantasia;
                        itemAprovacao.CodMotorista = item.CODMOT;
                        itemAprovacao.NomeMotorista = "TRANSPORTADORA";
                        itemAprovacao.DataHrGeracao = item.DATGER.ToString();
                        itemAprovacao.UsuarioGeracao = item.USUGER;
                        loginUsuario = N9999USUDataAccess.ListaDadosUsuarioPorCodigo(itemAprovacao.UsuarioGeracao).LOGIN;
                        itemAprovacao.NomeUsuarioGeracao = ActiveDirectoryDataAccess.ListaDadosUsuarioAD(loginUsuario).Nome;

                        itemAprovacao.CodSituacaoRegistro = item.SITREG;
                        if (itemAprovacao.CodSituacaoRegistro == (int)Enums.SituacaoRegistroOcorrencia.Fechado)
                        {
                            itemAprovacao.DescSituacaoRegistro = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.SituacaoRegistroOcorrencia.Fechado).GetValue<string>();
                        }
                        else if (itemAprovacao.CodSituacaoRegistro == (int)Enums.SituacaoRegistroOcorrencia.PreAprovado)
                        {
                            itemAprovacao.DescSituacaoRegistro = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.SituacaoRegistroOcorrencia.PreAprovado).GetValue<string>();
                        }

                        itemAprovacao.DataFechamento = item.DATFEC.ToString();
                        itemAprovacao.Observacao = item.OBSREG;
                        listaAprovacao.Add(itemAprovacao);
                    }
                }

                return listaAprovacao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para pesquisar protolos pendentes
        /// </summary>
        /// <param name="campoNumeroRegistro">Código da ocorrência</param>
        /// <param name="campoFilial">Código da Filial</param>
        /// <param name="campoEmbarque">Código de embarque</param>
        /// <param name="campoPlaca">Placa</param>
        /// <param name="campoPeriodoInicial">Périodo Inicial</param>
        /// <param name="campoPeriodoFinal">Periodo Final</param>
        /// <param name="campoCliente">Cliente</param>
        /// <param name="campoSituacao">Situação</param>
        /// <param name="campoDataFaturamento">Data de Faturamento</param>
        /// <param name="codigoUsuario">Codigo do Usuário</param>
        /// <returns></returns>
        public List<Ocorrencia> PesquisaProtocoloPendentes(string campoNumeroRegistro, string campoFilial, string campoEmbarque, string campoPlaca, string campoPeriodoInicial, string campoPeriodoFinal, string campoCliente, string campoSituacao, string campoDataFaturamento, long codigoUsuario)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegistros = new List<Ocorrencia>();
                listaRegistros = N0203REGDataAccess.pesquisaOcorrencia(campoNumeroRegistro, campoFilial, campoEmbarque, campoPlaca, campoPeriodoInicial, campoPeriodoFinal, campoCliente, campoSituacao, campoDataFaturamento, codigoUsuario, "D", "U");

                return listaRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int pedidosFaturarIndenizacao()
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.pedidosFaturarIndenizacao();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<RelatorioGraficoOcorrencia> relatorioGraficoOcorrencias(string mes, string ano, string indicador)
        {
            try { 
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegistros = new List<RelatorioGraficoOcorrencia>();
                listaRegistros = N0203REGDataAccess.relatorioGraficoOcorrencias(mes, ano, indicador);
                return listaRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para buscar informações dos indicadores
        /// </summary>
        /// <param name="status">Situação</param>
        /// <param name="mes">Mês</param>
        /// <param name="filtroAgrup">Filtro de Grupo</param>
        /// <param name="indicador">Indicador</param>
        /// <param name="ano">Ano</param>
        /// <returns></returns>
        public List<Ocorrencia> CarregarIndicadoresTabela(string status, string mes, string filtroAgrup, string indicador, string ano)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegistros = new List<Ocorrencia>();
                listaRegistros = N0203REGDataAccess.CarregarIndicadorIndustria(status, mes, filtroAgrup, indicador, ano);

                return listaRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RelatorioGraficoItens> RelatorioGraficoItens(string status, string mes, string filtroAgrup, string indicador, string ano)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegristros = new List<RelatorioGraficoItens>();
                listaRegristros = N0203REGDataAccess.RelatorioGraficoItens(status, mes, filtroAgrup, indicador, ano);

                return listaRegristros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Chama a classe para listar as informações
        /// </summary>
        /// <param name="numreg">Código da ocorrência</param>
        /// <returns></returns>
        public List<String> listarObservacoes(string numreg)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listarObservacoes = new List<String>();
                listarObservacoes = N0203REGDataAccess.listarObservacoes(numreg);

                return listarObservacoes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para carregar os indicadores por setor
        /// </summary>
        /// <param name="status">Situação</param>
        /// <param name="mes">Mês</param>
        /// <param name="indicador">Indicador</param>
        /// <param name="ano">Ano</param>
        /// <returns></returns>
        public List<Ocorrencia> carregarIndicadorSetores(string status, string mes, string indicador, string ano)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegistros = new List<Ocorrencia>();
                listaRegistros = N0203REGDataAccess.CarregarIndicadorSetores(status, mes, indicador, ano);

                return listaRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para pesquisar os protocolos aprovados esperando faturamento
        /// </summary>
        /// <param name="campoNumeroRegistro">Código da ocorrência</param>
        /// <param name="campoFilial">Código da Filial</param>
        /// <param name="campoEmbarque">Código do Embarque</param>
        /// <param name="campoPlaca">Placa</param>
        /// <param name="campoPeriodoInicial">Periodo Inicial</param>
        /// <param name="campoPeriodoFinal">Periodo Final</param>
        /// <param name="campoCliente">Cliente</param>
        /// <param name="campoSituacao">Situação</param>
        /// <param name="campoDataFaturamento">Data de Faturamento</param>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <returns></returns>
        public List<Ocorrencia> carregarProtocolosForamAprovadosEsperandoFaturamento(string campoNumeroRegistro, string campoFilial, string campoEmbarque, string campoPlaca, string campoPeriodoInicial, string campoPeriodoFinal, string campoCliente, string campoSituacao, string campoDataFaturamento, long codigoUsuario)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegistros = new List<Ocorrencia>();
                listaRegistros = N0203REGDataAccess.pesquisaOcorrencia(campoNumeroRegistro, campoFilial, campoEmbarque, campoPlaca, campoPeriodoInicial, campoPeriodoFinal, campoCliente, campoSituacao, campoDataFaturamento, codigoUsuario, "D", "");
                return listaRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Chama a classe para pesquisar os protocolos pendentes de aprovação para o dashboard
        /// </summary>
        /// <param name="campoNumeroRegistro">Código da ocorrência</param>
        /// <param name="campoFilial">Código da filial</param>
        /// <param name="campoEmbarque">Embarque</param>
        /// <param name="campoPlaca">Placa</param>
        /// <param name="campoPeriodoInicial">Periodo Inicial</param>
        /// <param name="campoPeriodoFinal">Periodo Final</param>
        /// <param name="campoCliente">Cliente</param>
        /// <param name="campoSituacao">Situação</param>
        /// <param name="campoDataFaturamento">Data de Faturamento</param>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <returns></returns>
        public List<Ocorrencia> PesquisarProtocolosPendentesAprovacaoDashBoard(string campoNumeroRegistro, string campoFilial, string campoEmbarque, string campoPlaca, string campoPeriodoInicial, string campoPeriodoFinal, string campoCliente, string campoSituacao, string campoDataFaturamento, long codigoUsuario)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegistros = new List<Ocorrencia>();
                listaRegistros = N0203REGDataAccess.pesquisaOcorrencia(campoNumeroRegistro, campoFilial, campoEmbarque, campoPlaca, campoPeriodoInicial, campoPeriodoFinal, campoCliente, campoSituacao, campoDataFaturamento, codigoUsuario, "D", "");

                return listaRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Ocorrencia> PesquisaProtocolosIndenizados(string campoNumeroRegistro, string campoFilial, string campoEmbarque, string campoPlaca, string campoPeriodoInicial, string campoPeriodoFinal, string campoCliente, string campoSituacao, string campoDataFaturamento, long codigoUsuario)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegistros = new List<Ocorrencia>();
                listaRegistros = N0203REGDataAccess.pesquisaOcorrencia(campoNumeroRegistro, campoFilial, campoEmbarque, campoPlaca, campoPeriodoInicial, campoPeriodoFinal, campoCliente, campoSituacao, campoDataFaturamento, codigoUsuario, "D", "");

                return listaRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">Situação</param>
        /// <param name="mes">Mês</param>
        /// <param name="filtroAgrup">Filtro do grupo</param>
        /// <param name="indicador">Indicador</param>
        /// <param name="ano">Ano</param>
        /// <returns></returns>
        public List<Ocorrencia> ocorrenciaDrill(string status, string mes, string filtroAgrup, string indicador, string ano)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegistros = new List<Ocorrencia>();
                listaRegistros = N0203REGDataAccess.ocorrenciaDrill(status, mes, filtroAgrup, indicador, ano);

                return listaRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Retorna uma lista de ocorrências com faturamento em dia
        /// </summary>
        /// <returns>listaRegistros</returns>
        public List<Ocorrencia> carregarOcorrenciasFaturamentoEmDia()
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegistros = new List<Ocorrencia>();
                listaRegistros = N0203REGDataAccess.carregarOcorrenciaFaturamentoEmDia();

                return listaRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Retorna uma lista de ocorrrências com faturamento em atraso
        /// </summary>
        /// <returns>listaRegistros</returns>
        public List<Ocorrencia> carregarOcorrenciasFaturamentoAtraso()
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegistros = new List<Ocorrencia>();
                listaRegistros = N0203REGDataAccess.carregarOcorrenciaFaturamentoAtrasado();

                return listaRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Retorna uma lista de Ocorrência
        /// </summary>
        /// <param name="campoNumeroRegistro">Código da ocorrência</param>
        /// <param name="campoFilial">Filial</param>
        /// <param name="campoEmbarque">Ambarque</param>
        /// <param name="campoPlaca">Placa</param>
        /// <param name="campoPeriodoInicial">Periodo Inicial</param>
        /// <param name="campoPeriodoFinal">Periodo Final</param>
        /// <param name="campoCliente">Cliente</param>
        /// <param name="campoSituacao">Situação</param>
        /// <param name="campoDataFaturamento">Data de Faturamento</param>
        /// <param name="codigoUsuario">Código de Usuário</param>
        /// <returns>listaRegistros</returns>
        public List<Ocorrencia> PesquisaProtocolosAprovadosDashBoard(string campoNumeroRegistro, string campoFilial, string campoEmbarque, string campoPlaca, string campoPeriodoInicial, string campoPeriodoFinal, string campoCliente, string campoSituacao, string campoDataFaturamento, long codigoUsuario)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegistros = new List<Ocorrencia>();
                listaRegistros = N0203REGDataAccess.pesquisaOcorrencia(campoNumeroRegistro, campoFilial, campoEmbarque, campoPlaca, campoPeriodoInicial, campoPeriodoFinal, campoCliente, campoSituacao, campoDataFaturamento, codigoUsuario, "D", "U");

                return listaRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroOcorrencia">Código da ocorrência</param>
        /// <returns>listaRegistros</returns>
        public List<TimeLine> timeLine(long numeroOcorrencia)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegistros = new List<TimeLine>();
                listaRegistros = N0203REGDataAccess.timeLine(numeroOcorrencia);

                return listaRegistros;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Retorna a quantidade de protocolos aprovados X pendentes de aprovação pesquisado por código de usuário logado
        /// </summary>
        /// <param name="codUsuarioLogado">Código do Usuário logado</param>
        /// <returns>quantidade</returns>
        public List<long> PesquisaProtocolosAprovadosXPendentesAprovacao(long codUsuarioLogado)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegistros = new List<ProtocolosAprovacaoModel>();
                List<long> quantidade = N0203REGDataAccess.PesquisaProtocolosAprovadosXPendentesAprovacao(codUsuarioLogado);

                return quantidade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Retorna a quantidade de ocorrências com faturamento em atraso
        /// </summary>
        /// <returns>quantidade</returns>
        public ArrayList carregarAtrasoFaturamento()
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var listaRegistros = new List<ProtocolosAprovacaoModel>();
                ArrayList quantidade = N0203REGDataAccess.ocorrenciasAtrasoFaturamento();

                return quantidade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Retorna uma lista de protocolos pendentes de aprovação
        /// </summary>
        /// <param name="codUsuarioLogado">Código do Usuário logado</param>
        /// <returns>listaAprovacao</returns>
        public List<ProtocolosAprovacaoModel> PesquisaProtocolosPendentesAprovacao(long codUsuarioLogado)
        {
            try
            {
                DebugEmail email = new DebugEmail();

                var N0203REGDataAccess = new N0203REGDataAccess();

                var listaAprovacao = new List<ProtocolosAprovacaoModel>();

                var itemAprovacao = new ProtocolosAprovacaoModel();

                var listaRegistros = new List<N0203REG>();

                listaRegistros = N0203REGDataAccess.PesquisaProtocolosPendentesAprovacao(codUsuarioLogado);

                if (listaRegistros.Count > 0)
                {
                    var N0204ATDDataAccess = new N0204ATDDataAccess();
                    var N0204ORIDataAccess = new N0204ORIDataAccess();
                    var E073MOTDataAccess = new E073MOTDataAccess();
                    var E085CLIDataAccess = new E085CLIDataAccess();
                    var N9999USUDataAccess = new N9999USUDataAccess();
                    var ActiveDirectoryDataAccess = new ActiveDirectoryDataAccess();
                    var loginUsuario = string.Empty;
                    foreach (var item in listaRegistros)
                    {
                        itemAprovacao = new ProtocolosAprovacaoModel();
                        itemAprovacao.CodigoRegistro = item.NUMREG;
                        itemAprovacao.CodTipoAtendimento = item.TIPATE;
                        itemAprovacao.DescTipoAtendimento = N0204ATDDataAccess.PesquisaTipoAtendimento().Where(c => c.CODATD == item.TIPATE).FirstOrDefault().DESCATD;
                        itemAprovacao.CodOrigemOcorrencia = item.ORIOCO;
                        itemAprovacao.DescOrigemOcorrencia = N0204ORIDataAccess.PesquisaOrigemOcorrencia().Where(c => c.CODORI == item.ORIOCO).FirstOrDefault().DESCORI;
                        itemAprovacao.CodCliente = item.CODCLI;
                        
                        itemAprovacao.NomeCliente = E085CLIDataAccess.PesquisasClientes(item.CODCLI).FirstOrDefault().NomeFantasia;

                        if (itemAprovacao.NomeCliente == null)
                        {
                            itemAprovacao.NomeCliente = "Nome Fantasia ";
                        }

                        itemAprovacao.CodMotorista = item.CODMOT;
                        itemAprovacao.NomeMotorista = "TRANSPORTADORA";
                        if (item.CODMOT != 0)
                        {
                            itemAprovacao.NomeMotorista = E073MOTDataAccess.PesquisasMotoristas(item.CODMOT).FirstOrDefault().Nome;
                            if (item.PLACA != null)
                                itemAprovacao.CodPlaca = item.PLACA.Substring(0, 3) + "-" + item.PLACA.Substring(3, 4);
                        }
                        itemAprovacao.DataHrGeracao = item.DATGER.ToString();
                        itemAprovacao.UsuarioGeracao = item.USUGER;
                        loginUsuario = N9999USUDataAccess.ListaDadosUsuarioPorCodigo(itemAprovacao.UsuarioGeracao).LOGIN;
                        itemAprovacao.NomeUsuarioGeracao = ActiveDirectoryDataAccess.ListaDadosUsuarioAD(loginUsuario).Nome;
                        itemAprovacao.CodSituacaoRegistro = item.SITREG;
                        if (itemAprovacao.CodSituacaoRegistro == (int)Enums.SituacaoRegistroOcorrencia.Fechado)
                        {
                            itemAprovacao.DescSituacaoRegistro = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.SituacaoRegistroOcorrencia.Fechado).GetValue<string>();
                        }
                        else if (itemAprovacao.CodSituacaoRegistro == (int)Enums.SituacaoRegistroOcorrencia.PreAprovado)
                        {
                            itemAprovacao.DescSituacaoRegistro = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.SituacaoRegistroOcorrencia.PreAprovado).GetValue<string>();
                        }
                        itemAprovacao.UltimaAlteracao = item.DATULT.ToString();
                        itemAprovacao.UsuarioUltimaAlteracao = item.USUULT;
                        loginUsuario = N9999USUDataAccess.ListaDadosUsuarioPorCodigo(itemAprovacao.UsuarioUltimaAlteracao).LOGIN;
                        itemAprovacao.NomeUsuarioUltimaAlteracao = ActiveDirectoryDataAccess.ListaDadosUsuarioAD(loginUsuario).Nome;
                        itemAprovacao.DataFechamento = item.DATFEC.ToString();
                        itemAprovacao.UsuarioFechamento = item.USUFEC;
                        loginUsuario = N9999USUDataAccess.ListaDadosUsuarioPorCodigo(long.Parse(itemAprovacao.UsuarioFechamento.ToString())).LOGIN;
                        itemAprovacao.NomeUsuarioFechamento = ActiveDirectoryDataAccess.ListaDadosUsuarioAD(loginUsuario).Nome;
                        itemAprovacao.Observacao = item.OBSREG;
                        if (item.N0203IPV.Count > 0)
                        {
                            var cCRegistro = item.N0203IPV.GroupBy(c => c.CODDEP).ToList().OrderBy(c => c.Key);
                            foreach (var itemCentroCusto in cCRegistro)
                            {
                               itemAprovacao.CentrosCustos = itemAprovacao.CentrosCustos + itemCentroCusto.Key + " - ";
                            }
                            itemAprovacao.CentrosCustos = itemAprovacao.CentrosCustos.Substring(0, itemAprovacao.CentrosCustos.Length - 3);
                        }
                        var totalDev = (from b in item.N0203IPV
                                        group b by new { b.NUMREG } into grupo
                                        select new
                                        {
                                            ValorBruto = grupo.Sum(v => v.QTDDEV * v.PREUNI),
                                            //ValorIpi = grupo.Sum(v => (v.QTDDEV * v.PREUNI) * float.Parse(((v.PERIPI / 100).ToString()))),
                                            ValorIpi = grupo.Sum(v => v.QTDDEV * (v.VLRIPI / v.QTDFAT)),
                                            //ValorDev = grupo.Sum(v => (v.QTDDEV * v.PREUNI) + (v.QTDDEV * v.PREUNI) * float.Parse(((v.PERIPI / 100).ToString())))
                                            ValorDev = grupo.Sum(v => decimal.Parse((v.QTDDEV * v.PREUNI).ToString()) + (v.QTDDEV * (v.VLRIPI / v.QTDFAT)) + (v.QTDDEV * (v.VLRST / v.QTDFAT)))
                                        }).FirstOrDefault();
                        itemAprovacao.ValorDevolucao = Convert.ToDouble(totalDev.ValorDev).ToString("###,###,##0.00");
                        listaAprovacao.Add(itemAprovacao);
                     }
                }
                return listaAprovacao;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        /// Chama a classe para fazer a aprovação de registros de ocorrências
        /// </summary>
        /// <param name="codigoRegistro">Código da ocorrência</param>
        /// <param name="codUsuarioLogado">Código de Usuário ligado</param>
        /// <param name="observacaoAprovacao">Observação de Aprovação</param>
        /// <returns></returns>
        public bool AprovarRegistrosOcorrenciaNivel1(long codigoRegistro, long codUsuarioLogado, string observacaoAprovacao)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.AprovarRegistrosOcorrenciaNivel1(codigoRegistro, codUsuarioLogado, observacaoAprovacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classse para reprovar os registros de ocorrências
        /// </summary>
        /// <param name="codigoRegistro">Código da ocorrência</param>
        /// <param name="codUsuarioLogado">Código de Usuário Logado</param>
        /// <param name="observacaoReprovacao">Observação de reprovação</param>
        /// <returns></returns>
        public bool ReprovarRegistrosOcorrenciaNivel1(long codigoRegistro, long codUsuarioLogado, string observacaoReprovacao)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.ReprovarRegistrosOcorrenciaNivel1(codigoRegistro, codUsuarioLogado, observacaoReprovacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para consultar por parâmetro se esta justificativa existe no banco de dados.
        /// </summary>
        /// <param name="loginUsuario">Login Usuário</param>
        /// <returns></returns>
        public bool consultarParametroJustificativaColeta(string loginUsuario)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.consultarParametroJustificativaColeta(loginUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para verificar se este parâmetro esta cadastrado para este usuário
        /// </summary>
        /// <param name="loginUsuario">Login do Usuário</param>
        /// <returns></returns>
        public string consultarParametroJustificativa(string loginUsuario)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.consultarParametroJustificativa(loginUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para inserir o parâmetro para este usuário
        /// </summary>
        /// <param name="loginUsuario">Login do Usuário</param>
        /// <param name="operacao">Operação</param>
        /// <returns></returns>
        public bool inserirVinculo(string loginUsuario, string operacao)
        {
            try
            {
                N0203REGDataAccess N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.inserirVinculo(loginUsuario, operacao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para validar todas as notas de outros protocolos
        /// </summary>
        /// <param name="codProtocolo">Código da ocorrência</param>
        /// <param name="lista">Lista</param>
        /// <param name="tipAtend">Tipo de Atendimento</param>
        /// <param name="msgRetorno">Mensagem de retorno</param>
        /// <returns></returns>
        public bool ValidaNotasOutroProtocolo(long? codProtocolo, List<Tuple<long, long>> lista, string tipAtend, out string msgRetorno, string usuarioLogado)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.ValidaNotasOutroProtocolo(codProtocolo, lista, tipAtend, out msgRetorno, usuarioLogado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para verificar a quantidade de protolos por area
        /// </summary>
        /// <param name="dias">Dias </param>
        /// <param name="situacao">Situação</param>
        /// <returns></returns>
        public List<N0204ORI> quantidadeProtocolosPorArea(int dias, int situacao)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var quantidadeItensPorArea = new List<N0204ORI>();
                quantidadeItensPorArea = N0203REGDataAccess.quantidadeProtocolosPorArea(dias, situacao);

                return quantidadeItensPorArea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para verificar a quantidade de protocolos por area mes
        /// </summary>
        /// <param name="dias">Dias</param>
        /// <param name="situacao">Situação</param>
        /// <returns></returns>
        public List<N0204ORI> quantidadeProtocolosPorAreaMeses(int dias, int situacao)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var quantidadeItensPorArea = new List<N0204ORI>();
                quantidadeItensPorArea = N0203REGDataAccess.quantidadeProtocolosPorAreaMeses(dias, situacao);

                return quantidadeItensPorArea;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para validar as notas dos protocolos reprovados
        /// </summary>
        /// <param name="codProtocolo">Código da ocorrência</param>
        /// <param name="lista">Lista</param>
        /// <returns></returns>
        public bool ValidaNotasProtocoloReprovado(long codProtocolo, List<Tuple<long, long>> lista)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.ValidaNotasProtocoloReprovado(codProtocolo, lista);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para pegar as informações do mes por ocorrência
        /// </summary>
        /// <returns></returns>
        public List<MesXOcorrencia> mesXOcorrencia()
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var mesOcorrencia = new List<MesXOcorrencia>();
                mesOcorrencia = N0203REGDataAccess.mesXOcorrencia();

                return mesOcorrencia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para pegar as informações mes x origem 
        /// </summary>
        /// <returns></returns>
        public List<MesXOrigem> mesXOrigem()
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                var mesOrigem = new List<MesXOrigem>();
                mesOrigem = N0203REGDataAccess.mesXOrigem();

                return mesOrigem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para pegar a media por protocolo pre aprovado
        /// </summary>
        /// <returns></returns>
        public int mediaPreAprovado()
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.mediaPreAprovado();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para verificar a média dos protocolos aprovados
        /// </summary>
        /// <returns></returns>
        public int mediaAprovado()
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.mediaAprovado();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para imprimir relatório analitico 
        /// </summary>
        /// <param name="campoNumeroRegistro">Código da ocorrência</param>
        /// <param name="campoFilial">Filial</param>
        /// <param name="campoEmbarque">Embarque</param>
        /// <param name="campoPlaca">Placa</param>
        /// <param name="campoPeriodoInicial">Periodo Inicial</param>
        /// <param name="campoPeriodoFinal">Periodo Final</param>
        /// <param name="campoCliente">Cliente</param>
        /// <param name="campoSituacao">Situação</param>
        /// <param name="campoDataFaturamento">Data de Faturamento</param>
        /// <returns></returns>
        public List<RelatorioAnalitico> imprimirRelatorioAnaliticoRegistroOcorrencia(string campoNumeroRegistro, string campoFilial, string campoEmbarque, string campoPlaca, string campoPeriodoInicial, string campoPeriodoFinal, string campoCliente, string campoSituacao, string campoDataFaturamento)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.imprimirRelatorioAnaliticoRegistroOcorrencia(campoNumeroRegistro, campoFilial, campoEmbarque, campoPlaca, campoPeriodoInicial, campoPeriodoFinal, campoCliente, campoSituacao, campoDataFaturamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Chama a classe para imprimir o relatório sintetico de registro de ocorrência
        /// </summary>
        /// <param name="campoNumeroRegistro">Código da ocorrência</param>
        /// <param name="campoFilial">Filial</param>
        /// <param name="campoEmbarque">Embarque</param>
        /// <param name="campoPlaca">Placa</param>
        /// <param name="campoPeriodoInicial">Periodo Inicial</param>
        /// <param name="campoPeriodoFinal">Periodo Final</param>
        /// <param name="campoCliente">Cliente</param>
        /// <param name="campoSituacao">Situação</param>
        /// <param name="campoDataFaturamento">Data de Faturamento</param>
        /// <returns></returns>
        public List<RelatorioAnalitico> imprimirRelatorioSinteticoRegistroOcorrencia(string campoNumeroRegistro, string campoFilial, string campoEmbarque, string campoPlaca, string campoPeriodoInicial, string campoPeriodoFinal, string campoCliente, string campoSituacao, string campoDataFaturamento)
        {
            try
            {
                var N0203REGDataAccess = new N0203REGDataAccess();
                return N0203REGDataAccess.imprimirRelatorioSinteticoRegistroOcorrencia(campoNumeroRegistro, campoFilial, campoEmbarque, campoPlaca, campoPeriodoInicial, campoPeriodoFinal, campoCliente, campoSituacao, campoDataFaturamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public long codUsuarioLogado { get; set; }
    }
}

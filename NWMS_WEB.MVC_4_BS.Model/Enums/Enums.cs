
namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class Enums
    {
        public enum Sistema
        {
            /// <summary>
            /// Sistema NWMS
            /// </summary>
            [Attributes.KeyValue("Descricao", "NWMS")]
            NWMS = 1,

            /// <summary>
            /// Sistema NWORKFLOW
            /// </summary>
            [Attributes.KeyValue("Descricao", "NWORKFLOW")]
            NWORKFLOW = 2
        }

        public enum Logado
        {
            /// <summary>
            /// Logado Sim
            /// </summary>
            [Attributes.KeyValue("Descricao", "Sim")]
            Sim = 'S',

            /// <summary>
            /// Logado Não
            /// </summary>
            [Attributes.KeyValue("Descricao", "Não")]
            Nao = 'N'
        }

        public enum TipoPessoa
        {
            /// <summary>
            /// Tipo de Pessoa Física
            /// </summary>
            [Attributes.KeyValue("Descricao", "Física")]
            Fisica = 'F',

            /// <summary>
            /// Tipo de Pessoa Juridica
            /// </summary>
            [Attributes.KeyValue("Descricao", "Juridica")]
            Juridica = 'J'
        }

        public enum SituacaoRegistro
        {
            /// <summary>
            /// Situação Registro - Ativo
            /// </summary>
            [Attributes.KeyValue("Descricao", "Ativo")]
            Ativo = 'A',

            /// <summary>
            /// Situação Registro - Inativo
            /// </summary>
            [Attributes.KeyValue("Descricao", "Inativo")]
            Inativo = 'I'
        }

        public enum OracleStringConnection
        {
            /// <summary>
            /// String de Conexão Banco Sapiens Produção
            /// </summary>

#if DEBUG
            [Attributes.KeyValue("Descricao", "DATA SOURCE=ORA11GT; PASSWORD=nwms4651teste;USER ID=NWMS_PRODUCAO")]
#else
            [Attributes.KeyValue("Descricao", "DATA SOURCE=ORA11G; PASSWORD=nplii4600;USER ID=NWMS_PRODUCAO")]
#endif
            Sapiens = 'S'
        }

        //string connectionString = ConfigurationManager.ConnectionStrings["Context"].ConnectionString;
        //connectionString = connectionString.Substring(connectionString.Length - 13, 13);

        public enum DataSourceOracle
        {
            [Attributes.KeyValue("Descricao", "NWMS_HOMOLOGA")]
            BANCO_NWMS_HOMOLOGA = 1,

            [Attributes.KeyValue("Descricao", "NWMS_PRODUCAO")]
            BANCO_NWMS_PRODUCAO = 2
        }

        public enum Operacao
        {
            /// <summary>
            /// Operacao Pesquisar
            /// </summary>
            [Attributes.KeyValue("Descricao", "Pesquisar")]
            Pesquisar = 'P',

            /// <summary>
            /// Operacao Inserir
            /// </summary>
            [Attributes.KeyValue("Descricao", "Inserir")]
            Inserir = 'I',

            /// <summary>
            /// Operacao Alterar
            /// </summary>
            [Attributes.KeyValue("Descricao", "Alterar")]
            Alterar = 'A',

            /// <summary>
            /// Operacao Excluir
            /// </summary>
            [Attributes.KeyValue("Descricao", "Excluir")]
            Excluir = 'E'
        }

        public enum MotivoDevolucao
        {
            [Attributes.KeyValue("Descricao", "Selecione...")]
            Selecione = 0,

            /// <summary>
            /// Motivo de Devolucao Mercadoria Quebrada
            /// </summary>
            [Attributes.KeyValue("Descricao", "Mercadoria Quebrada")]
            MercadoriaQuebrada = 1,

            /// <summary>
            /// Motivo de Devolucao Cliente Não Pediu
            /// </summary>
            [Attributes.KeyValue("Descricao", "Cliente Não Pediu")]
            ClienteNaoPediu = 2,

            /// <summary>
            /// Motivo de Devolucao Devolução por Falta
            /// </summary>
            [Attributes.KeyValue("Descricao", "Devolução por Falta")]
            DevolucaoPorFalta = 3,

            /// <summary>
            /// Motivo de Devolucao Desacordo Financeiro
            /// </summary>
            [Attributes.KeyValue("Descricao", "Desacordo Financeiro")]
            DesacordoFinanceiro = 4,

            /// <summary>
            /// Motivo de Devolucao Devolução NF
            /// </summary>
            [Attributes.KeyValue("Descricao", "Devolução NF")]
            DevolucaoNF = 5,

            /// <summary>
            /// Motivo de Devolucao Código de Barras Errado
            /// </summary>
            [Attributes.KeyValue("Descricao", "Código de Barras Errado")]
            CodigoBarrasErrado = 6,

            /// <summary>
            /// Motivo de Devolucao Cor Errada
            /// </summary>
            [Attributes.KeyValue("Descricao", "Cor Errada")]
            CorErrada = 7,

            /// <summary>
            /// Motivo de Devolucao Tamanho Errado
            /// </summary>
            [Attributes.KeyValue("Descricao", "Tamanho Errado")]
            TamanhoErrado = 8,

            /// <summary>
            /// Motivo de Devolucao Modelo Errado
            /// </summary>
            [Attributes.KeyValue("Descricao", "Modelo Errado")]
            ModeloErrado = 9,

            /// <summary>
            /// Motivo de Devolucao Produto Vencido
            /// </summary>
            [Attributes.KeyValue("Descricao", "Produto Vencido")]
            ProdutoVencido = 10,

            /// <summary>
            /// Motivo de Devolucao Entrada Fora do Prazo
            /// </summary>
            [Attributes.KeyValue("Descricao", "Entrada Fora do Prazo")]
            EntradaForaPrazo = 11,

            /// <summary>
            /// Motivo de Devolucao Desistência Cliente
            /// </summary>
            [Attributes.KeyValue("Descricao", "Desistência Cliente")]
            DesistenciaCliente = 12,

            /// <summary>
            /// Motivo de Devolucao Defeito no Produto
            /// </summary>
            [Attributes.KeyValue("Descricao", "Defeito no Produto")]
            DefeitoProduto = 13
        }


        public enum OrigemOcorrencia
        {
            [Attributes.KeyValue("Descricao", "Selecione...")]
            Selecione = '0',

            /// <summary>
            /// Origem de Ocorrência Acerto
            /// </summary>
            [Attributes.KeyValue("Descricao", "Acerto")]
            Acerto = '1',

            /// <summary>
            /// Origem de Ocorrência Cliente
            /// </summary>
            [Attributes.KeyValue("Descricao", "Cliente")]
            Cliente = '2',

            /// <summary>
            /// Origem de Ocorrência Comercial
            /// </summary>
            [Attributes.KeyValue("Descricao", "Comercial")]
            Comercial = '3',

            /// <summary>
            /// Origem de Ocorrência Entrega
            /// </summary>
            [Attributes.KeyValue("Descricao", "Entrega")]
            Entrega = '4',

            /// <summary>
            /// Origem de Ocorrência Expedição
            /// </summary>
            [Attributes.KeyValue("Descricao", "Expedição")]
            Expedicao = '5',

            /// <summary>
            /// Origem de Ocorrência Faturamento
            /// </summary>
            [Attributes.KeyValue("Descricao", "Faturamento")]
            Faturamento = '6',

            /// <summary>
            /// Origem de Ocorrência Financeiro
            /// </summary>
            [Attributes.KeyValue("Descricao", "Financeiro")]
            Financeiro = '7',

            /// <summary>
            /// Origem de Ocorrência Indústria
            /// </summary>
            [Attributes.KeyValue("Descricao", "Indústria")]
            Industria = '8',

            /// <summary>
            /// Origem de Ocorrência Representante
            /// </summary>
            [Attributes.KeyValue("Descricao", "Representante")]
            Representante = '9'
        }

        public enum TipoAtendimento
        {
            [Attributes.KeyValue("Descricao", "Devolução de Mercadorias")]
            DevolucaoMercadorias = 1,

            [Attributes.KeyValue("Descricao", "Troca de Mercadorias")]
            TrocaMercadorias = 2
        }

        public enum SituacaoRegistroOcorrencia
        {
            [Attributes.KeyValue("Descricao", "Rascunho")]
            Pendente = 1,

            [Attributes.KeyValue("Descricao", "Aguardando Aprovação")]
            Fechado = 2,

            [Attributes.KeyValue("Descricao", "Integrado")]
            Aprovado = 3,

            [Attributes.KeyValue("Descricao", "Aprovado")]
            PreAprovado = 4,

            [Attributes.KeyValue("Descricao", "Reprovado")]
            Reprovado = 1,

            [Attributes.KeyValue("Descricao", "Reaprovar")]
            Reaprovar = 6,

            [Attributes.KeyValue("Descricao", "Coleta")]
            Coleta = 8,

            [Attributes.KeyValue("Descricao", "Conferido")]
            Recebido = 9,

            [Attributes.KeyValue("Descricao", "Faturado")]
            Faturado = 10,

            [Attributes.KeyValue("Descricao", "Cancelado")]
            Cancelado = 7,

            [Attributes.KeyValue("Descricao", "Indenizado")]
            Indenizado = 11,
        }

        public enum TipoPesquisaRegistroOcorrencia
        {
            [Attributes.KeyValue("Descricao", "Número Registro")]
            NumeroRegistro = 1,

            [Attributes.KeyValue("Descricao", "Análise de Embarque")]
            AnaliseEmbarque = 2,

            [Attributes.KeyValue("Descricao", "Período")]
            Periodo = 3,

            [Attributes.KeyValue("Descricao", "Situação do Registro")]
            Situacao = 4,

            [Attributes.KeyValue("Descricao", "Cliente")]
            Cliente = 5,

            [Attributes.KeyValue("Descricao", "Período e Situação do Registro")]
            Periodo_SitReg = 6,

            [Attributes.KeyValue("Descricao", "Período e Cliente")]
            Periodo_Cliente = 7,

            [Attributes.KeyValue("Descricao", "Situação e Cliente")]
            Situacao_Cliente = 8,

            [Attributes.KeyValue("Descricao", "Período, Situação do Registro Cliente e Cliente")]
            Periodo_Situacao_Cliente = 9,

            [Attributes.KeyValue("Descricao", "Placa e Período")]
            Placa_Periodo = 10,

            [Attributes.KeyValue("Descricao", "Data de Faturamento")]
            Placa_Data_Faturamento = 11,
        }

        public enum ActiveDirectory
        {
            [Attributes.KeyValue("Descricao", "LDAP://srvdc01.nutriplan.com.br/OU=nutriplan,DC=nutriplan,DC=com,DC=br")]
            Endereco = 1,

            [Attributes.KeyValue("Descricao", "WEB")]
            Usuario = 2,

            [Attributes.KeyValue("Descricao", "WEBnutri2014")]
            Senha = 3,
        }

        public enum OperacaoAprovacaoFaturamento
        {
            [Attributes.KeyValue("Descricao", "Aprovar")]
            Aprovar = 1,

            [Attributes.KeyValue("Descricao", "Cancelar")]
            Cancelar = 2,

            [Attributes.KeyValue("Descricao", "Reaprovar")]
            Reaprovar = 3
        }

        public enum TipoNotaDevolucao
        {
            [Attributes.KeyValue("Descricao", "Nutriplan")]
            Nutriplan = 3,

            [Attributes.KeyValue("Descricao", "Cliente")]
            Cliente = 2
        }

        public enum Email
        {
            [Attributes.KeyValue("Descricao", @"<div><style>body {
                                            margin: 0;
                                        }
                                
                                        body {
                                            font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
                                            font-size: 14px;
                                            line-height: 1.42857143;
                                            color: #555555;
                                            background-color: #ffffff;
                                        }
                                
                                        table {
                                            border-collapse: collapse;
                                            border-spacing: 0;
                                        }
                                
                                        td,
                                        th {
                                            padding: 0;
                                        }
                                
                                        th {
                                            text-align: left;
                                        }
                                
                                        h3 {
                                            font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;
                                            font-weight: 500;
                                            line-height: 1.1;
                                            color: #317eac;
                                        }
                                
                                        .panel {
                                            margin-bottom: 20px;
                                            background-color: #ffffff;
                                            border: 1px solid transparent;
                                            border-radius: 4px;
                                            -webkit-box-shadow: 0 1px 1px ;
                                            box-shadow: 0 1px 1px ;
                                        }
                                
                                        .panel-success {
                                            border-color: #dddddd;
                                        }
                                
                                        .panel-heading {
                                            padding: 10px 15px;
                                            border-bottom: 1px solid transparent;
                                            border-top-right-radius: 3px;
                                            border-top-left-radius: 3px;
                                        }
                                
                                        .panel-success .panel-heading {
                                            color: #468847;
                                            background-color: #73a839;
                                            border-color: #dddddd;
                                        }
                                
                                        .panel-success .panel-heading,
                                        .panel-warning .panel-heading,
                                        .panel-warning .panel-title,
                                        .panel-success .panel-title {
                                            color: #fff;
                                        }
                                
                                        .panel-title {
                                            margin-top: 0;
                                            margin-bottom: 0;
                                            font-size: 16px;
                                            color: inherit;
                                        }
                                
                                        .panel-body {
                                            padding: 6px;
                                        }
                                
                                        .panel-warning {
                                            border-color: #dddddd;
                                        }
                                
                                            .panel-warning .panel-heading {
                                                color: #c09853;
                                                background-color: #dd5600;
                                                border-color: #dddddd;
                                            }
                                
                                                .panel-warning .panel-heading + .panel-collapse .panel-body {
                                                    border-top-color: #dddddd;
                                                }
                                
                                            .panel-warning .panel-footer + .panel-collapse .panel-body {
                                                border-bottom-color: #dddddd;
                                            }
                                
                                        .table {
                                            width: 100%;
                                            margin-bottom: 20px;
                                        }
                                
                                            .table thead tr th,
                                            .table tbody tr th,
                                            .table thead tr td,
                                            .table tbody tr td {
                                                padding: 8px;
                                                line-height: 1.42857143;
                                                vertical-align: top;
                                                border-top: 1px solid #dddddd;
                                            }
                                
                                            .table thead tr th {
                                                vertical-align: bottom;
                                                border-bottom: 2px solid #dddddd;
                                            }
                                
                                                .table thead tr td.active,
                                                .table tbody tr td.active,
                                                .table thead tr th.active,
                                                .table tbody tr th.active,
                                                .table thead tr.active td,
                                                .table tbody tr.active td,
                                                .table thead tr.active th,
                                                .table tbody tr.active th {
                                                    background-color: #f5f5f5;
                                                }
                                
                                        .alert {
                                            padding: 15px;
                                            margin-bottom: 20px;
                                            border: 1px solid transparent;
                                            border-radius: 4px;
                                        }
                                
                                        .alert-danger {
                                            background-color: #f2dede;
                                            border-color: #eed3d7;
                                            color: #b94a48;
                                        }
                                
                                        .well {
                                            min-height: 20px;
                                            padding: 19px;
                                            margin-bottom: 20px;
                                            background-color: #f5f5f5;
                                            border: 1px solid #e3e3e3;
                                            border-radius: 4px;
                                            -webkit-box-shadow: inset 0 1px 1px ;
                                            box-shadow: inset 0 1px 1px ;
                                        }
                                
                                            .well blockquote {
                                                border-color: #ddd;
                                                border-color: ;
                                            }
                                
                                        .well-lg {
                                            padding: 24px;
                                            border-radius: 6px;
                                        }
                                
                                        .well-sm {
                                            padding: 9px;
                                            border-radius: 3px;
                                        }
                                
                                        .well-sm {
                                            padding: 9px;
                                            border-radius: 3px;
                                        }</style>
                                        <div class='alert alert-danger'>
                                            <strong> E - mail enviado automaticamente pelo sistema de ocorrência. Por favor não responda essa mensagem.</ strong >
                                        </div>")]
            Cabecalho = 1,

            [Attributes.KeyValue("Descricao", "<div>")]
            Corpo = 2,

            [Attributes.KeyValue("Descricao", @"<strong>http://ocorrencia.nutriplan.com.br<br>
                                                NWORKFLOW WEB.</strong></div></div></div>")]
            Rodape = 3,
        }
    }
}

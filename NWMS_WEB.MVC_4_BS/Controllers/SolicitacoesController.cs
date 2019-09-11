using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using Microsoft.Reporting.WebForms;
using System.IO;
using NWORKFLOW_WEB.MVC_4_BS.Models;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.Business;
using NUTRIPLAN_WEB.MVC_4_BS.Model.Models;
using Oracle.DataAccess.Client;
using System.Data;


namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class SolicitacoesController : BaseController
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();
        #region Rotina de Envio de Email

        protected void MontarEmailAprovacaoProtocolo(string codProtocolo, string emailDestino, string copiarEmails, Enums.TipoAtendimento tipoAtendimento)
        {
#if DEBUG
            emailDestino = "sistema02@nutriplan.com.br";
            //copiarEmails = string.Empty;
#endif
            string Assunto = "APROVAÇÃO DE OCORRÊNCIA DE DEVOLUÇÃO - PENDENTE";
            string tipoAtd = "DEVOLUÇÃO";

            if (tipoAtendimento == Enums.TipoAtendimento.TrocaMercadorias)
            {
                Assunto = "APROVAÇÃO DE OCORRÊNCIA DE TROCA - PENDENTE";
                tipoAtd = "TROCA";
            }

            var emailCabecalho = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Cabecalho).GetValue<string>();
            var emailCorpo = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Corpo).GetValue<string>();
            var emailRodape = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Rodape).GetValue<string>();

            StringBuilder Mensagem = new StringBuilder();
            Mensagem.AppendLine(emailCabecalho);
            Mensagem.AppendLine(emailCorpo);
            Mensagem.AppendLine(@"<div class='panel panel-success'>
                                    <div class='panel-heading'>
                                        <h3 class='panel-title'>APROVAÇÃO DE OCORRÊNCIA DE " + tipoAtd + @"</h3>
                                    </div>
                                    <div class='panel-body'>
                                        <strong>Olá,<br/><br/>
                                            O Ocorrência de " + tipoAtd.ToLower() + @" Nº " + codProtocolo + @" está pendente de sua aprovação.<br/><br/>
                                            Favor acessar o sistema pelo endereço abaixo para aprovação da mesma.<br/>
                                        </strong><br/>");
            Mensagem.AppendLine(emailRodape);

            var Email = new Email();
            Email.EnviarEmail(emailDestino, copiarEmails, Assunto, Mensagem.ToString());
        }


        #endregion

        #region ViewModels das telas de Solicitações

        public PesqLancRegistroOcorrenciaViewModel PesqLancRegistroOcorrencia { get; set; }

        public CadLancRegistroOcorrenciaViewModel CadLancRegistroOcorrencia { get; set; }

        public CadTipoAtendimentoViewModel CadastroTipoAtendimento { get; set; }

        public CadOrigemOcorrenciaViewModel CadastroOrigemOcorrencia { get; set; }

        public CadMotivoDevolucaoViewModel CadastroMotivoDevolucao { get; set; }

        public CadMotDevXOrigOcorViewModel CadastroMotDevXOrigOcor { get; set; }

        public CadTipAtendXOrigOcorViewModel CadastroTipAtendXOrigOcor { get; set; }

        public RelatorioAnaliticoViewModel ConsultaRelAnalitico { get; set; }

        public RelatorioSinteticoViewModel ConsultaRelSintetico { get; set; }

        public CadTipAtendXUsuarioModel CadastroTipAtendXUsuario { get; set; }

        public CadPrazoDevTrocaModel CadastroPrazoDevTrocaXUsuario { get; set; }

        #endregion

        #region Pesquisa

        /// <summary>
        /// Carregar a tela de Pesquisa de Cadastro de Lançamento de Registro de Ocorrência
        /// </summary>
        /// <returns>view de pesquisa</returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Pesquisar()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {

                var n9999MENBusiness = new N9999MENBusiness();

                // Validação para verificar se o usuário tem acesso quando digitar a url da pagina no navegador.
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "Solicitacoes/Cadastrar").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaViewPesquisa();

                return this.View("Pesquisar", this.PesqLancRegistroOcorrencia);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        /// <summary>
        /// Envio dos dados da "tela" de Pesquisa de Cadastro de Lançamento de Registro de Ocorrência
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pesquisar(PesqLancRegistroOcorrenciaViewModel modelo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {
                this.InicializaViewPesquisa();
                modelo.ListaMotivoDevolucao = this.PesqLancRegistroOcorrencia.ListaMotivoDevolucao;
                modelo.ListaOrigemOcorrencia = this.PesqLancRegistroOcorrencia.ListaOrigemOcorrencia;
                modelo.ListaTipoAtendimento = this.PesqLancRegistroOcorrencia.ListaTipoAtendimento;

                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                N0203REG N0203REG = new N0203REG();
                N0203ANX N0203ANX = new N0203ANX();
                var dataAtual = DateTime.Now;

                N0203REG.NUMREG = long.Parse(modelo.NumRegistro);
                N0203REG.SITREG = (long)Enums.SituacaoRegistroOcorrencia.Pendente;

                if (modelo.Acao == "Finalizar")
                {
                    N0203REG.DATFEC = dataAtual;
                    N0203REG.SITREG = (long)Enums.SituacaoRegistroOcorrencia.Fechado;
                    N0203REG.USUFEC = long.Parse(this.CodigoUsuarioLogado);
                }

                N0203REG.DATULT = dataAtual;
                N0203REG.OBSREG = " ";
                if (!string.IsNullOrEmpty(modelo.Observacoes))
                {
                    N0203REG.OBSREG = modelo.Observacoes;
                }

                N0203REG.USUULT = long.Parse(this.CodigoUsuarioLogado);

                string[] listaItensDev = new string[1];
                listaItensDev[0] = modelo.ListaItensDevolucao;
                N0203REG.N0203IPV = this.MontaListaItensDevolucao(listaItensDev, dataAtual, modelo.NumRegistro);

                if (modelo.AnexoArquivo[0] != null)
                {
                    foreach (HttpPostedFileBase file in modelo.AnexoArquivo)
                    {
                        N0203ANX = new N0203ANX();
                        N0203ANX.NUMREG = N0203REG.NUMREG;
                        N0203ANX.NOMANX = file.FileName;
                        N0203ANX.EXTANX = file.ContentType;

                        byte[] anexoBytes = null;
                        BinaryReader reader = new BinaryReader(file.InputStream);
                        anexoBytes = reader.ReadBytes((int)file.ContentLength);
                        N0203ANX.ANEXO = anexoBytes;

                        N0203REG.N0203ANX.Add(N0203ANX);
                    }
                }

                if (N0203REGBusiness.GravarRegistroOcorrenciaPesquisa(N0203REG))
                {
                    modelo.MensagemRetorno = "Registro salvo com sucesso! Ocorrência número: " + N0203REG.NUMREG.ToString() + ".";

                    DebugEmail email = new DebugEmail();
                    email.Email("Solicitação controller ", modelo.CodTra.ToString());

                    if (modelo.Acao == "Finalizar")
                    {
                        E140NFVBusiness E140NFVBusiness = new E140NFVBusiness();

                        var notas = (from a in N0203REG.N0203IPV
                                     group new { a } by new { a.CODFIL, a.NUMNFV } into grupo
                                     select new
                                     {
                                         CodFilial = grupo.Key.CODFIL,
                                         NumeroNota = grupo.Key.NUMNFV,
                                         ValorBruto = grupo.Sum(v => v.a.QTDDEV * v.a.PREUNI),
                                         ValorIpi = grupo.Sum(v => v.a.QTDDEV * (v.a.VLRIPI / v.a.QTDFAT)),
                                         ValorDev = grupo.Sum(v => decimal.Parse((v.a.QTDDEV * v.a.PREUNI).ToString()) + (v.a.QTDDEV * (v.a.VLRIPI / v.a.QTDFAT)) + (v.a.QTDDEV * (v.a.VLRST / v.a.QTDFAT)))
                                         //ValorBruto = decimal.Round(decimal.Parse(grupo.Sum(v => v.a.QTDDEV * v.a.PREUNI).ToString()), 2, MidpointRounding.AwayFromZero),
                                         //ValorIpi = decimal.Round(decimal.Parse(grupo.Sum(v => (v.a.QTDDEV * v.a.PREUNI) * float.Parse(((v.a.PERIPI / 100).ToString()))).ToString()), 2, MidpointRounding.AwayFromZero),
                                     }).ToList();


                        string notasEmail = string.Empty;
                        string cliente = string.Empty;

                        if (notas.Count > 0)
                        {
                            E085CLIBusiness E085CLIBusiness = new E085CLIBusiness();

                            var codCli = long.Parse(modelo.CodCliente);
                            var nomeCliente = E085CLIBusiness.PesquisaClientes(codCli).FirstOrDefault().NomeFantasia;
                            cliente = "Cliente: " + codCli + " - " + nomeCliente;

                            foreach (var item in notas)
                            {
                                // Nota Tipo Boleto
                                if (E140NFVBusiness.PesquisarTipoPagamentoNota(item.CodFilial, item.NumeroNota) == 2)
                                {
                                    var valorNota = E140NFVBusiness.PesquisarDadosNota(item.NumeroNota, item.CodFilial, null, "3").FirstOrDefault().ValorLiquido;
                                    var valorFatNotaMask = Convert.ToDouble(valorNota).ToString("###,###,##0.00");
                                    var valorNotaDev = item.ValorDev.ToString("###,###,##0.00");

                                    var d = valorNotaDev.ToString().Split(',');
                                    var x = d[1].Substring(0, 2).PadRight(2, '0');
                                    var valorDevNotaMask = d[0] + "," + x;

                                    double valorNotaD = Convert.ToDouble(valorFatNotaMask);
                                    double valorNotaDevD = Convert.ToDouble(valorNotaDev);
                                    var valorReceber = Convert.ToDouble(valorNotaD - valorNotaDevD).ToString("###,###,##0.00");

                                    notasEmail = notasEmail + "Número da Nota: " + item.NumeroNota.ToString() + " - Filial: " + item.CodFilial.ToString() + " - Val. Líquido: " + valorFatNotaMask + " - Val. Líquido Devolução: " + valorDevNotaMask + " - Valor a Receber: " + valorReceber + "&";
                                }
                            }
                        }

                        var tipoAtend = Enums.TipoAtendimento.DevolucaoMercadorias;
                        //16/01/2017 - Rafael Baccin
                        //if (int.Parse(modelo.TipoAtendimento) == (int)Enums.TipoAtendimento.TrocaMercadorias)
                        if ((int)tipoAtend == (int)Enums.TipoAtendimento.TrocaMercadorias)
                            tipoAtend = Enums.TipoAtendimento.TrocaMercadorias;
                        //GRAVANDO REGISTRO
                        if (!string.IsNullOrEmpty(notasEmail))
                        {
                            //this.MontarEmailFinanceiroNotasBoleto(N0203REG.NUMREG.ToString(), notasEmail, cliente, tipoAtend);
                        }

                        N0203REGBusiness n0203REGBusiness = new N0203REGBusiness();
                        n0203REGBusiness.InserirTransporteIndenizado(N0203REG.NUMREG, modelo.CodTra);

                        // Email Usuários Aprovadores
                        var areasAprovadoras = N0203REG.N0203IPV.GroupBy(c => c.ORIOCO).Select(c => c.Key).ToList();

                        N0203UAPBusiness N0203UAPBusiness = new N0203UAPBusiness();
                        var emailsUsuarios = N0203UAPBusiness.PesquisaUsuariosAprovadoresPorOrigens(areasAprovadoras, tipoAtend);

                        if (emailsUsuarios != "")
                        {
                            var auxEmails = emailsUsuarios.Split('&');
                            var emailDestino = string.Empty;
                            var copiarEmails = string.Empty;

                            emailDestino = auxEmails[0];

                            for (int i = 1; i < auxEmails.Length - 1; i++)
                            {
                                copiarEmails = copiarEmails + auxEmails[i] + "&";
                            }

                            this.MontarEmailAprovacaoProtocolo(modelo.NumRegistro, emailDestino, copiarEmails, tipoAtend);
                        }
                    }
                }

                return View("Pesquisar", modelo);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public JsonResult PesquisarRegistro(string codigoRegistro)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                long? codigoReg = null;
                if (!string.IsNullOrEmpty(codigoRegistro))
                {
                    codigoReg = long.Parse(codigoRegistro);
                }

                var ListaN0203REGPesquisa = new List<ListaN0203REGPesquisa>();
                var N0203REGBusiness = new N0203REGBusiness();
                var listaRegistros = new List<N0203REG>();

                listaRegistros = N0203REGBusiness.PesquisaRegistrosOcorrencia(codigoReg, long.Parse(this.CodigoUsuarioLogado));

                if (listaRegistros.Count > 0)
                {
                    ListaN0203REGPesquisa = new List<ListaN0203REGPesquisa>();

                    foreach (N0203REG item in listaRegistros)
                    {
                        ListaN0203REGPesquisa.Add((ListaN0203REGPesquisa)item);
                    }
                }

                if (!string.IsNullOrEmpty(codigoRegistro))
                {
                    return this.Json(new { ListaN0203REGPesquisa, PesquisaUmRegistro = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return this.Json(new { ListaN0203REGPesquisa, PesquisaTodosRegistros = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PesquisarItensDevolucao(string codigoRegistro)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<ListaN0203IPVPesquisa> ListaN0203IPVPesquisa = new List<ListaN0203IPVPesquisa>();
                N0203IPVBusiness N0203IPVBusiness = new N0203IPVBusiness();
                List<N0203IPV> listaItens = new List<N0203IPV>();

                listaItens = N0203IPVBusiness.PesquisarItensDevolucao(long.Parse(codigoRegistro));

                if (listaItens.Count > 0)
                {
                    ListaN0203IPVPesquisa = new List<ListaN0203IPVPesquisa>();

                    foreach (N0203IPV item in listaItens)
                    {
                        ListaN0203IPVPesquisa.Add((ListaN0203IPVPesquisa)item);
                    }
                }

                // Adiciona as notas notas já adicionadas na ultima posição da lista para atualizar os totais de devolução na view de pesquisa
                var listaNotasAdd = ListaN0203IPVPesquisa.GroupBy(c => c.NumeroNota).ToList();
                foreach (var item in listaNotasAdd)
                {
                    var aux = ListaN0203IPVPesquisa[ListaN0203IPVPesquisa.Count - 1].NotasAdicionadas;
                    ListaN0203IPVPesquisa[ListaN0203IPVPesquisa.Count - 1].NotasAdicionadas = aux + item.FirstOrDefault().NumeroNota.ToString() + "-";
                }

                return this.Json(new { ListaN0203IPVPesquisa, PesquisaSucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CancelarRegistrosOcorrencia(string codigoRegistro, string motivoCancelamento)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                bool sucesso = false;
                if (motivoCancelamento == null || motivoCancelamento == "")
                {
                    string msgRetorno = "Cancelamento não Efetuado, Motivo de Cancelamento não Informado, favor informar!";
                    return this.Json(new { msgRetorno, ExcluidoSucesso = sucesso }, JsonRequestBehavior.AllowGet);
                }
                else { 
                    string msgRetorno = "Registro de devolução Nº " + codigoRegistro + " cancelado com sucesso.";
                    sucesso = N0203REGBusiness.CancelarRegistrosOcorrencia(long.Parse(codigoRegistro), long.Parse(this.CodigoUsuarioLogado), motivoCancelamento);
                    return this.Json(new { msgRetorno, ExcluidoSucesso = sucesso }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PesquisarAnexos(string codigoRegistro)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<ListaN0203ANXPesquisa> ListaN0203ANXPesquisa = new List<ListaN0203ANXPesquisa>();
                N0203ANXBusiness N0203ANXBusiness = new N0203ANXBusiness();
                List<N0203ANX> listaAnexo = new List<N0203ANX>();
                bool PesquisaSucesso = false;
                listaAnexo = N0203ANXBusiness.PesquisarAnexos(long.Parse(codigoRegistro));

                if (listaAnexo.Count > 0)
                {
                    ListaN0203ANXPesquisa = new List<ListaN0203ANXPesquisa>();
                    PesquisaSucesso = true;

                    foreach (N0203ANX item in listaAnexo)
                    {
                        ListaN0203ANXPesquisa.Add((ListaN0203ANXPesquisa)item);
                    }
                }

                return this.Json(new { ListaN0203ANXPesquisa, PesquisaSucesso = PesquisaSucesso }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult descTransportadoraIndenizacao(long CodTra)
        {
            N0203REGBusiness n0203REGBusiness = new N0203REGBusiness();
            var DescTransportadora = n0203REGBusiness.descTransportadoraIndenizacao(CodTra);
            return this.Json(new { DescTransportadora = DescTransportadora, redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PesquisarItemAnexo(string codigoRegistro, string idLinhaAnexo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0203ANXBusiness N0203ANXBusiness = new N0203ANXBusiness();
                N0203ANX itemAnexo = new N0203ANX();
                itemAnexo = N0203ANXBusiness.PesquisarItemAnexo(long.Parse(codigoRegistro), long.Parse(idLinhaAnexo));

                //string imageBase64 = Convert.ToBase64String(imageBytes);
                //string imageSrc = string.Format("data:image/jpg;base64,{0}", imageBase64);

                //var base64EncodedPDF = System.Convert.ToBase64String(reportBytes);
                //return this.Json("data:application/pdf;base64, " + base64EncodedPDF);
                //return this.Json("data:application/octet-stream;base64," + base64Encoded);

                if (itemAnexo != null)
                {
                    var base64Encoded = System.Convert.ToBase64String(itemAnexo.ANEXO);
                    return this.Json(new { Anexo = "data:" + itemAnexo.EXTANX + ";base64, " + base64Encoded, TipoAnexo = itemAnexo.EXTANX });
                }

                return this.Json(itemAnexo);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExcluirItemAnexo(string codigoRegistro, string idLinhaAnexo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0203ANXBusiness N0203ANXBusiness = new N0203ANXBusiness();
                string msgRetorno = "Anexo excluído com sucesso.";
                bool sucesso = N0203ANXBusiness.ExcluirItemAnexo(long.Parse(codigoRegistro), long.Parse(idLinhaAnexo));
                return this.Json(new { msgRetorno, ExcluidoSucesso = sucesso }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ValidaNotasProtocoloReprovado(string codProtocolo, string lista)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var listaNotasAgrupadas = this.ListaNotasAgrupadas(lista);
                var N0203REGBusiness = new N0203REGBusiness();
                var validaNotas = N0203REGBusiness.ValidaNotasProtocoloReprovado(long.Parse(codProtocolo), listaNotasAgrupadas);
                return this.Json(new { validaNotas = validaNotas }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Cadastro

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Cadastrar()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {

                var n9999MENBusiness = new N9999MENBusiness();

                // Validação para verificar se o usuário tem acesso quando digitar a url da pagina no navegador.
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "Solicitacoes/Cadastrar").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaViewCadastro();
                this.CadLancRegistroOcorrencia.ListaTipoAtendimento = ListaTipoAtendimentoPorUsuario();


                string protocolosAbertos = string.Empty;
                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                if (N0203REGBusiness.VerificarProtocolosAberto(long.Parse(this.CodigoUsuarioLogado), out protocolosAbertos))
                {
                    this.CadLancRegistroOcorrencia.MensagemRetorno = "As ocorrências apresentadas abaixo se encontram com situação Pendente ou Reprovado. Para poder gerar outras ocorrências, finalize a edição dos mesmos. Registro(s): " + protocolosAbertos + ".";
                }

                return this.View("Cadastrar", this.CadLancRegistroOcorrencia);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar(CadLancRegistroOcorrenciaViewModel modelo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {
                this.InicializaViewCadastro();
                modelo.ListaMotivoDevolucao = this.CadLancRegistroOcorrencia.ListaMotivoDevolucao;
                modelo.ListaOrigemOcorrencia = this.CadLancRegistroOcorrencia.ListaOrigemOcorrencia;
                modelo.ListaTipoAtendimento = ListaTipoAtendimentoPorUsuario();

                ModelState.Remove("NumNotaSug");

                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                N0203REG N0203REG = new N0203REG();
                N0203ANX N0203ANX = new N0203ANX();
                modelo.MensagemRetorno = string.Empty;
                var dataAtual = DateTime.Now;
                long codProtocolo;

                N0203REG.CODCLI = long.Parse(modelo.CodCliente);
                N0203REG.CODMOT = long.Parse(modelo.CodMotorista);

                if (!string.IsNullOrEmpty(modelo.CodPlaca))
                {
                    N0203REG.PLACA = modelo.CodPlaca.Replace("-", "").ToUpper();
                }
                else
                {
                    ModelState.Remove("CodPlaca");
                    ModelState.Remove("DescricaoPlaca");
                }

                N0203REG.SITREG = (long)Enums.SituacaoRegistroOcorrencia.Pendente;

                if (modelo.Acao == "Finalizar")
                {
                    N0203REG.DATFEC = dataAtual;
                    N0203REG.SITREG = (long)Enums.SituacaoRegistroOcorrencia.Fechado;
                    N0203REG.USUFEC = long.Parse(this.CodigoUsuarioLogado);
                }

                N0203REG.DATGER = dataAtual;
                N0203REG.DATULT = dataAtual;
                N0203REG.OBSREG = " ";

                if (!string.IsNullOrEmpty(modelo.Observacoes))
                {
                    N0203REG.OBSREG = modelo.Observacoes;
                }

                N0203REG.ORIOCO = modelo.OrigemOcorrencia;
                N0203REG.TIPATE = modelo.TipoAtendimento;
                N0203REG.USUGER = long.Parse(this.CodigoUsuarioLogado);
                N0203REG.USUULT = long.Parse(this.CodigoUsuarioLogado);

                string[] listaItensDev = new string[1];
                listaItensDev[0] = modelo.ListaItensDevolucao;
                N0203REG.N0203IPV = this.MontaListaItensDevolucao(listaItensDev, dataAtual, string.Empty);

                if (modelo.AnexoArquivo[0] != null)
                {
                    foreach (HttpPostedFileBase file in modelo.AnexoArquivo)
                    {
                        N0203ANX = new N0203ANX();
                        N0203ANX.NOMANX = file.FileName;
                        N0203ANX.EXTANX = file.ContentType;

                        byte[] anexoBytes = null;
                        BinaryReader reader = new BinaryReader(file.InputStream);
                        anexoBytes = reader.ReadBytes((int)file.ContentLength);
                        N0203ANX.ANEXO = anexoBytes;

                        N0203REG.N0203ANX.Add(N0203ANX);
                    }
                }

                if (N0203REGBusiness.InserirRegistroOcorrencia(N0203REG, out codProtocolo))
                {
                    modelo.MensagemRetorno = "Registro salvo com sucesso! Ocorrência número: " + codProtocolo.ToString() + ".";

                    DebugEmail email = new DebugEmail();
                    email.Email("Solicitação controller 2", modelo.CodTra.ToString());

                    N0203REGBusiness n0203REGBusiness = new N0203REGBusiness();
                    n0203REGBusiness.InserirTransporteIndenizado(codProtocolo, modelo.CodTra);

                    if (modelo.Acao == "Finalizar")
                    {
                        E140NFVBusiness E140NFVBusiness = new E140NFVBusiness();

                        var notas = (from a in N0203REG.N0203IPV
                                     group new { a } by new { a.CODFIL, a.NUMNFV } into grupo
                                     select new
                                     {
                                         CodFilial = grupo.Key.CODFIL,
                                         NumeroNota = grupo.Key.NUMNFV,
                                         ValorBruto = decimal.Round(decimal.Parse(grupo.Sum(v => v.a.QTDDEV * v.a.PREUNI).ToString()), 2, MidpointRounding.AwayFromZero),
                                         ValorIpi = decimal.Round(decimal.Parse(grupo.Sum(v => (v.a.QTDDEV * v.a.PREUNI) * float.Parse(((v.a.PERIPI / 100).ToString()))).ToString()), 2, MidpointRounding.AwayFromZero),
                                     }).ToList();


                        string notasEmail = string.Empty;
                        string cliente = string.Empty;

                        if (notas.Count > 0)
                        {
                            E085CLIBusiness E085CLIBusiness = new E085CLIBusiness();

                            var codCli = long.Parse(modelo.CodCliente);
                            var nomeCliente = E085CLIBusiness.PesquisaClientes(codCli).FirstOrDefault().NomeFantasia;
                            cliente = "Cliente: " + codCli + " - " + nomeCliente;

                            foreach (var item in notas)
                            {
                                // Nota Tipo Boleto
                                if (E140NFVBusiness.PesquisarTipoPagamentoNota(item.CodFilial, item.NumeroNota) == 2)
                                {
                                    var valorNota = E140NFVBusiness.PesquisarDadosNota(item.NumeroNota, item.CodFilial, null, "3").FirstOrDefault().ValorLiquido;
                                    var valorFatNotaMask = Convert.ToDouble(valorNota).ToString("###,###,##0.00");
                                    var valorNotaDev = Convert.ToDouble(item.ValorBruto + item.ValorIpi).ToString("###,###,##0.00");

                                    var d = valorNotaDev.ToString().Split(',');
                                    var x = d[1].Substring(0, 2).PadRight(2, '0');
                                    var valorDevNotaMask = d[0] + "," + x;

                                    double valorNotaD = Convert.ToDouble(valorFatNotaMask);
                                    double valorNotaDevD = Convert.ToDouble(valorNotaDev);
                                    var valorReceber = Convert.ToDouble(valorNotaD - valorNotaDevD).ToString("###,###,##0.00");

                                    notasEmail = notasEmail + "Número da Nota: " + item.NumeroNota.ToString() + " - Filial: " + item.CodFilial.ToString() + " - Val. Líquido: " + valorFatNotaMask + " - Val. Líquido Devolução: " + valorDevNotaMask + " - Valor a Receber: " + valorReceber + "&";
                                }
                            }
                        }

                        var tipoAtend = Enums.TipoAtendimento.DevolucaoMercadorias;
                        if (modelo.TipoAtendimento == (int)Enums.TipoAtendimento.TrocaMercadorias)
                            tipoAtend = Enums.TipoAtendimento.TrocaMercadorias;
                        // INSERIR REGISTRO
                        //if (!string.IsNullOrEmpty(notasEmail))
                        //{
                            //   this.MontarEmailFinanceiroNotasBoleto(N0203REG.NUMREG.ToString(), notasEmail, cliente, tipoAtend);
                        //}

                        // Email Usuários Aprovadores
                        var areasAprovadoras = N0203REG.N0203IPV.GroupBy(c => c.ORIOCO).Select(c => c.Key).ToList();

                        N0203UAPBusiness N0203UAPBusiness = new N0203UAPBusiness();
                        var emailsUsuarios = N0203UAPBusiness.PesquisaUsuariosAprovadoresPorOrigens(areasAprovadoras, tipoAtend);

                        if (emailsUsuarios != "")
                        {
                            var auxEmails = emailsUsuarios.Split('&');
                            var emailDestino = string.Empty;
                            var copiarEmails = string.Empty;

                            emailDestino = auxEmails[0];

                            for (int i = 1; i < auxEmails.Length - 1; i++)
                            {
                                copiarEmails = copiarEmails + auxEmails[i] + "&";
                            }

                            this.MontarEmailAprovacaoProtocolo(codProtocolo.ToString(), emailDestino, copiarEmails, tipoAtend);
                        }
                    }
                }

                return View("Cadastrar", modelo);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public List<N0203IPV> MontaListaItensDevolucao(string[] listaItensDev, DateTime dataAtual, string codigoRegistro)
        {
            try
            {
                string[] lista = listaItensDev[0].Replace("[", "").Replace("]", "").Replace("\\", "").Replace("\"", "").Replace("R$", "").Replace("%", "").Replace(",", "").Replace("!", ",").Split('&');

                List<N0203IPV> listaN0203IPV = new List<N0203IPV>();
                N0203IPV itemLista = new N0203IPV();

                if (lista.Length > 0)
                {
                    for (int i = 0; i < lista.Length - 1; i++)
                    {
                        itemLista = new N0203IPV();

                        if (!string.IsNullOrEmpty(codigoRegistro))
                        {
                            itemLista.NUMREG = long.Parse(codigoRegistro);
                        }

                        itemLista.CODEMP = long.Parse(lista[i]);
                        itemLista.CODFIL = long.Parse(lista[i + 1]);
                        itemLista.CODSNF = lista[i + 2];
                        itemLista.NUMNFV = long.Parse(lista[i + 3]);
                        itemLista.SEQIPV = long.Parse(lista[i + 4]);
                        itemLista.TNSPRO = lista[i + 5];
                        itemLista.CODPRO = lista[i + 6];
                        itemLista.CODDER = lista[i + 7];
                        itemLista.CPLIPV = lista[i + 8];
                        itemLista.QTDFAT = long.Parse(lista[i + 9]);
                        itemLista.QTDDEV = long.Parse(lista[i + 10]);
                        itemLista.ORIOCO = ListaOrigens().Where(c => c.Descricao == lista[i + 11]).FirstOrDefault().Codigo;
                        itemLista.CODMOT = ListaMotivos().Where(c => c.Descricao == lista[i + 12]).FirstOrDefault().Codigo;
                        itemLista.PREUNI = float.Parse(lista[i + 13]);
                        itemLista.PEROFE = decimal.Parse(lista[i + 14]);
                        itemLista.PERIPI = decimal.Parse(lista[i + 15]);
                        itemLista.VLRIPI = decimal.Parse(lista[i + 16]);
                        itemLista.VLRLIQ = decimal.Parse(lista[i + 17]);
                        itemLista.VLRST = decimal.Parse(lista[i + 18]);
                        itemLista.CODDEP = lista[i + 19];
                        //itemLista.DATULT = DateTime.Parse(lista[i + 20]);
                        itemLista.DATULT = dataAtual;
                        string[] usuario = lista[i + 21].Split('-');
                        itemLista.USUULT = long.Parse(usuario[0].Replace(" ", ""));
                        listaN0203IPV.Add(itemLista);
                        i = i + 21;
                    }
                }

                return listaN0203IPV;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult PesquisaCliente(string codigoCliente)
        {
            try
            {
                List<E085CLIModel> listaClientes = new List<E085CLIModel>();
                E085CLIBusiness E085CLIBusiness = new E085CLIBusiness();

                if (!string.IsNullOrEmpty(codigoCliente))
                {
                    listaClientes = E085CLIBusiness.PesquisaClientes(long.Parse(codigoCliente));
                    return this.Json(new { listaClientes, PesquisaUmCliente = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    listaClientes = E085CLIBusiness.PesquisaClientes(null);
                    var jsonResult = Json(new { listaClientes, PesquisaTodosClientes = true }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PesquisaMotorista(string codigoMotorista)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<E073MOTModel> listaMotoristas = new List<E073MOTModel>();
                E073MOTBusiness E073MOTBusiness = new E073MOTBusiness();

                if (!string.IsNullOrEmpty(codigoMotorista))
                {
                    listaMotoristas = E073MOTBusiness.PesquisasMotoristas(long.Parse(codigoMotorista));
                    return this.Json(new { listaMotoristas, PesquisaUmMotorista = true }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    listaMotoristas = E073MOTBusiness.PesquisasMotoristas(null);
                    var jsonResult = Json(new { listaMotoristas, PesquisaTodosMotoristas = true }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PesquisarCaminhaoPorPlaca(string codPlaca)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<E073VEIModel> listaCaminhao = new List<E073VEIModel>();
                E073VEIBusiness E073VEIBusiness = new E073VEIBusiness();

                listaCaminhao = E073VEIBusiness.PesquisarCaminhaoPorPlaca(codPlaca.ToUpper());
                return this.Json(new { listaCaminhao }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }


        public string consultaString(String tabela, String coluna, String where)
        {
            String sql = "SELECT " + coluna + " AS COLUNA FROM " + tabela + " WHERE " + where + "";

            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return dr["COLUNA"].ToString();
            }
            return "VAZIO";
        }

        public JsonResult PesquisarDadosNota(string codigoNota, string tipAte)
        {


            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {

                List<E140NFVModel> listaDadosNota = new List<E140NFVModel>();
                E140NFVBusiness E140NFVBusiness = new E140NFVBusiness();
                //VERIFICA PERMISSÃO PARA ABRIR MAIS DE UMA OCORRÊNCIAS
                if (consultaString("NWMS_PRODUCAO.N9999PAR", "OPERACAO", "OPERACAO LIKE '%3%' AND LOGIN = '" + this.LoginUsuario + "'") != "VAZIO" && (tipAte == "1" || tipAte == "2"))
                {
                    listaDadosNota = E140NFVBusiness.PesquisarDadosNotaParametrizacao(long.Parse(codigoNota), 1, 0);
                    return this.Json(new { listaDadosNota }, JsonRequestBehavior.AllowGet);
                }
                //VERIFICA PERMISSÃO PARA ABRIR DUAS OCORRÊNCIAS DE MESMA DATA
                if (consultaString("NWMS_PRODUCAO.N9999PAR", "OPERACAO", "OPERACAO LIKE '%2%' AND LOGIN = '" + this.LoginUsuario + "'") != "VAZIO" && (tipAte == "1" || tipAte == "2"))
                {
                    //VERIFICA SE É DE MESMA DATA

                    if (consultaString(@"NWMS_PRODUCAO.N0203REG REG
                                         INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                                            ON REG.NUMREG = IPV.NUMREG
                                         INNER JOIN SAPIENS.E140NFV NFV
                                            ON NFV.NUMNFV = IPV.NUMNFV", "DISTINCT REG.DATGER",
                                      "IPV.NUMNFV = " + codigoNota + " ") != "VAZIO")
                    {
                        var equalsDate = consultaString(@"
                                          NWMS_PRODUCAO.N0203REG REG
                                         INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                                            ON REG.NUMREG = IPV.NUMREG
                                         INNER JOIN SAPIENS.E140NFV NFV
                                            ON NFV.NUMNFV = IPV.NUMNFV", "DISTINCT REG.DATGER",
                                     "IPV.NUMNFV = " + codigoNota + " " +
                                     " AND TO_CHAR(REG.DATGER, 'dd/mm/yyyy') = TO_CHAR(SYSDATE, 'dd/mm/yyyy')");

                        if (equalsDate != "VAZIO")
                        {
                            listaDadosNota = E140NFVBusiness.PesquisarDadosNotaParametrizacao(long.Parse(codigoNota), 1, 1);
                        }
                        else
                        {
                            return this.Json(new { listaDadosNota, mensagem = "Existe ocorrência para esta nota " + codigoNota + " com data de geração diferente da data atual." }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    else
                    {
                        listaDadosNota = E140NFVBusiness.PesquisarDadosNotaParametrizacao(long.Parse(codigoNota), 1, 1);
                    }
                }
                else
                {
                    listaDadosNota = E140NFVBusiness.PesquisarDadosNota(long.Parse(codigoNota), 1, 1, tipAte);
                }

                return this.Json(new { listaDadosNota }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PesquisaRegistrosOcorrenciaPorSituacao(string codigoRegistro, string situacao)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<ListaN0203REGPesquisa> ListaN0203REGPesquisa = new List<ListaN0203REGPesquisa>();
                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();

                var lista = N0203REGBusiness.PesquisaRegistrosOcorrenciaPorSituacao(long.Parse(codigoRegistro), int.Parse(situacao));

                if (lista != null)
                {
                    ListaN0203REGPesquisa.Add((ListaN0203REGPesquisa)lista);
                }

                return this.Json(new { ListaN0203REGPesquisa }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }


        #endregion

        #region Pesquisa - Cadastro

        public JsonResult PesquisarNotasFiscaisSaida(string codigoCliente, string codigoMotorista, string codPlaca, string tipoAtendimento)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                E140NFVBusiness E140NFVBusiness = new E140NFVBusiness();

                if (!string.IsNullOrEmpty(codPlaca))
                {
                    codPlaca = codPlaca.Replace("-", "").ToUpper();
                }

                var tipoAtend = Enums.TipoAtendimento.DevolucaoMercadorias;
                if (int.Parse(tipoAtendimento) == (int)Enums.TipoAtendimento.TrocaMercadorias)
                    tipoAtend = Enums.TipoAtendimento.TrocaMercadorias;

                var listaNotasSaida = E140NFVBusiness.PesquisarNotasFiscaisSaida(int.Parse(codigoCliente), int.Parse(codigoMotorista), codPlaca, tipoAtend, long.Parse(this.CodigoUsuarioLogado));
                bool PesquisaSucesso = false;

                if (listaNotasSaida.Count > 0)
                {
                    PesquisaSucesso = true;
                }

                return this.Json(new { listaNotasSaida, PesquisaSucesso = PesquisaSucesso }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PesquisarItensNotasFiscaisSaida(string filial, string numeroNota, string serieNota)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                E140IPVBusiness E140IPVBusiness = new E140IPVBusiness();
                var listaItens = E140IPVBusiness.PesquisarItensNotasFiscaisSaida(int.Parse(filial), long.Parse(numeroNota), serieNota);
                bool PesquisaSucesso = false;

                if (listaItens.Count > 0)
                {
                    PesquisaSucesso = true;
                }

                return this.Json(new { listaItens, PesquisaSucesso = PesquisaSucesso }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ValidaNotasOutroProtocolo(string codProtocolo, string tipAtend, string lista)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var listaNotasAgrupadas = this.ListaNotasAgrupadas(lista);
                var N0203REGBusiness = new N0203REGBusiness();
                string msgRetorno = string.Empty;
                long? auxLong = null;
                var codReg = string.IsNullOrEmpty(codProtocolo) ? auxLong : long.Parse(codProtocolo);

                if (consultaString("NWMS_PRODUCAO.N9999PAR", "OPERACAO", "OPERACAO LIKE '%3%' AND LOGIN = '" + this.LoginUsuario + "'") != "VAZIO" && tipAtend == "1")
                {
                    return this.Json(new { validaNotas = true, msgRetorno = "" }, JsonRequestBehavior.AllowGet);
                }


                if (consultaString("NWMS_PRODUCAO.N9999PAR", "OPERACAO", "OPERACAO LIKE '%2%' AND LOGIN = '" + this.LoginUsuario + "'") != "VAZIO" && tipAtend == "1")
                {
                    var equalsDate = consultaString(@"
                                          NWMS_PRODUCAO.N0203REG REG
                                         INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                                            ON REG.NUMREG = IPV.NUMREG
                                         INNER JOIN SAPIENS.E140NFV NFV
                                            ON NFV.NUMNFV = IPV.NUMNFV", "DISTINCT REG.DATGER",
                                    "IPV.NUMNFV = " + listaNotasAgrupadas[0].Item2.ToString() + " " +
                                    " AND TO_CHAR(REG.DATGER, 'dd/mm/yyyy') = TO_CHAR(SYSDATE, 'dd/mm/yyyy')");

                    if (equalsDate != "VAZIO")
                    {
                        return this.Json(new { validaNotas = true, msgRetorno = "" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return this.Json(new { validaNotas = false, msgRetorno = "Sua permissão está ativa mas as ocorrências não são de mesma data." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    var validaNotas = N0203REGBusiness.ValidaNotasOutroProtocolo(codReg, listaNotasAgrupadas, tipAtend, out msgRetorno, this.LoginUsuario);
                    return this.Json(new { validaNotas = validaNotas, msgRetorno = msgRetorno }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<Tuple<long, long>> ListaNotasAgrupadas(string lista)
        {
            try
            {
                var arrayLista = lista.Split('&');
                var listaNova = new List<Tuple<long, long>>();

                if (arrayLista.Length > 0)
                {
                    for (int i = 0; i < arrayLista.Length - 1; i++)
                    {
                        listaNova.Add(new Tuple<long, long>(long.Parse(arrayLista[i]), long.Parse(arrayLista[i + 1])));
                        i = i + 1;
                    }
                }

                return listaNova;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Relatório Analítico

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RelatorioAnalitico()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {

                var n9999MENBusiness = new N9999MENBusiness();

                // Validação para verificar se o usuário tem acesso quando digitar a url da pagina no navegador.
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "Solicitacoes/RelatorioAnalitico").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaView();

                return this.View("RelatorioAnalitico", this.ConsultaRelAnalitico);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public JsonResult montarPermissao(string login)
        {
            try
            {

                var montaPermissao = new N9999MENBusiness();
                var listaDadosUsuario = new N9999USUBusiness();

                var dadosUsuario = listaDadosUsuario.ListaDadosUsuarioPorLogin(login);
                var permissoes = montaPermissao.montaPermissoes(Convert.ToInt64(dadosUsuario.CODUSU), (int)Enums.Sistema.NWORKFLOW);

                return this.Json(new { permissoes = permissoes }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult removerAcesso(long codMen, string usuario)
        {
            try
            {

                var acesso = new N9999MENBusiness();
                var listaDadosUsuario = new N9999USUBusiness();
                var dadosUsuario = listaDadosUsuario.ListaDadosUsuarioPorLogin(usuario);

                var permissoes = acesso.removerAcesso(codMen, Convert.ToInt64(dadosUsuario.CODUSU));

                return this.Json(new { permissoes = permissoes }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult adicionarAcesso(long codMen, string usuario)
        {
            try
            {
                var acesso = new N9999MENBusiness();
                var listaDadosUsuario = new N9999USUBusiness();

                var dadosUsuario = listaDadosUsuario.ListaDadosUsuarioPorLogin(usuario);
                var permissoes = acesso.adicionarAcesso(codMen, Convert.ToInt64(dadosUsuario.CODUSU));

                return this.Json(new { permissoes = permissoes }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RelatorioRegistrosOcorrencia(string codigoRegistro, string codFilial, string analiseEmbarque, string dataInicial, string dataFinal, string situacaoReg, string cliente, string codPlaca, string dataFaturamento, string tipoPesquisa)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<RelatorioAnalitico> listaRegistros = new List<RelatorioAnalitico>();
                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                string msgRetorno = "Nenhum registro encontrado.";

                long? auxLong = null;
                int? auxInt = null;
                Nullable<DateTime> auxData = null;
                var codReg = string.IsNullOrEmpty(codigoRegistro) ? auxLong : long.Parse(codigoRegistro);
                var analEmb = string.IsNullOrEmpty(analiseEmbarque) ? auxLong : long.Parse(analiseEmbarque);
                var dtFat = string.IsNullOrEmpty(dataFaturamento) ? auxData : DateTime.Parse(dataFaturamento);
                var codFil = string.IsNullOrEmpty(codFilial) ? auxLong : long.Parse(codFilial);
                var dtIni = string.IsNullOrEmpty(dataInicial) ? auxData : DateTime.Parse(dataInicial + " 00:00:00");
                var dtFim = string.IsNullOrEmpty(dataFinal) ? auxData : DateTime.Parse(dataFinal + " 23:59:59");
                var sitReg = string.IsNullOrEmpty(situacaoReg) ? auxInt : int.Parse(situacaoReg);
                var codCli = string.IsNullOrEmpty(cliente) ? auxLong : long.Parse(cliente);
                var tipPesq = int.Parse(tipoPesquisa);

                listaRegistros = N0203REGBusiness.RelatorioAnalitico(codReg, codFil, analEmb, dtIni, dtFim, sitReg, codCli, codPlaca.ToUpper(), dtFat, tipPesq);

                if (listaRegistros.Count == 0)
                {
                    return this.Json(new { msgRetorno = msgRetorno, listaVazia = true }, JsonRequestBehavior.AllowGet);
                }

                DateTime dataEmissao = DateTime.Now;
                listaRegistros[0].DATAEMISSAO = dataEmissao.ToString();
                listaRegistros[0].USUIMPR = this.NomeUsuarioLogado;

                LocalReport report = new LocalReport();

                report.ReportPath = Server.MapPath("~/Reports/RelatorioAnalitico.rdlc");

                var reportRelatorio = new ReportDataSource("RelatorioAnaliticoDataSet", listaRegistros);
                report.Refresh();
                report.DataSources.Add(reportRelatorio);

                string reportType = "PDF";
                string mineType;
                byte[] reportBytes;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;

                string deviceInfo =
                "<DeviceInfo>" +
                " <OutputFormat>PDF</OutputFormat>" +
                " <PageWidth>in</PageWidth>" +
                " <PageHeight>in</PageHeight>" +
                " <MarginTop>in</MarginTop>" +
                " <MarginLeft>in</MarginLeft>" +
                " <MarginRight>in</MarginRight>" +
                " <MarginBottom>in</MarginBottom>" +
                "</DeviceInfo>";

                reportBytes = report.Render(
                    reportType,
                    deviceInfo,
                    out mineType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                var base64EncodedPDF = System.Convert.ToBase64String(reportBytes);
                return this.Json("data:application/pdf;base64, " + base64EncodedPDF);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PesquisaTramites(string codigoRegistro)
        {
            try
            {
                var ListaN0203TRAPesquisa = new List<ListaN0203TRAPesquisa>();
                var N0203TRABusiness = new N0203TRABusiness();
                var listaTramites = N0203TRABusiness.PesquisaTramites(long.Parse(codigoRegistro));

                if (listaTramites.Count > 0)
                {
                    ListaN0203TRAPesquisa = new List<ListaN0203TRAPesquisa>();

                    foreach (var item in listaTramites)
                    {
                        ListaN0203TRAPesquisa.Add((ListaN0203TRAPesquisa)item);
                    }
                }

                return this.Json(new { ListaN0203TRAPesquisa = ListaN0203TRAPesquisa }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Relatório Sintético

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RelatorioSintetico()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {

                var n9999MENBusiness = new N9999MENBusiness();

                // Validação para verificar se o usuário tem acesso quando digitar a url da pagina no navegador.
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "Solicitacoes/RelatorioSintetico").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaView();


                return this.View("RelatorioSintetico", this.ConsultaRelSintetico);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public JsonResult RelatorioSintetico(string codFilial, string analiseEmbarque, string codPlaca, string dataInicial, string dataFinal, string tipoPesquisa, string dataFaturamento, long? codigoCliente)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            if (codPlaca == "")
            {
                return this.Json(new { Campos = true }, JsonRequestBehavior.AllowGet);
            }
            if (dataFaturamento == "")
            {
                return this.Json(new { Campos = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<RelatorioSintetico> listaRegistros = new List<RelatorioSintetico>();
                List<ItensTroca> listaItensTroca = new List<ItensTroca>();
                List<ItensColeta> listaItensColeta = new List<ItensColeta>();
                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                // string msgRetorno = "Nenhum registro encontrado.";

                long? auxLong = null;
                Nullable<DateTime> auxData = null;
                var analEmb = string.IsNullOrEmpty(analiseEmbarque) ? auxLong : long.Parse(analiseEmbarque);
                var codFil = string.IsNullOrEmpty(codFilial) ? auxLong : long.Parse(codFilial);
                var dtIni = string.IsNullOrEmpty(dataInicial) ? auxData : DateTime.Parse(dataInicial + " 00:00:00");
                var dtFim = string.IsNullOrEmpty(dataFinal) ? auxData : DateTime.Parse(dataFinal + " 23:59:59");
                var dtFat = string.IsNullOrEmpty(dataFaturamento) ? auxData : DateTime.Parse(dataFaturamento);
                var tipPesq = int.Parse(tipoPesquisa);

                if (N0203REGBusiness.validarPlaca(codPlaca.ToUpper()))
                {
                    return this.Json(new { placa = false }, JsonRequestBehavior.AllowGet);
                }

                /***********************************************************************************************************************************************/
                listaRegistros = N0203REGBusiness.RelatorioSintetico(codFil, analEmb, codPlaca.ToUpper(), dtIni, dtFim, tipPesq, dtFat, codigoCliente);

                if (listaRegistros.Count != 0)
                {
                    DateTime dataEmissao = DateTime.Now;
                    listaRegistros[0].DATAEMISSAO = dataEmissao.ToString();
                    listaRegistros[0].USUIMPR = this.NomeUsuarioLogado;
                }

                /***********************************************************************************************************************************************/
                listaItensTroca = N0203REGBusiness.ItensTroca(codPlaca.ToUpper(), dataFaturamento);
                /***********************************************************************************************************************************************/
                listaItensColeta = N0203REGBusiness.ItensColeta(codPlaca.ToUpper());
                /***********************************************************************************************************************************************/
                LocalReport report = new LocalReport();

                report.ReportPath = Server.MapPath("~/Reports/RelatorioSintetico.rdlc");

                var reportRelatorio = new ReportDataSource("RelatorioSinteticoDataSet", listaRegistros);
                report.Refresh();
                report.DataSources.Add(reportRelatorio);

                var reportItens = new ReportDataSource("ItensTrocaDataSet", listaItensTroca);
                report.Refresh();
                report.DataSources.Add(reportItens);

                var reportItensColeta = new ReportDataSource("ItensColetaDataSet", listaItensColeta);
                report.Refresh();
                report.DataSources.Add(reportItensColeta);

                string reportType = "PDF";
                string mineType;
                byte[] reportBytes;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;

                string deviceInfo =
                "<DeviceInfo>" +
                " <OutputFormat>PDF</OutputFormat>" +
                " <PageWidth>in</PageWidth>" +
                " <PageHeight>in</PageHeight>" +
                " <MarginTop>in</MarginTop>" +
                " <MarginLeft>in</MarginLeft>" +
                " <MarginRight>in</MarginRight>" +
                " <MarginBottom>in</MarginBottom>" +
                "</DeviceInfo>";

                reportBytes = report.Render(
                    reportType,
                    deviceInfo,
                    out mineType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                var base64EncodedPDF = System.Convert.ToBase64String(reportBytes);
                return this.Json("data:application/pdf;base64, " + base64EncodedPDF);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult RelatorioSinteticoConferencia(string codFilial, string analiseEmbarque, string codPlaca, string dataInicial, string dataFinal, string tipoPesquisa, string dataFaturamento, long? codigoCliente)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            if (codPlaca == "")
            {
                return this.Json(new { Campos = true }, JsonRequestBehavior.AllowGet);
            }
            if (dataFaturamento == "")
            {
                return this.Json(new { Campos = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<ItensSinteticoCarga> listaRegistros = new List<ItensSinteticoCarga>();
                List<ItensSinteticoTroca> listaItensTroca = new List<ItensSinteticoTroca>();
                List<ItensSinteticoColeta> listaItensColeta = new List<ItensSinteticoColeta>();
                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                //string msgRetorno = "Nenhum registro encontrado.";

                long? auxLong = null;
                Nullable<DateTime> auxData = null;
                var analEmb = string.IsNullOrEmpty(analiseEmbarque) ? auxLong : long.Parse(analiseEmbarque);
                var codFil = string.IsNullOrEmpty(codFilial) ? auxLong : long.Parse(codFilial);
                var dtIni = string.IsNullOrEmpty(dataInicial) ? auxData : DateTime.Parse(dataInicial + " 00:00:00");
                var dtFim = string.IsNullOrEmpty(dataFinal) ? auxData : DateTime.Parse(dataFinal + " 23:59:59");
                var dtFat = string.IsNullOrEmpty(dataFaturamento) ? auxData : DateTime.Parse(dataFaturamento);
                var tipPesq = int.Parse(tipoPesquisa);

                /***********************************************************************************************************************************************/
                listaRegistros = N0203REGBusiness.RelatorioSinteticoConferencia(codPlaca, dataFaturamento, codigoCliente);

                /* if (listaRegistros.Count == 0)
                 {
                     return this.Json(new { msgRetorno = msgRetorno, listaVazia = true }, JsonRequestBehavior.AllowGet);
                 }
                 else if (listaRegistros[0].ExisteProtocoloFechado == true)
                 {
                     msgRetorno = "Não será possível gerar o relatório pois existem ocorrências(fechadas) pendentes de pré aprovação.";
                     return this.Json(new { msgRetorno = msgRetorno, ExisteProtocoloFechado = true }, JsonRequestBehavior.AllowGet);
                 }*/
                if (listaRegistros.Count != 0)
                {
                    DateTime dataEmissao = DateTime.Now;
                    listaRegistros[0].DATAEMISSAO = dataEmissao.ToString();
                    listaRegistros[0].USUIMPR = this.NomeUsuarioLogado;
                }


                if (N0203REGBusiness.validarPlaca(codPlaca))
                {
                    return this.Json(new { placa = false }, JsonRequestBehavior.AllowGet);
                }

                /***********************************************************************************************************************************************/
                listaItensTroca = N0203REGBusiness.ItensTrocaConferencia(codPlaca.ToUpper(), dataFaturamento, codigoCliente);
                /***********************************************************************************************************************************************/
                /***********************************************************************************************************************************************/
                listaItensColeta = N0203REGBusiness.ItensColetaConferencia(codPlaca.ToUpper(), dataFaturamento);
                /***********************************************************************************************************************************************/
                LocalReport report = new LocalReport();

                report.ReportPath = Server.MapPath("~/Reports/RelatorioSinteticoConferencia.rdlc");

                var reportRelatorio = new ReportDataSource("Carga", listaRegistros);
                report.Refresh();
                report.DataSources.Add(reportRelatorio);

                var reportItensTroca = new ReportDataSource("Troca", listaItensTroca);
                report.Refresh();
                report.DataSources.Add(reportItensTroca);

                var reportItensColeta = new ReportDataSource("Coleta", listaItensColeta);
                report.Refresh();
                report.DataSources.Add(reportItensColeta);

                string reportType = "PDF";
                string mineType;
                byte[] reportBytes;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;

                string deviceInfo =
                "<DeviceInfo>" +
                " <OutputFormat>PDF</OutputFormat>" +
                " <PageWidth>in</PageWidth>" +
                " <PageHeight>in</PageHeight>" +
                " <MarginTop>in</MarginTop>" +
                " <MarginLeft>in</MarginLeft>" +
                " <MarginRight>in</MarginRight>" +
                " <MarginBottom>in</MarginBottom>" +
                "</DeviceInfo>";

                reportBytes = report.Render(
                    reportType,
                    deviceInfo,
                    out mineType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                var base64EncodedPDF = System.Convert.ToBase64String(reportBytes);
                return this.Json("data:application/pdf;base64, " + base64EncodedPDF);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Relatório Tempo Etapas
        public JsonResult RelatorioTempoCarga(string fililal, string numeroNotaFiscal, string codCliente, string dataInicial, string dataFinal, string embarque, string motivo, string situacao, string origem, string tipo, string dataInicialOCR, string dataFinalOCR, string codPlaca, string transpotadora)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                List<RelatorioTempoCarga> listaRegistros = new List<RelatorioTempoCarga>();
                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                string msgRetorno = "Nenhum registro encontrado.";

                listaRegistros = N0203REGBusiness.RelatorioTempoCarga(fililal, numeroNotaFiscal, codCliente, dataInicial, dataFinal, embarque, motivo, situacao, origem, tipo, dataInicialOCR, dataFinalOCR, codPlaca, transpotadora);
                if (listaRegistros.Count == 0)
                {
                    return this.Json(new { msgRetorno = msgRetorno, listaVazia = true }, JsonRequestBehavior.AllowGet);
                }
                LocalReport report = new LocalReport();
                report.ReportPath = Server.MapPath("~/Reports/RelatorioEtapas.rdlc");

                var reportRelatorio = new ReportDataSource("RelatorioEtapas", listaRegistros);
                report.Refresh();
                report.DataSources.Add(reportRelatorio);
                string reportType = "PDF";
                string mineType;
                byte[] reportBytes;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;

                string deviceInfo =
                "<DeviceInfo>" +
                " <OutputFormat>PDF</OutputFormat>" +
                " <PageWidth>in</PageWidth>" +
                " <PageHeight>in</PageHeight>" +
                " <MarginTop>in</MarginTop>" +
                " <MarginLeft>in</MarginLeft>" +
                " <MarginRight>in</MarginRight>" +
                " <MarginBottom>in</MarginBottom>" +
                "</DeviceInfo>";

                reportBytes = report.Render(
                    reportType,
                    deviceInfo,
                    out mineType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                var base64EncodedPDF = System.Convert.ToBase64String(reportBytes);
                return this.Json("data:application/pdf;base64, " + base64EncodedPDF);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Relatório Sintético Ocorrência
        public JsonResult RelatorioSinteticoOcorrencia(string filial, string numeroNotaFiscal, string codCliente, string dataInicial, string dataFinal, string embarque, string motivo, string situacao, string origem, string tipo, string dataInicialOCR, string dataFinalOCR, string codPlaca, string transportadora)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<RelatorioSinteticoOcorrencia> listaRegistros = new List<RelatorioSinteticoOcorrencia>();
                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                string msgRetorno = "Nenhum registro encontrado.";

                listaRegistros = N0203REGBusiness.RelatorioSinteticoOcorrencia(filial, embarque, numeroNotaFiscal, codCliente, dataInicial, dataFinal, motivo, situacao, origem, tipo, dataInicialOCR, dataFinalOCR, codPlaca, transportadora);
                

                if (listaRegistros.Count != 0)
                {
                    DateTime dataEmissao = DateTime.Now;
                    listaRegistros[0].DATAEMISSAO = dataEmissao.ToString();
                    listaRegistros[0].USUIMPR = this.NomeUsuarioLogado;
                }

                if (listaRegistros.Count == 0)
                {
                    return this.Json(new { msgRetorno = msgRetorno, listaVazia = true }, JsonRequestBehavior.AllowGet);
                }

                LocalReport report = new LocalReport();

                report.ReportPath = Server.MapPath("~/Reports/RelatorioOcorrencia.rdlc");

                var reportRelatorio = new ReportDataSource("RelatorioOcorrencia", listaRegistros);
                report.Refresh();
                report.DataSources.Add(reportRelatorio);
                string reportType = "PDF";
                string mineType;
                byte[] reportBytes;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;

                string deviceInfo =
                "<DeviceInfo>" +
                " <OutputFormat>PDF</OutputFormat>" +
                " <PageWidth>in</PageWidth>" +
                " <PageHeight>in</PageHeight>" +
                " <MarginTop>in</MarginTop>" +
                " <MarginLeft>in</MarginLeft>" +
                " <MarginRight>in</MarginRight>" +
                " <MarginBottom>in</MarginBottom>" +
                "</DeviceInfo>";

                reportBytes = report.Render(
                    reportType,
                    deviceInfo,
                    out mineType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                var base64EncodedPDF = System.Convert.ToBase64String(reportBytes);
                return this.Json("data:application/pdf;base64, " + base64EncodedPDF);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        public JsonResult anexosOcorrencia(string codigoRegistro)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0203ANXBusiness N0203ANXBusiness = new N0203ANXBusiness();
                N0203ANX itemAnexo = new N0203ANX();
                itemAnexo = N0203ANXBusiness.PesquisarItemAnexo(long.Parse(codigoRegistro), 1);

                if (itemAnexo != null)
                {
                    var base64Encoded = System.Convert.ToBase64String(itemAnexo.ANEXO);
                    return this.Json(new { Anexo = "data:" + itemAnexo.EXTANX + ";base64, " + base64Encoded, TipoAnexo = itemAnexo.EXTANX });
                }
                else
                {
                    return this.Json(new { MensagemRetorno = "Ocorrência sem Anexo", ErroExcecao = true });
                }
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CadVinculoColeta(string codigoRegistro)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {

                var n9999MENBusiness = new N9999MENBusiness();

                // Validação para verificar se o usuário tem acesso quando digitar a url da pagina no navegador.
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "Solicitacoes/CadVinculoColeta").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaView();


                return this.View("CadVinculoColeta");
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }


        #region Cadastro Tipo de Atendimento

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CadTipoAtendimento()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {

                var n9999MENBusiness = new N9999MENBusiness();

                // Validação para verificar se o usuário tem acesso quando digitar a url da pagina no navegador.
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "Solicitacoes/CadTipoAtendimento").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaView();


                return this.View("CadTipoAtendimento", this.CadastroTipoAtendimento);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CadTipoAtendimento(CadTipoAtendimentoViewModel modelo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {
                this.InicializaView();
                CadastroTipoAtendimento = modelo;

                if (ModelState.IsValid)
                {

                }

                return View("CadTipoAtendimento", this.CadastroTipoAtendimento);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public JsonResult PesquisaTipoAtendimento()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<ListaN0204ATDPesquisa> ListaN0204ATDPesquisa = new List<ListaN0204ATDPesquisa>();
                ListaN0204ATDPesquisa = ListaTiposAtendimento();

                return this.Json(new { ListaN0204ATDPesquisa }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<ListaN0204ATDPesquisa> ListaTiposAtendimento()
        {
            N0204ATDBusiness N0204ATDBusiness = new N0204ATDBusiness();
            List<N0204ATD> listaTipoAtendimento = new List<N0204ATD>();
            List<ListaN0204ATDPesquisa> ListaN0204ATDPesquisa = new List<ListaN0204ATDPesquisa>();
            listaTipoAtendimento = N0204ATDBusiness.PesquisaTipoAtendimento();

            if (listaTipoAtendimento.Count > 0)
            {
                ListaN0204ATDPesquisa = new List<ListaN0204ATDPesquisa>();

                foreach (N0204ATD item in listaTipoAtendimento)
                {
                    ListaN0204ATDPesquisa.Add((ListaN0204ATDPesquisa)item);
                }
            }

            return ListaN0204ATDPesquisa;
        }

        public List<ListaN0204ATDPesquisa> ListaTiposAtendimentoPorUsuario()
        {
            N0204ATDBusiness N0204ATDBusiness = new N0204ATDBusiness();
            List<N0204ATD> listaTipoAtendimento = new List<N0204ATD>();
            List<ListaN0204ATDPesquisa> ListaN0204ATDPesquisa = new List<ListaN0204ATDPesquisa>();
            listaTipoAtendimento = N0204ATDBusiness.PesquisaTipoAtendimentoPorUsuario(int.Parse(this.CodigoUsuarioLogado));

            if (listaTipoAtendimento.Count > 0)
            {
                ListaN0204ATDPesquisa = new List<ListaN0204ATDPesquisa>();

                foreach (N0204ATD item in listaTipoAtendimento)
                {
                    ListaN0204ATDPesquisa.Add((ListaN0204ATDPesquisa)item);
                }
            }

            return ListaN0204ATDPesquisa;
        }

        public JsonResult InserirTipoAtendimento(string descricao)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0204ATDBusiness N0204ATDBusiness = new N0204ATDBusiness();
                string msgRetorno = "Tipo de Atendimento cadastrado com sucesso!";
                bool sucesso = N0204ATDBusiness.InserirTipoAtendimento(descricao);
                return this.Json(new { msgRetorno, GravadoSucesso = sucesso }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AlterarTipoAtendimento(string codigo, string descricao)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0204ATDBusiness N0204ATDBusiness = new N0204ATDBusiness();
                string msgRetorno = "Tipo de Atendimento alterado com sucesso!";
                bool sucesso = N0204ATDBusiness.AlterarTipoAtendimento(int.Parse(codigo), descricao);
                return this.Json(new { msgRetorno, AlteradoSucesso = sucesso }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExcluirTipoAtendimento(string codigo, string descricao)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0204ATDBusiness N0204ATDBusiness = new N0204ATDBusiness();
                string msgRetorno = "Tipo de Atendimento " + codigo + " - " + descricao + " excluído com sucesso!";
                bool sucesso = N0204ATDBusiness.ExcluirTipoAtendimento(int.Parse(codigo));
                return this.Json(new { msgRetorno, ExcluidoSucesso = sucesso }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PesquisaOrigemPorTipoAtend(string codTipAtend)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0204AORBusiness N0204AORBusiness = new N0204AORBusiness();
                List<N0204AOR> ListaN0204AOR = new List<N0204AOR>();
                List<ListaN0204ORIPesquisa> listaTipoAtendOrigem = new List<ListaN0204ORIPesquisa>();
                bool PesquisaSucesso = false;

                ListaN0204AOR = N0204AORBusiness.PesquisaOrigemPorTipoAtend(long.Parse(codTipAtend));

                if (ListaN0204AOR.Count > 0)
                {
                    listaTipoAtendOrigem = new List<ListaN0204ORIPesquisa>();
                    PesquisaSucesso = true;

                    foreach (N0204AOR item in ListaN0204AOR)
                    {
                        listaTipoAtendOrigem.Add((ListaN0204ORIPesquisa)item);
                    }
                }

                return this.Json(new { listaTipoAtendOrigem = listaTipoAtendOrigem, PesquisaSucesso = PesquisaSucesso }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        /// <summary>
        /// Pesquisar dados do usuário informado
        /// </summary>
        /// <param name="codigoUser">código do usuário</param>
        /// <returns>dados</returns>
        public JsonResult PesquisarUsuarios(string loginUsuario)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var listaUsuariosAD = new List<UsuarioADModel>();
                var ActiveDirectoryBusiness = new ActiveDirectoryBusiness();

                if (!string.IsNullOrEmpty(loginUsuario))
                {
                    var usuario = ActiveDirectoryBusiness.ListaDadosUsuarioAD(loginUsuario);

                    if (usuario.Nome != null)
                    {
                        listaUsuariosAD.Add(usuario);
                    }
                }
                else
                {
                    listaUsuariosAD = ActiveDirectoryBusiness.ListaTodosUsuariosAD();
                }

                return Json(new { listaUsuariosAD }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ConsultarVinculo(string loginUsuario)
        {

            try
            {

                N0203REGBusiness N0203REG = new N0203REGBusiness();
                var retorno = N0203REG.consultarParametroJustificativa(loginUsuario);

                return Json(new { retorno }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult InserirVinculo(string loginUsuario, string operacao)
        {
            bool campos = true;
            try
            {
                if (loginUsuario == "")
                {
                    return Json(new { campos = false }, JsonRequestBehavior.AllowGet);
                }
                N0203REGBusiness N0203REG = new N0203REGBusiness();
                var retorno = N0203REG.inserirVinculo(loginUsuario, operacao);

                return Json(new { retorno, campos }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #region Cadastro Motivo de Devolução

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CadMotivoDevolucao()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {

                var n9999MENBusiness = new N9999MENBusiness();

                // Validação para verificar se o usuário tem acesso quando digitar a url da pagina no navegador.
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "Solicitacoes/CadMotivoDevolucao").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaView();


                return this.View("CadMotivoDevolucao", this.CadastroMotivoDevolucao);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CadMotivoDevolucao(CadMotivoDevolucaoViewModel modelo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {
                this.InicializaView();
                CadastroMotivoDevolucao = modelo;

                if (ModelState.IsValid)
                {

                }

                return View("CadMotivoDevolucao", this.CadastroMotivoDevolucao);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public JsonResult PesquisaMotivoDevolucao()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<ListaN0204MDVPesquisa> ListaN0204MDVPesquisa = new List<ListaN0204MDVPesquisa>();
                ListaN0204MDVPesquisa = ListaMotivos();

                return this.Json(new { ListaN0204MDVPesquisa }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PesquisaMotivoporOrigem(long codigoOrigem)
        {
            if(this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0204MDOBusiness N0204MDOBusiness = new N0204MDOBusiness();
                var lista = N0204MDOBusiness.PesquisaMotivoporOrigem(codigoOrigem);
                return this.Json(new { lista }, JsonRequestBehavior.AllowGet);
             }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public JsonResult PesquisaOrigensPorMotivo(string codigoMotivo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0204MDOBusiness N0204MDOBusiness = new N0204MDOBusiness();
                List<N0204MDO> ListaN0204MDO = new List<N0204MDO>();
                List<ListaN0204MDOPesquisa> ListaOrigensMotivo = new List<ListaN0204MDOPesquisa>();
                bool PesquisaSucesso = false;

                ListaN0204MDO = N0204MDOBusiness.PesquisaOrigensPorMotivo(long.Parse(codigoMotivo));

                if (ListaN0204MDO.Count > 0)
                {
                    ListaOrigensMotivo = new List<ListaN0204MDOPesquisa>();
                    PesquisaSucesso = true;

                    foreach (N0204MDO item in ListaN0204MDO)
                    {
                        ListaOrigensMotivo.Add((ListaN0204MDOPesquisa)item);
                    }
                }

                return this.Json(new { ListaOrigensMotivo = ListaOrigensMotivo, PesquisaSucesso = PesquisaSucesso }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<ListaN0204MDVPesquisa> ListaMotivos()
        {
            N0204MDVBusiness N0204MDVBusiness = new N0204MDVBusiness();
            List<N0204MDV> listaMotivoDevolucao = new List<N0204MDV>();
            List<ListaN0204MDVPesquisa> ListaN0204MDVPesquisa = new List<ListaN0204MDVPesquisa>();
            listaMotivoDevolucao = N0204MDVBusiness.PesquisaMotivoDevolucao();

            if (listaMotivoDevolucao.Count > 0)
            {
                ListaN0204MDVPesquisa = new List<ListaN0204MDVPesquisa>();

                foreach (N0204MDV item in listaMotivoDevolucao)
                {
                    ListaN0204MDVPesquisa.Add((ListaN0204MDVPesquisa)item);
                }
            }

            return ListaN0204MDVPesquisa;
        }

        public JsonResult InserirMotivoDevolucao(string descricao)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0204MDVBusiness N0204MDVBusiness = new N0204MDVBusiness();
                string msgRetorno = "Motivo de Devolução cadastrado com sucesso!";
                bool sucesso = N0204MDVBusiness.InserirMotivoDevolucao(descricao);
                return this.Json(new { msgRetorno, GravadoSucesso = sucesso }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AlterarMotivoDevolucao(string codigo, string descricao)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0204MDVBusiness N0204MDVBusiness = new N0204MDVBusiness();
                string msgRetorno = "Motivo de Devolução alterado com sucesso!";
                bool sucesso = N0204MDVBusiness.AlterarMotivoDevolucao(int.Parse(codigo), descricao);
                return this.Json(new { msgRetorno, AlteradoSucesso = sucesso }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExcluirMotivoDevolucao(string codigo, string descricao)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0204MDVBusiness N0204MDVBusiness = new N0204MDVBusiness();
                string msgRetorno = "Motivo de Devolução excluído com sucesso!";
                bool sucesso = N0204MDVBusiness.ExcluirMotivoDevolucao(int.Parse(codigo));
                return this.Json(new { msgRetorno, ExcluidoSucesso = sucesso }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RelatorioMotivosDevolucao()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                LocalReport report = new LocalReport();
                report.ReportPath = Server.MapPath("~/Reports/MotivosDevolucao.rdlc");

                var lista = new List<Tuple<string, string>>();
                lista.Add(new Tuple<string, string>(this.NomeUsuarioLogado, DateTime.Now.ToString()));

                var reportRelatorio = new ReportDataSource("MotivosDevolucaoDataSet", lista);
                report.Refresh();
                report.DataSources.Add(reportRelatorio);

                string reportType = "PDF";
                string mineType;
                byte[] reportBytes;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;

                string deviceInfo =
                "<DeviceInfo>" +
                " <OutputFormat>PDF</OutputFormat>" +
                " <PageWidth>in</PageWidth>" +
                " <PageHeight>in</PageHeight>" +
                " <MarginTop>in</MarginTop>" +
                " <MarginLeft>in</MarginLeft>" +
                " <MarginRight>in</MarginRight>" +
                " <MarginBottom>in</MarginBottom>" +
                "</DeviceInfo>";

                reportBytes = report.Render(
                    reportType,
                    deviceInfo,
                    out mineType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                var base64EncodedPDF = System.Convert.ToBase64String(reportBytes);
                return this.Json("data:application/pdf;base64, " + base64EncodedPDF);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Cadastro Origem da Ocorrência

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CadOrigemOcorrencia()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {

                var n9999MENBusiness = new N9999MENBusiness();

                // Validação para verificar se o usuário tem acesso quando digitar a url da pagina no navegador.
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "Solicitacoes/CadOrigemOcorrencia").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaView();


                return this.View("CadOrigemOcorrencia", this.CadastroOrigemOcorrencia);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CadOrigemOcorrencia(CadOrigemOcorrenciaViewModel modelo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {
                this.InicializaView();
                CadastroOrigemOcorrencia = modelo;

                if (ModelState.IsValid)
                {

                }

                return View("CadOrigemOcorrencia", this.CadastroOrigemOcorrencia);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public JsonResult PesquisaOrigemOcorrencia()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<ListaN0204ORIPesquisa> ListaN0204ORIPesquisa = new List<ListaN0204ORIPesquisa>();
                ListaN0204ORIPesquisa = ListaOrigens();

                return this.Json(new { ListaN0204ORIPesquisa }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<ListaN0204ORIPesquisa> ListaOrigens()
        {
            N0204ORIBusiness N0204ORIBusiness = new N0204ORIBusiness();
            List<N0204ORI> listaOrigemOcorrencia = new List<N0204ORI>();
            List<ListaN0204ORIPesquisa> ListaN0204ORIPesquisa = new List<ListaN0204ORIPesquisa>();
            listaOrigemOcorrencia = N0204ORIBusiness.PesquisaOrigemOcorrencia();

            if (listaOrigemOcorrencia.Count > 0)
            {
                ListaN0204ORIPesquisa = new List<ListaN0204ORIPesquisa>();

                foreach (N0204ORI item in listaOrigemOcorrencia)
                {
                    ListaN0204ORIPesquisa.Add((ListaN0204ORIPesquisa)item);
                }
            }

            return ListaN0204ORIPesquisa;
        }


        public JsonResult InserirOrigemOcorrencia(string descricao)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0204ORIBusiness N0204ORIBusiness = new N0204ORIBusiness();
                string msgRetorno = "Origem da Ocorrência cadastrada com sucesso!";
                bool sucesso = N0204ORIBusiness.InserirOrigemOcorrencia(descricao);
                return this.Json(new { msgRetorno, GravadoSucesso = sucesso }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AlterarOrigemOcorrencia(string codigo, string descricao)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0204ORIBusiness N0204ORIBusiness = new N0204ORIBusiness();
                string msgRetorno = "Origem da Ocorrência alterada com sucesso!";
                bool sucesso = N0204ORIBusiness.AlterarOrigemOcorrencia(int.Parse(codigo), descricao);
                return this.Json(new { msgRetorno, AlteradoSucesso = sucesso }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExcluirOrigemOcorrencia(string codigo, string descricao)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                N0204ORIBusiness N0204ORIBusiness = new N0204ORIBusiness();
                string msgRetorno = "Origem da Ocorrência " + codigo + " - " + descricao + " excluída com sucesso!";
                bool sucesso = N0204ORIBusiness.ExcluirOrigemOcorrencia(int.Parse(codigo));
                return this.Json(new { msgRetorno, ExcluidoSucesso = sucesso }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Tipo de Atendimento X Origem da Ocorrência

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CadTipAtendXOrigOcor()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {

                var n9999MENBusiness = new N9999MENBusiness();

                // Validação para verificar se o usuário tem acesso quando digitar a url da pagina no navegador.
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "Solicitacoes/CadTipAtendXOrigOcor").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaView();


                return this.View("CadTipAtendXOrigOcor", this.CadastroTipAtendXOrigOcor);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CadTipAtendXOrigOcor(CadTipAtendXOrigOcorViewModel modelo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {
                this.InicializaView();
                this.CadastroTipAtendXOrigOcor = modelo;

                if (ModelState.IsValid)
                {

                }

                return View("CadTipAtendXOrigOcor", this.CadastroTipAtendXOrigOcor);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public JsonResult PesquisaTipoAtendXOrigemOcorrencia(string codigoTipoAtend)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<RelacionamentoTipoAtendOrigemModel> listaTipAtendOrigem = new List<RelacionamentoTipoAtendOrigemModel>();
                N0204AORBusiness N0204AORBusiness = new N0204AORBusiness();
                listaTipAtendOrigem = N0204AORBusiness.PesquisaTipoAtendOrigem(long.Parse(codigoTipoAtend));
                return this.Json(new { listaTipAtendOrigem, PesquisaSucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SalvarTipoAtendXOrigemOcorrencia(string codigoTipoAtend, string listaOrigemOcorrencia)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string msgRetorno = "Lista de origem da ocorrência atrelada ao tipo de atendimento selecionado com sucesso!";
                string situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString();
                N0204AORBusiness N0204AORBusiness = new N0204AORBusiness();
                List<N0204AOR> listaTipAtendOrigem = new List<N0204AOR>();
                N0204AOR itemLista = new N0204AOR();

                long codTipAtd = long.Parse(codigoTipoAtend);

                if (!string.IsNullOrEmpty(listaOrigemOcorrencia))
                {
                    string[] lista = listaOrigemOcorrencia.Split('-');

                    for (int i = 0; i < lista.Length; i++)
                    {
                        itemLista = new N0204AOR();
                        itemLista.CODATD = codTipAtd;
                        itemLista.CODORI = long.Parse(lista[i]);
                        itemLista.SITREL = situacao;
                        listaTipAtendOrigem.Add(itemLista);
                    }
                }
                else
                {
                    msgRetorno = "A associação da lista de origem da ocorrência foi removida do tipo de atendimento selecionado com sucesso!";
                }

                var sucesso = N0204AORBusiness.GravarTipoAtendOrigem(codTipAtd, listaTipAtendOrigem);
                return this.Json(new { msgRetorno, GravadoSucesso = sucesso }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Motivo de Devolução X Origem da Ocorrência

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CadMotDevXOrigOcor()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {

                var n9999MENBusiness = new N9999MENBusiness();

                // Validação para verificar se o usuário tem acesso quando digitar a url da pagina no navegador.
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "Solicitacoes/CadMotDevXOrigOcor").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaView();


                return this.View("CadMotDevXOrigOcor", this.CadastroMotDevXOrigOcor);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CadMotDevXOrigOcor(CadMotDevXOrigOcorViewModel modelo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {
                this.InicializaView();
                CadastroMotDevXOrigOcor = modelo;

                if (ModelState.IsValid)
                {

                }

                return View("CadMotDevXOrigOcor", this.CadastroMotDevXOrigOcor);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public JsonResult PesquisaMotivoDevXOrigemOcorrencia(string codigoMotivo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<RelacionamentoMotivoOrigemModel> listaMotivoOrigem = new List<RelacionamentoMotivoOrigemModel>();
                N0204MDOBusiness N0204MDOBusiness = new N0204MDOBusiness();

                listaMotivoOrigem = N0204MDOBusiness.PesquisaMotivoOrigem(long.Parse(codigoMotivo));
                return this.Json(new { listaMotivoOrigem, PesquisaSucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SalvarMotivoDevXOrigemOcorrencia(string codigoMotivo, string listaOrigemOcorrencia)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string msgRetorno = "Lista de origem da ocorrência atrelada ao motivo de devolução selecionado com sucesso!";
                string situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString();
                N0204MDOBusiness N0204MDOBusiness = new N0204MDOBusiness();
                List<N0204MDO> listaMotivosOrigens = new List<N0204MDO>();
                N0204MDO itemLista = new N0204MDO();

                long codMot = long.Parse(codigoMotivo);

                if (!string.IsNullOrEmpty(listaOrigemOcorrencia))
                {
                    string[] lista = listaOrigemOcorrencia.Split('-');

                    for (int i = 0; i < lista.Length; i++)
                    {
                        itemLista = new N0204MDO();
                        itemLista.CODMDV = codMot;
                        itemLista.CODORI = long.Parse(lista[i]);
                        itemLista.SITREL = situacao;
                        listaMotivosOrigens.Add(itemLista);
                    }
                }
                else
                {
                    msgRetorno = "A associação da lista de origem da ocorrência foi removida do motivo de devolução selecionado com sucesso!";
                }

                var sucesso = N0204MDOBusiness.GravarMotivoDevXOrigemOcorrencia(codMot, listaMotivosOrigens);
                return this.Json(new { msgRetorno, GravadoSucesso = sucesso }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Tipo de Atendimento X Usuário

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CadTipAtendXUsuario()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {

                var n9999MENBusiness = new N9999MENBusiness();

                // Validação para verificar se o usuário tem acesso quando digitar a url da pagina no navegador.
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "Solicitacoes/CadTipAtendXUsuario").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaView();


                return this.View("CadTipAtendXUsuario", this.CadastroTipAtendXUsuario);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CadTipAtendXUsuario(CadTipAtendXUsuarioModel modelo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {
                this.InicializaView();
                this.CadastroTipAtendXUsuario = modelo;

                if (ModelState.IsValid)
                {

                }

                return View("CadTipAtendXUsuario", this.CadastroTipAtendXUsuario);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public JsonResult PesquisarTipoAtendimentoUsuario(string loginUsuario)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                loginUsuario = loginUsuario.ToLower();

                var N9999USUBusiness = new N9999USUBusiness();
                // Busca código do usuário
                var dadosUsuario = N9999USUBusiness.ListaDadosUsuarioPorLogin(loginUsuario);
                if (dadosUsuario == null)
                {
                    // Se usuário não cadastrado no NWORKFLOW, cadastra o mesmo.
                    N9999USUBusiness.CadastrarUsuario(loginUsuario);
                    // Busca código do usuário cadastrado
                    dadosUsuario = N9999USUBusiness.ListaDadosUsuarioPorLogin(loginUsuario);
                }

                var N0204AUSBusiness = new N0204AUSBusiness();
                var listaTipoAtendiUsuarios = N0204AUSBusiness.PesquisarTipoAtendimentoUsuario(dadosUsuario.CODUSU);
                return this.Json(new { listaTipoAtendiUsuarios = listaTipoAtendiUsuarios }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GravarTipoAtendimentoUsuario(string loginUsuario, string listaTipoAtendiUsuarios)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string msgRetorno = "Lista de tipo de atendimento cadastrada para o usuário selecionado com sucesso!";

                var N0204AUSBusiness = new N0204AUSBusiness();
                var listaAtdUsuario = new List<N0204AUS>();

                var N9999USUBusiness = new N9999USUBusiness();
                // Busca código do usuário
                var codUsu = N9999USUBusiness.ListaDadosUsuarioPorLogin(loginUsuario.ToLower()).CODUSU;

                if (!string.IsNullOrEmpty(listaTipoAtendiUsuarios))
                {
                    string[] lista = listaTipoAtendiUsuarios.Split('-');

                    for (int i = 0; i < lista.Length; i++)
                    {
                        var itemLista = new N0204AUS();
                        itemLista.CODUSU = codUsu;
                        itemLista.CODATD = long.Parse(lista[i]);
                        itemLista.SITREL = ((char)Enums.SituacaoRegistro.Ativo).ToString();
                        listaAtdUsuario.Add(itemLista);
                    }
                }
                else
                {
                    msgRetorno = "A associação da lista de tipo de atendimento foi removida do usuário selecionado com sucesso!";
                }

                var sucesso = true;
                if (!N0204AUSBusiness.GravarTipoAtendimentoUsuario(codUsu, listaAtdUsuario))
                {
                    sucesso = false;
                    msgRetorno = "Não foi possível cadastrar a lista de tipo de atendimento ao usuário selecionado pois o mesmo não possui cadastro de usuário no sistema de ocorrência.";
                }

                return this.Json(new { msgRetorno, GravadoSucesso = sucesso }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Prazo de Devolução / Troca X Usuário

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CadPrazoDevTrocaXUsuario()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {

                var n9999MENBusiness = new N9999MENBusiness();

                // Validação para verificar se o usuário tem acesso quando digitar a url da pagina no navegador.
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "Solicitacoes/CadPrazoDevTrocaXUsuario").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaView();

                var padrao = ListaPrazoDevolucaoTroca().Where(c => string.IsNullOrEmpty(c.loginUsuario)).FirstOrDefault();
                if (padrao != null)
                {
                    this.CadastroPrazoDevTrocaXUsuario.PrazoDeTroca = padrao.prazoTroca;
                    this.CadastroPrazoDevTrocaXUsuario.PrazoDevolucao = padrao.prazoDev;
                }

                return this.View("CadPrazoDevTrocaXUsuario", this.CadastroPrazoDevTrocaXUsuario);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CadPrazoDevTrocaXUsuario(CadPrazoDevTrocaModel modelo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {
                this.InicializaView();
                this.CadastroPrazoDevTrocaXUsuario = modelo;

                if (ModelState.IsValid)
                {

                }

                return View("CadPrazoDevTrocaXUsuario", this.CadastroTipAtendXUsuario);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public JsonResult PesquisaPrazoDevolucaoTroca()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var ListaN0204PPUPesquisa = ListaPrazoDevolucaoTroca().Where(c => !string.IsNullOrEmpty(c.loginUsuario)).ToList();

                return this.Json(new { ListaN0204PPUPesquisa }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<ListaN0204PPUPesquisa> ListaPrazoDevolucaoTroca()
        {
            var N0204PPUBusiness = new N0204PPUBusiness();
            var listPrazosDevTroca = new List<N0204PPU>();
            var ListaN0204PPUPesquisa = new List<ListaN0204PPUPesquisa>();

            listPrazosDevTroca = N0204PPUBusiness.PesquisaPrazoDevolucaoTroca();

            if (listPrazosDevTroca.Count > 0)
            {
                ListaN0204PPUPesquisa = new List<ListaN0204PPUPesquisa>();

                foreach (var item in listPrazosDevTroca)
                {
                    ListaN0204PPUPesquisa.Add((ListaN0204PPUPesquisa)item);
                }
            }

            return ListaN0204PPUPesquisa;
        }

        public JsonResult SalvarPrazoDevolucaoTroca(string prazoDev, string prazoTroca, string listaUsuarios)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                string msgRetorno = "Prazo padrão salvo com sucesso!";

                var N0204PPUBusiness = new N0204PPUBusiness();
                var listPrazosDevTroca = new List<N0204PPU>();
                var N9999USUBusiness = new N9999USUBusiness();

                if (!string.IsNullOrEmpty(listaUsuarios))
                {
                    string[] lista = listaUsuarios.Split('-');

                    for (int i = 0; i < lista.Length; i++)
                    {
                        var itemLista = new N0204PPU();
                        itemLista.QTDDEV = long.Parse(prazoDev);
                        itemLista.QTDTRC = long.Parse(prazoTroca);

                        var loginUsuario = lista[i];
                        // Busca código do usuário
                        var dadosUsuario = N9999USUBusiness.ListaDadosUsuarioPorLogin(loginUsuario);
                        if (dadosUsuario == null)
                        {
                            // Se usuário não cadastrado no NWORKFLOW, cadastra o mesmo.
                            N9999USUBusiness.CadastrarUsuario(loginUsuario);
                            // Busca código do usuário cadastrado
                            dadosUsuario = N9999USUBusiness.ListaDadosUsuarioPorLogin(loginUsuario);
                        }

                        itemLista.CODUSU = dadosUsuario.CODUSU;
                        listPrazosDevTroca.Add(itemLista);
                    }

                    msgRetorno = "Prazo de devolução e troca cadastrado ao(s) usuário(s) selecionado(s) com sucesso!";
                }
                else
                {
                    var itemLista = new N0204PPU();
                    itemLista.QTDDEV = long.Parse(prazoDev);
                    itemLista.QTDTRC = long.Parse(prazoTroca);
                    listPrazosDevTroca.Add(itemLista);
                }

                var sucesso = N0204PPUBusiness.InserirPrazoDevolucaoTroca(listPrazosDevTroca);
                return this.Json(new { msgRetorno, GravadoSucesso = sucesso }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExcluirPrazoDevolucaoTroca(string loginUsuario)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var N0204PPUBusiness = new N0204PPUBusiness();
                string msgRetorno = "Prazo de devolução e troca removido do usuário selecionado com sucesso.";

                loginUsuario = loginUsuario.ToLower();
                var N9999USUBusiness = new N9999USUBusiness();
                // Busca código do usuário
                var dadosUsuario = N9999USUBusiness.ListaDadosUsuarioPorLogin(loginUsuario);

                bool sucesso = N0204PPUBusiness.ExcluirPrazoDevolucaoTroca((int)dadosUsuario.CODUSU);
                return this.Json(new { msgRetorno, ExcluidoSucesso = sucesso }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Situação Protocolos

        public JsonResult RelatorioSituacaoProtocolos()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                LocalReport report = new LocalReport();
                report.ReportPath = Server.MapPath("~/Reports/SituacaoProtocolos.rdlc");

                var lista = new List<Tuple<string, string>>();
                lista.Add(new Tuple<string, string>(this.NomeUsuarioLogado, DateTime.Now.ToString()));

                var reportRelatorio = new ReportDataSource("SituacaoProtocolosDataSet", lista);
                report.Refresh();
                report.DataSources.Add(reportRelatorio);

                string reportType = "PDF";
                string mineType;
                byte[] reportBytes;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;

                string deviceInfo =
                "<DeviceInfo>" +
                " <OutputFormat>PDF</OutputFormat>" +
                " <PageWidth>in</PageWidth>" +
                " <PageHeight>in</PageHeight>" +
                " <MarginTop>in</MarginTop>" +
                " <MarginLeft>in</MarginLeft>" +
                " <MarginRight>in</MarginRight>" +
                " <MarginBottom>in</MarginBottom>" +
                "</DeviceInfo>";

                reportBytes = report.Render(
                    reportType,
                    deviceInfo,
                    out mineType,
                    out encoding,
                    out fileNameExtension,
                    out streams,
                    out warnings);

                var base64EncodedPDF = System.Convert.ToBase64String(reportBytes);
                return this.Json("data:application/pdf;base64, " + base64EncodedPDF);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Inicializa Views

        private void InicializaViewPesquisa()
        {
            if (this.PesqLancRegistroOcorrencia == null)
            {
                this.PesqLancRegistroOcorrencia = new PesqLancRegistroOcorrenciaViewModel();

                if (this.PesqLancRegistroOcorrencia.ListaTipoAtendimento == null)
                {
                    this.PesqLancRegistroOcorrencia.ListaTipoAtendimento = ListaTipoAtendimento();
                }

                if (this.PesqLancRegistroOcorrencia.ListaMotivoDevolucao == null)
                {
                    this.PesqLancRegistroOcorrencia.ListaMotivoDevolucao = ListaMotivoDevolucao();
                }

                if (this.PesqLancRegistroOcorrencia.ListaOrigemOcorrencia == null)
                {
                    this.PesqLancRegistroOcorrencia.ListaOrigemOcorrencia = ListaOrigemOcorrencia();
                }
            }
        }

        private void InicializaViewCadastro()
        {
            if (this.CadLancRegistroOcorrencia == null)
            {
                this.CadLancRegistroOcorrencia = new CadLancRegistroOcorrenciaViewModel();

                if (this.CadLancRegistroOcorrencia.ListaTipoAtendimento == null)
                {
                    this.CadLancRegistroOcorrencia.ListaTipoAtendimento = ListaTipoAtendimento();
                }

                if (this.CadLancRegistroOcorrencia.ListaMotivoDevolucao == null)
                {
                    this.CadLancRegistroOcorrencia.ListaMotivoDevolucao = ListaMotivoDevolucao();
                }

                if (this.CadLancRegistroOcorrencia.ListaOrigemOcorrencia == null)
                {
                    this.CadLancRegistroOcorrencia.ListaOrigemOcorrencia = ListaOrigemOcorrencia();
                }
            }
        }

        /// <summary>
        /// Inicializa as views de Solicitações 
        /// </summary>
        private void InicializaView()
        {
            if (this.CadastroTipoAtendimento == null)
            {
                this.CadastroTipoAtendimento = new CadTipoAtendimentoViewModel();
            }

            if (this.CadastroOrigemOcorrencia == null)
            {
                this.CadastroOrigemOcorrencia = new CadOrigemOcorrenciaViewModel();
            }

            if (this.CadastroMotivoDevolucao == null)
            {
                this.CadastroMotivoDevolucao = new CadMotivoDevolucaoViewModel();
            }

            if (this.CadastroMotDevXOrigOcor == null)
            {
                this.CadastroMotDevXOrigOcor = new CadMotDevXOrigOcorViewModel();

                if (this.CadastroMotDevXOrigOcor.ListaMotivoDevolucao == null)
                {
                    this.CadastroMotDevXOrigOcor.ListaMotivoDevolucao = ListaMotivoDevolucao();
                }
            }

            if (this.CadastroTipAtendXOrigOcor == null)
            {
                this.CadastroTipAtendXOrigOcor = new CadTipAtendXOrigOcorViewModel();

                if (this.CadastroTipAtendXOrigOcor.ListaTipoAtendimento == null)
                {
                    this.CadastroTipAtendXOrigOcor.ListaTipoAtendimento = ListaTipoAtendimento();
                }
            }

            if (this.CadastroTipAtendXUsuario == null)
            {
                this.CadastroTipAtendXUsuario = new CadTipAtendXUsuarioModel();

                if (this.CadastroTipAtendXUsuario.ListaTipoAtendimento == null)
                {
                    this.CadastroTipAtendXUsuario.ListaTipoAtendimento = ListaTipoAtendimento();
                }
            }

            if (this.ConsultaRelAnalitico == null)
            {
                this.ConsultaRelAnalitico = new RelatorioAnaliticoViewModel();

                if (this.ConsultaRelAnalitico.ListaSituacaoRegistroOcorrencia == null)
                {
                    PopulaSituacaoRegOcorr();
                }
            }

            if (this.ConsultaRelSintetico == null)
            {
                this.ConsultaRelSintetico = new RelatorioSinteticoViewModel();

                if (this.ConsultaRelSintetico.ListaTipoPesquisaReg == null)
                {
                    PopulaTipoPesquisaRelSintetico();
                }
                if (this.ConsultaRelSintetico.ListaSituacaoRegistroOcorrencia == null)
                {
                    PopulaSituacaoRegOcorrSintetico();
                }
                if (this.ConsultaRelSintetico.listaMotivoDevolucao == null)
                {
                    this.ConsultaRelSintetico.listaMotivoDevolucao = ListaMotivoDevolucao();
                }
                if (this.ConsultaRelSintetico.listaOrigemOcorrencia == null)
                {
                    this.ConsultaRelSintetico.listaOrigemOcorrencia = ListaOrigemOcorrencia();
                }
                if (this.ConsultaRelSintetico.listaTipoAtendimento == null)
                {
                    this.ConsultaRelSintetico.listaTipoAtendimento = ListaTipoAtendimento();
                }
            }

            if (this.CadastroPrazoDevTrocaXUsuario == null)
            {
                this.CadastroPrazoDevTrocaXUsuario = new CadPrazoDevTrocaModel();
            }
        }

        public List<ListaN0204ATDPesquisa> ListaTipoAtendimento()
        {
            List<ListaN0204ATDPesquisa> ListaTipoAtendimento = new List<ListaN0204ATDPesquisa>();
            ListaTipoAtendimento = ListaTiposAtendimento();
            ListaN0204ATDPesquisa itemListaTp = new ListaN0204ATDPesquisa();
            itemListaTp.Codigo = 0;
            itemListaTp.Descricao = "Selecione...";
            itemListaTp.Situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString(); ;
            ListaTipoAtendimento.Add(itemListaTp);
            return ListaTipoAtendimento.OrderBy(c => c.Codigo).ToList();
        }

        public List<ListaN0204ATDPesquisa> ListaTipoAtendimentoPorUsuario()
        {
            List<ListaN0204ATDPesquisa> ListaTipoAtendimento = new List<ListaN0204ATDPesquisa>();
            ListaTipoAtendimento = ListaTiposAtendimentoPorUsuario();
            ListaN0204ATDPesquisa itemListaTp = new ListaN0204ATDPesquisa();
            itemListaTp.Codigo = 0;
            itemListaTp.Descricao = "Selecione...";
            itemListaTp.Situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString(); ;
            ListaTipoAtendimento.Add(itemListaTp);
            return ListaTipoAtendimento.OrderBy(c => c.Codigo).ToList();
        }

        public List<ListaN0204MDVPesquisa> ListaMotivoDevolucao()
        {
            List<ListaN0204MDVPesquisa> ListaMotivoDevolucao = new List<ListaN0204MDVPesquisa>();
            ListaMotivoDevolucao = ListaMotivos();
            ListaN0204MDVPesquisa itemListaMd = new ListaN0204MDVPesquisa();
            itemListaMd.Codigo = 0;
            itemListaMd.Descricao = "Selecione...";
            itemListaMd.Situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString(); ;
            ListaMotivoDevolucao.Add(itemListaMd);
            return ListaMotivoDevolucao.OrderBy(c => c.Codigo).ToList();
        }

        public List<ListaN0204ORIPesquisa> ListaOrigemOcorrencia()
        {
            List<ListaN0204ORIPesquisa> ListaOrigemOcorrencia = new List<ListaN0204ORIPesquisa>();
            ListaOrigemOcorrencia = ListaOrigens();
            ListaN0204ORIPesquisa itemListaOr = new ListaN0204ORIPesquisa();
            itemListaOr.Codigo = 0;
            itemListaOr.Descricao = "Selecione...";
            itemListaOr.Situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString(); ;
            ListaOrigemOcorrencia.Add(itemListaOr);
            return ListaOrigemOcorrencia.OrderBy(c => c.Codigo).ToList();
        }

        #endregion

        #region Popula Enums

        private void PopulaSituacaoRegOcorr()
        {
            this.ConsultaRelAnalitico.ListaSituacaoRegistroOcorrencia = new List<SitRegOcorModel>();
            SitRegOcorModel item = new SitRegOcorModel();
            item.Id = 0;
            item.Descricao = "Selecione...";
            this.ConsultaRelAnalitico.ListaSituacaoRegistroOcorrencia.Add((SitRegOcorModel)item);
            this.ConsultaRelAnalitico.ListaSituacaoRegistroOcorrencia.Add((SitRegOcorModel)Enums.SituacaoRegistroOcorrencia.Pendente);
            this.ConsultaRelAnalitico.ListaSituacaoRegistroOcorrencia.Add((SitRegOcorModel)Enums.SituacaoRegistroOcorrencia.Fechado);
            this.ConsultaRelAnalitico.ListaSituacaoRegistroOcorrencia.Add((SitRegOcorModel)Enums.SituacaoRegistroOcorrencia.PreAprovado);
            this.ConsultaRelAnalitico.ListaSituacaoRegistroOcorrencia.Add((SitRegOcorModel)Enums.SituacaoRegistroOcorrencia.Aprovado);
            this.ConsultaRelAnalitico.ListaSituacaoRegistroOcorrencia.Add((SitRegOcorModel)Enums.SituacaoRegistroOcorrencia.Reprovado);
            this.ConsultaRelAnalitico.ListaSituacaoRegistroOcorrencia.Add((SitRegOcorModel)Enums.SituacaoRegistroOcorrencia.Cancelado);
        }

        private void PopulaSituacaoRegOcorrSintetico()
        {
            this.ConsultaRelSintetico.ListaSituacaoRegistroOcorrencia = new List<SitRegOcorModel>();
            SitRegOcorModel item = new SitRegOcorModel();
            item.Id = 0;
            item.Descricao = "Selecione...";
            this.ConsultaRelSintetico.ListaSituacaoRegistroOcorrencia.Add((SitRegOcorModel)item);
            this.ConsultaRelSintetico.ListaSituacaoRegistroOcorrencia.Add((SitRegOcorModel)Enums.SituacaoRegistroOcorrencia.Pendente);
            this.ConsultaRelSintetico.ListaSituacaoRegistroOcorrencia.Add((SitRegOcorModel)Enums.SituacaoRegistroOcorrencia.Fechado);
            this.ConsultaRelSintetico.ListaSituacaoRegistroOcorrencia.Add((SitRegOcorModel)Enums.SituacaoRegistroOcorrencia.PreAprovado);
            this.ConsultaRelSintetico.ListaSituacaoRegistroOcorrencia.Add((SitRegOcorModel)Enums.SituacaoRegistroOcorrencia.Aprovado);
            this.ConsultaRelSintetico.ListaSituacaoRegistroOcorrencia.Add((SitRegOcorModel)Enums.SituacaoRegistroOcorrencia.Reprovado);
            this.ConsultaRelSintetico.ListaSituacaoRegistroOcorrencia.Add((SitRegOcorModel)Enums.SituacaoRegistroOcorrencia.Cancelado);
        }

        /// <summary>
        /// Atribui as opções de Tipos de Pesquisa de Registros 
        /// </summary>
        private void PopulaTipoPesquisaRelSintetico()
        {
            this.ConsultaRelSintetico.ListaTipoPesquisaReg = new List<TipoPesquisaRegModel>();
            this.ConsultaRelSintetico.ListaTipoPesquisaReg.Add((TipoPesquisaRegModel)Enums.TipoPesquisaRegistroOcorrencia.AnaliseEmbarque);
        }
    }

        #endregion
}

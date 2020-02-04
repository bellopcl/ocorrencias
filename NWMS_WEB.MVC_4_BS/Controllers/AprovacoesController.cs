using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Text;
using NWORKFLOW_WEB.MVC_4_BS.Models;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.Business;
namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class AprovacoesController : BaseController
    {
        public AprovarRegistroOcorrenciaViewModel AprovarRegistroView { get; set; }
        public UsuAprXOrigOcorViewModel CadUsuarioAprovadorView { get; set; }
        public CadUsuarioOperFatViewModel CadUsuarioOperFatView { get; set; }
        #region Rotina de Envio de Email
        protected void MontarEmailProtocoloReprovado(string numeroProtocolo, string observacao, Enums.TipoAtendimento tipoAtendimento, string emailDestino)
        {
            string EmailDestino = emailDestino;
            string CopiarEmails = string.Empty;
//#if DEBUG
//            EmailDestino = "sistema02@nutriplan.com.br";
//#endif
            string Assunto = "REGISTRO DE DEVOLUÇÃO REPROVADO";
            string tipoAtd = "Devolução";
            if (tipoAtendimento == Enums.TipoAtendimento.TrocaMercadorias)
            {
                Assunto = "REGISTRO DE TROCA REPROVADO";
                tipoAtd = "Troca";
            }
            var emailCabecalho = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Cabecalho).GetValue<string>();
            var emailCorpo = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Corpo).GetValue<string>();
            var emailRodape = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Rodape).GetValue<string>();
            StringBuilder Mensagem = new StringBuilder();
            Mensagem.AppendLine(emailCabecalho);
            Mensagem.AppendLine(emailCorpo);
            Mensagem.AppendLine(@"<div class='panel panel-success'>
                                    <div class='panel-heading'>
                                        <h3 class='panel-title'>Protocolo de " + tipoAtd + @" Nº " + numeroProtocolo + @"</h3>
                                    </div>
                                    <div class='panel-body'>
                                        <strong>Olá,<br/><br/>
                                        O Protocolo de " + tipoAtd + @" Nº " + numeroProtocolo + @" foi reprovado mediante a seguinte observação.<br/><br/>" + observacao + @"<br/><br/>
                                        </strong><br/><br/>");
            Mensagem.AppendLine(emailRodape);
            var Email = new Email();
            Email.EnviarEmail(EmailDestino, CopiarEmails, Assunto, Mensagem.ToString());
        }
        protected void MontarEmailProtocoloAprovado(string numeroProtocolo, string observacao, Enums.TipoAtendimento tipoAtendimento, string emailDestino)
        {
            string EmailDestino = emailDestino;
            string CopiarEmails = string.Empty;
            
//#if DEBUG
//            EmailDestino = "sistema02@nutriplan.com.br";
//#endif
            string Assunto = "REGISTRO DE DEVOLUÇÃO APROVADO";
            string tipoAtd = "Devolução";
            if (tipoAtendimento == Enums.TipoAtendimento.TrocaMercadorias)
            {
                Assunto = "REGISTRO DE TROCA APROVADO";
                tipoAtd = "Troca";
            }
            var emailCabecalho = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Cabecalho).GetValue<string>();
            var emailCorpo = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Corpo).GetValue<string>();
            var emailRodape = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Rodape).GetValue<string>();
            StringBuilder Mensagem = new StringBuilder();
            Mensagem.AppendLine(emailCabecalho);
            Mensagem.AppendLine(emailCorpo);
            Mensagem.AppendLine(@"<div class='panel panel-success'>
                                    <div class='panel-heading'>
                                        <h3 class='panel-title'>Protocolo de " + tipoAtd + @" Nº " + numeroProtocolo + @"</h3>
                                    </div>
                                    <div class='panel-body'>
                                        <strong>Olá,<br/><br/>
                                        O Protocolo de " + tipoAtd + @" Nº " + numeroProtocolo + @" foi aprovado.<br/><br/>" + observacao + @"<br/><br/>
                                        </strong><br/><br/>");
            Mensagem.AppendLine(emailRodape);
            var Email = new Email();
            Email.EnviarEmail(EmailDestino, CopiarEmails, Assunto, Mensagem.ToString());
        }
        protected void MontarEmailErroIntegracaoSapiens(string numeroProtocolo, string observacao, Enums.TipoAtendimento tipoAtendimento)
        {
            string EmailDestino = "sistema02@nutriplan.com.br";
            string CopiarEmails = "sistema02@nutriplan.com.br";
            string Assunto = "REGISTRO DE DEVOLUÇÃO - ERRO DE INTEGRAÇÃO SAPIENS";
            string tipoAtd = "Devolução";
            if (tipoAtendimento == Enums.TipoAtendimento.TrocaMercadorias)
            {
                Assunto = "REGISTRO DE TROCA - ERRO DE INTEGRAÇÃO SAPIENS";
                tipoAtd = "Troca";
            }
            var emailCabecalho = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Cabecalho).GetValue<string>();
            var emailCorpo = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Corpo).GetValue<string>();
            var emailRodape = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Rodape).GetValue<string>();
            StringBuilder Mensagem = new StringBuilder();
            Mensagem.AppendLine(emailCabecalho);
            Mensagem.AppendLine(emailCorpo);
            Mensagem.AppendLine(@"<div class='panel panel-success'>
                                    <div class='panel-heading'>
                                        <h3 class='panel-title'>Protocolo de " + tipoAtd + @" Nº " + numeroProtocolo + @"</h3>
                                    </div>
                                    <div class='panel-body'>
                                        <strong>Olá,<br/><br/>
                                        Ocorreu um erro de integração do Protocolo de " + tipoAtd + @" Nº " + numeroProtocolo + @" com o sistema SAPIENS.<br/><br/>Retorno SAPIENS: " + observacao + @"<br/>");
            Mensagem.AppendLine(emailRodape);
            var Email = new Email();
            Email.EnviarEmail(EmailDestino, CopiarEmails, Assunto, Mensagem.ToString());
        }
        #endregion
        #region Cadastro de Usuário Aprovador X Motivo de Devolução
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CadUsuarioAprovador()
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
                if (listaAcesso.Where(p => p.ENDPAG == "Aprovacoes/CadUsuarioAprovador").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }
                this.InicializaView();
                return this.View("CadUsuarioAprovador", this.CadUsuarioAprovadorView);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }
        public JsonResult PesquisaUsuarioAprovadorXOrigemOcorrencia(string loginUsuario, long? tipoAtendimento)
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
                var tipoAtend = Enums.TipoAtendimento.DevolucaoMercadorias;
                if (tipoAtendimento == (long)Enums.TipoAtendimento.TrocaMercadorias)
                    tipoAtend = Enums.TipoAtendimento.TrocaMercadorias;
                N0203UAPBusiness N0203UAPBusiness = new N0203UAPBusiness();
                var listaOrigensUsuario = N0203UAPBusiness.PesquisaUsuarioAprovadorXOrigemOcorrencia(dadosUsuario.CODUSU, tipoAtend);
                return this.Json(new { listaOrigensUsuario = listaOrigensUsuario }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GravarUsuarioAprovadorXOrigemOcorrencia(string tipAtendimento, string loginUsuario, string listaOrigemOcorrencia)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                string msgRetorno = "Lista de origem da ocorrência cadastrada ao usuário aprovador selecionado com sucesso!";
                N0203UAPBusiness N0203UAPBusiness = new N0203UAPBusiness();
                var listaOrigensUsuario = new List<N0203UAP>();
                N0203UAP itemLista = new N0203UAP();
                var N9999USUBusiness = new N9999USUBusiness();
                // Busca código do usuário
                var codUsu = N9999USUBusiness.ListaDadosUsuarioPorLogin(loginUsuario.ToLower()).CODUSU;
                var tipoAtend = Enums.TipoAtendimento.DevolucaoMercadorias;
                if (int.Parse(tipAtendimento) == (int)Enums.TipoAtendimento.TrocaMercadorias)
                    tipoAtend = Enums.TipoAtendimento.TrocaMercadorias;
                if (!string.IsNullOrEmpty(listaOrigemOcorrencia))
                {
                    string[] lista = listaOrigemOcorrencia.Split('-');
                    for (int i = 0; i < lista.Length; i++)
                    {
                        itemLista = new N0203UAP();
                        itemLista.CODATD = (long)tipoAtend;
                        itemLista.CODUSU = codUsu;
                        itemLista.CODORI = long.Parse(lista[i]);
                        listaOrigensUsuario.Add(itemLista);
                    }
                }
                else
                {
                    msgRetorno = "A associação da lista de origem da ocorrência foi removida do usuário aprovador selecionado com sucesso!";
                }
                var sucesso = true;
                if (!N0203UAPBusiness.GravarUsuarioAprovadorXOrigemOcorrencia(codUsu, listaOrigensUsuario, tipoAtend))
                {
                    sucesso = false;
                    msgRetorno = "Não foi possível cadastrar a lista de origem da ocorrência ao usuário aprovador selecionado pois o mesmo não possui cadastro de usuário no sistema de protocolos.";
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
        #region Cadastro de Usuário Aprovador X Operação Aprovação Fat.
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult CadUsuarioOperFat()
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
                if (listaAcesso.Where(p => p.ENDPAG == "Aprovacoes/CadUsuarioOperFat").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }
                this.InicializaView();
                return this.View("CadUsuarioOperFat", this.CadUsuarioOperFatView);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }
        public JsonResult PesquisaUsuarioAprovadorXOperacaoFat(string loginUsuario, long? tipoAtendimento)
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
                var N0203UOFBusiness = new N0203UOFBusiness();
                var listaOperacoes = N0203UOFBusiness.PesquisaUsuarioAprovadorXOperacaoFat(dadosUsuario.CODUSU, tipoAtendimento);
                return this.Json(new { listaOperacoes = listaOperacoes }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GravarUsuarioAprovadorXOperacaoFat(string tipAtendimento, string loginUsuario, string listaOperacoes)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                string msgRetorno = "Lista de operações cadastrada ao usuário aprovador selecionado com sucesso!";
                var N0203UOFBusiness = new N0203UOFBusiness();
                var listaOperacoesUsuario = new List<N0203UOF>();
                var itemLista = new N0203UOF();
                var N9999USUBusiness = new N9999USUBusiness();
                // Busca código do usuário
                var codUsu = N9999USUBusiness.ListaDadosUsuarioPorLogin(loginUsuario.ToLower()).CODUSU;
                var tipAtd = long.Parse(tipAtendimento);
                long tipoAtendimento = long.Parse(tipAtendimento);
                if (!string.IsNullOrEmpty(listaOperacoes))
                {
                    string[] lista = listaOperacoes.Split('-');
                    for (int i = 0; i < lista.Length; i++)
                    {
                        itemLista = new N0203UOF();
                        itemLista.CODATD = tipAtd;
                        itemLista.CODUSU = codUsu;
                        itemLista.CODOPE = long.Parse(lista[i]);
                        listaOperacoesUsuario.Add(itemLista);
                    }
                }
                else
                {
                    msgRetorno = "A associação da lista de operações foi removida do usuário aprovador selecionado com sucesso!";
                }
                var sucesso = true;
                if (!N0203UOFBusiness.GravarUsuarioAprovadorXOperacaoFat(codUsu, listaOperacoesUsuario, tipoAtendimento))
                {
                    sucesso = false;
                    msgRetorno = "Não foi possível cadastrar a lista de operações ao usuário aprovador selecionado pois o mesmo não possui cadastro de usuário no sistema de protocolos.";
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
        #region Pesquisa Usuário Sapiens
        public JsonResult PesquisarUsuariosSapiens(string codigoUsuario)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                E099USUBusiness E099USUBusiness = new E099USUBusiness();
                long? auxLong = null;
                var codUsu = string.IsNullOrEmpty(codigoUsuario) ? auxLong : long.Parse(codigoUsuario);
                var listaUsuarios = E099USUBusiness.PesquisarUsuariosSapiens(codUsu);
                return this.Json(new { listaUsuarios = listaUsuarios }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Análise e Aprovação de Registro de Ocorrência
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AnaliseAprovacao()
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
                if (listaAcesso.Where(p => p.ENDPAG == "Aprovacoes/AnaliseAprovacao").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }
                this.InicializaView();
                return this.View("AnaliseAprovacao", this.AprovarRegistroView);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }
        public JsonResult PesquisaProtocolosPendentesAprovacao()
        {
            
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                var listaAprovacao = N0203REGBusiness.PesquisaProtocolosPendentesAprovacao(long.Parse(this.CodigoUsuarioLogado));
                return this.Json(new { listaAprovacao = listaAprovacao }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        protected void MontarEmailFinanceiroNotasBoleto(string numeroProtocolo, string notas, string cliente, Enums.TipoAtendimento tipoAtendimento)
        {
            string EmailDestino = "cobranca@nutriplan.com.br";
            string CopiarEmails = "contas.receber@nutriplan.com.br&";
//#if DEBUG
//            EmailDestino = "sistema02@nutriplan.com.br";
//            CopiarEmails = string.Empty;
//#endif
            string Assunto = "REGISTRO DE DEVOLUÇÃO - BOLETO";
            string tipoAtd = "Devolução";

            if (tipoAtendimento == Enums.TipoAtendimento.TrocaMercadorias)
            {
                Assunto = "REGISTRO DE TROCA - BOLETO";
                tipoAtd = "Troca";
            }

            string[] formatNotas = notas.Split('&');
            string notasFormatadas = string.Empty;
            for (int i = 0; i < formatNotas.Length - 1; i++)
            {
                notasFormatadas = notasFormatadas + formatNotas[i] + ".<br/>";
            }

            N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();

            var obsreg = N0203REGBusiness.pesquisarObservacaoSAC(Convert.ToInt64(numeroProtocolo));

            var emailCabecalho = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Cabecalho).GetValue<string>();
            var emailCorpo = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Corpo).GetValue<string>();
            var emailRodape = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.Email.Rodape).GetValue<string>();

            StringBuilder Mensagem = new StringBuilder();
            Mensagem.AppendLine(emailCabecalho);
            Mensagem.AppendLine(emailCorpo);
            Mensagem.AppendLine(@"<div class='panel panel-success'>
                                    <div class='panel-heading'>
                                        <h3 class='panel-title'>Registro de " + tipoAtd + @" Nº " + numeroProtocolo + @"</h3>
                                    </div>
                                    <div class='panel-body'>
                                        <strong>Olá,<br/><br/>
                                        O Registro de " + tipoAtd + @" Nº " + numeroProtocolo + @" contém notas fiscais de boleto.<br/><br/>" + cliente + @".<br/><br/>" + notasFormatadas + @" Observações SAC: " + obsreg + "</strong><br/> ");
            Mensagem.AppendLine(emailRodape);

            var Email = new Email();
            Email.EnviarEmail(EmailDestino, CopiarEmails, Assunto, Mensagem.ToString());
        }

        public JsonResult AprovarRegistrosOcorrenciaNivel1(string codigoRegistro, string observacaoAprovacao)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                N9999USU N9999USU = new N9999USU();
                N9999USUBusiness N9999USUBusiness = new N9999USUBusiness();
                UsuarioADModel usuarioAD = new UsuarioADModel();
                var ActiveDirectoryBusiness = new ActiveDirectoryBusiness();
                var dadosProtocolo = N0203REGBusiness.PesquisaRegistroOcorrencia(long.Parse(codigoRegistro), (int)Enums.SituacaoRegistroOcorrencia.Fechado);
                if (dadosProtocolo != null)
                {
                    var tipoAtend = Enums.TipoAtendimento.DevolucaoMercadorias;
                    if (dadosProtocolo.TIPATE == (int)Enums.TipoAtendimento.TrocaMercadorias)
                        tipoAtend = Enums.TipoAtendimento.TrocaMercadorias;
                    N9999USU = N9999USUBusiness.ListaDadosUsuarioPorCodigo(Convert.ToInt64(dadosProtocolo.USUGER));
                    usuarioAD = ActiveDirectoryBusiness.ListaDadosUsuarioAD(N9999USU.LOGIN);
                    bool AprovadoSucesso = N0203REGBusiness.AprovarRegistrosOcorrenciaNivel1(long.Parse(codigoRegistro), long.Parse(this.CodigoUsuarioLogado), observacaoAprovacao);
                    //#if DEBUG
                    //System.IO.File.WriteAllText(@"C:\Projetos_Desenvolvimento\Arquivo.txt", usuarioAD.Email);
                    //#endif
                 
                    this.MontarEmailProtocoloAprovado(codigoRegistro, "", tipoAtend, usuarioAD.Email);

                    if (AprovadoSucesso)
                    {
                        E140NFVBusiness E140NFVBusiness = new E140NFVBusiness();

                        var notas = (from a in dadosProtocolo.N0203IPV
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

                            var codCli = Convert.ToInt64(dadosProtocolo.CODCLI);
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

                        if (dadosProtocolo.TIPATE == (int)Enums.TipoAtendimento.TrocaMercadorias)
                            tipoAtend = Enums.TipoAtendimento.TrocaMercadorias;

                        if (!string.IsNullOrEmpty(notasEmail))
                        {
                            this.MontarEmailFinanceiroNotasBoleto(dadosProtocolo.NUMREG.ToString(), notasEmail, cliente, tipoAtend);
                        }
                    }
                    return this.Json(new { AprovadoSucesso = AprovadoSucesso }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var msgRetornoSapiens = string.Empty;
                    msgRetornoSapiens = "Registro de devolução Nº " + codigoRegistro + " não encontrado.";
                    return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ReprovarRegistrosOcorrenciaNivel1(string codigoRegistro, string observacaoReprovacao)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                N9999USU N9999USU = new N9999USU();
                N9999USUBusiness N9999USUBusiness = new N9999USUBusiness();
                UsuarioADModel usuarioAD = new UsuarioADModel();
                var ActiveDirectoryBusiness = new ActiveDirectoryBusiness();
                var dadosProtocolo = N0203REGBusiness.PesquisaRegistroOcorrencia(long.Parse(codigoRegistro), (int)Enums.SituacaoRegistroOcorrencia.Fechado);
                if (dadosProtocolo != null)
                {
                    var tipoAtend = Enums.TipoAtendimento.DevolucaoMercadorias;
                    if (dadosProtocolo.TIPATE == (int)Enums.TipoAtendimento.TrocaMercadorias)
                        tipoAtend = Enums.TipoAtendimento.TrocaMercadorias;
                    N9999USU = N9999USUBusiness.ListaDadosUsuarioPorCodigo(Convert.ToInt64(dadosProtocolo.USUGER));
                    usuarioAD = ActiveDirectoryBusiness.ListaDadosUsuarioAD(N9999USU.LOGIN);
                    bool ReprovadoSucesso = N0203REGBusiness.ReprovarRegistrosOcorrenciaNivel1(long.Parse(codigoRegistro), long.Parse(this.CodigoUsuarioLogado), observacaoReprovacao);
                    this.MontarEmailProtocoloReprovado(codigoRegistro, observacaoReprovacao, tipoAtend, usuarioAD.Email);
                    return this.Json(new { ReprovadoSucesso = ReprovadoSucesso }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var msgRetornoSapiens = string.Empty;
                    msgRetornoSapiens = "Registro de devolução Nº " + codigoRegistro + " não encontrado.";
                    return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Aprovar Registro de Ocorrência - Faturamento
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AprovarRegistro()
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
                if (listaAcesso.Where(p => p.ENDPAG == "Aprovacoes/AprovarRegistro").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }
                this.InicializaView();
                return this.View("AprovarRegistro", this.AprovarRegistroView);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }
        public JsonResult EmitirNotaEntradaSapiens(string codigoRegistro, int codTra, string operacao, string tipoNota, string observacao)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var N0203REGBusiness = new N0203REGBusiness();
                var E000NFCBusiness = new E000NFCBusiness();
                E085CLIBusiness E085CLIBusiness = new E085CLIBusiness();
                var aprovadoSucesso = false;
                var msgRetornoSapiens = string.Empty;
                var msgRetornoPedido = string.Empty;

                // Operação ==> Aprovar e Tipo Nota ==> Nutriplan
                
                if (int.Parse(operacao) == (int)Enums.OperacaoAprovacaoFaturamento.Aprovar && int.Parse(tipoNota) == (int)Enums.TipoNotaDevolucao.Nutriplan)
                {
                    var dadosProtocolo = N0203REGBusiness.PesquisaRegistroOcorrencia(long.Parse(codigoRegistro), (int)Enums.SituacaoRegistroOcorrencia.Recebido);
                    if (dadosProtocolo != null)
                    {
                        var cnpj = E085CLIBusiness.PesquisaClientes(dadosProtocolo.CODCLI).FirstOrDefault().CnpjCpf;
                        // Agrupa notas para validar se há xml de cliente com a mesma nota do protocolo
                        var listNotas = (from a in dadosProtocolo.N0203IPV
                                         group new { a } by new { a.CODEMP, a.CODFIL, a.NUMNFV } into grupo
                                         select new { grupo.Key.CODFIL, grupo.Key.NUMNFV }).ToList();
                        var nota = listNotas.FirstOrDefault();
                        var validarNotas = E000NFCBusiness.ValidarNotaCliente(nota.NUMNFV.ToString(), nota.CODFIL.ToString(), cnpj);
                        if (validarNotas.Count() == 0)
                        {
                            // Lançar Nota No SISTEMA SAPIENS
                            bool Motivo = N0203REGBusiness.ConsultarOrigem(Convert.ToInt32(codigoRegistro));

                            if (Motivo == true)
                            { 
                                N0203REGBusiness.PedidosViaOcorrencia(Convert.ToInt32(codigoRegistro), int.Parse(this.CodigoUsuarioLogado), out msgRetornoPedido);
                            }
                            if (!N0203REGBusiness.EmitirLancamentoNfe(long.Parse(codigoRegistro), dadosProtocolo, out msgRetornoSapiens))
                            {
                                msgRetornoSapiens = "Registro de devolução Nº " + codigoRegistro + " não aprovado.<br/><br/>Erro de integração com o sistema sapiens.<br/><br/>" + msgRetornoSapiens + " " + msgRetornoPedido;
                                var tipoAtend = Enums.TipoAtendimento.DevolucaoMercadorias;
                                if (dadosProtocolo.TIPATE == (int)Enums.TipoAtendimento.TrocaMercadorias)
                                    tipoAtend = Enums.TipoAtendimento.TrocaMercadorias;
                                // Envia Email para TI informando ERRO de integração...
                                this.MontarEmailErroIntegracaoSapiens(codigoRegistro, msgRetornoSapiens, tipoAtend);
                            }
                            else
                            {
                                aprovadoSucesso = true;
                            }
                        }
                        else
                        {
                            N0203REGBusiness.rollbackAprovacao(codigoRegistro);
                            msgRetornoSapiens = "Registro de devolução Nº " + codigoRegistro + " não aprovado.<br/><br/>Verifique as notas " + string.Join(",", validarNotas) + " do cliente.<br/><br/>Selecione o tipo de nota Cliente.";
                        }
                    }
                    else
                    {
                        msgRetornoSapiens = "Registro de devolução Nº " + codigoRegistro + " não encontrado.";
                    }
                }
                if (aprovadoSucesso == false)
                {
                    N0203REGBusiness.rollbackAprovacao(codigoRegistro);
                }
                return this.Json(new { msgRetornoSapiens, AprovadoSucesso = aprovadoSucesso }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ConsultarTransportadora(int ocorrencia, string tipo)
        {
            var N0203REGBusiness = new N0203REGBusiness();
            var listaTransportador = N0203REGBusiness.ConsultaTransportadora(ocorrencia, tipo);
            return this.Json(new { listaTransportador = listaTransportador, redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult OrigemOcorrencia(int Numreg)
        {
            var N0203REGBusiness = new N0203REGBusiness();
            var ListaOrigemOcorrencia = N0203REGBusiness.OrigemOcorrencia(Numreg);
            return this.Json(new { ListaOrigemOcorrencia = ListaOrigemOcorrencia, redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult AprovarRegistrosOcorrencia(string codigoRegistro, string operacao, string tipoOperacao, string tipoNota, string observacao, string msgRetornoSapiens)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                var N0203REGBusiness = new N0203REGBusiness();
                var aprovadoSucesso = true;
                string msgRetorno = "";
                string msgRetornoPedido = "";
                DebugEmail email = new DebugEmail();

                bool Motivo = N0203REGBusiness.ConsultarOrigem(Convert.ToInt32(codigoRegistro));
                email.Email("Email ", codigoRegistro);
                if (Motivo == true)
                {
                    N0203REGBusiness.PedidosViaOcorrencia(Convert.ToInt32(codigoRegistro), int.Parse(this.CodigoUsuarioLogado), out msgRetornoPedido);
                }

                if(msgRetornoPedido == "" || msgRetornoPedido == "OK") { 
                    msgRetorno = N0203REGBusiness.AprovarRegistrosOcorrencia(long.Parse(codigoRegistro), long.Parse(this.CodigoUsuarioLogado), observacao, int.Parse(operacao), tipoOperacao);
                } 
                else
                {
                    aprovadoSucesso = false;
                }

                if (msgRetorno.Contains("Operação não permitida, verifique se a ocorrência está com a situação integrado ou faturada.") || msgRetorno.Contains("Operação não permitida, verifique se a ocorrência está com a situação recebida") || msgRetorno.Contains("não está com a situação recebido") || msgRetorno.Contains("Registro de Ocorrência está vinculada a um agrupamento."))
                {
                    aprovadoSucesso = false;
                }
                else if (msgRetorno != "Registros de ocorrências agrupadas foram integradas com sucesso!")
                {
                    if (int.Parse(operacao) == (int)Enums.OperacaoAprovacaoFaturamento.Aprovar && int.Parse(tipoNota) == (int)Enums.TipoNotaDevolucao.Nutriplan)
                    {
                        var descNota = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.TipoNotaDevolucao.Nutriplan).GetValue<string>();
                        if (msgRetornoPedido != "")
                        {
                            msgRetorno += "<br></br> Retorno Sapiens: " + msgRetornoSapiens + "<br/>Tipo Nota: " + tipoNota + " - " + descNota + ". Obs Aprovação Faturamento: " + observacao + " <br/>Pedido Indenizado: " + msgRetornoPedido;
                        }
                        else 
                        { 
                            msgRetorno += "<br></br> Retorno Sapiens: " + msgRetornoSapiens + "<br/>Tipo Nota: " + tipoNota + " - " + descNota + ". Obs Aprovação Faturamento: " + observacao;
                        }
                    }

                    else if (int.Parse(operacao) == (int)Enums.OperacaoAprovacaoFaturamento.Aprovar && int.Parse(tipoNota) == (int)Enums.TipoNotaDevolucao.Cliente)
                    {
                        var descNota = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.TipoNotaDevolucao.Cliente).GetValue<string>();
                        if (msgRetornoPedido != "") { 
                            msgRetorno += "<br></br> Tipo Nota: " + tipoNota + " - " + descNota + ". Obs Aprovação Faturamento: " + observacao + " <br/>Pedido Indenizado: " + msgRetornoPedido;
                        }
                        else
                        {
                            msgRetorno += "<br></br> Tipo Nota: " + tipoNota + " - " + descNota + ". Obs Aprovação Faturamento: " + observacao;
                        }
                    }

                    if (int.Parse(tipoNota) == (int)Enums.TipoNotaDevolucao.Nutriplan)
                    {
                        if (msgRetornoPedido != "")
                        {
                            msgRetorno += msgRetorno + "<br/><br/>Retorno Sapiens: " + msgRetornoSapiens + "<br/> Pedido Indenizado: " + msgRetornoPedido;
                        }
                        else 
                        { 
                            msgRetorno += msgRetorno + "<br/><br/>Retorno Sapiens: " + msgRetornoSapiens;
                        }
                    }
                }

                return this.Json(new { msgRetorno, AprovadoSucesso = aprovadoSucesso }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Inicializa Views
        private void InicializaView()
        {
            if (this.AprovarRegistroView == null)
            {
                this.AprovarRegistroView = new AprovarRegistroOcorrenciaViewModel();
                if (this.AprovarRegistroView.ListaOperacoesAprovacao == null)
                {
                    PopulaSituacaoRegOcorrTelaAprovacao();
                }
            }
            if (this.CadUsuarioAprovadorView == null)
            {
                this.CadUsuarioAprovadorView = new UsuAprXOrigOcorViewModel();
                if (this.CadUsuarioAprovadorView.ListaTipoAtendimento == null)
                {
                    this.CadUsuarioAprovadorView.ListaTipoAtendimento = ListaTipoAtendimento();
                }
            }
            if (this.CadUsuarioOperFatView == null)
            {
                this.CadUsuarioOperFatView = new CadUsuarioOperFatViewModel();
                if (this.CadUsuarioOperFatView.ListaTipoAtendimento == null)
                {
                    this.CadUsuarioOperFatView.ListaTipoAtendimento = ListaTipoAtendimento();
                }
            }
        }
        public List<ListaN0204ATDPesquisa> ListaTipoAtendimento()
        {
            var ListaTipoAtendimento = ListaTiposAtendimento();
            var itemListaTp = new ListaN0204ATDPesquisa();
            itemListaTp.Codigo = 0;
            itemListaTp.Descricao = "Selecione...";
            itemListaTp.Situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString(); ;
            ListaTipoAtendimento.Add(itemListaTp);
            return ListaTipoAtendimento.OrderBy(c => c.Codigo).ToList();
        }
        public List<ListaN0204ATDPesquisa> ListaTiposAtendimento()
        {
            var N0204ATDBusiness = new N0204ATDBusiness();
            var listaTipoAtendimento = new List<N0204ATD>();
            var ListaN0204ATDPesquisa = new List<ListaN0204ATDPesquisa>();
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
        #endregion
        #region Popula Enums
        private void PopulaSituacaoRegOcorrTelaAprovacao()
        {
            var N0203UOFBusiness = new N0203UOFBusiness();
            var listaOperacoes = N0203UOFBusiness.PesquisaOperacoesAprovFatPorUsuario(long.Parse(this.CodigoUsuarioLogado));
            var itemNew = new OperacaoAprovacaoModel();
            itemNew.CodigoOperacao = 0;
            itemNew.DescricaoOperacao = "Selecione...";
            this.AprovarRegistroView.ListaOperacoesAprovacao = new List<OperacaoAprovacaoModel>();
            this.AprovarRegistroView.ListaOperacoesAprovacao.Add((OperacaoAprovacaoModel)itemNew);
            if (listaOperacoes.Count > 0)
            {
                foreach (var item in listaOperacoes)
                {
                    this.AprovarRegistroView.ListaOperacoesAprovacao.Add((OperacaoAprovacaoModel)item);
                }
            }
        }
        #endregion
    }
}
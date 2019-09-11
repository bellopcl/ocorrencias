using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using NWORKFLOW_WEB.MVC_4_BS.Models;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.Business;
using System.Configuration;
using System.Xml;
using System.Net;
using System.IO;
namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class LoginController : BaseController
    {
        public LoginViewModel loginViewModel { get; set; }
        List<ListaN0203TRAPesquisa> ListaN0203TRAPesquisaa = new List<ListaN0203TRAPesquisa>();
        List<ProtocolosAprovacaoModel> listaAprovacao = new List<ProtocolosAprovacaoModel>();
        N9999USU dadosUsuario;
        public MudarSenhaViewModel mudarSenhaViewModel { get; set; }
        /// <summary>
        ///  Carregar tela de login
        /// </summary>
        /// <returns>view</returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Login()
        {
            this.InicializaView();

            this.Logado = ((char)Enums.Logado.Nao).ToString();
            this.PermissoesDeAcesso = null;
            this.NomeUsuarioLogado = null;
            this.LoginUsuario = null;
            this.CodigoUsuarioLogado = null;

            return this.View("Login", this.loginViewModel);
        }

        /// <summary>
        /// Método para realizar logoff
        /// </summary>
        /// <returns>view de login</returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult FazerLogoff()
        {
            this.InicializaView();
            this.Logado = ((char)Enums.Logado.Nao).ToString();
            this.PermissoesDeAcesso = null;
            this.NomeUsuarioLogado = null;
            this.LoginUsuario = null;
            this.CodigoUsuarioLogado = null;
            this.Session["ExceptionErro"] = null;

            return this.View("Login", this.loginViewModel);
        }
        /// <summary>
        /// Carregar a tela de alteração de senha
        /// </summary>
        /// <returns>view de alteração</returns>
        //[AcceptVerbs(HttpVerbs.Get)]
        //public ActionResult MudarSenha()
        //{
        //    this.InicializaView();

        //    if (this.Logado != ((char)Enums.Logado.Sim).ToString())
        //    {
        //        return this.RedirectToAction("Login", "Login");
        //    }

        //    return this.View("MudarSenha", this.mudarSenhaViewModel);
        //}

        public int listaTramites()
        {
            if (this.TramitesNotificao.Count > 0)
            {
                return 1;
            }
            else
            {
                var n0203TRABusiness = new N0203TRABusiness();
                var listaTram = n0203TRABusiness.PesquisaTramitesUsuario(Convert.ToInt64(this.CodigoUsuarioLogado));

                if (listaTram.Count > 0)
                {
                    ListaN0203TRAPesquisaa = new List<ListaN0203TRAPesquisa>();

                    foreach (var item in listaTram)
                    {
                        ListaN0203TRAPesquisaa.Add((ListaN0203TRAPesquisa)item);
                    }
                }
            }
            this.TramitesNotificao = ListaN0203TRAPesquisaa;
            return 2;
        }

        public int listaProtocolosPendentes()
        {
            if (this.ProtocolosPendentes.Count > 0)
            {
                return 1;
            }
            var N0203REGBusiness = new N0203REGBusiness();
            listaAprovacao = N0203REGBusiness.PesquisaProtocolosPendentesAprovacaoNotificacao(Convert.ToInt64(this.CodigoUsuarioLogado));
            this.ProtocolosPendentes = listaAprovacao;
            return 2;
        }
        public void teste()
        {
            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            config.AppSettings.Settings.Remove("MyVariable");
            config.AppSettings.Settings.Add("MyVariable", "MyValue");
            config.Save();
        }


        public static void CallWebService()
        {
            var _url = "http://eucalipto.nutriplan.com.br:8080/g5-senior-services/sapiens_Syncnutriplan_ven_pedidos?wsdl";
            var _action = "AprovarPedido";

            XmlDocument soapEnvelopeXml = CreateSoapEnvelope();
            HttpWebRequest webRequest = CreateWebRequest(_url, _action);
            InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {
                    soapResult = rd.ReadToEnd();
                }
                Console.Write(soapResult);
            }
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope()
        {
            XmlDocument soapEnvelop = new XmlDocument();
            soapEnvelop.LoadXml(@"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ser='http://services.senior.com.br'>
                               <soapenv:Header/>
                               <soapenv:Body>
                                  <ser:AprovarPedido>  
                                     <user>null</user>
                                     <password></password>
                                     <encryption>0</encryption>
                                     <parameters>
                                        <dados>1;3;6;7878</dados>
                                     </parameters>
                                  </ser:AprovarPedido>
                               </soapenv:Body>
                            </soapenv:Envelope>");

            return soapEnvelop;
        }

        private static void InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
            }
        }
        /// <summary>
        /// Método para validar login
        /// </summary>
        /// <param name="modelo">dados de login</param>
        /// <returns>tela incial</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel modelo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this.InicializaView();

                    modelo.UserName = modelo.UserName.ToLower();
                    if (Membership.ValidateUser(modelo.UserName, modelo.Password))
                    {
                        
                        var N9999USUBusiness = new N9999USUBusiness();
                        // Busca código do usuário
                        
                        dadosUsuario = N9999USUBusiness.ListaDadosUsuarioPorLogin(modelo.UserName);
                        modelo.versaoSistema = "Produção";

                        if (dadosUsuario != null)
                        {
                            var n9999MENBusiness = new N9999MENBusiness();
                            var n9999SIS = new N9999SIS();
                            var lista = n9999MENBusiness.MontarMenu(dadosUsuario.CODUSU, (int)Enums.Sistema.NWORKFLOW);

                            N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                            if (lista.Count > 0)
                            {
                                this.Logado = ((char)Enums.Logado.Sim).ToString();
                                this.PermissoesDeAcesso = lista;
                                this.TramitesNotificao = ListaN0203TRAPesquisaa;
                                this.ProtocolosPendentes = listaAprovacao;
                                var ActiveDirectoryBusiness = new ActiveDirectoryBusiness();
                                this.NomeUsuarioLogado = Abreviar(ActiveDirectoryBusiness.ListaDadosUsuarioAD(modelo.UserName).Nome, true);
                                this.LoginUsuario = modelo.UserName;
                                this.CodigoUsuarioLogado = dadosUsuario.CODUSU.ToString();
                                this.Empresa = "NUTRIPLAST INDÚSTRIA E COMÉRCIO LTDA";
                                this.EmpresaFilial = "CASCAVEL";
                                //this.EmpresaFilialArmazem = "CENTRO DE DISTRIBUIÇÃO";
                                this.NomeAbreviadoEmpresa = "NUTRIPLAN";
                                this.CnpjEmpresa = "78.575.511/0001-29";
                                this.EnderecoEmpresa = "Av. Das Agroindústrias, 1829 - Distrito Industrial Domiciano Theobaldo Bresolin";
                                this.CepEmpresa = "85818-560";

                                return this.RedirectToAction("InformacoesProtocolo", "InformacoesProtocolo");
                            }
                            else
                            {
                                ModelState.AddModelError("", "Usuário não possuí acesso ao Sistema de Ocorrência. Favor abrir chamado solicitando acesso.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Usuário não possuí acesso ao Sistema de Ocorrência. Favor abrir chamado solicitando acesso.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Usuário ou senha inválida.");
                    }
                }

                return this.View("Login", this.loginViewModel);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }
        private string Abreviar(string s, bool nomesDoMeio)
        {

            // Quebro os nomes...

            string[] nomes = s.Split(' ');
            int inicio = 0;
            int fim = nomes.Length - 1;
            // Se eu não quiser abreviar o primeiro e o ultimo nome

            if (nomesDoMeio)
            {
                inicio = 1;
                fim = nomes.Length - 2;
            }

            // Monto o retorno
            string retorno = "";
            for (int i = 0; i < nomes.Length; i++)
            {

                if (!PalavrasExcecoes(nomes[i]) && i >= inicio && i <= fim)
                    retorno += nomes[i][0] + ". ";
                else
                    retorno += nomes[i] + " ";
            }
            return retorno.Trim();
        }

        System.Text.RegularExpressions.Regex regExc = new System.Text.RegularExpressions.Regex("DA|da|DE|de|DO|do|DAS|das|DOS|dos");


        private bool PalavrasExcecoes(string palavra)
        {
            return regExc.Match(palavra).Success;
        }


        /// <summary>
        /// Método para alterar senha
        /// </summary>
        /// <param name="modelo">dados de alteração</param>
        /// <returns>view de alteração</returns>
        //[AcceptVerbs(HttpVerbs.Post)]
        //[ValidateAntiForgeryToken]
        //public ActionResult MudarSenha(MudarSenhaViewModel modelo)
        //{
        //    try
        //    {
        //        this.InicializaView();
        //        this.mudarSenhaViewModel = modelo;

        //        if (ModelState.IsValid)
        //        {
        //            SYS_USUARIOBusiness sYS_USUARIOBusiness = new SYS_USUARIOBusiness();

        //            if (sYS_USUARIOBusiness.AlterarSenha(this.LoginUsuario, modelo.SenhaAtual, modelo.NovaSenha))
        //            {
        //                ViewBag.Message = "Senha alterada com sucesso.";
        //                this.mudarSenhaViewModel.SenhaAtual = string.Empty;
        //                this.mudarSenhaViewModel.NovaSenha = string.Empty;
        //            }
        //            else
        //            {
        //                ViewBag.Message = "Senha Atual inválida.";
        //            }
        //        }

        //        return this.View("MudarSenha", this.mudarSenhaViewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Session["ExceptionErro"] = ex;
        //        return this.RedirectToAction("ErroException", "Erro");
        //    }
        //}

        /// <summary>
        /// Inicializa as view de login
        /// </summary>
        private void InicializaView()
        {
            if (this.loginViewModel == null)
            {
                this.loginViewModel = new LoginViewModel();
            }

            if (this.mudarSenhaViewModel == null)
            {
                this.mudarSenhaViewModel = new MudarSenhaViewModel();
            }
        }
    }
}

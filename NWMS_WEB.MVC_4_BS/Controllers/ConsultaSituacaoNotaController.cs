using System;
using System.Linq;
using System.Web.Mvc;
using NWORKFLOW_WEB.MVC_4_BS.Models;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.Business;

namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class ConsultaSituacaoNotaController : BaseController
    {

        public ConsultaSituacaoNotaViewModel ConsultaSituacaoNotaViewModel { get; set; }

        public ActionResult ConsultaSituacaoNota()
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

                if (listaAcesso.Where(p => p.ENDPAG == "ConsultaSituacaoNota/ConsultaSituacaoNota").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaView();

                return this.View("ConsultaSituacaoNota", this.ConsultaSituacaoNotaViewModel);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
           
        }

        public JsonResult PesquisarNotaFiscalSituacao(string numeroNota, string codFilial)

        {
            
            E140NFVBusiness E140NFVBusiness = new E140NFVBusiness();

            var listaNotas = E140NFVBusiness.PesquisarSituacaoNota(Convert.ToInt64(numeroNota), Convert.ToInt64(this.CodigoUsuarioLogado), codFilial);

            return this.Json(new { listaNotas, sucesso = true }, JsonRequestBehavior.AllowGet);
        }

      
        public void InicializaView()
        {
            if (this.ConsultaSituacaoNotaViewModel == null)
            {
                this.ConsultaSituacaoNotaViewModel = new ConsultaSituacaoNotaViewModel();
            }
        }
    }
}

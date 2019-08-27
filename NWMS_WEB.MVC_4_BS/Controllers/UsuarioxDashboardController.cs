using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.Business;

namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class UsuarioxDashboardController : BaseController
    {
        
        public ActionResult UsuarioxDashboard()
        {
            return View();
        }
        
        public JsonResult PesquisaOrigemOcorrencia(string loginUsuario)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var N9999USUBusiness = new N9999USUBusiness();
                // Busca código do usuário
                var dadosUsuario = N9999USUBusiness.ListaDadosUsuarioPorLogin(loginUsuario);

                List<N0204DORI> ListaN0204DORI = new List<N0204DORI>();
                N0204DORIBusiness N0204DORIBusiness = new N0204DORIBusiness();
                if (dadosUsuario != null)
                {
                    ListaN0204DORI = N0204DORIBusiness.PesquisarPermissaoDashBoard(dadosUsuario.CODUSU);
                }
                return this.Json(new { ListaN0204DORI }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GravarPermissaoDashUsuario(string loginUsuario, string itensCodigo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var N9999USUBusiness = new N9999USUBusiness();
                // Busca código do usuário
                var dadosUsuario = N9999USUBusiness.ListaDadosUsuarioPorLogin(loginUsuario);

                string[] lista = itensCodigo.Split('-');
                N0204DORIBusiness N0204DORIBusiness = new N0204DORIBusiness();
                N0204DORIBusiness.GravarPermissaoDashUsuario(dadosUsuario.CODUSU, lista);
                return this.Json(new { GravadoSucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

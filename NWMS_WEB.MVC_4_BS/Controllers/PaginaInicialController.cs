using System.Web.Mvc;
using NUTRIPLAN_WEB.MVC_4_BS.Model;


namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class PaginaInicialController : BaseController
    {
        /// <summary>
        /// Carrega a tela inicial do sistema
        /// </summary>
        /// <returns>view da tela inicial</returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Index()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            return View("PaginaInicial");
        }
    }
}

using System.Web.Mvc;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class ErroController : BaseController
    {
       
        /// <summary>
        /// Carrega a tela de erro de acesso
        /// </summary>
        /// <returns>view de erro de acesso</returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ErroAcesso()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            this.Logado = ((char)Enums.Logado.Nao).ToString();
            this.PermissoesDeAcesso = null;
            this.NomeUsuarioLogado = null;
            this.LoginUsuario = null;
            this.CodigoUsuarioLogado = null;

            return View("Error");
        }

        /// <summary>
        /// Carrega a tela de erro de exceção
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>view de erro de exceção</returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult ErroException()
        {
            //if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            //{
            //    return this.RedirectToAction("Login", "Login");
            //}

            this.Logado = ((char)Enums.Logado.Nao).ToString();
            this.PermissoesDeAcesso = null;
            this.NomeUsuarioLogado = null;
            this.LoginUsuario = null;
            this.CodigoUsuarioLogado = null;

            return View("ErroException");
        }
    }
}

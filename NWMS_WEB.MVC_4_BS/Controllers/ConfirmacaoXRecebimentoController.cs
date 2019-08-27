using NUTRIPLAN_WEB.MVC_4_BS.Business;
using System;
using System.Web.Mvc;

namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class ConfirmacaoXRecebimentoController : BaseController
    {
        public ActionResult ConfirmacaoXRecebimento()
        {
            return View();
        }

        public JsonResult consultarPlacaPOC(string NUMREG)
        {
            
            if (NUMREG == "")
            {
                bool campos = true;
                return this.Json(new { campos }, JsonRequestBehavior.AllowGet);
            }
            N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
            var resposta = N0203REGBusiness.consultarPlacaPOC(NUMREG);
            return this.Json(new { resposta }, JsonRequestBehavior.AllowGet);
        }

    
        public JsonResult confirmarRecebimento(string NUMREG, string PLACA)
        {
            PLACA = PLACA.Replace("-", "");
            if (NUMREG == "" || PLACA == "")
            {
                bool campos = true;
                return this.Json(new { campos }, JsonRequestBehavior.AllowGet);
            }
            N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
            var resposta = N0203REGBusiness.confirmarRecebimento(NUMREG, PLACA.ToUpper(), Convert.ToInt64(this.CodigoUsuarioLogado));
            return this.Json(new { resposta }, JsonRequestBehavior.AllowGet);
        }

    }
}

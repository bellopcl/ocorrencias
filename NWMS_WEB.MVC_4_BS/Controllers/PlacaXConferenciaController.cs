using NUTRIPLAN_WEB.MVC_4_BS.Business;
using System;
using System.Web.Mvc;

namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class PlacaXConferenciaController : BaseController
    {

        public ActionResult PlacaXConferencia()
        {
            return View();
        }
        public JsonResult validarPermissao()
        {
            if (!consultarAcesso())
            {

                return this.Json(new { acesso = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return this.Json(new { acesso = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Vincular(int numeroRegistro, string codPlaca, string observacao)
        {
            bool acesso = true;
            N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
            DateTime localDate = DateTime.Now;

            if (N0203REGBusiness.validarPlaca(codPlaca.ToUpper()))
            {
                return this.Json(new { placa = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {

            }
            if (N0203REGBusiness.ValidarOcorrenciaTransportadora(numeroRegistro.ToString()))
            {
                return this.Json(new { transportadora = true }, JsonRequestBehavior.AllowGet);
            }

            bool retorno = N0203REGBusiness.Vincular(numeroRegistro.ToString(), codPlaca, this.CodigoUsuarioLogado, localDate.ToString(), observacao);
            return this.Json(new { retorno, acesso }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Revincular(int numeroRegistro, string codPlaca, string observacao)
        {
            N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
            DateTime localDate = DateTime.Now;
            if (!consultarAcesso())
            {
                return this.Json(new { acesso = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool retorno = N0203REGBusiness.Revincular(numeroRegistro.ToString(), codPlaca, this.CodigoUsuarioLogado, localDate.ToString(), observacao);
            }
         
            return this.Json(new { retorno = true }, JsonRequestBehavior.AllowGet);
        }
        public bool consultarAcesso()
        {
            try
            {
                N0203REGBusiness N0203REG = new N0203REGBusiness();
                return N0203REG.consultarParametroJustificativaColeta(this.LoginUsuario);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

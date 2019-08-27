using System;
using System.Collections.Generic;
using System.Web.Mvc;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.Business;

namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class AgrupamentoController : BaseController
    {
        //
        // GET: /Agrupamento/

        public ActionResult Agrupamento()
        {
            return View();
        }

        public JsonResult PesquisarOcorrenciasAgp()
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<Agrupamento> AGP = new List<Agrupamento>();
                AGP = N0203REGBusines.pesquisarOcorrenciaAGP();

                return this.Json(new { AGP, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PesquisarPorOcorrenciaAGP(long numreg)
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<Agrupamento> AGP = new List<Agrupamento>();
                AGP = N0203REGBusines.pesquisarPorOcorrenciaAGP(numreg);

                return this.Json(new { AGP, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult pesquisarAgrupamento(long codigoCliente, int filtro)
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<Agrupamento> AGP = new List<Agrupamento>();
                AGP = N0203REGBusines.pesquisarAgrupamento(codigoCliente, filtro);

                return this.Json(new { AGP, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult excluirAgrupamento(string agrupamento)
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();

                bool resposta = N0203REGBusines.excluirAgrupamento(agrupamento);
                string dados = resposta ? "" : "Não é possível excluir agrupamento já integrado!";
                return this.Json(new { resposta, dados }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult gravarAgrupamento(string ocorrencias, string dataGeracao)
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                var retorno = N0203REGBusines.GravarAgrupamento(ocorrencias, dataGeracao, this.CodigoUsuarioLogado);

                return this.Json(new { retorno, sucesso = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

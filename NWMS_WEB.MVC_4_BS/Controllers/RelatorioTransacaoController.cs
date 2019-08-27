
using Microsoft.Reporting.WebForms;
using NUTRIPLAN_WEB.MVC_4_BS.Business;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class RelatorioTransacaoController : BaseController
    {
        //
        // GET: /RelatorioTransacao/RelatorioTransacao
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RelatorioTransacao()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }
            try
            {
                var n9999MENBusiness = new N9999MENBusiness();
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);
                if (listaAcesso.Where(p => p.ENDPAG == "RelatorioTransacao/RelatorioTransacao").ToList().Count == 0) 
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }
                return this.View("RelatorioTransacao");
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public JsonResult ImprimirRelatorioTransacao(long NumReg)
        {
            try
            {
                List<RelatorioTransacoes> listaRelatorio = new List<RelatorioTransacoes>();
                N0203TRABusiness N0203TRABusiness = new N0203TRABusiness();
                string msgRetorno = "Nenhum Registro Encontrado.";

                listaRelatorio = N0203TRABusiness.RelatorioTransacao(NumReg);

                if (listaRelatorio.Count == 0)
                {
                    return this.Json(new { msg = msgRetorno }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    listaRelatorio[0].nomeUsuario = this.NomeUsuarioLogado;
                }
                LocalReport report = new LocalReport();

                report.ReportPath = Server.MapPath("~/Reports/RelatorioTransacao.rdlc");
                var reportRelatorio = new ReportDataSource("Corpo", listaRelatorio);
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
                return this.Json("data:application/pdf;base64, " + base64EncodedPDF, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}

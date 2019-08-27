using Microsoft.Reporting.WebForms;
using NUTRIPLAN_WEB.MVC_4_BS.Business;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class RelatorioPermissaoController : BaseController
    {
        //
        // GET: /RelatorioPermissao/
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RelatorioPermissao()
        {
            if(this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }
            try
            {
                var n9999MENBusiness = new N9999MENBusiness();
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if(listaAcesso.Where(p => p.ENDPAG == "RelatorioPermissao/RelatorioPermissao").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }
                return this.View("RelatorioPermissao");
            }
            catch(Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
            
        }

        public JsonResult imprimirRelatorioPermissao(string usuario, char status)
        {
            try
            {
                List<RelPermissaoTela> PermissaoTela = new List<RelPermissaoTela>();
                PERMISSAOBusiness pERMISSAOBusiness = new PERMISSAOBusiness();
                PermissaoTela = pERMISSAOBusiness.relPermissaoTelas(usuario, status);

                List<RelPermissaoAprovadorOrigem> PermissaoAprovadoOrigem = new List<RelPermissaoAprovadorOrigem>();
                PermissaoAprovadoOrigem = pERMISSAOBusiness.relPermissaoAprovadorOrigems(usuario, status);

                List<RelPermissaoUsuAproFaturamento> PermissaoUsuAproFaturamento = new List<RelPermissaoUsuAproFaturamento>();
                PermissaoUsuAproFaturamento = pERMISSAOBusiness.relPermissaoUsuAproFaturamentos(usuario, status);

                List<PermissaoDevTrocaUsu> PermissaoDevTroca = new List<PermissaoDevTrocaUsu>();
                PermissaoDevTroca = pERMISSAOBusiness.permissaoDevTrocaUsus(usuario, status);

                if(PermissaoTela.Count != 0)
                {
                    PermissaoTela[0].UsuarioImpressao = this.NomeUsuarioLogado;
                    PermissaoTela[0].Emissao = DateTime.Now;
                }

                LocalReport report = new LocalReport();
                report.ReportPath = Server.MapPath("~/Reports/RelatorioPermissao.rdlc");
                var reportRelatorio = new ReportDataSource("RelatorioPermissao", PermissaoTela);
                var reportAprovador = new ReportDataSource("Aprovador", PermissaoAprovadoOrigem);
                var reportFaturamento = new ReportDataSource("Faturamento", PermissaoUsuAproFaturamento);
                var reportDevolucaoTroca = new ReportDataSource("DevolucaoTroca", PermissaoDevTroca);

                report.Refresh();
                report.DataSources.Add(reportRelatorio);
                report.DataSources.Add(reportAprovador);
                report.DataSources.Add(reportFaturamento);
                report.DataSources.Add(reportDevolucaoTroca);

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

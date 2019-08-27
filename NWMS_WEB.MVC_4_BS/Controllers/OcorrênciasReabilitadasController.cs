using Microsoft.Reporting.WebForms;
using NUTRIPLAN_WEB.MVC_4_BS.Business;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUTRIPLAN_WEB.MVC_4_BS;

namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class OcorrênciasReabilitadasController : BaseController
    {
        //
        // GET: /OcorrênciasReabilitadas/
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult OcorrênciasReabilitadas()
        {
            if(this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }
            try
            {
                var n9999MENBusiness = new N9999MENBusiness();
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);
                
                if(listaAcesso.Where(p => p.ENDPAG == "OcorrênciasReabilitadas/OcorrênciasReabilitadas").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }
                return this.View("OcorrênciasReabilitadas");
            }
            catch(Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public JsonResult ImprimirRelatorioOcorrenciaReabilitada(long? Numreg, string dateInicial, string dateFinal, string operacao)
        {
            try
            {
                List<RelatorioOcorrenciasReabilitadas> listaRelatorio = new List<RelatorioOcorrenciasReabilitadas>();
                OCORREABILITADOBusiness oCORREABILITADOBusiness = new OCORREABILITADOBusiness();
                //string msgRetorno = "Nenhum Registro Encontrado.";
                
                listaRelatorio = oCORREABILITADOBusiness.pesquisarOcorrenciasHabilitadas(Numreg, dateInicial, dateFinal, operacao);
                if (listaRelatorio.Count != 0) { 
                    listaRelatorio[0].nomeUsuario = this.NomeUsuarioLogado;
                    listaRelatorio[0].Emissao = DateTime.Now;
                }
                LocalReport report = new LocalReport();
                report.ReportPath = Server.MapPath("~/Reports/OcorrenciaReabilitada.rdlc");
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

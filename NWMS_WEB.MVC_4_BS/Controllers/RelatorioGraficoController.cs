using Microsoft.Reporting.WebForms;
using NUTRIPLAN_WEB.MVC_4_BS.Business;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUTRIPLAN_WEB.MVC_4_BS;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using System.Xml;
using System.Xml.Linq;
using System.Web;

namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class RelatorioGraficoController : BaseController
    {
        //
        // GET: /RelatorioGrafico/
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RelatorioGrafico()
        {
            if(this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }
            try
            {
                var n9999MENBusiness = new N9999MENBusiness();
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "RelatorioGrafico/RelatorioGrafico").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }
                return this.View("RelatorioGrafico");
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
            
        }

        public JsonResult imprimirRelatorioExcel()
        {
            try
            {
                List<RelatorioGraficoItens> ListaRelatorioItens = new List<RelatorioGraficoItens>();
                N0203REGBusiness n0203REGBusiness = new N0203REGBusiness();
               // string msgRetorno = "Nenhum Registro Encontrado.";

                ListaRelatorioItens = n0203REGBusiness.RelatorioGraficoItens("", "12", "", "myBarChartIndustrial", "2018");

                List<RelatorioGraficoOcorrencia> ListaRelatorioOcorrencia = new List<RelatorioGraficoOcorrencia>();
                //ListaRelatorioOcorrencia = n0203REGBusiness.relatorioGraficoOcorrencias();


                LocalReport report = new LocalReport();
                report.ReportPath = Server.MapPath("~/Reports/RelatorioGrafico.rdlc");
                var reportRelatorio = new ReportDataSource("Itens", ListaRelatorioItens);

                var relatorioGraficoOcorrencia = new ReportDataSource("Ocorrencia", ListaRelatorioOcorrencia);

               // string nomeRelatorio = "Relatorio";

                report.Refresh();
                report.DataSources.Add(reportRelatorio);
                report.DataSources.Add(relatorioGraficoOcorrencia);
                
                string reportType = "Excel";
                string mineType;
                byte[] reportBytes;
                string encoding;
                string fileNameExtension;
                Warning[] warnings;
                string[] streams;

                string deviceInfo =
                "<DeviceInfo>" +
                " <OutputFormat>Excel</OutputFormat>" +
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
                return this.Json("data:application/Excel;base64, " + base64EncodedPDF, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ImprimirGrafico()
        {
            try
            {
                List<RelatorioGraficoItens> ListaRelatorioItens = new List<RelatorioGraficoItens>();
                N0203REGBusiness n0203REGBusiness = new N0203REGBusiness();
                //string msgRetorno = "Nenhum Registro Encontrado.";

                ListaRelatorioItens = n0203REGBusiness.RelatorioGraficoItens("", "12", "", "myBarChartIndustrial", "2018");

                List<RelatorioGraficoOcorrencia> ListaRelatorioOcorrencia = new List<RelatorioGraficoOcorrencia>();
                //ListaRelatorioOcorrencia = n0203REGBusiness.relatorioGraficoOcorrencias(mes,);


                LocalReport report = new LocalReport();
                report.ReportPath = Server.MapPath("~/Reports/RelatorioGrafico.rdlc");
                var reportRelatorio = new ReportDataSource("Itens", ListaRelatorioItens);

                var relatorioGraficoOcorrencia = new ReportDataSource("Ocorrencia", ListaRelatorioOcorrencia);

                

                report.Refresh();
                report.DataSources.Add(reportRelatorio);
                report.DataSources.Add(relatorioGraficoOcorrencia);

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
                return this.Json("data:application/PDF;base64, " + base64EncodedPDF, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }

        }

    }
}

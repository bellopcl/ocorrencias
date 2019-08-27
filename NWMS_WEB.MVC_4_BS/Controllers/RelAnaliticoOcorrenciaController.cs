using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using NWORKFLOW_WEB.MVC_4_BS.Models;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.Business;


namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class RelAnaliticoOcorrenciaController : BaseController
    {
        public RelAnaliticoOcorrenciaViewModel RelatorioAnaliticoOcorrencia { get; set; }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult RelAnaliticoOcorrencia()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {
                var n9999MENBusiness = new N9999MENBusiness();

                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "RelAnaliticoOcorrencia/RelAnaliticoOcorrencia").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                return this.View("RelAnaliticoOcorrencia", this.RelatorioAnaliticoOcorrencia);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public JsonResult ImprimirRelatorioRegistroOCorrencia(string campoNumeroRegistro, string campoFilial, string campoEmbarque, string campoPlaca, string campoPeriodoInicial, string campoPeriodoFinal, string campoCliente, string campoSituacao, string campoDataFaturamento)
        {
            try
            {
                var N0203REGBusiness = new N0203REGBusiness();
                var listaRegistros = N0203REGBusiness.imprimirRelatorioAnaliticoRegistroOcorrencia(campoNumeroRegistro, campoFilial, campoEmbarque, campoPlaca, campoPeriodoInicial, campoPeriodoFinal, campoCliente, campoSituacao, campoDataFaturamento);


                if (listaRegistros.Count == 0)
                {
                    return this.Json(new { msgRetorno = "Vazio", listaVazia = true }, JsonRequestBehavior.AllowGet);
                }

                DateTime dataEmissao = DateTime.Now;
                listaRegistros[0].DATAEMISSAO = dataEmissao.ToString();
                listaRegistros[0].USUIMPR = this.NomeUsuarioLogado;

                LocalReport report = new LocalReport();

                report.ReportPath = Server.MapPath("~/Reports/RelatorioAnalitico.rdlc");

                var reportRelatorio = new ReportDataSource("RelatorioAnaliticoDataSet", listaRegistros);
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
            //    return this.Json("data:application/pdf;base64, " + base64EncodedPDF);
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

using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Collections;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.Business;
using Microsoft.Reporting.WebForms;

namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class InformacoesProtocoloController : BaseController
    {
        public ActionResult InformacoesProtocolo()
        {
            return View();
        }
        #region TABELA

        public JsonResult PesquisarProtocolosPendentes()
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<Ocorrencia> ListaProtocolosPendentes = new List<Ocorrencia>();
                ListaProtocolosPendentes = N0203REGBusines.PesquisaProtocoloPendentes("", "", "", "", "", "", "", "2", "", Convert.ToInt64(this.CodigoUsuarioLogado));

                return this.Json(new { ListaProtocolosPendentes, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult pedidosFaturarIndenizacao()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness n0203REGBusiness = new N0203REGBusiness();
                var contador = n0203REGBusiness.pedidosFaturarIndenizacao();
                return this.Json(new { contador, sucesso = true }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CarregarIndicadoresTabela(string status, string mes, string filtroAgrup, string indicador, string ano)
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<Ocorrencia> ListaIndicador = new List<Ocorrencia>();
                ListaIndicador = N0203REGBusines.CarregarIndicadoresTabela(status, mes, filtroAgrup, indicador, ano);

                return this.Json(new { ListaIndicador, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult carregarObservacoes(string numreg)
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
       
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<String> listarObservacoes = new List<String>();
                listarObservacoes = N0203REGBusines.listarObservacoes(numreg);

                return this.Json(new { listarObservacoes, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult imprimirRelatorioExcel(string status, string mes, string filtroAgrup, string indicador, string ano)
        {
            try
            {
                List<RelatorioGraficoItens> ListaRelatorioItens = new List<RelatorioGraficoItens>();
                N0203REGBusiness n0203REGBusiness = new N0203REGBusiness();
                //string msgRetorno = "Nenhum Registro Encontrado.";
                
                ListaRelatorioItens = n0203REGBusiness.RelatorioGraficoItens(status, mes, filtroAgrup, indicador, ano);

                List<RelatorioGraficoOcorrencia> ListaRelatorioOcorrencia = new List<RelatorioGraficoOcorrencia>();
                ListaRelatorioOcorrencia = n0203REGBusiness.relatorioGraficoOcorrencias(mes, ano, indicador);


                LocalReport report = new LocalReport();
                report.ReportPath = Server.MapPath("~/Reports/RelatorioGrafico.rdlc");
                var reportRelatorio = new ReportDataSource("Itens", ListaRelatorioItens);

                var relatorioGraficoOcorrencia = new ReportDataSource("Ocorrencia", ListaRelatorioOcorrencia);

                //string nomeRelatorio = "Relatorio";

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

        public JsonResult ImprimirGrafico(string status, string mes, string filtroAgrup, string indicador, string ano)
        {
            try
            {
                List<RelatorioGraficoItens> ListaRelatorioItens = new List<RelatorioGraficoItens>();
                N0203REGBusiness n0203REGBusiness = new N0203REGBusiness();
                //string msgRetorno = "Nenhum Registro Encontrado.";

                ListaRelatorioItens = n0203REGBusiness.RelatorioGraficoItens(status, mes, filtroAgrup, indicador, ano);

                List<RelatorioGraficoOcorrencia> ListaRelatorioOcorrencia = new List<RelatorioGraficoOcorrencia>();
                ListaRelatorioOcorrencia = n0203REGBusiness.relatorioGraficoOcorrencias(mes, ano, indicador);


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

        public JsonResult carregarIndicadorSetores(string status, string mes, string indicador, string ano)
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<Ocorrencia> ListaIndicador = new List<Ocorrencia>();
                ListaIndicador = N0203REGBusines.carregarIndicadorSetores(status, mes, indicador, ano);

                return this.Json(new { ListaIndicador, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PesquisarProtocolosAprovados()
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<Ocorrencia> ListaProtocolosPendentes = new List<Ocorrencia>();
                ListaProtocolosPendentes = N0203REGBusines.PesquisaProtocolosAprovadosDashBoard("", "", "", "", "", "", "", "4", "", Convert.ToInt64(this.CodigoUsuarioLogado));

                return this.Json(new { ListaProtocolosPendentes, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PesquisarProtocolosAtrasados()
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                int quantidadeAtrasado = N0203REGBusines.PesquisaProtocolosAtrasados(Convert.ToInt64(this.CodigoUsuarioLogado));

                return this.Json(new { quantidadeAtrasado, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult timeLine(string numeroOcorrencia)
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<TimeLine> listaTimeLine = new List<TimeLine>();
                listaTimeLine = N0203REGBusines.timeLine(Convert.ToInt64(numeroOcorrencia));

                return this.Json(new { listaTimeLine, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PesquisaProtocolosIndenizados()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness n0203REGBusiness = new N0203REGBusiness();
                List<Ocorrencia> ListaProtocolosPendentes = new List<Ocorrencia>();
                ListaProtocolosPendentes = n0203REGBusiness.PesquisaProtocolosIndenizados("", "", "", "", "", "", "", "11", "", Convert.ToInt64(this.CodigoUsuarioLogado));
                return this.Json(new { ListaProtocolosPendentes, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult PesquisarProtocolosPendentesAprovacaoDashBoard()
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<Ocorrencia> ListaProtocolosPendentes = new List<Ocorrencia>();
                ListaProtocolosPendentes = N0203REGBusines.PesquisarProtocolosPendentesAprovacaoDashBoard("", "", "", "", "", "", "", "2", "", Convert.ToInt64(this.CodigoUsuarioLogado));
                return this.Json(new { ListaProtocolosPendentes, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ocorrenciaDrill(string status, string mes, string filtroAgrup, string indicador, string ano)
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<Ocorrencia> ListaProtocolosPendentes = new List<Ocorrencia>();
                ListaProtocolosPendentes = N0203REGBusines.ocorrenciaDrill(status, mes, filtroAgrup, indicador, ano);
                return this.Json(new { ListaProtocolosPendentes, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult carregarOcorrenciasFaturamentoAtraso()
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<Ocorrencia> ListaProtocolosPendentes = new List<Ocorrencia>();
                ListaProtocolosPendentes = N0203REGBusines.carregarOcorrenciasFaturamentoAtraso();
                return this.Json(new { ListaProtocolosPendentes, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult carregarOcorrenciasFaturamentoEmDia()
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<Ocorrencia> ListaProtocolosPendentes = new List<Ocorrencia>();
                ListaProtocolosPendentes = N0203REGBusines.carregarOcorrenciasFaturamentoEmDia();
                return this.Json(new { ListaProtocolosPendentes, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        public JsonResult PesquisaProtocolosAprovadosXPendentesAprovacao()
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<long> quantidade = N0203REGBusines.PesquisaProtocolosAprovadosXPendentesAprovacao(Convert.ToInt64(this.CodigoUsuarioLogado));

                return this.Json(new { quantidade, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult carregarAtrasoFaturamento()
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                ArrayList quantidade = N0203REGBusines.carregarAtrasoFaturamento();

                return this.Json(new { quantidade, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AprovadosEsperandoFaturamento()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
              
                int quantidadeEmDia = 0;
                int quantidadeEmAtraso = 0;
                N0203REGBusiness N0203REGBusines = new N0203REGBusiness();
                List<Ocorrencia> lista = new List<Ocorrencia>();
                lista = N0203REGBusines.carregarProtocolosForamAprovadosEsperandoFaturamento("", "", "", "", "", "", "", "4,6,8,9,11", "", Convert.ToInt64(this.CodigoUsuarioLogado));
                DateTime dateForButton = DateTime.Now.AddDays(-30);
                foreach (var item in lista)
                {
                    if (Convert.ToDateTime(item.DataHrGeracao) < dateForButton)
                    {
                        quantidadeEmAtraso++;
                    }
                    else
                    {
                        quantidadeEmDia++;
                    }
                }

                return this.Json(new { lista, quantidadeEmAtraso, quantidadeEmDia }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }


        #region ACESSO
        public JsonResult PesquisarAcessoDashboard(string loginUsuario)
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var N9999USUBusiness = new N9999USUBusiness();
                // Busca código do usuário
                var dadosUsuario = N9999USUBusiness.ListaDadosUsuarioPorLogin(this.LoginUsuario);

                List<N0204DUSU> ListaN0204DUSU = new List<N0204DUSU>();
                N0204DUSUBusiness N0204DUSUBusiness = new N0204DUSUBusiness();
                if (dadosUsuario != null)
                {
                    ListaN0204DUSU = N0204DUSUBusiness.PesquisarAcessoDashBoard(dadosUsuario.CODUSU);
                }
                return this.Json(new { ListaN0204DUSU }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult QuantidadeProtocolosPorArea(int dias, int situacao)
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var N9999USUBusiness = new N9999USUBusiness();

                List<N0204ORI> ListaN0204ORI = new List<N0204ORI>();
                N0203REGBusiness N0203ORIBusiness = new N0203REGBusiness();
                ListaN0204ORI = N0203ORIBusiness.quantidadeProtocolosPorArea(dias, situacao);

                return this.Json(new { ListaN0204ORI }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        public JsonResult QuantidadeProtocolosPorAreaMeses(int dias, int situacao)
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var N9999USUBusiness = new N9999USUBusiness();
                // Busca código do usuário
                var dadosUsuario = N9999USUBusiness.ListaDadosUsuarioPorLogin(this.LoginUsuario);

                List<N0204ORI> ListaN0204ORI = new List<N0204ORI>();
                N0203REGBusiness N0203ORIBusiness = new N0203REGBusiness();
                if (dadosUsuario != null)
                {
                    ListaN0204ORI = N0203ORIBusiness.quantidadeProtocolosPorAreaMeses(dias, situacao);
                }
                return this.Json(new { ListaN0204ORI }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult mesXOcorrencia()
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<MesXOcorrencia> mesOcorrencia = new List<MesXOcorrencia>();
                N0203REGBusiness N0203ORIBusiness = new N0203REGBusiness();
                mesOcorrencia = N0203ORIBusiness.mesXOcorrencia();
                return this.Json(new { mesOcorrencia }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult mesXOrigem()
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                List<MesXOrigem> mesOrigem = new List<MesXOrigem>();
                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                mesOrigem = N0203REGBusiness.mesXOrigem();
                return this.Json(new { mesOrigem }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult mediaPreAprovado()
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                int media;
                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                media = N0203REGBusiness.mediaPreAprovado();
                return this.Json(new { media }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult mediaAprovado()
        {

            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                int media;
                List<MediaAprovado> MediaAprovado = new List<MediaAprovado>();
                N0203REGBusiness N0203REGBusiness = new N0203REGBusiness();
                media = N0203REGBusiness.mediaAprovado();
                return this.Json(new { media }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
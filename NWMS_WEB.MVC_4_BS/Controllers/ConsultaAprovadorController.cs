using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NWORKFLOW_WEB.MVC_4_BS.Models;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.Business;

namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class 
        
        AprovadorController : BaseController
    {
        public ConsultaAprovadorViewModel ConsultaAprovadorViewModel { get; set; }

        public ActionResult ConsultaAprovador()
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {

                var n9999MENBusiness = new N9999MENBusiness();

                // Validação para verificar se o usuário tem acesso quando digitar a url da pagina no navegador.
                var listaAcesso = n9999MENBusiness.MontarMenu(long.Parse(this.CodigoUsuarioLogado), (int)Enums.Sistema.NWORKFLOW);

                if (listaAcesso.Where(p => p.ENDPAG == "ConsultaAprovador/ConsultaAprovador").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaView();

                return this.View("ConsultaAprovador", this.ConsultaAprovadorViewModel);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        public JsonResult PesquisaAprovador(long codigoOrigem, long codigoAtendimento)
        {
                   
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }
            try
            {
                List<UsuarioADModel> ListaUsuariosAD = new List<UsuarioADModel>();
                ListaUsuariosAD = ListaUsuariosAprovadores(codigoOrigem, codigoAtendimento);

                return this.Json(new { ListaUsuariosAD, sucesso = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public List<UsuarioADModel> ListaUsuariosAprovadores(long codigoOrigem, long codigoAtedimento)
        {
            // Pegar Codigo dos Usuarios Aprovadores
            List<N0203UAP> itensUsuarios = new List<N0203UAP>();
            N0203UAPBusiness N0203UAPBusiness = new N0203UAPBusiness();
            itensUsuarios = N0203UAPBusiness.PesquisarUsuariosAprovadores(codigoOrigem, codigoAtedimento);
           
            // Pegar Informacoes de Login Usuario Aprovadores
            List<N9999USU> itensLoginUsuario = new List<N9999USU>();
            N9999USUBusiness N9999USUBusiness = new N9999USUBusiness();

            foreach (var item in itensUsuarios)
	        {
		        itensLoginUsuario.Add(N9999USUBusiness.ListaDadosUsuarioPorCodigo(item.CODUSU));
	        }

            ActiveDirectoryBusiness AD = new ActiveDirectoryBusiness();
            List<UsuarioADModel> usuarioAD = new List<UsuarioADModel>();
            foreach (var itens in itensLoginUsuario)
	        {
                usuarioAD.Add(AD.ListaDadosUsuarioAD(itens.LOGIN));
            }
            return usuarioAD;
        }

        public List<ListaN0204ORIPesquisa> ListaOriOcorrencia()
        {
            List<N0204ORI> itensOrigens = new List<N0204ORI>();
            N0204ORIBusiness N0204ORIBusiness = new N0204ORIBusiness();
            List<ListaN0204ORIPesquisa> ListaN0204ORIPesquisa = new List<ListaN0204ORIPesquisa>();

            itensOrigens = N0204ORIBusiness.PesquisaOrigemOcorrencia();
            ListaN0204ORIPesquisa itemListaOr = new ListaN0204ORIPesquisa();

            itemListaOr.Codigo = 0;
            itemListaOr.Descricao = "Selecione...";
            itemListaOr.Situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString(); ;
            ListaN0204ORIPesquisa.Add(itemListaOr);

            if (itensOrigens.Count > 0)
            {
                foreach (N0204ORI item in itensOrigens)
                {
                    ListaN0204ORIPesquisa.Add((ListaN0204ORIPesquisa)item);
                }
            }
           
            return ListaN0204ORIPesquisa;
        }

        public List<ListaN0204ATDPesquisa> ListaTipoAtendimento()
        {
            List<ListaN0204ATDPesquisa> ListaTipoAtendimento = new List<ListaN0204ATDPesquisa>();
            ListaTipoAtendimento = ListaTiposAtendimento();
            ListaN0204ATDPesquisa itemListaTp = new ListaN0204ATDPesquisa();
            itemListaTp.Codigo = 0;
            itemListaTp.Descricao = "Selecione...";
            itemListaTp.Situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString(); ;
            ListaTipoAtendimento.Add(itemListaTp);
            return ListaTipoAtendimento.OrderBy(c => c.Codigo).ToList();
        }

        public List<ListaN0204ATDPesquisa> ListaTiposAtendimento()
        {
            N0204ATDBusiness N0204ATDBusiness = new N0204ATDBusiness();
            List<N0204ATD> listaTipoAtendimento = new List<N0204ATD>();
            List<ListaN0204ATDPesquisa> ListaN0204ATDPesquisa = new List<ListaN0204ATDPesquisa>();
            listaTipoAtendimento = N0204ATDBusiness.PesquisaTipoAtendimento();

            if (listaTipoAtendimento.Count > 0)
            {
                ListaN0204ATDPesquisa = new List<ListaN0204ATDPesquisa>();

                foreach (N0204ATD item in listaTipoAtendimento)
                {
                    ListaN0204ATDPesquisa.Add((ListaN0204ATDPesquisa)item);
                }
            }

            return ListaN0204ATDPesquisa;
        }

        public void InicializaView()
        {
            if (this.ConsultaAprovadorViewModel == null)
            {
                this.ConsultaAprovadorViewModel = new ConsultaAprovadorViewModel();

                if (this.ConsultaAprovadorViewModel.ListaOrigemOcorrencia == null)
                {
                    this.ConsultaAprovadorViewModel.ListaOrigemOcorrencia = ListaOriOcorrencia();
                }
                if (this.ConsultaAprovadorViewModel.ListaTipoAtendimento == null)
                {
                    this.ConsultaAprovadorViewModel.ListaTipoAtendimento = ListaTipoAtendimento();
                }
            }
        }
    }
}

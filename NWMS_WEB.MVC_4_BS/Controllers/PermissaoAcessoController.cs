using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NWORKFLOW_WEB.MVC_4_BS.Models;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.Business;

namespace NWORKFLOW_WEB.MVC_4_BS.Controllers
{
    public class PermissaoAcessoController : BaseController
    {
        public PermissaoAcessoViewModel permissaoAcesso { get; set; }

        /// <summary>
        /// Carregar a tela de permissão
        /// </summary>
        /// <returns>view</returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Permissao()
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

                if (listaAcesso.Where(p => p.ENDPAG == "PermissaoAcesso/Permissao").ToList().Count == 0)
                {
                    return this.RedirectToAction("ErroAcesso", "Erro");
                }

                this.InicializaView();
                this.PermissoesDeAcessoGerenciamento = null;

                return this.View("Permissao", this.permissaoAcesso);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        /// <summary>
        /// Carregar as permissões do usuário pesquisado
        /// </summary>
        /// <param name="modelo">dados da tela</param>
        /// <returns>view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Permissao(PermissaoAcessoViewModel modelo)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.RedirectToAction("Login", "Login");
            }

            try
            {
                this.InicializaView();

                permissaoAcesso = modelo;
                modelo.LoginUsuario = modelo.LoginUsuario.ToLower();

                var N9999USUBusiness = new N9999USUBusiness();

                // Busca código do usuário
                var dadosUsuario = N9999USUBusiness.ListaDadosUsuarioPorLogin(modelo.LoginUsuario);

                if (dadosUsuario == null)
                {
                    // Se usuário não cadastrado no NWORKFLOW, cadastra o mesmo.
                    N9999USUBusiness.CadastrarUsuario(modelo.LoginUsuario);

                    // Busca código do usuário cadastrado
                    dadosUsuario = N9999USUBusiness.ListaDadosUsuarioPorLogin(modelo.LoginUsuario);
                }

                var n9999MENBusiness = new N9999MENBusiness();

                // Lista todos os itens do menu, telas e operações que o usuário pesquisado já possuí algum tipo de operação
                var lista = n9999MENBusiness.MontarTreeViewPermissoes(dadosUsuario.CODUSU, (int)Enums.Sistema.NWORKFLOW);

                // Id dos itens da treeview que o usuário já possuí acesso para "checkagem" após o load da treeview;
                foreach (MenuModel item in lista.Where(p => p.PERMEN == ((char)Enums.Operacao.Pesquisar).ToString()).ToList())
                {
                    permissaoAcesso.menusOperacaoUser = permissaoAcesso.menusOperacaoUser + "#" + item.CODMEN.ToString() + item.PERMEN + "-";

                    if (item.INSMEN == ((char)Enums.Operacao.Inserir).ToString())
                    {
                        permissaoAcesso.menusOperacaoUser = permissaoAcesso.menusOperacaoUser + "#" + item.CODMEN.ToString() + item.INSMEN + "-";
                    }
                    if (item.ALTMEN == ((char)Enums.Operacao.Alterar).ToString())
                    {
                        permissaoAcesso.menusOperacaoUser = permissaoAcesso.menusOperacaoUser + "#" + item.CODMEN.ToString() + item.ALTMEN + "-";
                    }
                    if (item.EXCMEN == ((char)Enums.Operacao.Excluir).ToString())
                    {
                        permissaoAcesso.menusOperacaoUser = permissaoAcesso.menusOperacaoUser + "#" + item.CODMEN.ToString() + item.EXCMEN + "-";
                    }
                }

                this.PermissoesDeAcessoGerenciamento = lista;

                return this.View("Permissao", this.permissaoAcesso);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.RedirectToAction("ErroException", "Erro");
            }
        }

        /// <summary>
        /// Pesquisar dados do usuário informado
        /// </summary>
        /// <param name="codigoUser">código do usuário</param>
        /// <returns>dados</returns>
        public JsonResult PesquisarUsuarios(string loginUsuario)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var listaUsuariosAD = new List<UsuarioADModel>();
                var ActiveDirectoryBusiness = new ActiveDirectoryBusiness();

                if (!string.IsNullOrEmpty(loginUsuario))
                {
                    var usuario = ActiveDirectoryBusiness.ListaDadosUsuarioAD(loginUsuario);

                    if (usuario.Nome != null)
                    {
                        listaUsuariosAD.Add(usuario);
                    }
                }
                else
                {
                    listaUsuariosAD = ActiveDirectoryBusiness.ListaTodosUsuariosAD();
                }

                return Json(new { listaUsuariosAD }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Gravar as permissoes de autonomia para o usuário selecionado.
        /// </summary>
        /// <param name="operacoesSelecionadas"></param>
        /// <param name="codUser"></param>
        /// <returns></returns>
        public JsonResult GravarPermissoesUsuario(string operacoesSelecionadas, string loginUsuario)
        {
            if (this.Logado != ((char)Enums.Logado.Sim).ToString())
            {
                return this.Json(new { redirectUrl = Url.Action("Login", "Login"), Logado = true }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var N9999USUBusiness = new N9999USUBusiness();

                // Busca código do usuário
                var codUser = N9999USUBusiness.ListaDadosUsuarioPorLogin(loginUsuario.ToLower()).CODUSU;

                var lista = new List<N9999USM>();
                var item = new N9999USM();
                var n9999UXMBusiness = new N9999UXMBusiness();

                string[] operacoes = operacoesSelecionadas.Split('-');

                if (operacoes[0] == "")
                {
                    operacoes = new string[0];
                }

                bool retorno = false;

                for (int i = 0; i < operacoes.Length; i++)
                {
                    item = new N9999USM();
                    long codMenu = long.Parse(operacoes[i].Substring(0, operacoes[i].Length - 1));

                    string[] grupoMenu = operacoes.Where(p => p.Substring(0, p.Length - 1) == codMenu.ToString()).ToArray();

                    item.CODUSU = codUser;
                    item.CODMEN = codMenu;
                    item.CODSIS = (long)Enums.Sistema.NWORKFLOW;
                    item.PERMEN = item.INSMEN = item.ALTMEN = item.EXCMEN = "X";

                    for (int x = 0; x < grupoMenu.Length; x++)
                    {
                        if (grupoMenu[x].Substring(grupoMenu[x].Length - 1, 1) == ((char)Enums.Operacao.Pesquisar).ToString())
                        {
                            item.PERMEN = ((char)Enums.Operacao.Pesquisar).ToString();
                        }
                        else if (grupoMenu[x].Substring(grupoMenu[x].Length - 1, 1) == ((char)Enums.Operacao.Inserir).ToString())
                        {
                            item.INSMEN = ((char)Enums.Operacao.Inserir).ToString();
                        }
                        else if (grupoMenu[x].Substring(grupoMenu[x].Length - 1, 1) == ((char)Enums.Operacao.Alterar).ToString())
                        {
                            item.ALTMEN = ((char)Enums.Operacao.Alterar).ToString();
                        }
                        else if (grupoMenu[x].Substring(grupoMenu[x].Length - 1, 1) == ((char)Enums.Operacao.Excluir).ToString())
                        {
                            item.EXCMEN = ((char)Enums.Operacao.Excluir).ToString();
                        }
                    }

                    if (grupoMenu.Length > 1)
                    {
                        i = i + grupoMenu.Length - 1;
                    }

                    lista.Add(item);
                }

                string msgRetorno = "Ocorreu algum erro durante o processamento. Favor informar o setor de TI.";

                if (n9999UXMBusiness.GravarPermissoesUser(lista, codUser, (int)Enums.Sistema.NWORKFLOW))
                {
                    msgRetorno = "Operações gravadas com sucesso.";
                    retorno = true;

                    // Se o usuário logado for o mesmo que está sofrendo alterações de permissões de acesso, seta logado para false para que o usuário efetue login novamente.
                    if (loginUsuario.ToLower() == this.LoginUsuario)
                    {
                        this.Logado = ((char)Enums.Logado.Nao).ToString();
                    }
                }

                return this.Json(new { msgRetorno = msgRetorno, RetornoOk = retorno }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                this.Session["ExceptionErro"] = ex;
                return this.Json(new { redirectUrl = Url.Action("ErroException", "Erro"), ErroExcecao = true }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Inicializa as views de permissão 
        /// </summary>
        private void InicializaView()
        {
            if (this.permissaoAcesso == null)
            {
                this.permissaoAcesso = new PermissaoAcessoViewModel();
            }
        }

    }
}

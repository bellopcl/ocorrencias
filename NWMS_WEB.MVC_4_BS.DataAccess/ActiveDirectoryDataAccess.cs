using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class ActiveDirectoryDataAccess
    {
        public string EnderecoAD = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.ActiveDirectory.Endereco).GetValue<string>();
        public string UsuarioAD = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.ActiveDirectory.Usuario).GetValue<string>();
        public string SenhaAD = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.ActiveDirectory.Senha).GetValue<string>();
        public string[] propriedades = new string[3] { "sAMAccountName", "name", "mail" };
        /// <summary>
        /// Pesquisa dados do usuário.
        /// </summary>
        /// <param name="loginUsuario">Login do Usuário</param>
        /// <returns>dadosUsuario</returns>
        public UsuarioADModel ListaDadosUsuarioAD(string loginUsuario)
        {
            try
            {
                string usuario = EnderecoAD + UsuarioAD + SenhaAD;
                
                DirectoryEntry DirectoryEntry = new DirectoryEntry(EnderecoAD, UsuarioAD, SenhaAD);
                string filtro = "(&(objectClass=user)(sAMAccountName=" + loginUsuario + "))";
                DirectorySearcher dSearch = new DirectorySearcher(DirectoryEntry, filtro, propriedades, SearchScope.Subtree);

                UsuarioADModel dadosUsuario = new UsuarioADModel();

                foreach (SearchResult sResultSet in dSearch.FindAll())
                {
                    dadosUsuario.Login = GetProperty(sResultSet, propriedades[0]).ToLower();
                    dadosUsuario.Nome = GetProperty(sResultSet, propriedades[1]).ToUpper();
                    dadosUsuario.Email = GetProperty(sResultSet, propriedades[2]).ToLower();
                }
                
                return dadosUsuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <returns>listaUsuario</returns>
        public List<UsuarioADModel> ListaTodosUsuariosAD()
        {
            try
            {
                DirectoryEntry DirectoryEntry = new DirectoryEntry(EnderecoAD, UsuarioAD, SenhaAD);
                // Filtro para usuários ativos
                string filtro = "(&(objectCategory=person)(objectClass=user)(!userAccountControl:1.2.840.113556.1.4.803:=2))";
                DirectorySearcher dSearch = new DirectorySearcher(DirectoryEntry, filtro, propriedades, SearchScope.Subtree);
                var listaUsuario = new List<UsuarioADModel>();
                var itemListaUsuario = new UsuarioADModel();

                foreach (SearchResult sResultSet in dSearch.FindAll())
                {
                    itemListaUsuario = new UsuarioADModel();
                    itemListaUsuario.Login = GetProperty(sResultSet, propriedades[0]).ToLower();
                    itemListaUsuario.Nome = GetProperty(sResultSet, propriedades[1]).ToUpper();
                    itemListaUsuario.Email = GetProperty(sResultSet, propriedades[2]).ToLower();
                    listaUsuario.Add(itemListaUsuario);
                }

                return listaUsuario.OrderBy(c => c.Nome).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchResult"></param>
        /// <param name="PropertyName"></param>
        /// <returns></returns>
        public static string GetProperty(SearchResult searchResult, string PropertyName)
        {
            if (searchResult.Properties.Contains(PropertyName))
            {
                return searchResult.Properties[PropertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

    }
}

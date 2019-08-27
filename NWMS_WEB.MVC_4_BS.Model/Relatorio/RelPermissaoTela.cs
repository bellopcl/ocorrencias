
using System;

namespace NUTRIPLAN_WEB.MVC_4_BS.Model
{
    public class RelPermissaoTela
    {
        public string CodUsuario { get; set; }
        public string Usuario { get; set; }
        public string Menu { get; set; }
        public string Inserir { get; set; }
        public string Alterar { get; set; }
        public string Excluir { get; set; }
        public string UsuarioImpressao { get; set; }
        public DateTime Emissao { get; set; }
    }
}

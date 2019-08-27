using NUTRIPLAN_WEB.MVC_4_BS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Business;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class ListaN0204PPUPesquisa
    {
        public string nomeUsuario { get; set; }
        public string loginUsuario { get; set; }
        public long prazoDev { get; set; }
        public long prazoTroca { get; set; }

        public static explicit operator ListaN0204PPUPesquisa(N0204PPU N0204PPU)
        {
            var item = new ListaN0204PPUPesquisa();

            if (N0204PPU.CODUSU.HasValue)
            {
                var ActiveDirectoryBusiness = new ActiveDirectoryBusiness();
                var N9999USUBusiness = new N9999USUBusiness();

                // Busca código do usuário
                var dadosUsuario = N9999USUBusiness.ListaDadosUsuarioPorCodigo((int)N0204PPU.CODUSU);
                var usuario = ActiveDirectoryBusiness.ListaDadosUsuarioAD(dadosUsuario.LOGIN);

                item.loginUsuario = dadosUsuario.LOGIN;
                item.nomeUsuario = usuario.Nome;
            }

            item.prazoDev = N0204PPU.QTDDEV;
            item.prazoTroca = N0204PPU.QTDTRC;
            return item;
        }
    }
}
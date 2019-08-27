using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class ListaN0204ORIPesquisa
    {
        public long Codigo { get; set; }
        public long CodigoTipoAtend { get; set; }
        public string Descricao { get; set; }
        public string Situacao { get; set; }

        public static explicit operator ListaN0204ORIPesquisa(N0204ORI N0204ORI)
        {
            ListaN0204ORIPesquisa item = new ListaN0204ORIPesquisa();
            item.Codigo = N0204ORI.CODORI;
            item.Descricao = N0204ORI.DESCORI;
            item.Situacao = N0204ORI.SITORI;
            return item;
        }

        public static explicit operator ListaN0204ORIPesquisa(N0204AOR N0204AOR)
        {
            ListaN0204ORIPesquisa item = new ListaN0204ORIPesquisa();
            item.CodigoTipoAtend = N0204AOR.CODATD;
            item.Codigo = N0204AOR.CODORI;
            item.Descricao = N0204AOR.N0204ORI.DESCORI;
            item.Situacao = N0204AOR.SITREL;
            return item;
        }
    }
}
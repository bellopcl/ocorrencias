using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class ListaN0204ATDPesquisa
    {
        public long Codigo { get; set; }
        public string Descricao { get; set; }
        public string Situacao { get; set; }

        public static explicit operator ListaN0204ATDPesquisa(N0204ATD N0204ATD)
        {
            ListaN0204ATDPesquisa item = new ListaN0204ATDPesquisa();
            item.Codigo = N0204ATD.CODATD;
            item.Descricao = N0204ATD.DESCATD;
            item.Situacao = N0204ATD.SITATD;
            return item;
        }
    }
}
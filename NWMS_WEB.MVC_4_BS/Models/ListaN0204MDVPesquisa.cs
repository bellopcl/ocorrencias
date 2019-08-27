using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class ListaN0204MDVPesquisa
    {
        public long Codigo { get; set; }
        public string Descricao { get; set; }
        public string Situacao { get; set; }

        public static explicit operator ListaN0204MDVPesquisa(N0204MDV N0204MDV)
        {
            ListaN0204MDVPesquisa item = new ListaN0204MDVPesquisa();
            item.Codigo = N0204MDV.CODMDV;
            item.Descricao = N0204MDV.DESCMDV;
            item.Situacao = N0204MDV.SITMDV;
            return item;
        }
    }
}
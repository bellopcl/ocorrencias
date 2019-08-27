using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class ListaN0204MDOPesquisa
    {
        public long CodigoMotivo { get; set; }
        public long CodigoOrigem { get; set; }
        public string DescricaoOrigem { get; set; }
        public string Situacao { get; set; }

        public static explicit operator ListaN0204MDOPesquisa(N0204MDO N0204MDO)
        {
            ListaN0204MDOPesquisa item = new ListaN0204MDOPesquisa();
            item.CodigoMotivo = N0204MDO.CODMDV;
            item.CodigoOrigem = N0204MDO.CODORI;
            item.DescricaoOrigem = N0204MDO.N0204ORI.DESCORI;
            item.Situacao = N0204MDO.SITREL;
            return item;
        }
    }
}
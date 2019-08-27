using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class ListaN0203ANXPesquisa
    {
        public decimal IdLinha { get; set; }
        public long NumRegistro { get; set; }
        public byte[] Anexo { get; set; }
        public string NomeAnexo { get; set; }
        public string TipoAnexo { get; set; }

        public static explicit operator ListaN0203ANXPesquisa(N0203ANX N0203ANX)
        {
            ListaN0203ANXPesquisa item = new ListaN0203ANXPesquisa();
            item.IdLinha = N0203ANX.IDROW;
            item.NumRegistro = N0203ANX.NUMREG;
            //item.Anexo = N0203ANX.ANEXO;
            item.NomeAnexo = N0203ANX.NOMANX;
            item.TipoAnexo = N0203ANX.EXTANX;
            return item;
        }
    }
}
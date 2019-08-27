using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Business;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class ListaN0203TRAPesquisa
    {
        public long NUMREG { get; set; }
        public long SEQTRA { get; set; }
        public string DESTRA { get; set; }
        public long USUTRA { get; set; }
        public string NOMEUSUTRA { get; set; }
        public string DATTRA { get; set; }
        public string OBSTRA { get; set; }
        public long? CODORI { get; set; }

        public static explicit operator ListaN0203TRAPesquisa(N0203TRA N0203TRA)
        {

            var N9999USUBusiness = new N9999USUBusiness();
            var ActiveDirectoryBusiness = new ActiveDirectoryBusiness();
            var loginUsuario = string.Empty;
            var item = new ListaN0203TRAPesquisa();
            item.NUMREG = N0203TRA.NUMREG;
            item.SEQTRA = N0203TRA.SEQTRA;
            item.DESTRA = N0203TRA.DESTRA;
            item.USUTRA = N0203TRA.USUTRA;
            loginUsuario = N9999USUBusiness.ListaDadosUsuarioPorCodigo(item.USUTRA).LOGIN;
            item.NOMEUSUTRA = ActiveDirectoryBusiness.ListaDadosUsuarioAD(loginUsuario).Nome;
            item.DATTRA = N0203TRA.DATTRA.ToString();
            item.OBSTRA = N0203TRA.OBSTRA;
            item.CODORI = N0203TRA.CODORI;
            return item;
        }
    }
}
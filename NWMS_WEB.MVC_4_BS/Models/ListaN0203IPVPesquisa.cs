using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using NUTRIPLAN_WEB.MVC_4_BS.Business;

namespace NWORKFLOW_WEB.MVC_4_BS.Models
{
    public class ListaN0203IPVPesquisa
    {
        public long codRegistro { get; set; }
        public long Empresa { get; set; }
        public long Filial { get; set; }
        public string SerieNota { get; set; }
        public long NumeroNota { get; set; }
        public long SeqNota { get; set; }
        public string CodPro { get; set; }
        public string CodDer { get; set; }
        public string DescPro { get; set; }
        public string CodDepartamento { get; set; }
        public long QtdeFat { get; set; }
        public float PrecoUnitario { get; set; }
        public string PrecoUnitarioS { get; set; }
        public decimal ValorLiquido { get; set; }
        public string ValorLiquidoS { get; set; }
        public long CodOrigemOcorrencia { get; set; }
        public string DescOrigemOcorrencia { get; set; }
        public long CodMotivoDevolucao { get; set; }
        public string DescMotivoDevolucao { get; set; }
        public long QtdeDevolucao { get; set; }
        public string TipoTransacao { get; set; }
        public long UsuarioUltimaAlteracao { get; set; }
        public string NomeUsuarioUltimaAlteracao { get; set; }
        public string DataUltimaAlteracao { get; set; }
        public decimal PercDesconto { get; set; }
        public decimal PercIpi { get; set; }
        public decimal ValorIpi { get; set; }
        public string ValorIpiS { get; set; }
        public decimal ValorSt { get; set; }
        public string ValorStS { get; set; }
        public string NotasAdicionadas { get; set; }

        public static explicit operator ListaN0203IPVPesquisa(N0203IPV N0203IPV)
        {
            var N0204ORIBusiness = new N0204ORIBusiness();
            var N0204MDVBusiness = new N0204MDVBusiness();
            var item = new ListaN0203IPVPesquisa();
            var N9999USUBusiness = new N9999USUBusiness();
            var ActiveDirectoryBusiness = new ActiveDirectoryBusiness();
            var loginUsuario = string.Empty;

            item.codRegistro = N0203IPV.NUMREG;
            item.Empresa = N0203IPV.CODEMP;
            item.Filial = N0203IPV.CODFIL;
            item.SerieNota = N0203IPV.CODSNF;
            item.NumeroNota = N0203IPV.NUMNFV;
            item.SeqNota = N0203IPV.SEQIPV;
            item.CodPro = N0203IPV.CODPRO;
            item.CodDer = N0203IPV.CODDER;
            item.DescPro = N0203IPV.CPLIPV;
            item.CodDepartamento = N0203IPV.CODDEP;
            item.QtdeFat = N0203IPV.QTDFAT;
            item.PrecoUnitario = N0203IPV.PREUNI;
            item.PrecoUnitarioS = N0203IPV.PREUNI.ToString("###,###,##0.00");
            item.ValorLiquido = N0203IPV.VLRLIQ;
            item.ValorLiquidoS = N0203IPV.VLRLIQ.ToString("###,###,##0.00");
            item.CodOrigemOcorrencia = N0203IPV.ORIOCO;
            item.DescOrigemOcorrencia = N0204ORIBusiness.PesquisaOrigemOcorrencia().Where(c => c.CODORI == N0203IPV.ORIOCO).FirstOrDefault().DESCORI;
            item.CodMotivoDevolucao = N0203IPV.CODMOT;
            item.DescMotivoDevolucao = N0204MDVBusiness.PesquisaMotivoDevolucao().Where(c => c.CODMDV == N0203IPV.CODMOT).FirstOrDefault().DESCMDV;
            item.QtdeDevolucao = N0203IPV.QTDDEV;
            item.TipoTransacao = N0203IPV.TNSPRO;
            item.UsuarioUltimaAlteracao = N0203IPV.USUULT;

            loginUsuario = N9999USUBusiness.ListaDadosUsuarioPorCodigo(N0203IPV.USUULT).LOGIN;
            item.NomeUsuarioUltimaAlteracao = ActiveDirectoryBusiness.ListaDadosUsuarioAD(loginUsuario).Nome;

            item.DataUltimaAlteracao = N0203IPV.DATULT.ToString();
            item.PercDesconto = N0203IPV.PEROFE;
            item.PercIpi = N0203IPV.PERIPI;
            item.ValorIpi = N0203IPV.VLRIPI;
            item.ValorIpiS = N0203IPV.VLRIPI.ToString("###,###,##0.00");
            item.ValorSt = N0203IPV.VLRST;
            item.ValorStS = N0203IPV.VLRST.ToString("###,###,##0.00");
            return item;
        }
    }
}
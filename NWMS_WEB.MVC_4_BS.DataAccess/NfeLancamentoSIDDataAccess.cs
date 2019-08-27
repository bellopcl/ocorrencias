//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NUTRIPLAN_WEB.MVC_4_BS.DataAccess.WS_NFE_DEV_SID;
//using NUTRIPLAN_WEB.MVC_4_BS.Model;

//namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
//{
//    public class NfeLancamentoSIDDataAccess
//    {
//        private sapiens_Synccom_senior_g5_co_ger_sidClient NfeClient { get; set; }

//        public bool EmitirLancamentoNfe()
//        {
//            try
//            {
//                using (this.NfeClient = new sapiens_Synccom_senior_g5_co_ger_sidClient())
//                {
//                    sidExecutarIn dadosNfe = new sidExecutarIn();
//                    dadosNfe.SID = new sidExecutarInSID[2];
//                    dadosNfe.SID[0] = new sidExecutarInSID();
//                    dadosNfe.SID[1] = new sidExecutarInSID();

//                    StringBuilder strxml = new StringBuilder();
//                    dadosNfe.SID[0].param = "ACAO=SID.SRV.XML";

//                    strxml.Append("<sidxml>");
//                    strxml.Append("<param retorno='XML' />");
//                    strxml.Append("<sid acao='SID.Srv.AltEmpFil' retorno='RET_ALTEMP'>");
//                    strxml.Append("<param nome='CodEmp' valor='1' />");
//                    strxml.Append("<param nome='CodFil' valor='1' />");
//                    strxml.Append("</sid>");

//                    strxml.Append("<sid acao='SID.Nfc.Gravar' retorno='RET_NUMNFC'>");
//                    strxml.Append("<param nome='CodFor' valor='3525' />");
//                    strxml.Append("<param nome='CodSnf' valor='NFE' />");
//                    strxml.Append("<param nome='TipNfe' valor='3' />");
//                    strxml.Append("<param nome='CodEdc' valor='1' />");
//                    strxml.Append("<param nome='DatEnt' valor='22/01/2014' />");
//                    strxml.Append("<param nome='DatEmi' valor='22/01/2014' />");
//                    strxml.Append("<param nome='TnsPro' valor='2201A' />");
//                    // Qtde Dev x Preço Unitário ==> Todos itens
//                    strxml.Append("<param nome='VlrInf ' valor='3.74' />");
//                    strxml.Append("</sid>");

//                    strxml.Append("<sid acao='SID.Nfc.GravarProduto' retorno='RET_SEQIPC'>");
//                    strxml.Append("<param nome='CodFor' valor='3525' />");
//                    strxml.Append("<param nome='NumNfc' valor='@RET_NUMNFC' />");
//                    strxml.Append("<param nome='CodSnf' valor='NFE' />");
//                    strxml.Append("<param nome='TnsPro' valor='2201A' />");
//                    strxml.Append("<param nome='CodPro' valor='8000101' />");
//                    strxml.Append("<param nome='CodDer' valor='U' />");
//                    strxml.Append("<param nome='UniMed' valor='UN' />");
//                    // Qtde Dev
//                    strxml.Append("<param nome='QtdRec' valor='1' />");
//                    strxml.Append("<param nome='PreUni' valor='3.74' />");
//                    strxml.Append("<param nome='EmpNfv' valor='1' />");
//                    strxml.Append("<param nome='FilNfv' valor='1' />");
//                    strxml.Append("<param nome='SnfNfv' valor='NFE' />");
//                    strxml.Append("<param nome='NumNfv' valor='169750' />");
//                    strxml.Append("<param nome='SeqIpv' valor='1' />");
//                    strxml.Append("</sid>");
//                    strxml.Append("</sidxml>");

//                    dadosNfe.SID[1].param = strxml.ToString();

//                    var retorno = NfeClient.Executar("nworkflow.web", "!nfr@t1n", 0, dadosNfe);

//                    return true;
//                }

//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//    }
//}

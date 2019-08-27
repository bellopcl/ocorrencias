using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System;
using System.Collections.Generic;
using System.Data.OracleClient;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    public class OCORREABILITADODataAccess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();

        public List<RelatorioOcorrenciasReabilitadas> pesquisarOcorrenciasHabilitadas(long? Numreg, string dateInicial, string dateFinal, string operacao)
        {
            try
            {
                string sql = @"SELECT
                        TRA.NUMREG AS OCORRENCIAS,
                        REG.CODCLI AS COD_CLIENTE,
                        CLI.NOMCLI AS CLIENTE,
                        to_char(TRA.DATTRA, 'dd/mm/yyyy hh:mi:ss pm', 'nls_date_language=''english') AS DATA_TRANSACAO,
                        TRA.OBSTRA AS MOTIVO
                    FROM
                        N0203TRA TRA,
                        N0203REG REG,
                        SAPIENS.E085CLI CLI
                    WHERE
                        TRA.DESTRA LIKE " + "'%" + operacao + "%'";
                sql += " AND TRA.NUMREG = REG.NUMREG";
                if(dateInicial != "" && dateFinal != "") { 
                    sql += " AND TO_CHAR(TRA.DATTRA, 'DD/MM/YYYY') between  TO_DATE(" + "'" + dateInicial + "'" + ",'YYYY/MM/DD') AND TO_DATE(" + "'"+ dateFinal + "'" + ",'YYYY/MM/DD')";
                }
                if(Numreg != null && Numreg != 0) {
                    sql += " AND REG.NUMREG = " + Numreg + "";
                }
                sql += " AND REG.CODCLI = CLI.CODCLI";
                sql += " ORDER BY TRA.DATTRA";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = System.Data.CommandType.Text;
                conn.Open();
                List<RelatorioOcorrenciasReabilitadas> itens = new List<RelatorioOcorrenciasReabilitadas>();
                RelatorioOcorrenciasReabilitadas REL = new RelatorioOcorrenciasReabilitadas();
                
                OracleDataReader dr = cmd.ExecuteReader();
                

                while (dr.Read())
                {
                    REL = new RelatorioOcorrenciasReabilitadas();
                    REL.Numreg = Convert.ToInt32(dr["OCORRENCIAS"]);
                    REL.Cliente = dr["COD_CLIENTE"].ToString() + "-" +  dr["CLIENTE"].ToString();
                    REL.DataTransacao = dr["DATA_TRANSACAO"].ToString();
                    REL.Observacao = dr["MOTIVO"].ToString();
                    REL.operacao = operacao;
                    itens.Add(REL);
                }

                dr.Close();
                conn.Close();
                return itens;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

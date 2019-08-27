using System;
using System.Collections.Generic;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using System.Globalization;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N0203TRADataAccess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();
        /// <summary>
        /// Pesquisa os tramites das ocorrências por codigo de ocorrência
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        /// <returns></returns>
        public List<N0203TRA> PesquisaTramites(long codigoRegistro)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    return contexto.N0203TRA.Where(c => c.NUMREG == codigoRegistro).OrderBy(c => c.SEQTRA).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Relatório de transações 
        /// </summary>
        /// <param name="NumReg"></param>
        /// <returns></returns>
        public List<RelatorioTransacoes> RelatorioTransacao(long NumReg)
        {
            try
            {
                string sql = @"SELECT
                            NUMREG,
                            SEQTRA,
                            USU.LOGIN AS LOGIN,
                            DESTRA,
                            USUTRA, 
                            to_char(DATTRA, 'dd/mm/yyyy hh:mi:ss pm', 'nls_date_language=''english') AS DATA_TRANSACAO,
                            OBSTRA
                        FROM
                            N0203TRA TRA, N9999USU USU
                        WHERE 
                            TRA.USUTRA = USU.CODUSU
                        AND NUMREG = " + NumReg;
                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                List<RelatorioTransacoes> itens = new List<RelatorioTransacoes>();
                RelatorioTransacoes rel = new RelatorioTransacoes();
                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    rel = new RelatorioTransacoes();
                    rel.NUMREG = Convert.ToInt32(dr["NUMREG"]);
                    rel.DESTRA = dr["DESTRA"].ToString();
                    rel.USUTRA = Convert.ToInt32(dr["USUTRA"]);
                    rel.DATTRA = dr["DATA_TRANSACAO"].ToString();
                    rel.OBSTRA = dr["OBSTRA"].ToString();
                    rel.USULOGIN = dr["LOGIN"].ToString();
                    itens.Add(rel);
                }

                return itens;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa os tramites das ocorrências por código de usuário
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <returns>N0203TRA</returns>
        public List<N0203TRA> PesquisaTramiteUsuario(long codigoUsuario)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    return contexto.N0203TRA.Where(c => c.N0203REG.USUGER == codigoUsuario && c.USUTRA != codigoUsuario).OrderBy(c => c.SEQTRA).Take(8).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

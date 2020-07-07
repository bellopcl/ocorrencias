using System;
using System.Collections.Generic;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using System.Globalization;
using NWORKFLOW_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N0203REGDataAccess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();

        /// <summary>
        /// Exclui os itens que foram adicinados para devolução no cadastro do protocolo e na edição (Rascunho) foram excluídos da devolução (Fase 3 - Pesquisa)
        /// Agrupar notas para gravar data de emissão
        /// </summary>
        /// <param name="N0203REG">Lista de Registro</param>
        /// <returns>true/false</returns>
        public bool GravarRegistroOcorrenciaPesquisa(N0203REG N0203REG)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    E140IPVDataAccess E140IPVDataAccess = new E140IPVDataAccess();
                    USU_T135LANDataAccess USU_T135LANDataAccess = new USU_T135LANDataAccess();
                    
                    
                    var itensDev = contexto.N0203IPV.Where(c => c.NUMREG == N0203REG.NUMREG).ToList();
                    foreach (N0203IPV item in itensDev)
                    {
                        var validaExclusao = N0203REG.N0203IPV.Where(c => c.NUMREG == item.NUMREG && c.CODEMP == item.CODEMP && c.CODFIL == item.CODFIL && c.CODSNF == item.CODSNF && c.NUMNFV == item.NUMNFV && c.SEQIPV == item.SEQIPV).FirstOrDefault();

                        if (validaExclusao == null)
                        {
                            contexto.N0203IPV.Remove(item);
                        }
                    }

                    var original = contexto.N0203REG.Where(c => c.NUMREG == N0203REG.NUMREG).FirstOrDefault();

                    if (original != null)
                    {
                        original.SITREG = N0203REG.SITREG;
                        original.OBSREG = N0203REG.OBSREG;

                        N0203TRA itemTramites = new N0203TRA();
                        itemTramites.NUMREG = N0203REG.NUMREG;
                        itemTramites.DATTRA = N0203REG.DATULT;
                        itemTramites.USUTRA = N0203REG.USUULT;
                        itemTramites.DESTRA = "REGISTRO DE OCORRENCIA SALVO - RASCUNHO";
                        itemTramites.SEQTRA = contexto.N0203TRA.Where(c => c.NUMREG == N0203REG.NUMREG).Max(p => p.SEQTRA + 1);

                        if (N0203REG.SITREG == (long)Enums.SituacaoRegistroOcorrencia.Fechado)
                        {
                            original.DATFEC = N0203REG.DATFEC;
                            original.USUFEC = N0203REG.USUFEC;
                            itemTramites.DESTRA = "REGISTRO DE OCORRENCIA FECHADO";
                        }
                        else
                        {
                            original.DATULT = N0203REG.DATULT;
                            original.USUULT = N0203REG.USUULT;
                        }

                        original.N0203TRA.Add(itemTramites);

                        var listNotas = (from a in N0203REG.N0203IPV
                                         group new { a } by new { a.CODEMP, a.CODFIL, a.NUMNFV } into grupo
                                         select new { grupo.Key.CODFIL, grupo.Key.NUMNFV }).ToList();

                        E140NFVDataAccess E140NFVDataAccess = new E140NFVDataAccess();

                        foreach (var item in listNotas)
                        {
                            // Data de emissão da nota
                            var dataEmi = E140NFVDataAccess.PesquisarDadosNota(item.NUMNFV, item.CODFIL, null, "3").FirstOrDefault().DataEmissao;
                            var datTime = DateTime.Parse(dataEmi);

                            N0203REG.N0203IPV.Where(c => c.CODFIL == item.CODFIL && c.NUMNFV == item.NUMNFV).ToList().ForEach(c => c.DATEMI = datTime);
                        }

                        bool valida = false;
                        foreach (N0203ANX item in N0203REG.N0203ANX)
                        {
                            item.NUMREG = N0203REG.NUMREG;
                            if (!valida && contexto.N0203ANX.Where(c => c.NUMREG == item.NUMREG).Count() == 0)
                            {
                                item.IDROW = 1;
                                valida = true;
                            }
                            else if (N0203REG.N0203ANX.Where(c => c.IDROW != 0).Count() > 0)
                            {
                                item.IDROW = N0203REG.N0203ANX.Last(c => c.IDROW != 0).IDROW + 1;
                            }
                            else
                            {
                                item.IDROW = contexto.N0203ANX.Where(c => c.NUMREG == item.NUMREG).Max(c => c.IDROW + 1);
                            }

                            original.N0203ANX.Add(item);
                        }

                        foreach (N0203IPV itemNovo in N0203REG.N0203IPV)
                        {
                            itemNovo.NUMREG = N0203REG.NUMREG;
                            var originalItens = original.N0203IPV.Where(c => c.NUMREG == itemNovo.NUMREG && c.CODEMP == itemNovo.CODEMP && c.CODFIL == itemNovo.CODFIL && c.CODSNF == itemNovo.CODSNF && c.NUMNFV == itemNovo.NUMNFV && c.SEQIPV == itemNovo.SEQIPV).FirstOrDefault();

                            if (originalItens == null)
                            {
                                long analiseEmbaque;
                                E140IPVDataAccess.PesquisaAnaliseEmbarquePorNota(itemNovo.NUMNFV, itemNovo.CODFIL, out analiseEmbaque);
                                itemNovo.NUMANE = analiseEmbaque;

                                if (itemNovo.CODFIL == 101)
                                {
                                    long analiseEmbaqueFilial;
                                    USU_T135LANDataAccess.PesquisaRelacionamentoAneEmbarqueEntreFilial(analiseEmbaque, out analiseEmbaqueFilial);
                                    itemNovo.NUMANE_REL = analiseEmbaqueFilial;
                                }

                                original.N0203IPV.Add(itemNovo);
                            }
                            else
                            {
                                var itemOri = original.N0203IPV.Where(c => c.NUMREG == itemNovo.NUMREG && c.CODEMP == itemNovo.CODEMP && c.CODFIL == itemNovo.CODFIL && c.CODSNF == itemNovo.CODSNF && c.NUMNFV == itemNovo.NUMNFV && c.SEQIPV == itemNovo.SEQIPV).FirstOrDefault();

                                if (itemOri != null)
                                {
                                    if (itemOri.CODMOT != itemNovo.CODMOT || itemOri.ORIOCO != itemNovo.ORIOCO || itemOri.QTDDEV != itemNovo.QTDDEV)
                                    {
                                        itemOri.CODMOT = itemNovo.CODMOT;
                                        itemOri.ORIOCO = itemNovo.ORIOCO;
                                        itemOri.QTDDEV = itemNovo.QTDDEV;
                                        itemOri.DATULT = N0203REG.DATULT;
                                        itemOri.USUULT = N0203REG.USUULT;
                                    }
                                }
                            }
                        }

                        contexto.SaveChanges();
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Pesquisa por itens de agrupamento
        /// </summary>
        /// <param name="codigoCliente">Código do Cliente</param>
        /// <param name="filtro">Filtro</param>
        /// <returns>itens</returns>
        public List<Agrupamento> pesquisarAgrupamento(long codigoCliente, int filtro)
        {
            try
            {
                string sql = @"SELECT DISTINCT AGR1.AGRREG AS AGRREG, COUNT(DISTINCT AGR1.NUMREG) QUANTIDADE, TO_CHAR(ROUND(SUM((VLRLIQ / QTDFAT) * QTDDEV), 2), 'FM999G999G999D90','nls_numeric_characters='',.''') VALORTOTAL, CASE WHEN AGR1.STAGR = 'I' THEN 'INTEGRADO'  ELSE 'NÃO INTEGRADO' END STATUS " +
                              "    FROM NWMS_PRODUCAO.N0203REG REG" +
                              "   INNER JOIN NWMS_PRODUCAO.N0203IPV IPV" +
                              "      ON REG.NUMREG = IPV.NUMREG" +
                              "   INNER JOIN SAPIENS.E085CLI CLI" +
                              "      ON CLI.CODCLI = REG.CODCLI" +
                              "   INNER JOIN NWMS_PRODUCAO.N0203AGR AGR1" +
                              "      ON REG.NUMREG = AGR1.NUMREG" +
                              "  WHERE CLI.CODCLI = " + codigoCliente + "";
                sql += filtro == 1 ? "GROUP BY AGR1.AGRREG, AGR1.STAGR" : "AND AGR1.STAGR = 'N' GROUP BY AGR1.AGRREG, AGR1.STAGR";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                Agrupamento AGP = new Agrupamento();
                List<Agrupamento> itens = new List<Agrupamento>();
                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    AGP = new Agrupamento();
                    AGP.AGRREG = Convert.ToInt32(dr["AGRREG"]);
                    AGP.QTDAGRREG = Convert.ToInt32(dr["QUANTIDADE"]);
                    AGP.VLRLIQ = dr["VALORTOTAL"].ToString();
                    AGP.STATUS = dr["STATUS"].ToString();
                    itens.Add(AGP);
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
        /// <summary>
        /// Exclui agrupamento
        /// </summary>
        /// <param name="excluirAgrupamentoSelecionado">Exclui Agrupamento Selecionado</param>
        /// <returns></returns>
        public bool excluirAgrupamento(string excluirAgrupamentoSelecionado)
        {
            try
            {
                var statusAgrupamento = consultaArrayString("NWMS_PRODUCAO.N0203AGR", "STAGR", "AGRREG IN (" + excluirAgrupamentoSelecionado + ")");
                foreach (var item in statusAgrupamento)
                {
                    if (item == "I")
                    {
                        return false;
                    }
                }
                string sql = "DELETE FROM NWMS_PRODUCAO.N0203AGR AGR WHERE AGR.STAGR = 'N' AND AGR.AGRREG IN( " + excluirAgrupamentoSelecionado + ")";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                Agrupamento AGP = new Agrupamento();
                OracleDataReader dr = cmd.ExecuteReader();

                dr.Close();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Busca coluna no banco de dados
        /// </summary>
        /// <param name="tabela">Nome da Tabela</param>
        /// <param name="coluna">Nome da Coluna</param>
        /// <param name="where">Seleção</param>
        /// <returns>COLUNA</returns>
        public string consultaString(String tabela, String coluna, String where)
        {
            String sql = "SELECT " + coluna + " AS COLUNA FROM " + tabela + " WHERE " + where + "";

            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return dr["COLUNA"].ToString();
            }
            return "VAZIO";
        }

        public bool verificaAprovador(long CodOri, long CodAtendimento)
        {
            string sql = "SELECT COUNT(CODORI) AS QUANTIDADE FROM N0203UAP WHERE CODORI = " + CodOri + " AND CODATD = " + CodAtendimento;

            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                if (Convert.ToInt32(dr["QUANTIDADE"]) > 0){
                    return  true;
                }
            }
            return false;
        }
        /// <summary>
        /// Retorna uma lista de colunas do banco de dados
        /// </summary>
        /// <param name="tabela">Nome da Tabela</param>
        /// <param name="coluna">Nome da Coluna</param>
        /// <param name="where">Comendo de Seleção</param>
        /// <returns>itens</returns>
        public List<String> consultaArrayString(String tabela, String coluna, String where)
        {
            String sql = "SELECT " + coluna + " AS COLUNA FROM " + tabela + " WHERE " + where + "";

            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();
            List<string> itens = new List<string>();
            if (dr.Read())
            {
                itens.Add(dr["COLUNA"].ToString());
            }
            return itens;
        }

        public bool InserirTransporteIndenizado(long NumReg, int CodTra)
        {
            string sql = "UPDATE N0203REG SET TRACLI = " + CodTra + " WHERE NUMREG = " + NumReg;

            //string sql = "select USUGER from N0203REG WHERE NUMREG = " + NumReg;

            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();
            
            //if (dr.Read()) { 
            //    email.Email("inserir ocorrencia ", dr["USUGER"].ToString());
            //}
            conn.Close();

            return true;
        }

        public listaTransportadora ConsultaTransportadora(int NotaOcorrencia, string Tipo)
        {
            String sql = "";
            if (Tipo == "O") { 
                sql = "SELECT DISTINCT(NFV.CODTRA) AS CODTRA, TRA.NOMTRA AS NOMTRA, TRA.APETRA AS APETRA, NFV.CODRED AS CODRED FROM  " +
                    "SAPIENS.E140NFV NFV, N0203IPV IPV, SAPIENs.E073TRA TRA" +
                    " WHERE NFV.CODEMP = IPV.CODEMP AND NFV.CODFIL = IPV.CODFIL AND " +
                    "NFV.CODSNF = IPV.CODSNF AND TRA.CODTRA = NFV.CODTRA AND" +
                    " NFV.NUMNFV = IPV.NUMNFV AND IPV.NUMREG = " + NotaOcorrencia;
            }
            else
            {
                sql = "SELECT DISTINCT(NFV.CODTRA) AS CODTRA, " +
                      "TRA.NOMTRA AS NOMTRA, " +
                      "TRA.APETRA AS APETRA, " +
                      "NFV.CODRED AS CODRED " +
                      "FROM SAPIENS.E140NFV NFV, SAPIENS.E073TRA TRA " +
                      "WHERE TRA.CODTRA = NFV.CODTRA AND NFV.NUMNFV = " + NotaOcorrencia;
            }

            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();
            listaTransportadora lista = new listaTransportadora();
            int codRed = 0;

            if (dr.Read())
            {
                lista.CODTRA = dr["CODTRA"].ToString();
                lista.NOMTRA = dr["NOMTRA"].ToString();
                lista.APETRA = dr["APETRA"].ToString();

                lista.CODRED = dr["CODRED"].ToString();
                codRed = Convert.ToInt32(dr["CODRED"]);
            }

            conn.Close();

            if (codRed > 0)
            {
                sql = "SELECT NOMTRA AS NOMETRAREDES, APETRA AS CODREDAPETRA FROM SAPIENS.E073TRA WHERE CODTRA = " + lista.CODRED;
                OracleConnection con2 = new OracleConnection(OracleStringConnection);
                OracleCommand cmd2 = new OracleCommand(sql, con2);
                cmd2.CommandType = CommandType.Text;
                con2.Open();
                
                OracleDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    lista.NOMETRAREDES = dr2["NOMETRAREDES"].ToString();
                    lista.CODREDAPETRA = dr2["CODREDAPETRA"].ToString();
                }
                con2.Close(); 
            }
            

            return lista;
        }

        public string OrigemOcorrencia(long NumReg)
        {
            string origem = "";
            string sql = "SELECT ORIOCO FROM N0203REG WHERE NUMREG = " + NumReg;
            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                origem = dr["ORIOCO"].ToString();
            }
            conn.Close();
            return origem;

        }

        public int pegaTransportadoraOcorrencia(long NumReg)
        {
            string sql = "SELECT TRACLI FROM N0203REG WHERE NUMREG = " + NumReg;

            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();
            int TraCli = 0;
            if (dr.Read())
            {
                TraCli = Convert.ToInt32(dr["TRACLI"]);
            }
            return TraCli;
        }

        public string GravarTransacaoIndenizado(long NumReg, long Usuario)
        {
            string sql = "SELECT MAX(SEQTRA) AS SEQTRA FROM N0203TRA WHERE NUMREG = " + NumReg;

            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();
            long sequencia = 0;
            if (dr.Read())
            {
                sequencia = Convert.ToInt32(dr["SEQTRA"]);
            }

            conn.Close();

            sequencia = sequencia + 1;

            DateTime DataAtual = DateTime.Now;

            sql = "INSERT INTO N0203TRA VALUES(" + NumReg + "," + sequencia + ", 'REGISTO DE OCORRENCIA INDENIZADO', " + Usuario + ",'" + DataAtual + "', '', '')";

            OracleConnection con2 = new OracleConnection(OracleStringConnection);
            OracleCommand cmd2 = new OracleCommand(sql, con2);
            cmd2.CommandType = CommandType.Text;
            con2.Open();
            OracleDataReader dr2 = cmd2.ExecuteReader();
            con2.Close();


            return "";
        }

        /// <summary>
        /// Grava os agrupamentos
        /// </summary>
        /// <param name="ocorrencias">Tipo de Ocorrência</param>
        /// <param name="dataGeracao">Data de Geração</param>
        /// <param name="usuarioGeracao">Usuário que Gerou</param>
        /// <returns></returns>
        public string GravarAgrupamento(string ocorrencias, string dataGeracao, string usuarioGeracao)
        {
            var retorno = "";
            var res = ocorrencias.Split(',');
            var codigoAgrupador = consultaString("NWMS_PRODUCAO.N0203AGR", "MAX(AGRREG) + 1", "1=1");

            for (int i = 0; i < res.Count(); i++)
            {
                try
                {
                    string sql = "INSERT INTO NWMS_PRODUCAO.N0203AGR VALUES (" + codigoAgrupador + "," + res[i] + ",TO_DATE('" + DateTime.Now.ToString("dd/MM/yyyy") + "'), " + usuarioGeracao + ", 'N')";
                    sql = sql.Replace("00:00:00", "");
                    OracleConnection conn = new OracleConnection(OracleStringConnection);
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    OracleDataReader dr = cmd.ExecuteReader();

                    if (dr.Read()) { }

                    dr.Close();
                    conn.Close();
                    retorno = "ok";
                }
                catch (Exception ex)
                {
                    retorno = "Ocorreu um errro ao agrupar as ocorrências";
                }
            }
            return "Ocorrências agrupadas com Sucesso, o Código Agrupador é: " + codigoAgrupador;
        }
        /// <summary>
        /// Pesquisa ocorrências agrupadas
        /// </summary>
        /// <returns>Lista de Ocorrências</returns>
        public List<Agrupamento> pesquisarOcorrenciaAGP()
        {

            try
            {

                string atr = @"   SELECT DISTINCT
                                        REG.NUMREG,
                                        IPV.NUMNFV,
                                        SUM(IPV.VLRLIQ),
                                        SUM(IPV.PERIPI),
                                        SUM(IPV.VLRST)
                                    FROM
                                        NWMS_PRODUCAO.N0203REG REG
                                    INNER JOIN
                                        NWMS_PRODUCAO.N0203IPV IPV
                                    ON
                                        REG.NUMREG = IPV.NUMREG
                                    WHERE
                                        IPV.NUMNFV = 254777
                                    AND NOT EXISTS
                                        (
                                            SELECT
                                                AGR.NUMREG
                                            FROM
                                                NWMS_PRODUCAO.N0203AGR AGR
                                            WHERE
                                                AGR.NUMREG = REG.NUMREG)
                                    GROUP BY
                                        REG.NUMREG,
                                        IPV.NUMNFV";
                string sql = @"  SELECT DISTINCT
                                        REG.NUMREG,
                                        IPV.NUMNFV,
                                        IPV.VLRLIQ,
                                        IPV.PERIPI,
                                        IPV.VLRST
                                    FROM
                                        NWMS_PRODUCAO.N0203REG REG
                                    INNER JOIN
                                        NWMS_PRODUCAO.N0203IPV IPV
                                    ON
                                        REG.NUMREG = IPV.NUMREG";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                Agrupamento AGP = new Agrupamento();
                List<Agrupamento> itens = new List<Agrupamento>();
                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
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
        /// <summary>
        /// Pesquisa Ocorrência de Agrupamento
        /// </summary>
        /// <param name="numreg">Código de Ocorrência</param>
        /// <returns>itens</returns>
        public List<Agrupamento> pesquisarPorOcorrenciaAGP(long numreg)
        {
            try
            {
                string sql = @"SELECT DISTINCT  REG.NUMREG,
                                                (SELECT WM_CONCAT(WIPV.NUMNFV)
                                                FROM NWMS_PRODUCAO.N0203IPV WIPV
                                                WHERE WIPV.NUMREG = REG.NUMREG) NUMNFV,
                                                ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV),
                                                      2) VLRLIQ,
                                                ROUND(SUM(IPV.VLRIPI),
                                                      2) PERIPI,
                                                ROUND(SUM(IPV.VLRST),
                                                      0) VLRST,
                                                ROUND(SUM(IPV.QTDDEV * IPV.PREUNI),
                                                      2) VLRBRT,
                                                COALESCE(ROUND(SUM(IPV.VLRFRE),2),
                                                      0) VLRFRE
                                  FROM NWMS_PRODUCAO.N0203REG REG
                                 INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                                    ON REG.NUMREG = IPV.NUMREG
                                 INNER JOIN SAPIENS.E085CLI CLI
                                    ON CLI.CODCLI = REG.CODCLI
                                 WHERE 1 = 1
                                       AND REG.NUMREG NOT IN (SELECT AGR1.NUMREG
                                          FROM NWMS_PRODUCAO.N0203AGR AGR1
                                         INNER JOIN NWMS_PRODUCAO.N0203REG REG1
                                            ON REG1.NUMREG = AGR1.NUMREG)
                                 AND ";
                sql += "CLI.CODCLI = " + numreg + "  AND REG.SITREG = 9";
                sql += " GROUP BY REG.NUMREG ORDER BY NUMREG";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                Agrupamento AGP = new Agrupamento();
                List<Agrupamento> itens = new List<Agrupamento>();
                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    AGP = new Agrupamento();
                    AGP.NUMREG = Convert.ToInt32(dr["NUMREG"]);
                    AGP.NUMNFV = dr["NUMNFV"].ToString();
                    AGP.VLRLIQ = dr["VLRLIQ"].ToString() != "0" ? string.Format(CultureInfo.GetCultureInfo("pt-BR"), "R$ {0:#,###.##}", dr["VLRLIQ"]) : "R$ 0,00";
                    AGP.PERIPI = dr["PERIPI"].ToString() != "0" ? string.Format(CultureInfo.GetCultureInfo("pt-BR"), "R$ {0:#,###.##}", dr["PERIPI"]) : "R$ 0,00";
                    AGP.VLRST = dr["VLRST"].ToString() != "0" ? string.Format(CultureInfo.GetCultureInfo("pt-BR"), "R$ {0:#,###.##}", dr["VLRST"]) : "R$ 0,00";
                    AGP.VLRFRE = dr["VLRFRE"].ToString() != "0" ? string.Format(CultureInfo.GetCultureInfo("pt-BR"), "R$ {0:#,###.##}", dr["VLRFRE"]) : "R$ 0,00";
                    AGP.ICMS = "0,00";
                    AGP.VLRBRT = dr["VLRBRT"].ToString() != "0" ? string.Format(CultureInfo.GetCultureInfo("pt-BR"), "R$ {0:#,###.##}", dr["VLRBRT"]) : "R$ 0,00";
                    itens.Add(AGP);
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
        /// <summary>
        /// Pesquisa agrupamento por número de acorrência
        /// </summary>
        /// <param name="numreg">Código de Ocorrência</param>
        /// <returns>itens</returns>
        public List<int> PesquisarRegistroOcorrenciaAgrupado(string numreg)
        {

            try
            {
                string sql = @"SELECT NUMREG FROM NWMS_PRODUCAO.N0203AGR WHERE AGRREG = " + numreg + "";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                int ocorrencias = new int();
                List<int> itens = new List<int>();
                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ocorrencias = new int();
                    ocorrencias = Convert.ToInt32(dr["NUMREG"]);
                    itens.Add(ocorrencias);
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
        /// <summary>
        /// Verifica se existe placas cadastradas de coleta para esta ocorrência
        /// </summary>
        /// <param name="numeroRegistro">Código de Ocorrência</param>
        /// <param name="placa">Placa</param>
        /// <returns></returns>
        public bool verificarVinculo(string numeroRegistro, string placa)
        {
            try
            {
                string sql = "SELECT COUNT(1) AS LINHA FROM NWMS_PRODUCAO.N0204POC WHERE NUMREG = '" + numeroRegistro + "' ";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (Convert.ToInt64(dr["LINHA"]) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                dr.Close();
                conn.Close();
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Verifica se existe redespacho cadastrado para esta ocorrência
        /// </summary>
        /// <param name="numeroRegistro">Código de Ocorrência</param>
        /// <returns></returns>
        public bool verificarRedespacho(string numeroRegistro)
        {
            try
            {

                string sql = "         SELECT COUNT(1) AS LINHA " +
                      "            FROM NWMS_PRODUCAO.N0203IPV IPV" +
                      "           INNER JOIN NWMS_PRODUCAO.N0203REG REG" +
                      "              ON REG.NUMREG = IPV.NUMREG" +
                      "           WHERE EXISTS (SELECT 1" +
                      "                    FROM SAPIENS.E140NFV" +
                      "                   WHERE CODRED <> 0" +
                      "                         AND IPV.CODEMP = CODEMP" +
                      "                         AND IPV.CODFIL = CODFIL" +
                      "                         AND IPV.CODSNF = CODSNF" +
                      "                         AND IPV.NUMNFV = NUMNFV)" +
                      "                 AND REG.CODMOT = 0" +
                      "                 AND REG.NUMREG = " + numeroRegistro + "" +
                      "                 AND REG.SITREG IN (3,4,8)" +
                      "           ORDER BY REG.NUMREG DESC";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (Convert.ToInt64(dr["LINHA"]) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                dr.Close();
                conn.Close();
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Valida se esta ocorrência esta registrado para esta transportadora
        /// </summary>
        /// <param name="numeroRegistro">Código de Ocorrência</param>
        /// <returns>true/false</returns>
        public bool ValidarOcorrenciaTransportadora(string numeroRegistro)
        {
            try
            {
                string sql = "SELECT COUNT(1) AS LINHA FROM NWMS_PRODUCAO.N0203REG WHERE NUMREG = '" + numeroRegistro + "' AND CODMOT = '0' AND SITREG IN (3,4,8)";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (Convert.ToInt64(dr["LINHA"]) > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return verificarRedespacho(numeroRegistro) ? false : true;
                    }
                }
                dr.Close();
                conn.Close();
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta situação da ocorrência
        /// </summary>
        /// <param name="numeroRegistro">Código de Ocorrência</param>
        /// <returns>true/false</returns>
        public String consultaStatusRegistro(string numeroRegistro)
        {
            try
            {
                string sql = "SELECT SITREG FROM NWMS_PRODUCAO.N0203REG REG WHERE REG.NUMREG = " + numeroRegistro + "";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return dr["SITREG"].ToString();
                }
                dr.Close();
                conn.Close();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Consulta situação Anterior
        /// </summary>
        /// <param name="numeroRegistro">Código de Ocorrência</param>
        /// <returns>true/false</returns>
        public String consultaStatusAnterior(string numeroRegistro)
        {
            try
            {
                string sql = "SELECT SITREG FROM NWMS_PRODUCAO.N0204POC POC WHERE POC.NUMREG = " + numeroRegistro + "";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return dr["SITREG"].ToString();
                }
                dr.Close();
                conn.Close();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Insere a ocorrência na tabela de coleta
        /// </summary>
        /// <param name="numReg">Código de Ocorrência</param>
        /// <param name="usuGer">Usuário Gerador</param>
        /// <param name="datVin">Data Vinculado</param>
        /// <param name="codPlaca">Placa</param>
        /// <param name="observacao">Observação</param>
        /// <returns>true/false</returns>
        public Boolean Vincular(string numReg, string usuGer, string datVin, string codPlaca, string observacao)
        {
            try
            {
                String sitReg = consultaStatusRegistro(numReg);
                long sequenciaTrammite;
                if (verificarVinculo(numReg, codPlaca.ToUpper()))
                {
                    return false;
                }

                var numeroRegistro = Convert.ToInt64(numReg);

                using (Context contexto = new Context())
                {
                    var original = contexto.N0203REG.Where(c => c.NUMREG == numeroRegistro).SingleOrDefault();
                    N0203TRA itemTramites = new N0203TRA();
                    sequenciaTrammite = original.N0203TRA.OrderBy(c => c.SEQTRA).Last().SEQTRA + 1;
                }
                
                var sql = "INSERT INTO NWMS_PRODUCAO.N0204POC (NUMREG, USUGER, DATVIN, SITPOC, PLACA, OBSREG, SITREG ) VALUES ('" + numeroRegistro + "', '" + usuGer + "', '" + datVin + "', '1', '" + codPlaca.ToUpper() + "', '" + observacao + "', '" + sitReg + "')";
                
                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                dr.Close();
                conn.Close();
                alterarStatusOcorrencia(numReg);

                string sqlTrammite = "INSERT INTO NWMS_PRODUCAO.N0203TRA (NUMREG, SEQTRA, DESTRA, USUTRA, DATTRA, OBSTRA, CODORI) VALUES ('" + numeroRegistro + "', '" + sequenciaTrammite + "','OCORRENCIA CONFERIDO','" + usuGer + "', TO_DATE('" + DateTime.Now.ToString() + "', 'DD-MM-YYYY HH24:MI:SS'),'MERCADORIA CONFERIDA', '1')";
                OracleConnection connTrammite = new OracleConnection(OracleStringConnection);
                OracleCommand cmdTrammite = new OracleCommand(sqlTrammite, connTrammite);
                cmdTrammite.CommandType = CommandType.Text;
                connTrammite.Open();
                cmdTrammite.ExecuteNonQuery();
                connTrammite.Close();
                return true;

            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// Atualiza a tabela de coleta
        /// </summary>
        /// <param name="numeroRegistro">Código de Ocorrência</param>
        /// <returns>true/false</returns>
        public bool atualizarPOC(string numeroRegistro)
        {
            try
            {
                string sql = "UPDATE NWMS_PRODUCAO.N0204POC " +
                                "SET SITPOC = '2'" +
                              " WHERE NUMREG = '" + numeroRegistro + "' AND SITPOC = 1";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Atualiza o recebimento da tabela de coleta
        /// </summary>
        /// <param name="numeroRegistro">Código de Ocorrência</param>
        /// <returns>true/false</returns>
        public bool atualizarRecebimentoPOC(string numeroRegistro)
        {
            try
            {
                string sql = "UPDATE NWMS_PRODUCAO.N0204POC " +
                                "SET SITPOC = '3'" +
                              " WHERE NUMREG = '" + numeroRegistro + "' AND SITPOC = 1";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta se o parametro de justificativa existe no banco de dados para este usuário
        /// </summary>
        /// <param name="loginUsuario">Login do Usuário</param>
        /// <returns>true/false</returns>
        public bool consultarParametroJustificativaColeta(string loginUsuario)
        {
            try
            {
                if (consultaString("NWMS_PRODUCAO.N9999PAR", "OPERACAO", "OPERACAO LIKE '%1%' AND LOGIN = '" + loginUsuario + "'") != "VAZIO")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Verifica se o parâmetro esta cadastrado para este usuário
        /// </summary>
        /// <param name="loginUsuario">Login do Usuário</param>
        /// <returns>true/false</returns>
        public string consultarParametroJustificativa(string loginUsuario)
        {
            try
            {
                string sql = "SELECT OPERACAO AS LINHA FROM NWMS_PRODUCAO.N9999PAR WHERE LOGIN = '" + loginUsuario + "'";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return dr["LINHA"].ToString();
                }

                dr.Close();
                conn.Close();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return "";
        }

        /// <summary>
        /// Insere o parâmetro para o usuário Tabela de parâmetro de usuário N9999PAR
        /// </summary>
        /// <param name="loginUsuario">Login do Usuário</param>
        /// <param name="operacao">Operação</param>
        /// <returns>true/false</returns>
        public bool inserirVinculo(string loginUsuario, string operacao)
        {
            try
            {
                string sql = "SELECT COUNT(1) AS LINHA FROM NWMS_PRODUCAO.N9999PAR WHERE LOGIN = '" + loginUsuario + "'";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (Convert.ToInt64(dr["LINHA"]) > 0)
                    {
                        dr.Close();
                        conn.Close();
                        try
                        {
                            string sql1 = "UPDATE NWMS_PRODUCAO.N9999PAR SET OPERACAO = '" + operacao + "'";
                            sql1 += " WHERE LOGIN = '" + loginUsuario + "'";
                            OracleConnection conn1 = new OracleConnection(OracleStringConnection);
                            OracleCommand cmd1 = new OracleCommand(sql1, conn1);
                            cmd1.CommandType = CommandType.Text;
                            conn1.Open();

                            OracleDataReader dr1 = cmd1.ExecuteReader();

                            dr1.Close();
                            conn1.Close();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            return false;
                            throw ex;
                        }
                    }
                    else
                    {
                        dr.Close();
                        conn.Close();
                        try
                        {
                            var sql2 = "INSERT INTO NWMS_PRODUCAO.N9999PAR (LOGIN, OPERACAO) VALUES ('" + loginUsuario + "','" + operacao + "')";

                            OracleConnection conn2 = new OracleConnection(OracleStringConnection);
                            OracleCommand cmd2 = new OracleCommand(sql2, conn2);
                            cmd2.CommandType = CommandType.Text;
                            conn2.Open();

                            OracleDataReader dr2 = cmd2.ExecuteReader();

                            dr2.Close();
                            conn2.Close();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// Verifica se existe registro para esta ocorrência na tabela de coleta
        /// SELECT COUNT(1) AS LINHA FROM NWMS_PRODUCAO.N0204POC WHERE NUMREG
        /// </summary>
        /// <param name="numeroRegistro">Código de Ocorrência</param>
        /// <returns>true/false</returns>
        public bool consultarVinculo(string numeroRegistro)
        {
            try
            {
                string sql = "SELECT COUNT(1) AS LINHA FROM NWMS_PRODUCAO.N0204POC WHERE NUMREG = '" + numeroRegistro + "'";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (Convert.ToInt64(dr["LINHA"]) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                dr.Close();
                conn.Close();
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta se a ocorrência esta com status de aprovado ou coleta
        /// SELECT COUNT(1) AS LINHA FROM NWMS_PRODUCAO.N0203REG WHERE NUMREG = " + numeroRegistro + " and SITREG IN (4,8
        /// </summary>
        /// <param name="numeroRegistro">Código de Ocorrência</param>
        /// <returns>true/false</returns>
        public bool consultarOcorrencia(string numeroRegistro)
        {
            try
            {
                string sql = "SELECT COUNT(1) AS LINHA FROM NWMS_PRODUCAO.N0203REG WHERE NUMREG = " + numeroRegistro + " and SITREG IN (4,6,8)";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (Convert.ToInt64(dr["LINHA"]) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                dr.Close();
                conn.Close();
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta número da placa da coleta da ocorrência
        /// </summary>
        /// <param name="numeroRegistro">Código de Ocorrência</param>
        /// <returns>Placa</returns>
        public string consultarPlacaPOC(string numeroRegistro)
        {
            try
            {
                string sql = "SELECT PLACA FROM NWMS_PRODUCAO.N0204POC WHERE NUMREG = " + numeroRegistro + " and SITPOC = 1";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return dr["PLACA"].ToString();
                }
                dr.Close();
                conn.Close();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Altera a situação da ocorrência do status integrado para o status de recebido
        /// UPDATE NWMS_PRODUCAO.N0203REG " + "SET SITREG = '9'" + " WHERE NUMREG = '" + codigoRegistro + "' AND SITREG IN (3)"
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        public void rollbackAprovacao(string codigoRegistro)
        {
            string sql = "UPDATE NWMS_PRODUCAO.N0203REG " +
                          "SET SITREG = '9'" +
                        " WHERE NUMREG = '" + codigoRegistro + "' AND SITREG IN (3)";
            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        /// <summary>
        /// Verifica se a placa é a mesma que foi registrado na coleta em caso de sim, altera a situação da ocorrência para recebido
        /// e grava na tabela de tramite (N0203TRA)
        /// </summary>
        /// <param name="NUMREG">Código de Ocorrência</param>
        /// <param name="PLACA">Placa</param>
        /// <param name="usuarioLogado">Código Usuário Logado</param>
        /// <returns>true/false</returns>
        public bool confirmarRecebimento(string NUMREG, string PLACA, long usuarioLogado)
        {
            string placaParametro = PLACA;
            long sequenciaTrammite;
            try
            {
                if (consultarVinculo(NUMREG))
                {
                    PLACA = consultarPlacaPOC(NUMREG);

                    if (PLACA == "" || placaParametro.ToUpper() != PLACA)
                    {
                        return false;
                    }
                    else
                    {

                        var numeroRegistro = Convert.ToInt64(NUMREG);

                        using (Context contexto = new Context())
                        {
                            var original = contexto.N0203REG.Where(c => c.NUMREG == numeroRegistro).SingleOrDefault();
                            N0203TRA itemTramites = new N0203TRA();
                            sequenciaTrammite = original.N0203TRA.OrderBy(c => c.SEQTRA).Last().SEQTRA + 1;
                        }
                        
                        atualizarRecebimentoPOC(NUMREG);
                        string sql = "UPDATE NWMS_PRODUCAO.N0203REG " +
                            "SET SITREG = '9'" +
                          " WHERE NUMREG = '" + NUMREG + "' AND SITREG IN (4,6,8)";
                        OracleConnection conn = new OracleConnection(OracleStringConnection);
                        OracleCommand cmd = new OracleCommand(sql, conn);
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();

                        string sqlTrammite = "INSERT INTO NWMS_PRODUCAO.N0203TRA (NUMREG, SEQTRA, DESTRA, USUTRA, DATTRA, OBSTRA, CODORI) VALUES ('" + NUMREG + "', '" + sequenciaTrammite + "','OCORRENCIA CONFERIDO','" + usuarioLogado + "', TO_DATE('" + DateTime.Now.ToString() + "', 'DD-MM-YYYY HH24:MI:SS'),'MERCADORIA CONFERIDO', '4')";

                        OracleConnection connTrammite = new OracleConnection(OracleStringConnection);
                        OracleCommand cmdTrammite = new OracleCommand(sqlTrammite, connTrammite);
                        cmdTrammite.CommandType = CommandType.Text;
                        connTrammite.Open();
                        cmdTrammite.ExecuteNonQuery();
                        connTrammite.Close();
                        return true;
                    }

                }
                else if (consultarOcorrencia(NUMREG))
                {
                    var numeroRegistro = Convert.ToInt64(NUMREG);

                    using (Context contexto = new Context())
                    {
                        var original = contexto.N0203REG.Where(c => c.NUMREG == numeroRegistro).SingleOrDefault();
                        N0203TRA itemTramites = new N0203TRA();
                        sequenciaTrammite = original.N0203TRA.OrderBy(c => c.SEQTRA).Last().SEQTRA + 1;
                    }

                    string sql = "UPDATE NWMS_PRODUCAO.N0203REG SET SITREG = '9'  " +
                               " WHERE NUMREG = " + NUMREG + " ";
                    
                    OracleConnection conn = new OracleConnection(OracleStringConnection);
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    string sqlTrammite = "INSERT INTO NWMS_PRODUCAO.N0203TRA (NUMREG, SEQTRA, DESTRA, USUTRA, DATTRA, OBSTRA, CODORI) VALUES ('" + NUMREG + "', '" + sequenciaTrammite + "','OCORRENCIA CONFERIDO','" + usuarioLogado + "', TO_DATE('" + DateTime.Now.ToString() + "', 'DD-MM-YYYY HH24:MI:SS'),'MERCADORIA CONFERIDO', '4')";

                    OracleConnection connTrammite = new OracleConnection(OracleStringConnection);
                    OracleCommand cmdTrammite = new OracleCommand(sqlTrammite, connTrammite);
                    cmdTrammite.CommandType = CommandType.Text;
                    connTrammite.Open();
                    cmdTrammite.ExecuteNonQuery();
                    connTrammite.Close();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// Altera o status da ocorrência para "Coleta"
        /// </summary>
        /// <param name="numeroRegistro">Código de Ocorrência</param>
        /// <returns>true/false</returns>
        public bool alterarStatusOcorrencia(string numeroRegistro)
        {
            try
            {
                string sql = "UPDATE NWMS_PRODUCAO.N0203REG " +
                                "SET SITREG = '8'" +
                              " WHERE NUMREG = '" + numeroRegistro + "'";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        /// <summary>
        /// Verifica todas as ocorrências que estão com atraso de faturamento
        /// </summary>
        /// <returns>true/false</returns>
        public ArrayList ocorrenciasAtrasoFaturamento()
        {
            try
            {
                //string sql;
                string sql = @"SELECT SUM(MAIORQUE24HORAS) MAIORQUE24HORAS,
                                       SUM(MENOR24HRS) AS MENOR24HRS
                                  FROM (SELECT COUNT(DISTINCT(REG.NUMREG)) AS MAIORQUE24HORAS,
                                               0 AS MENOR24HRS
                                          FROM NWMS_PRODUCAO.N0203TRA TRA
                                         INNER JOIN NWMS_PRODUCAO.N0203REG REG
                                            ON REG.NUMREG = TRA.NUMREG
                                         WHERE (TRA.DESTRA Like 'OCORR%NCIA RECEBIDA' OR TRA.DESTRA Like 'OCORR%NCIA CONFERIDO') AND REG.SITREG = 9 AND
                                               SYSDATE - TRA.DATTRA > 1 AND REG.NUMREG NOT IN (SELECT NUMREG FROM NWMS_PRODUCAO.N0203TRA 
                                               WHERE SYSDATE - DATTRA < 1 AND (DESTRA Like 'OCORR%NCIA RECEBIDA' OR DESTRA Like 'OCORR%NCIA CONFERIDO'))
                                               OR (TRA.DESTRA Like 'REGISTRO DE OCORR%NCIA APROVADO' AND
                                               REG.SITREG = 3 AND SYSDATE - TRA.DATTRA > 1)
                                        UNION ALL
                                        SELECT 0 AS MAIORQUE24HORAS,
                                               COUNT(DISTINCT(REG.NUMREG)) AS MENOR24HRS
                                          FROM NWMS_PRODUCAO.N0203TRA TRA
                                         INNER JOIN NWMS_PRODUCAO.N0203REG REG
                                            ON REG.NUMREG = TRA.NUMREG
                                         WHERE ((TRA.DESTRA like 'OCORR%NCIA RECEBIDA' OR TRA.DESTRA like 'OCORR%NCIA CONFERIDO') AND REG.SITREG = 9 AND
                                               SYSDATE - TRA.DATTRA < 1)
                                               OR (TRA.DESTRA like 'REGISTRO DE OCORR%NCIA APROVADO' AND
                                               REG.SITREG = 3 AND SYSDATE - TRA.DATTRA < 1))";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                OracleDataReader dr = cmd.ExecuteReader();
                
                ArrayList lista = new ArrayList();
                if (dr.Read())
                {
                    ArrayList itens = new ArrayList();
                    itens.Add(dr.GetInt32(0));
                    itens.Add(dr.GetInt32(1));
                    lista.Add(itens);
                }
                dr.Close();
                conn.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inativa o vinculo de coleta da ocorrência
        /// </summary>
        /// <param name="numeroRegistro">Código de Ocorrência</param>
        /// <param name="placa">Placa</param>
        /// <returns>true/false</returns>
        public bool inativarVinculoAnterior(string numeroRegistro, string placa)
        {
            try
            {
                string sql = "UPDATE NWMS_PRODUCAO.N0204POC " +
                                "SET SITPOC = '2'" +
                              " WHERE NUMREG = '" + numeroRegistro + "'";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gera uma nova solicitação de coleta
        /// </summary>
        /// <param name="numReg">Código de Ocorrência</param>
        /// <param name="usuGer">Usuário Gerado</param>
        /// <param name="datVin">Data do Vinculo</param>
        /// <param name="codPlaca">Placa</param>
        /// <param name="observacao">Observação</param>
        /// <returns>true/false</returns>
        public Boolean ReVincular(string numReg, string usuGer, string datVin, string codPlaca, string observacao)
        {
            long sequenciaTrammite;
            try
            {
                if (inativarVinculoAnterior(numReg, codPlaca.ToUpper()))
                {
                    return false;
                }

                var numeroRegistro = Convert.ToInt64(numReg);

                using (Context contexto = new Context())
                {
                    var original = contexto.N0203REG.Where(c => c.NUMREG == numeroRegistro).SingleOrDefault();
                    N0203TRA itemTramites = new N0203TRA();
                    sequenciaTrammite = original.N0203TRA.OrderBy(c => c.SEQTRA).Last().SEQTRA + 1;
                }

                var sql = "INSERT INTO NWMS_PRODUCAO.N0204POC (NUMREG, USUGER, DATVIN, SITPOC, PLACA, OBSREG ) VALUES ('" + numReg + "', '" + usuGer + "', '" + datVin + "', '1', '" + codPlaca.ToUpper() + "', '" + observacao + "')";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                dr.Close();
                conn.Close();

                string sqlTrammite = "INSERT INTO NWMS_PRODUCAO.N0203TRA (NUMREG, SEQTRA, DESTRA, DESTRA, OBSTRA, USUTRA,DATTRA, CODORI) VALUES ('" + numReg + "','OCORRENCIA CONFERIDO', '" + sequenciaTrammite + "','" + usuGer + "',TO_DATE('" + DateTime.Now.ToString() + "', 'DD-MM-YYYY HH24:MI:SS'), '4')";

                OracleConnection connTrammite = new OracleConnection(OracleStringConnection);
                OracleCommand cmdTrammite = new OracleCommand(sqlTrammite, connTrammite);
                cmdTrammite.CommandType = CommandType.Text;
                connTrammite.Open();
                cmdTrammite.ExecuteNonQuery();
                connTrammite.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        /// <summary>
        /// Cria um novo registro na ocorrência
        /// </summary>
        /// <param name="N0203REG">Tabela de Ocorrência</param>
        /// <param name="codProtocolo">Código de Ocorrência</param>
        /// <returns>true/false</returns>
        public bool InserirRegistroOcorrencia(N0203REG N0203REG, out long codProtocolo)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    E140IPVDataAccess E140IPVDataAccess = new E140IPVDataAccess();
                    USU_T135LANDataAccess USU_T135LANDataAccess = new USU_T135LANDataAccess();

                    if (contexto.N0203REG.Count() == 0)
                    {
                        N0203REG.NUMREG = 1;
                    }
                    else
                    {
                        N0203REG.NUMREG = contexto.N0203REG.Max(p => p.NUMREG + 1);
                    }

                    codProtocolo = N0203REG.NUMREG;

                    N0203TRA itemTramites = new N0203TRA();
                    itemTramites.DATTRA = N0203REG.DATGER;
                    itemTramites.DESTRA = "REGISTRO DE OCORRENCIA FECHADO";

                    if (N0203REG.SITREG == (long)Enums.SituacaoRegistroOcorrencia.Pendente)
                    {
                        itemTramites.DESTRA = "REGISTRO DE OCORRENCIA SALVO - RASCUNHO";
                    }

                    itemTramites.NUMREG = N0203REG.NUMREG;

                    if (contexto.N0203TRA.Where(c => c.NUMREG == N0203REG.NUMREG).Count() == 0)
                    {
                        itemTramites.SEQTRA = 1;
                    }
                    else
                    {
                        itemTramites.SEQTRA = contexto.N0203TRA.Where(c => c.NUMREG == N0203REG.NUMREG).Max(p => p.SEQTRA + 1);
                    }

                    itemTramites.USUTRA = N0203REG.USUULT;
                    N0203REG.N0203TRA.Add(itemTramites);

                    // Agrupar notas para gravar data de emissão
                    var listNotas = (from a in N0203REG.N0203IPV
                                     group new { a } by new { a.CODEMP, a.CODFIL, a.NUMNFV } into grupo
                                     select new { grupo.Key.CODFIL, grupo.Key.NUMNFV }).ToList();

                    E140NFVDataAccess E140NFVDataAccess = new E140NFVDataAccess();

                    foreach (var item in listNotas)
                    {
                        // Data de emissão da nota
                        var dataEmi = E140NFVDataAccess.PesquisarDadosNota(item.NUMNFV, item.CODFIL, null, "3").FirstOrDefault().DataEmissao;
                        var datTime = DateTime.Parse(dataEmi);

                        N0203REG.N0203IPV.Where(c => c.CODFIL == item.CODFIL && c.NUMNFV == item.NUMNFV).ToList().ForEach(c => c.DATEMI = datTime);
                    }

                    foreach (N0203IPV item in N0203REG.N0203IPV)
                    {
                        item.NUMREG = N0203REG.NUMREG;
                        long analiseEmbaque;
                        E140IPVDataAccess.PesquisaAnaliseEmbarquePorNota(item.NUMNFV, item.CODFIL, out analiseEmbaque);
                        item.NUMANE = analiseEmbaque;

                        if (item.CODFIL == 101)
                        {
                            long analiseEmbaqueFilial;
                            USU_T135LANDataAccess.PesquisaRelacionamentoAneEmbarqueEntreFilial(analiseEmbaque, out analiseEmbaqueFilial);
                            item.NUMANE_REL = analiseEmbaqueFilial;
                        }
                    }

                    bool valida = false;

                    foreach (N0203ANX item in N0203REG.N0203ANX)
                    {
                        item.NUMREG = N0203REG.NUMREG;

                        if (!valida && contexto.N0203ANX.Where(c => c.NUMREG == N0203REG.NUMREG).Count() == 0)
                        {
                            item.IDROW = 1;
                            valida = true;
                        }
                        else
                        {
                            item.IDROW = N0203REG.N0203ANX.Last(c => c.IDROW != 0).IDROW + 1;
                        }
                    }

                    contexto.N0203REG.Add(N0203REG);
                    contexto.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Pesquisa registros de ocorrência
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        /// <param name="codUsuarioLogado">Código Usuário Logado</param>
        /// <returns>Lista de Ocorrência</returns>
        public List<N0203REG> PesquisaRegistrosOcorrencia(long? codigoRegistro, long codUsuarioLogado)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var listaSituacao = new List<long>();
                    listaSituacao.Add((long)Enums.SituacaoRegistroOcorrencia.Pendente);
                    listaSituacao.Add((long)Enums.SituacaoRegistroOcorrencia.Reprovado);

                    N9999USUDataAccess N9999USUDataAccess = new N9999USUDataAccess();

                    ActiveDirectoryDataAccess ActiveDirectoryDataAccess = new ActiveDirectoryDataAccess();


                    var loginUsuario = N9999USUDataAccess.ListaDadosUsuarioPorCodigo(codUsuarioLogado).LOGIN;

                    var nomeUsu = ActiveDirectoryDataAccess.ListaDadosUsuarioAD(loginUsuario).Nome;

                    // Seta Lista
                    var lista = contexto.N0203REG.Where(c => c.NUMREG == null).ToList();

                    if (codigoRegistro == null)
                    {
                        lista = contexto.N0203REG.Where(c => listaSituacao.Contains(c.SITREG)).OrderBy(c => c.NUMREG).ToList();
                    }
                    else
                    {
                        lista = contexto.N0203REG.Where(c => c.NUMREG == codigoRegistro && listaSituacao.Contains(c.SITREG)).ToList();
                    }

                    return lista;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string descTransportadoraIndenizacao(long CodTRa)
        {
            try
            {
                string sql = "SELECT NOMTRA FROM SAPIENS.E073TRA WHERE CODTRA = " + CodTRa;
                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();
                string NomTra = "";
                while (dr.Read())
                {
                    NomTra = dr["NOMTRA"].ToString();
                }
                return NomTra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Lista todas as ocorrências de Coleta
        /// </summary>
        /// <returns></returns>
        public List<N0204POC> ocorrenciasColeta()
        {
            try
            {
                string sql = "    SELECT DISTINCT NUMREG," +
                     "                   USUGER," +
                     "                   DATVIN," +
                     "                   USU.LOGIN," +
                     "                   PLACA" +
                     "     FROM NWMS_PRODUCAO.N0204POC POC" +
                     "    INNER JOIN NWMS_PRODUCAO.N9999USU USU" +
                     "       ON USU.CODUSU = POC.USUGER" +
                     "    WHERE SITPOC = 1";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                var ActiveDirectoryDataAccess = new ActiveDirectoryDataAccess();
                cmd.CommandType = CommandType.Text;
                conn.Open();
                List<N0204POC> listaColeta = new List<N0204POC>();
                N0204POC itens = new N0204POC();
                OracleDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    itens = new N0204POC();
                    itens.NUMREG = Convert.ToInt64(dr["NUMREG"]);
                    itens.CODUSU = dr["USUGER"].ToString();
                    itens.NOMUSU = ActiveDirectoryDataAccess.ListaDadosUsuarioAD(dr["LOGIN"].ToString()).Nome;
                    itens.DATVIN = dr["DATVIN"].ToString();
                    itens.PLACA = dr["PLACA"].ToString().Insert(3, " - ");
                    listaColeta.Add(itens);
                }
                dr.Close();
                conn.Close();
                return listaColeta;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa todos os registros de ocorrências por situação
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        /// <param name="situacao">Situação</param>
        /// <returns>Lista de Ocorrências</returns>
        public N0203REG PesquisaRegistrosOcorrenciaPorSituacao(long codigoRegistro, int situacao)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    // Seta Lista
                    return contexto.N0203REG.Where(c => c.NUMREG == codigoRegistro && c.SITREG == situacao).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa os registros de ocorrências 
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        /// <param name="situacaoRegistro">Situação</param>
        /// <returns>Lista de Ocorrências</returns>
        public N0203REG PesquisaRegistroOcorrencia(long codigoRegistro, int situacaoRegistro)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    return contexto.N0203REG.Include("N0203IPV").Where(c => c.NUMREG == codigoRegistro).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Verifica se a ocorrência possui registro de troca
        /// </summary>
        /// <param name="numeroRegistro">Código de Ocorrência</param>
        /// <returns>true/false</returns>
        public bool verificarOcorrenciaVinculadaComTroca(string numeroRegistro)
        {
            try
            {
                string sql = "SELECT 1 FROM SAPIENS.E120PED WHERE USU_ALFREG = '" + numeroRegistro + "' ";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    if (Convert.ToInt64(dr["LINHA"]) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                dr.Close();
                conn.Close();
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cancela Registro de Ocorrência
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        /// <param name="usuarioTramite">Usuário Tramite</param>
        /// <returns>true/false</returns>
        public bool CancelarRegistrosOcorrencia(long codigoRegistro, long usuarioTramite, string motivoCancelamento)
        {
            try
            {
                if (verificarOcorrenciaVinculadaComTroca(codigoRegistro.ToString()))
                {
                    return false;
                }
                using (Context contexto = new Context())
                {
                    var original = contexto.N0203REG.Where(c => c.NUMREG == codigoRegistro).SingleOrDefault();
                    long sequencia = original.N0203TRA.Where(j => j.NUMREG == codigoRegistro).Last().SEQTRA + 1;

                    if (original.N0203TRA.Where(j => j.NUMREG == codigoRegistro && j.SEQTRA == sequencia) != null)
                    {
                        sequencia = sequencia + 1;
                    }

                    N0203TRA itemTramites = new N0203TRA();
                    itemTramites.NUMREG = original.NUMREG;
                    itemTramites.SEQTRA = sequencia;
                    itemTramites.DESTRA = "REGISTRO DE OCORRENCIA CANCELADO";
                    itemTramites.USUTRA = usuarioTramite;
                    itemTramites.DATTRA = DateTime.Now;
                    itemTramites.OBSTRA = "Cancelamento de Rascunho: " + motivoCancelamento;

                    original.N0203TRA.Add(itemTramites);

                    if (original != null)
                    {
                        original.SITREG = (long)Enums.SituacaoRegistroOcorrencia.Cancelado;
                        contexto.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// Verifica se existem protocolos com situação "Pendente" gerados anteriormente as últimas 24 horas corridas atrelados ao usuário logado
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <param name="protocolosAbertos">Ocorrências Abertas</param>
        /// <returns>true/false</returns>
        public bool VerificarProtocolosAberto(long codigoUsuario, out string protocolosAbertos)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var situacaoPendente = (long)Enums.SituacaoRegistroOcorrencia.Pendente;
                    var situacaoReprovado = (long)Enums.SituacaoRegistroOcorrencia.Reprovado;

                    protocolosAbertos = string.Empty;
                    TimeSpan hora = new TimeSpan(24, 0, 0);
                    DateTime data = DateTime.Now.Subtract(hora);

                    // Seta lista
                    var registros = contexto.N0203REG.Where(c => c.NUMREG == 0).ToList();

                    var N9999USUDataAccess = new N9999USUDataAccess();
                    var ActiveDirectoryDataAccess = new ActiveDirectoryDataAccess();
                    var loginUsuario = N9999USUDataAccess.ListaDadosUsuarioPorCodigo(codigoUsuario).LOGIN;
                    var nomeUsu = ActiveDirectoryDataAccess.ListaDadosUsuarioAD(loginUsuario).Nome;

                    if (nomeUsu == "WILLIAN ZMORZYNSKI" || nomeUsu == "LUCIANA VIEIRA")
                    {
                        registros = contexto.N0203REG.Where(c => c.USUGER == codigoUsuario && (c.SITREG == situacaoPendente || c.SITREG == situacaoReprovado) && c.DATGER <= data).OrderBy(c => c.NUMREG).ToList();
                    }
                    else
                    {
                        registros = contexto.N0203REG.Where(c => c.USUGER == codigoUsuario && c.SITREG == situacaoPendente && c.DATGER <= data).OrderBy(c => c.NUMREG).ToList();
                    }

                    if (registros.Count > 0)
                    {
                        foreach (N0203REG item in registros)
                        {
                            protocolosAbertos = protocolosAbertos + item.NUMREG.ToString() + " - ";
                        }

                        protocolosAbertos = protocolosAbertos.Substring(0, protocolosAbertos.Length - 3);

                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Busca as informações para o Relatório Analítico
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        /// <param name="codFilial">Código Filial</param>
        /// <param name="analiseEmbarque">Analise de Embarque</param>
        /// <param name="dataInicial">Data Inicial</param>
        /// <param name="dataFinal">Data Final</param>
        /// <param name="situacaoReg">Situação</param>
        /// <param name="cliente">Cliente</param>
        /// <param name="codPlaca">Placa</param>
        /// <param name="dataFaturamento">Data de Faturamento</param>
        /// <param name="tipoPesquisa">Tipo pesquisa</param>
        /// <returns></returns>
        public List<RelatorioAnalitico> RelatorioAnalitico(long? codigoRegistro, long? codFilial, long? analiseEmbarque, Nullable<DateTime> dataInicial, Nullable<DateTime> dataFinal, int? situacaoReg, long? cliente, string codPlaca, Nullable<DateTime> dataFaturamento, int tipoPesquisa)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    //int situacao = (int)Enums.SituacaoRegistroOcorrencia.Fechado;
                    List<RelatorioAnalitico> listaRegistros = new List<RelatorioAnalitico>();
                    RelatorioAnalitico itemLista = new RelatorioAnalitico();

                    // Seta Lista
                    var lista = contexto.N0203REG.Where(c => c.NUMREG == 0).ToList();

                    if (tipoPesquisa == (int)Enums.TipoPesquisaRegistroOcorrencia.NumeroRegistro)
                    {
                        lista = contexto.N0203REG.Where(c => c.NUMREG == codigoRegistro).ToList();
                    }
                    else if (tipoPesquisa == (int)Enums.TipoPesquisaRegistroOcorrencia.AnaliseEmbarque)
                    {
                        if (codFilial == 1)
                        {
                            lista = contexto.N0203REG.Where(c => c.N0203IPV.Any(d => d.CODFIL == codFilial && d.NUMANE == analiseEmbarque || d.CODFIL == 101 && d.NUMANE_REL == analiseEmbarque)).ToList();
                        }
                        else
                        {
                            var codAneRel = contexto.N0203IPV.Where(c => c.CODFIL == codFilial && c.NUMANE == analiseEmbarque).Select(c => c.NUMANE_REL).FirstOrDefault();
                            lista = contexto.N0203REG.Where(c => c.N0203IPV.Any(d => d.CODFIL == codFilial && d.NUMANE == analiseEmbarque || d.CODFIL == 1 && d.NUMANE == codAneRel)).ToList();
                        }
                    }
                    else if (tipoPesquisa == (int)Enums.TipoPesquisaRegistroOcorrencia.Placa_Data_Faturamento)
                    {
                        var dataFatIni = DateTime.Parse(dataFaturamento.ToString()).AddDays(-1);
                        var dataFatFim = DateTime.Parse(dataFaturamento.ToString()).AddDays(1);

                        lista = contexto.N0203REG.Where(c => c.PLACA == codPlaca && c.N0203IPV.Any(b => b.DATEMI >= dataFatIni && b.DATEMI <= dataFatFim)).ToList();
                    }
                    else if (tipoPesquisa == (int)Enums.TipoPesquisaRegistroOcorrencia.Periodo_Cliente)
                    {
                        if (cliente == null)
                        {
                            lista = contexto.N0203REG.Where(c => c.DATGER >= dataInicial && c.DATGER <= dataFinal).ToList();
                        }
                        else
                        {
                            lista = contexto.N0203REG.Where(c => c.DATGER >= dataInicial && c.DATGER <= dataFinal && c.CODCLI == cliente).ToList();
                        }

                    }
                    else if (tipoPesquisa == (int)Enums.TipoPesquisaRegistroOcorrencia.Periodo_Situacao_Cliente)
                    {
                        if (cliente == null)
                        {
                            lista = contexto.N0203REG.Where(c => c.DATGER >= dataInicial && c.DATGER <= dataFinal && c.SITREG == situacaoReg).ToList();
                        }
                        else
                        {
                            lista = contexto.N0203REG.Where(c => c.DATGER >= dataInicial && c.DATGER <= dataFinal && c.SITREG == situacaoReg && c.CODCLI == cliente).ToList();
                        }

                    }

                    if (lista != null)
                    {
                        var E085CLIDataAccess = new E085CLIDataAccess();
                        var E073MOTDataAccess = new E073MOTDataAccess();
                        var listaTipoAtendimento = contexto.N0204ATD.ToList();
                        var listaMotivoDevolucao = contexto.N0204MDV.ToList();
                        var listaOrigemOcorrencia = contexto.N0204ORI.ToList();
                        var N9999USUDataAccess = new N9999USUDataAccess();
                        var ActiveDirectoryDataAccess = new ActiveDirectoryDataAccess();

                        var countTotal = 0;
                        lista = lista.OrderBy(c => c.NUMREG).ToList();
                        foreach (var item in lista)
                        {
                            // Dados Gerais Registro
                            var CodigoRegistro = item.NUMREG;
                            var CodTipoAtendimento = item.TIPATE.ToString();
                            var DescTipoAtendimento = listaTipoAtendimento.Where(c => c.CODATD == item.TIPATE).FirstOrDefault().DESCATD;
                            var CodOrigemOcorrencia = item.ORIOCO.ToString();
                            var DescOrigemOcorrencia = listaOrigemOcorrencia.Where(c => c.CODORI == item.ORIOCO).FirstOrDefault().DESCORI;
                            var CodCliente = item.CODCLI.ToString();
                            var NomeCliente = E085CLIDataAccess.PesquisasClientes(item.CODCLI).FirstOrDefault().NomeFantasia;

                            var CodMotorista = item.CODMOT.ToString();
                            var NomeMotorista = "TRANSPORTADORA";
                            var placa = string.Empty;
                            var analiseEmbarqueTexto = string.Empty;
                            if (item.CODMOT != 0)
                            {
                                NomeMotorista = E073MOTDataAccess.PesquisasMotoristas(item.CODMOT).FirstOrDefault().Nome;
                                placa = item.PLACA.Substring(0, 3) + "-" + item.PLACA.Substring(3, 4);
                            }

                            var DataHrGeracao = item.DATGER.ToString();
                            var UsuarioGeracao = item.USUGER.ToString();
                            var loginUsuario = N9999USUDataAccess.ListaDadosUsuarioPorCodigo(item.USUGER).LOGIN;
                            var NomeUsuarioGeracao = ActiveDirectoryDataAccess.ListaDadosUsuarioAD(loginUsuario).Nome;
                            var CodSituacaoRegistro = item.SITREG.ToString();
                            var DescSituacaoRegistro = string.Empty;

                            if (item.SITREG == (int)Enums.SituacaoRegistroOcorrencia.Pendente)
                            {
                                DescSituacaoRegistro = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.SituacaoRegistroOcorrencia.Pendente).GetValue<string>();
                            }
                            else if (item.SITREG == (int)Enums.SituacaoRegistroOcorrencia.Fechado)
                            {
                                DescSituacaoRegistro = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.SituacaoRegistroOcorrencia.Fechado).GetValue<string>();
                            }
                            else if (item.SITREG == (int)Enums.SituacaoRegistroOcorrencia.Aprovado)
                            {
                                DescSituacaoRegistro = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.SituacaoRegistroOcorrencia.Aprovado).GetValue<string>();
                            }
                            else if (item.SITREG == (int)Enums.SituacaoRegistroOcorrencia.PreAprovado)
                            {
                                DescSituacaoRegistro = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.SituacaoRegistroOcorrencia.PreAprovado).GetValue<string>();
                            }
                            else if (item.SITREG == (int)Enums.SituacaoRegistroOcorrencia.Reprovado)
                            {
                                DescSituacaoRegistro = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.SituacaoRegistroOcorrencia.Reprovado).GetValue<string>();
                            }
                            else if (item.SITREG == (int)Enums.SituacaoRegistroOcorrencia.Cancelado)
                            {
                                DescSituacaoRegistro = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.SituacaoRegistroOcorrencia.Cancelado).GetValue<string>();
                            }
                            else if (item.SITREG == (int)Enums.SituacaoRegistroOcorrencia.Indenizado)
                            {
                                DescSituacaoRegistro = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.SituacaoRegistroOcorrencia.Indenizado).GetValue<string>();
                            }

                            var UltimaAlteracao = item.DATULT.ToString();
                            var UsuarioUltimaAlteracao = item.USUULT.ToString();
                            loginUsuario = N9999USUDataAccess.ListaDadosUsuarioPorCodigo(item.USUULT).LOGIN;
                            var NomeUsuarioUltimaAlteracao = ActiveDirectoryDataAccess.ListaDadosUsuarioAD(loginUsuario).Nome;
                            var Observacao = item.OBSREG;

                            decimal SomaTotalValorLiquido = 0;

                            var listaAnalises = new List<Tuple<long, long>>();
                            var countParcial = item.N0203IPV.Count;

                            item.N0203IPV = (from l in item.N0203IPV
                                             orderby l.NUMREG, l.CODFIL, l.NUMNFV, l.SEQIPV
                                             select l).ToList();

                            foreach (var itemDev in item.N0203IPV)
                            {
                                itemLista = new RelatorioAnalitico();
                                itemLista.CodigoRegistro = CodigoRegistro;
                                itemLista.CodTipoAtendimento = CodTipoAtendimento;
                                itemLista.DescTipoAtendimento = DescTipoAtendimento;
                                itemLista.CodOrigemOcorrencia = CodOrigemOcorrencia;
                                itemLista.DescOrigemOcorrencia = DescOrigemOcorrencia;
                                itemLista.CodCliente = CodCliente;
                                itemLista.NomeCliente = NomeCliente;
                                itemLista.CodMotorista = CodMotorista;
                                itemLista.CodPlaca = placa;
                                itemLista.NomeMotorista = NomeMotorista;
                                itemLista.DataHrGeracao = DataHrGeracao;
                                itemLista.UsuarioGeracao = UsuarioGeracao;
                                itemLista.NomeUsuarioGeracao = NomeUsuarioGeracao;
                                itemLista.CodSituacaoRegistro = CodSituacaoRegistro;
                                itemLista.DescSituacaoRegistro = DescSituacaoRegistro;
                                itemLista.UltimaAlteracao = UltimaAlteracao;
                                itemLista.UsuarioUltimaAlteracao = UsuarioUltimaAlteracao;
                                itemLista.NomeUsuarioUltimaAlteracao = NomeUsuarioUltimaAlteracao;
                                itemLista.Observacao = Observacao;

                                // Itens Devolução
                                itemLista.Empresa = itemDev.CODEMP.ToString();
                                itemLista.Filial = itemDev.CODFIL;

                                // Add item na Lista de analises de embarque
                                listaAnalises.Add(new Tuple<long, long>(itemDev.CODFIL, itemDev.NUMANE));

                                itemLista.SerieNota = itemDev.CODSNF;
                                itemLista.NumeroNota = itemDev.NUMNFV.ToString();
                                itemLista.SeqNota = itemDev.SEQIPV;
                                itemLista.CodPro = itemDev.CODPRO;
                                itemLista.CodDer = itemDev.CODDER;
                                itemLista.DescPro = itemDev.CPLIPV;
                                itemLista.CodDepartamento = itemDev.CODDEP;
                                itemLista.QtdeFat = itemDev.QTDFAT;
                                itemLista.PrecoUnitario = itemDev.PREUNI.ToString();
                                itemLista.CodOrigemOcorrenciaItemDev = itemDev.ORIOCO.ToString();
                                itemLista.DescOrigemOcorrenciaItemDev = listaOrigemOcorrencia.Where(c => c.CODORI == itemDev.ORIOCO).FirstOrDefault().DESCORI;
                                itemLista.CodMotivoDevolucao = itemDev.CODMOT.ToString();
                                itemLista.DescMotivoDevolucao = listaMotivoDevolucao.Where(c => c.CODMDV == itemDev.CODMOT).FirstOrDefault().DESCMDV;
                                itemLista.QtdeDevolucao = itemDev.QTDDEV;
                                itemLista.TipoTransacao = itemDev.TNSPRO;
                                itemLista.UsuarioUltimaAlteracaoItemDev = itemDev.USUULT.ToString();
                                loginUsuario = N9999USUDataAccess.ListaDadosUsuarioPorCodigo(itemDev.USUULT).LOGIN;
                                itemLista.NomeUsuarioUltimaAlteracao = ActiveDirectoryDataAccess.ListaDadosUsuarioAD(loginUsuario).Nome;
                                itemLista.DataUltimaAlteracao = itemDev.DATULT.ToString();
                                itemLista.PercDesconto = itemDev.PEROFE.ToString();
                                itemLista.PercIpi = itemDev.PERIPI.ToString();
                                itemLista.ValorBruto = decimal.Parse((itemDev.QTDDEV * itemDev.PREUNI).ToString());
                                itemLista.ValorBrutoS = itemLista.ValorBruto.ToString("###,###,##0.00");
                                itemLista.ValorIpi = itemDev.QTDDEV * (itemDev.VLRIPI / itemDev.QTDFAT);
                                itemLista.ValorIpiS = itemLista.ValorIpi.ToString("###,###,##0.00");
                                itemLista.ValorSt = itemDev.QTDDEV * (itemDev.VLRST / itemDev.QTDFAT);
                                itemLista.ValorStS = itemLista.ValorSt.ToString("###,###,##0.00");
                                itemLista.ValorLiquido = (itemDev.QTDDEV * decimal.Parse(itemDev.PREUNI.ToString())) + itemLista.ValorIpi + itemLista.ValorSt;
                                itemLista.ValorLiquidoS = itemLista.ValorLiquido.ToString("###,###,##0.00");
                                SomaTotalValorLiquido = SomaTotalValorLiquido + itemLista.ValorLiquido;

                                countTotal = countTotal + 1;

                                listaRegistros.Add(itemLista);
                            }

                            var newlist = listaAnalises.GroupBy(c => new { c.Item1, c.Item2 }).Select(c => new { c.Key.Item1, c.Key.Item2 }).ToList();

                            foreach (var aux in newlist)
                            {
                                analiseEmbarqueTexto = analiseEmbarqueTexto + aux.Item1.ToString() + " - ( " + aux.Item2.ToString() + " ) ";
                            }

                            listaRegistros[countTotal - countParcial].AnaliseEmbarque = analiseEmbarqueTexto;

                            var d = SomaTotalValorLiquido.ToString("###,###,##0.00").Split(',');
                            var u = d[1].Substring(2, d[1].Length - 2).PadRight(2, '0');
                            decimal valorForm = 0;

                            if (int.Parse(u) > 40)
                            {
                                valorForm = decimal.Parse(d[1].Substring(0, 2)) + 1;
                                var valor = decimal.Parse(d[0] + "," + valorForm);
                                listaRegistros.Last().TotalValorLiquido = valor;
                                listaRegistros.Last().TotalValorLiquidoS = Convert.ToDouble(valor.ToString()).ToString("###,###,##0.00");
                            }
                            else
                            {
                                var aux = decimal.Round(SomaTotalValorLiquido, 2, MidpointRounding.AwayFromZero);
                                listaRegistros.Last().TotalValorLiquido = aux;
                                listaRegistros.Last().TotalValorLiquidoS = Convert.ToDouble(aux.ToString()).ToString("###,###,##0.00");
                            }
                        }
                    }

                    return listaRegistros;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca as informações para o Relatório de tempo de carga
        /// </summary>
        /// <param name="filial">Código da Filial</param>
        /// <param name="embarque">Tipo de Embarque</param>
        /// <param name="numeroNotaFiscal">Número da Nota Fiscal</param>
        /// <param name="codCliente">Código Cliente</param>
        /// <param name="dataInicial">Data Inicial</param>
        /// <param name="dataFinal">Data Fácil</param>
        /// <param name="motivo">Motivo</param>
        /// <param name="situacao">Situação</param>
        /// <param name="origem">Origem</param>
        /// <param name="tipo">Tipo</param>
        /// <param name="dataInicialOCR">Data Inicial</param>
        /// <param name="dataFinalOCR">Data Final da Ocorrência</param>
        /// <param name="codPlaca">Placa</param>
        /// <param name="transportadora">Transportador</param>
        /// <returns></returns>
        public List<RelatorioTempoCarga> RelatorioTempoCarga( string filial, string embarque, string numeroNotaFiscal, string codCliente, string dataInicial, string dataFinal, string motivo, string situacao, string origem, string tipo, string dataInicialOCR, string dataFinalOCR, string codPlaca, string transportadora)
        {
            try
            {
                string sql = @"SELECT NUMREG,
                                       SITREG,
                                       DATGER,
                                       NOTA_ORIGEM,
                                       DATEMI,
                                       DESCORI,
                                       CODCLI,
                                       NUMANE,
                                       NOMCLI,
                                       PLACA,
                                       NUMPED,
                                       NOTA_ENTRADA,
                                       NOTA_FATURADA,
                                       DATTRA,
                                       TO_CHAR(ROUND(SUM((VLRLIQ / QTDFAT) * QTDDEV), 2), 'FM999G999G999D90',
                                               'nls_numeric_characters='',.''') VALORLIQUIDO
                                  FROM (SELECT DISTINCT NREG.NUMREG,
                                                        NREG.SITREG,
                                                        TO_CHAR(NREG.DATGER, 'DD/MM/RRRR') DATGER,
                                                        NIPV.NUMNFV NOTA_ORIGEM,
                                                        NIPV.DATEMI,
                                                        ORI.DESCORI,
                                                        NIPV.SEQIPV,
                                                        CLI.CODCLI,
                                                        PED_TROCA.ANALISE AS NUMANE,
                                                        CLI.NOMCLI,
                                                        NIPV.VLRLIQ,
                                                        NIPV.QTDFAT,
                                                        NIPV.QTDDEV,
                                                        NREG.PLACA,
                                                        (SELECT TO_CHAR(MAX(TRA1.DATTRA), 'DD/MM/YY hh24:mm')
                                                           FROM NWMS_PRODUCAO.N0203TRA TRA1
                                                          WHERE TRA1.NUMREG = NREG.NUMREG) DATTRA,
                                                        PED_TROCA.PEDIDO AS NUMPED,
                                                        IPC.NUMNFC NOTA_ENTRADA,
                                                        PED_TROCA.NOTA_FATURADA,
                                                        (SELECT MOT.NOMMOT
                                                           FROM SAPIENS.E140NFV NFV, SAPIENS.E073MOT MOT
                                                          WHERE NFV.CODEMP = NIPV.CODEMP
                                                            AND NFV.CODFIL = NIPV.CODFIL
                                                            AND NFV.CODSNF = NIPV.CODSNF
                                                            AND NFV.NUMNFV = NIPV.NUMNFV
                                                            AND NFV.CODTRA = MOT.CODTRA
                                                            AND NFV.CODMTR = MOT.CODMTR) AS MOTORISTA,
                                                        1
                                          FROM NWMS_PRODUCAO.N0203REG NREG
                                         INNER JOIN NWMS_PRODUCAO.N0203IPV NIPV
                                            ON (NIPV.NUMREG = NREG.NUMREG)
                                         INNER JOIN NWMS_PRODUCAO.N0204MDV NMDV
                                            ON (NMDV.CODMDV = NIPV.CODMOT)
                                         INNER JOIN NWMS_PRODUCAO.N0204ORI ORI
                                            ON (ORI.CODORI = NIPV.ORIOCO)
                                         INNER JOIN NWMS_PRODUCAO.N0204ATD ATD
                                            ON (NREG.TIPATE = ATD.CODATD)
                                          LEFT JOIN NWMS_PRODUCAO.PEDIDO_DE_TROCA PED_TROCA
                                            ON PED_TROCA.OCORRENCIA = NREG.NUMREG
                                          LEFT JOIN SAPIENS.E440IPC IPC
                                            ON IPC.USU_NUMREG = NREG.NUMREG
                                           AND IPC.CODEMP = NIPV.CODEMP
                                           AND IPC.FILNFV = NIPV.CODFIL
                                           AND IPC.SNFNFV = NIPV.CODSNF
                                           AND IPC.NUMNFV = NIPV.NUMNFV
                                           AND IPC.SEQIPV = NIPV.SEQIPV
                                         INNER JOIN SAPIENS.E085CLI CLI
                                            ON (NREG.CODCLI = CLI.CODCLI)
                                         WHERE 1 = 1 ";
                if (situacao == "2")
                {
                    sql += "AND NOT EXISTS (SELECT 1 " +
                     "           FROM NWMS_PRODUCAO.N0203TRA TRA" +
                     "          WHERE TRA.NUMREG = NREG.NUMREG" +
                     "            AND TRA.DESTRA = 'REGISTRO DE OCORRENCIA PRÉ APROVADO'" +
                     "            AND TRA.CODORI IN (SELECT DORI1.CODORI" +
                     "          FROM NWMS_PRODUCAO.N0204DORI DORI1" +
                     "         WHERE DORI1.CODUSU = 76))";
                }

                sql += dataInicial != "" && dataFinal != "" ? "AND TO_DATE(TO_CHAR(NIPV.DATEMI, 'DD/MM/RRRR')) BETWEEN '" + DateTime.Parse(dataInicial).ToShortDateString() + "' AND '" + DateTime.Parse(dataFinal).ToShortDateString() + "' " : " ";
                sql += dataInicialOCR != "" && dataFinalOCR != "" ? "AND TO_DATE(TO_CHAR(NREG.DATGER, 'DD/MM/RRRR')) BETWEEN '" + DateTime.Parse(dataInicialOCR).ToShortDateString() + "' AND '" + DateTime.Parse(dataFinalOCR).ToShortDateString() + "' " : " ";
                sql += filial != "" && embarque != "" ? "AND PES.FILPED = '" + filial + "' AND PES.NUMANE = '" + embarque + "' " : " ";
                sql += codCliente != "" ? "AND CLI.CODCLI =" + codCliente : " ";
                sql += situacao != "" ? "AND NREG.SITREG =" + situacao : " ";
                sql += motivo != "" ? "AND NIPV.CODMOT =" + motivo : " ";
                sql += origem != "" ? "AND NIPV.ORIOCO =" + origem : " ";
                sql += tipo != "" ? "AND NREG.TIPATE =" + tipo : " ";
                sql += codPlaca != "" ? "AND NREG.PLACA = UPPER('" + codPlaca + "')" : "";
                sql += transportadora != "" ? "AND COALESCE(UPPER(MOT.NOMMOT),'TRANSPORTADORA') = 'TRANSPORTADORA'" : "";
                sql += @")GROUP BY        NUMREG,
                                          SITREG,
                                          DATGER,
                                          NOTA_ORIGEM,
                                          DATEMI,
                                          DESCORI,
                                          CODCLI,
                                          NUMANE,
                                          NOMCLI,
                                          PLACA,
                                          DATTRA,
                                          NUMPED,
                                          NOTA_ENTRADA,
                                          NOTA_FATURADA
                                   ORDER BY NUMREG DESC";

                
                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                //Gravar Arquivo
                
                OracleDataReader dr = cmd.ExecuteReader();
                List<RelatorioTempoCarga> listaProtocolosPendentes = new List<RelatorioTempoCarga>();
                RelatorioTempoCarga itemProtocolo = new RelatorioTempoCarga();
                decimal valorTotal = 0;
                while (dr.Read())
                {
                    itemProtocolo = new RelatorioTempoCarga();
                    itemProtocolo.codigoProtocolo = Convert.ToInt64(dr["NUMREG"]);
                    itemProtocolo.filial = 1;
                    itemProtocolo.notaFiscal = Convert.ToInt64(dr["NOTA_ORIGEM"]);
                    itemProtocolo.codigoCliente = Convert.ToInt64(dr["CODCLI"]);
                    itemProtocolo.nomeCliente = itemProtocolo.codigoCliente + " - " + dr["NOMCLI"].ToString();
                    itemProtocolo.dataGeracao = Convert.ToDateTime(dr["DATGER"]);
                    itemProtocolo.status = dr["SITREG"].ToString() == "1" ? "Pendente" :
                        dr["SITREG"].ToString() == "2" ? "Aguardando Aprovação" :
                        dr["SITREG"].ToString() == "3" ? "Integrado" :
                        dr["SITREG"].ToString() == "4" ? "Aprovado" :
                        dr["SITREG"].ToString() == "5" ? "Reprovado" :
                        dr["SITREG"].ToString() == "6" ? "Reabilitado":
                        dr["SITREG"].ToString() == "8" ? "Coleta" :
                        dr["SITREG"].ToString() == "9" ? "Conferido" :
                        dr["SITREG"].ToString() == "10" ? "Faturado" :
                        dr["SITREG"].ToString() == "11" ? "Indenizado" :
                        dr["SITREG"].ToString() == "7" ? "Cancelado" : "";
                    itemProtocolo.valorTotal = dr["VALORLIQUIDO"].ToString();
                    valorTotal += Convert.ToDecimal(dr["VALORLIQUIDO"]); //aqui
                    itemProtocolo.valorTotalString = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorTotal);
                    itemProtocolo.embarque = dr["NUMANE"].ToString();
                    itemProtocolo.numeroPedido = dr["NUMPED"].ToString();
                    itemProtocolo.numeroNotaFiscalEntrada = dr["NOTA_ENTRADA"].ToString();
                    itemProtocolo.numeroNotaFiscalFaturada = dr["NOTA_FATURADA"].ToString();
                    itemProtocolo.dataSituacao = dr["DATTRA"].ToString();
                    itemProtocolo.dataFechamento = Convert.ToDateTime(dr["DATFEC"]);
                    listaProtocolosPendentes.Add(itemProtocolo);
                }

                dr.Close();
                conn.Close();
                return listaProtocolosPendentes;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca as informações para o Relatório Sintetico 
        /// </summary>
        /// <param name="filial">Código Filial</param>
        /// <param name="embarque">Embarque</param>
        /// <param name="numeroNotaFiscal">Número da Nota Fiscal</param>
        /// <param name="codCliente">Código Cliente</param>
        /// <param name="dataInicial">Data Inicial</param>
        /// <param name="dataFinal">Data Final</param>
        /// <param name="motivo">Motivo</param>
        /// <param name="situacao">Situação</param>
        /// <param name="origem">Origem</param>
        /// <param name="tipo">Tipo</param>
        /// <param name="dataInicialOCR">Data Inicial Ocorrência</param>
        /// <param name="dataFinalOCR">Data Final Ocorrência</param>
        /// <param name="codPlaca">Placa</param>
        /// <param name="transportadora">Transportador</param>
        /// <returns></returns>
        public List<RelatorioSinteticoOcorrencia> RelatorioSinteticoOcorrencia(string filial, string embarque, string numeroNotaFiscal, string codCliente, string dataInicial, string dataFinal, string motivo, string situacao, string origem, string tipo, string dataInicialOCR, string dataFinalOCR, string codPlaca, string transportadora)
        {
            try
            {
                string sql = @"SELECT NUMREG,
                                       SITREG,
                                       DATGER,
                                       NOTA_ORIGEM,
                                       DATEMI,
                                       DESCORI,
                                       CODCLI,
                                       NUMANE AS NUMANE,
                                       NOMCLI,
                                       PLACA,
                                       NUMPED AS NUMPED,
                                       NOTA_ENTRADA,
                                       NOTA_FATURADA,
                                       DATTRA,
                                       TO_CHAR(ROUND(SUM((VLRLIQ / QTDFAT) * QTDDEV), 2), 'FM999G999G999D90',
                                               'nls_numeric_characters='',.''') VALORLIQUIDO
                                  FROM (SELECT DISTINCT NREG.NUMREG,
                                                        NREG.SITREG,
                                                        TO_CHAR(NREG.DATGER, 'DD/MM/RRRR') DATGER,
                                                        NIPV.NUMNFV NOTA_ORIGEM,
                                                        NIPV.DATEMI,
                                                        ORI.DESCORI,
                                                        NIPV.SEQIPV,
                                                        CLI.CODCLI,
                                                        CLI.NOMCLI,
                                                        NIPV.NUMANE AS NUMANE,
                                                        ( SELECT PED.NUMPED FROM SAPIENS.E120PED PED WHERE PED.USU_ALFREG LIKE CONCAT(CONCAT('%',NREG.NUMREG),'%') AND ROWNUM = 1) AS NUMPED,
                                                         ( SELECT IPV.NUMNFV FROM SAPIENS.E140IPV IPV, SAPIENS.E120PED PED  WHERE PED.USU_ALFREG LIKE CONCAT(CONCAT('%',NREG.NUMREG),'%') AND PED.CODEMP = IPV.CODEMP
                                                            AND PED.CODFIL = IPV.CODFIL AND PED.NUMPED = IPV.NUMPED AND ROWNUM = 1) AS NOTA_FATURADA,
                                                        NIPV.VLRLIQ,
                                                        NIPV.QTDFAT,
                                                        NIPV.QTDDEV,
                                                        NREG.PLACA,
                                                        (SELECT TO_CHAR(MAX(TRA1.DATTRA), 'DD/MM/YY hh24:mm')
                                                           FROM NWMS_PRODUCAO.N0203TRA TRA1
                                                          WHERE TRA1.NUMREG = NREG.NUMREG) DATTRA,
                                                        IPC.NUMNFC NOTA_ENTRADA,
                                                        (SELECT MOT.NOMMOT
                                                           FROM SAPIENS.E140NFV NFV, SAPIENS.E073MOT MOT
                                                          WHERE NFV.CODEMP = NIPV.CODEMP
                                                            AND NFV.CODFIL = NIPV.CODFIL
                                                            AND NFV.CODSNF = NIPV.CODSNF
                                                            AND NFV.NUMNFV = NIPV.NUMNFV
                                                            AND NFV.CODTRA = MOT.CODTRA
                                                            AND NFV.CODMTR = MOT.CODMTR) AS MOTORISTA,
                                                        1
                                          FROM NWMS_PRODUCAO.N0203REG NREG
                                         INNER JOIN NWMS_PRODUCAO.N0203IPV NIPV
                                            ON (NIPV.NUMREG = NREG.NUMREG)
                                         INNER JOIN SAPIENS.E140IPV IPV
                                            ON (NIPV.CODEMP = IPV.CODEMP
                                           AND IPV.CODFIL = NIPV.CODFIL
                                           AND IPV.CODSNF = NIPV.CODSNF
                                           AND IPV.NUMNFV = NIPV.NUMNFV )
                                         INNER JOIN NWMS_PRODUCAO.N0204MDV NMDV
                                            ON (NMDV.CODMDV = NIPV.CODMOT)
                                         INNER JOIN NWMS_PRODUCAO.N0204ORI ORI
                                            ON (ORI.CODORI = NIPV.ORIOCO)
                                         INNER JOIN NWMS_PRODUCAO.N0204ATD ATD
                                            ON (NREG.TIPATE = ATD.CODATD)
                                          LEFT JOIN SAPIENS.E440IPC IPC
                                            ON IPC.USU_NUMREG = NREG.NUMREG
                                           AND IPC.CODEMP = NIPV.CODEMP
                                           AND IPC.FILNFV = NIPV.CODFIL
                                           AND IPC.SNFNFV = NIPV.CODSNF
                                           AND IPC.NUMNFV = NIPV.NUMNFV
                                           AND IPC.SEQIPV = NIPV.SEQIPV
                                         INNER JOIN SAPIENS.E085CLI CLI
                                            ON (NREG.CODCLI = CLI.CODCLI)
                                         WHERE 1 = 1 ";
                if (situacao == "2")
                {
                    sql += "AND NOT EXISTS (SELECT 1 " +
                     "           FROM NWMS_PRODUCAO.N0203TRA TRA" +
                     "          WHERE TRA.NUMREG = NREG.NUMREG" +
                     "            AND TRA.DESTRA = 'REGISTRO DE OCORRENCIA PRÉ APROVADO'" +
                     "            AND TRA.CODORI IN (SELECT DORI1.CODORI" +
                     "          FROM NWMS_PRODUCAO.N0204DORI DORI1" +
                     "         WHERE DORI1.CODUSU = 76))";
                }

                sql += dataInicial != "" && dataFinal != "" ? "AND TO_DATE(TO_CHAR(NIPV.DATEMI, 'DD/MM/RRRR')) BETWEEN '" + DateTime.Parse(dataInicial).ToShortDateString() + "' AND '" + DateTime.Parse(dataFinal).ToShortDateString() + "' " : " ";
                sql += dataInicialOCR != "" && dataFinalOCR != "" ? "AND TO_DATE(TO_CHAR(NREG.DATGER, 'DD/MM/RRRR')) BETWEEN '" + DateTime.Parse(dataInicialOCR).ToShortDateString() + "' AND '" + DateTime.Parse(dataFinalOCR).ToShortDateString() + "' " : " ";
                sql += filial != "" && embarque != "" ? "AND PES.FILPED = '" + filial + "' AND PES.NUMANE = '" + embarque + "' " : " ";
                sql += codCliente != "" ? "AND CLI.CODCLI =" + codCliente : " ";
                sql += situacao != "" ? "AND NREG.SITREG =" + situacao : " ";
                sql += motivo != "" ? "AND NIPV.CODMOT =" + motivo : " ";
                sql += origem != "" ? "AND NIPV.ORIOCO =" + origem : " ";
                sql += tipo != "" ? "AND NREG.TIPATE =" + tipo : " ";
                sql += codPlaca != "" ? "AND NREG.PLACA = UPPER('" + codPlaca + "')" : "";
                sql += transportadora != "" ? "AND COALESCE(UPPER(MOT.NOMMOT),'TRANSPORTADORA') = 'TRANSPORTADORA'" : "";
                sql += @")GROUP BY        NUMREG,
                                          SITREG,
                                          DATGER,
                                          NOTA_ORIGEM,
                                          DATEMI,
                                          DESCORI,
                                          CODCLI,
                                          NOMCLI,
                                          PLACA,
                                          NUMANE,
                                          NUMPED,
                                          DATTRA,
                                          NOTA_ENTRADA,
                                          NOTA_FATURADA
                                   ORDER BY NUMREG DESC";

                OracleConnection conn = new OracleConnection(OracleStringConnection);

                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                //Gravar Arquivo
                OracleDataReader dr = cmd.ExecuteReader();

                List<RelatorioSinteticoOcorrencia> listaProtocolosPendentes = new List<RelatorioSinteticoOcorrencia>();
                RelatorioSinteticoOcorrencia itemProtocolo = new RelatorioSinteticoOcorrencia();
                decimal valorTotal = 0;
                while (dr.Read())
                {
                    itemProtocolo = new RelatorioSinteticoOcorrencia();
                    itemProtocolo.codigoProtocolo = Convert.ToInt64(dr["NUMREG"]);
                    itemProtocolo.filial = 1;
                    itemProtocolo.notaFiscal = Convert.ToInt64(dr["NOTA_ORIGEM"]);
                    itemProtocolo.codigoCliente = Convert.ToInt64(dr["CODCLI"]);
                    itemProtocolo.nomeCliente = itemProtocolo.codigoCliente + " - " + dr["NOMCLI"].ToString();
                    itemProtocolo.dataGeracao = dr["DATGER"].ToString();
                    itemProtocolo.status = dr["SITREG"].ToString() == "1" ? "Pendente" :
                        dr["SITREG"].ToString() == "2" ? "Aguardando Aprovação" :
                        dr["SITREG"].ToString() == "3" ? "Integrado" :
                        dr["SITREG"].ToString() == "4" ? "Aprovado" :
                        dr["SITREG"].ToString() == "5" ? "Reprovado" :
                        dr["SITREG"].ToString() == "6" ? "Reabilitado":
                        dr["SITREG"].ToString() == "8" ? "Coleta" :
                        dr["SITREG"].ToString() == "9" ? "Conferido" :
                        dr["SITREG"].ToString() == "10" ? "Faturado" :
                        dr["SITREG"].ToString() == "11" ? "Indenizado" :
                        dr["SITREG"].ToString() == "7" ? "Cancelado" : "";
                    itemProtocolo.valorTotal = dr["VALORLIQUIDO"].ToString();
                    valorTotal += Convert.ToDecimal(dr["VALORLIQUIDO"]); //aqui
                    itemProtocolo.valorTotalString = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", valorTotal);
                    itemProtocolo.embarque = dr["NUMANE"].ToString();
                    itemProtocolo.numeroPedido = dr["NUMPED"].ToString();
                    itemProtocolo.numeroNotaFiscalEntrada = dr["NOTA_ENTRADA"].ToString();
                    itemProtocolo.numeroNotaFiscalFaturada = dr["NOTA_FATURADA"].ToString();
                    itemProtocolo.dataSituacao = dr["DATTRA"].ToString();
                    listaProtocolosPendentes.Add(itemProtocolo);
                }

                dr.Close();
                conn.Close();
                return listaProtocolosPendentes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta os itens de troca
        /// </summary>
        /// <param name="codPlaca">Placa</param>
        /// <param name="datFaturamento">Data Faturamento</param>
        /// <returns></returns>
        public List<ItensTroca> ItensTroca(string codPlaca, string datFaturamento)
        {
            try
            {
                string sql = "";
                if(codPlaca == "AAA2222") {
                    sql = "SELECT DISTINCT REGEXP_SUBSTR(OCORRENCIA, SEPARADOR, 1, LEVEL) OCORRENCIA " +
                             "FROM(SELECT PED.USU_ALFREG AS OCORRENCIA, '[^,]+' SEPARADOR " +
                             "FROM SAPIENS.E140NFV NFV " +
                             "INNER JOIN SAPIENS.E140IPV IPV " +
                             "ON IPV.CODEMP = NFV.CODEMP " +
                             "AND IPV.CODFIL = NFV.CODFIL " +
                             "AND IPV.CODSNF = NFV.CODSNF " +
                             "AND IPV.NUMNFV = NFV.NUMNFV " +
                             "INNER JOIN SAPIENS.E120PED PED " +
                             "ON PED.CODEMP = IPV.CODEMP " +
                             "AND PED.CODFIL = IPV.FILPED " +
                             "AND PED.NUMPED = IPV.NUMPED " +
                             "INNER JOIN NWMS_PRODUCAO.N0203IPV IPVREG " +
                             "ON PED.TNSPRO IN('90126', '90111') " +
                             "INNER JOIN NWMS_PRODUCAO.N0203REG REG " +
                             "ON IPVREG.NUMREG = REG.NUMREG " +
                             "WHERE NFV.DATEMI BETWEEN TO_DATE(TO_CHAR('" + datFaturamento + "')) AND TO_DATE(TO_CHAR('" + datFaturamento + "')) " +
                             "AND (REG.SITREG = 2 OR REG.SITREG = 3 OR REG.SITREG = 4)) " +
                             "CONNECT BY REGEXP_SUBSTR(OCORRENCIA, SEPARADOR, 1, LEVEL) IS NOT NULL";
                }
                else
                {
                    sql = "SELECT DISTINCT REGEXP_SUBSTR(OCORRENCIA, SEPARADOR, 1, LEVEL) OCORRENCIA " +
                                 "FROM(SELECT PED.USU_ALFREG AS OCORRENCIA, '[^,]+' SEPARADOR " +
                                 "FROM SAPIENS.E140NFV NFV " +
                                 "INNER JOIN SAPIENS.E140IPV IPV " +
                                 "ON IPV.CODEMP = NFV.CODEMP " +
                                 "AND IPV.CODFIL = NFV.CODFIL " +
                                 "AND IPV.CODSNF = NFV.CODSNF " +
                                 "AND IPV.NUMNFV = NFV.NUMNFV " +
                                 "INNER JOIN SAPIENS.E120PED PED " +
                                 "ON PED.CODEMP = IPV.CODEMP " +
                                 "AND PED.CODFIL = IPV.FILPED " +
                                 "AND PED.NUMPED = IPV.NUMPED " +
                                 "INNER JOIN NWMS_PRODUCAO.N0203IPV IPVREG " +
                                 "ON PED.TNSPRO IN('90126', '90111') " +
                                 "INNER JOIN NWMS_PRODUCAO.N0203REG REG " +
                                 "ON IPVREG.NUMREG = REG.NUMREG " +
                                 "WHERE NFV.DATEMI BETWEEN TO_DATE(TO_CHAR('" + datFaturamento + "')) AND TO_DATE(TO_CHAR('" + datFaturamento + "')) " +
                                 "AND NFV.PLAVEI = '" + codPlaca + "'AND (REG.SITREG = 2 OR REG.SITREG = 3 OR REG.SITREG = 4)) " +
                                 "CONNECT BY REGEXP_SUBSTR(OCORRENCIA, SEPARADOR, 1, LEVEL) IS NOT NULL";
                }

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<ItensTroca> listaItensTroca = new List<ItensTroca>();
                ItensTroca itensTroca = new ItensTroca();

                ArrayList Lista = new ArrayList();
                int total = 0;

                while (dr.Read())
                {
                    Lista.Add(Convert.ToInt32(dr["OCORRENCIA"]));
                    total++;
                }

                conn.Close();
                dr.Close();

                for (int sequencial = 0; sequencial < total; sequencial++)
                {
                    string sql2 = "SELECT DISTINCT IPVREG.CODPRO, IPVREG.CODDER, IPVREG.CPLIPV, IPVREG.QTDDEV FROM NWMS_PRODUCAO.N0203IPV IPVREG WHERE IPVREG.NUMREG = " + Lista[sequencial];

                    OracleConnection conn1 = new OracleConnection(OracleStringConnection);
                    OracleCommand cmd1 = new OracleCommand(sql2, conn1);
                    cmd1.CommandType = CommandType.Text;
                    conn1.Open();
                    OracleDataReader dr2 = cmd1.ExecuteReader();
                    while (dr2.Read())
                    {
                        itensTroca = new ItensTroca();
                        itensTroca.CodProTroca = dr2["CODPRO"].ToString();
                        itensTroca.CodDerTroca = dr2["CODDER"].ToString();
                        itensTroca.DescProTroca = dr2["CPLIPV"].ToString();
                        itensTroca.QtdeDevolucaoTroca = Convert.ToInt64(dr2["QTDDEV"]);
                       listaItensTroca.Add(itensTroca);
                    }
                    dr2.Close();
                    conn1.Close();
                }
                
                return listaItensTroca;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta os Itens de coleta
        /// </summary>
        /// <param name="codPlaca">Placa</param>
        /// <returns></returns>
        public List<ItensColeta> ItensColeta(string codPlaca)
        {
            try
            {
                string sql = "    SELECT DISTINCT IPV.CODPRO," +
                  "                      IPV.CODDER," +
                  "                      IPV.CPLIPV," +
                  "                      IPV.QTDDEV," +
                  "                      REG.SITREG" +
                  "        FROM NWMS_PRODUCAO.N0203REG REG" +
                  "       INNER JOIN NWMS_PRODUCAO.N0203IPV IPV" +
                  "          ON IPV.NUMREG = REG.NUMREG" +
                  "       INNER JOIN NWMS_PRODUCAO.N0204POC POC" +
                  "          ON POC.NUMREG = REG.NUMREG" +
                  "       WHERE REG.SITREG = 8" +
                  "             AND POC.PLACA = '" + codPlaca + "'";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<ItensColeta> listaItensTroca = new List<ItensColeta>();
                ItensColeta itensTroca = new ItensColeta();

                while (dr.Read())
                {
                    itensTroca = new ItensColeta();
                    itensTroca.CodProTroca = dr["CODPRO"].ToString();
                    itensTroca.CodDerTroca = dr["CODDER"].ToString();
                    itensTroca.DescProTroca = dr["CPLIPV"].ToString();
                    itensTroca.QtdeDevolucaoTroca = Convert.ToInt64(dr["QTDDEV"]);
                    listaItensTroca.Add(itensTroca);
                }
                dr.Close();
                conn.Close();
                return listaItensTroca;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta todos os itens de troca para conferência
        /// </summary>
        /// <param name="codPlaca">Placa</param>
        /// <param name="datFaturamento">Data de Faturamento</param>
        /// <returns></returns>
        public List<ItensSinteticoTroca> ItensTrocaConferencia(string codPlaca, string datFaturamento, long? codCli)
        {
            try
            {
                String sql;
                if (codCli == null) {
                    codCli = 0;
                }

                if (codPlaca == "AAA2222") { 
                    sql = " SELECT " +
                             "    OCORRENCIA, " +
                             "    ANALISE, " +
                             "    CLIENTE, " +
                             "    NREG.SITREG, " +
                             "    TO_CHAR(NREG.DATGER,'DD/MM/RRRR') DATGER, " +
                             "    NIPV.NUMNFV, " +
                             "    TO_CHAR(ROUND(SUM((NIPV.VLRLIQ / NIPV.QTDFAT) * NIPV.QTDDEV), 2), " +
                             "    'FM999G999G999D90', " +
                             "    'nls_numeric_characters='',.''') VALORLIQUIDO" +
                             " FROM " +
                             "    NWMS_PRODUCAO.N0203IPV NIPV, " +
                             "    (" +
                             "         SELECT DISTINCT " +
                             "    REGEXP_SUBSTR(OCORRENCIA, SEPARADOR, 1, LEVEL) OCORRENCIA, " +
                             "    ANALISE     AS ANALISE, " +
                             "    CLIENTE     AS CLIENTE " +
                             " FROM " +
                             "    (" +
                             "     SELECT " +
                             "         PED.USU_ALFREG AS OCORRENCIA, " +
                             "         PFA.NUMANE     AS ANALISE, " +
                             "         CLI.NOMCLI     AS CLIENTE, " +
                             "         '[^,]+'       SEPARADOR " +
                             "     FROM " +
                             "         SAPIENS.E140NFV NFV " +
                             "     INNER JOIN " +
                             "         SAPIENS.E140IPV IPV " +
                             "     ON " +
                             "         IPV.CODEMP = NFV.CODEMP " +
                             "     AND IPV.CODFIL = NFV.CODFIL " +
                             "     AND IPV.CODSNF = NFV.CODSNF " +
                             "     AND IPV.NUMNFV = NFV.NUMNFV " +
                             "     INNER JOIN " +
                             "         SAPIENS.E085CLI CLI " +
                             "     ON " +
                             "         CLI.CODCLI = NFV.CODCLI " +
                             "     INNER JOIN " +
                             "         SAPIENS.E120IPD IPD " +
                             "     ON " +
                             "         IPD.CODEMP = IPV.CODEMP " +
                             "     AND IPD.CODFIL = IPV.FILPED " +
                             "     AND IPD.NUMPED = IPV.NUMPED " +
                             "     AND IPD.SEQIPD = IPV.SEQIPD " +
                             "     INNER JOIN " +
                             "         SAPIENS.E120PED PED " +
                             "     ON " +
                             "         PED.CODEMP = IPD.CODEMP " +
                             "     AND PED.CODFIL = IPD.CODFIL " +
                             "     AND PED.NUMPED = IPD.NUMPED " +
                             "     AND PED.TNSPRO IN ('90126', '90111') " +
                             "     LEFT JOIN " +
                             "         SAPIENS.E135PFA PFA " +
                             "     ON " +
                             "         IPV.CODEMP = PFA.CODEMP " +
                             "    AND IPV.CODFIL = PFA.FILNFV " +
                             "    AND IPV.CODSNF = PFA.SNFNFV " +
                             "    AND IPV.NUMNFV = PFA.NUMNFV     " +
                             "    WHERE " +
                             "        NFV.DATEMI BETWEEN TO_DATE(TO_CHAR('" + datFaturamento + "')) AND TO_DATE(TO_CHAR('" + datFaturamento + "')) " +
                             "    AND NFV.PLAVEI = ' ' " +
                             "    AND CLI.CODCLI = " + codCli + 
                             "    AND NFV.SITNFV = '2')CONNECT BY REGEXP_SUBSTR(OCORRENCIA, SEPARADOR, 1, LEVEL) IS NOT NULL) " +
                             "    INNER JOIN " +
                             "        NWMS_PRODUCAO.N0203REG NREG " +
                             "    ON " +
                             "        NREG.NUMREG = OCORRENCIA " +
                             "    WHERE " +
                             "        NIPV.NUMREG = OCORRENCIA " +
                             "    GROUP BY " +
                             "        OCORRENCIA, " +
                             "        ANALISE, " +
                             "        CLIENTE, " +
                             "        NREG.SITREG, " +
                             "        NREG.DATGER, " +
                             "        NIPV.NUMNFV ";
                }
                else { 
                    sql = " SELECT " + 
                             "    OCORRENCIA, " +
                             "    ANALISE, " +
                             "    CLIENTE, " +
                             "    NREG.SITREG, " +
                             "    TO_CHAR(NREG.DATGER,'DD/MM/RRRR') DATGER, " +
                             "    NIPV.NUMNFV, " +
                             "    TO_CHAR(ROUND(SUM((NIPV.VLRLIQ / NIPV.QTDFAT) * NIPV.QTDDEV), 2), " +
                             "    'FM999G999G999D90', " +
                             "    'nls_numeric_characters='',.''') VALORLIQUIDO" +
                             " FROM " + 
                             "    NWMS_PRODUCAO.N0203IPV NIPV, " +
                             "    (" + 
                             "         SELECT DISTINCT " +
                             "    REGEXP_SUBSTR(OCORRENCIA, SEPARADOR, 1, LEVEL) OCORRENCIA, " +
                             "    ANALISE     AS ANALISE, " +
                             "    CLIENTE     AS CLIENTE " +
                             " FROM " + 
                             "    (" +
                             "     SELECT " +
                             "         PED.USU_ALFREG AS OCORRENCIA, " +
                             "         PFA.NUMANE     AS ANALISE, " +
                             "         CLI.NOMCLI     AS CLIENTE, " +
                             "         '[^,]+'       SEPARADOR " + 
                             "     FROM " + 
                             "         SAPIENS.E140NFV NFV " +
                             "     INNER JOIN " + 
                             "         SAPIENS.E140IPV IPV " +
                             "     ON " + 
                             "         IPV.CODEMP = NFV.CODEMP " +
                             "     AND IPV.CODFIL = NFV.CODFIL " +
                             "     AND IPV.CODSNF = NFV.CODSNF " +
                             "     AND IPV.NUMNFV = NFV.NUMNFV " +
                             "     INNER JOIN " + 
                             "         SAPIENS.E085CLI CLI " +
                             "     ON " + 
                             "         CLI.CODCLI = NFV.CODCLI " +
                             "     INNER JOIN " + 
                             "         SAPIENS.E120IPD IPD " +
                             "     ON " + 
                             "         IPD.CODEMP = IPV.CODEMP " +
                             "     AND IPD.CODFIL = IPV.FILPED " +
                             "     AND IPD.NUMPED = IPV.NUMPED " +
                             "     AND IPD.SEQIPD = IPV.SEQIPD " +
                             "     INNER JOIN " + 
                             "         SAPIENS.E120PED PED " +
                             "     ON " + 
                             "         PED.CODEMP = IPD.CODEMP " +
                             "     AND PED.CODFIL = IPD.CODFIL " +
                             "     AND PED.NUMPED = IPD.NUMPED " +
                             "     AND PED.TNSPRO IN ('90126', '90111') " +
                             "     LEFT JOIN " + 
                             "         SAPIENS.E135PFA PFA " +
                             "     ON " + 
                             "         IPV.CODEMP = PFA.CODEMP " +
                             "    AND IPV.CODFIL = PFA.FILNFV " +
                             "    AND IPV.CODSNF = PFA.SNFNFV " +
                             "    AND IPV.NUMNFV = PFA.NUMNFV     " +
                             "    WHERE " + 
                             "        NFV.DATEMI BETWEEN TO_DATE(TO_CHAR('" + datFaturamento + "')) AND TO_DATE(TO_CHAR('" + datFaturamento + "')) " +
                             "    AND NFV.PLAVEI = '" + codPlaca + "' " +
                             "    AND NFV.SITNFV = '2')CONNECT BY REGEXP_SUBSTR(OCORRENCIA, SEPARADOR, 1, LEVEL) IS NOT NULL) " +
                             "    INNER JOIN " + 
                             "        NWMS_PRODUCAO.N0203REG NREG " +
                             "    ON " + 
                             "        NREG.NUMREG = OCORRENCIA " +
                             "    WHERE " + 
                             "        NIPV.NUMREG = OCORRENCIA " +
                             "    GROUP BY " + 
                             "        OCORRENCIA, " +
                             "        ANALISE, " +
                             "        CLIENTE, " +
                             "        NREG.SITREG, " +
                             "        NREG.DATGER, " +
                             "        NIPV.NUMNFV ";
                }

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<ItensSinteticoTroca> listaItensTroca = new List<ItensSinteticoTroca>();
                ItensSinteticoTroca itensTroca = new ItensSinteticoTroca();
                decimal somaTotalTroca = 0;

                while (dr.Read())
                {
                    itensTroca = new ItensSinteticoTroca();
                    itensTroca.codigoProtocolo = Convert.ToInt64(dr["OCORRENCIA"].ToString().Replace(",", ""));
                    itensTroca.notaFiscal = Convert.ToInt64(dr["NUMNFV"]);
                    itensTroca.nomeCliente = dr["CLIENTE"].ToString();
                    itensTroca.status = dr["SITREG"].ToString() == "1" ? "Pendente" :
                       dr["SITREG"].ToString() == "2" ? "Aguardando Aprovação" :
                       dr["SITREG"].ToString() == "3" ? "Integrado" :
                       dr["SITREG"].ToString() == "4" ? "Aprovado" :
                       dr["SITREG"].ToString() == "5" ? "Reprovado" :
                       dr["SITREG"].ToString() == "8" ? "Coleta" :
                       dr["SITREG"].ToString() == "6" ? "Reabilitado":
                       dr["SITREG"].ToString() == "9" ? "Conferido" :
                       dr["SITREG"].ToString() == "10" ? "Faturado" :
                       dr["SITREG"].ToString() == "11" ? "Indenizado" :
                       dr["SITREG"].ToString() == "7" ? "Cancelado" : "";
                    itensTroca.dataGeracao = dr["DATGER"].ToString();
                    itensTroca.embarque = dr["ANALISE"].ToString();
                    somaTotalTroca += (decimal)Convert.ToDouble(dr["VALORLIQUIDO"]);
                    itensTroca.quantidade = somaTotalTroca.ToString("###,###,##0.00");
                    itensTroca.valorTotal = dr["VALORLIQUIDO"].ToString();
                    listaItensTroca.Add(itensTroca);
                }
                dr.Close();
                conn.Close();
                return listaItensTroca;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Valida se a placa existe
        /// </summary>
        /// <param name="codPlaca">Placa</param>
        /// <returns>PLACA</returns>
        public bool validarPlaca(string codPlaca)
        {
            try
            {
                string sql = "SELECT COUNT(PLAVEI) AS LINHA FROM SAPIENS.E073VEI WHERE PLAVEI = '" + codPlaca.ToUpper() + "'";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                OracleDataReader dr2 = cmd.ExecuteReader();

                if (dr2.Read())
                {
                    return Convert.ToInt64(dr2["LINHA"]) > 0 ? false : true;
                }

                dr2.Close();
                conn.Close();
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca as informações dos itens da carga para conferência
        /// </summary>
        /// <param name="codPlaca">Placa</param>
        /// <param name="datFaturamento">Data de Faturamento</param>
        /// <returns>listaItensCarga</returns>
        public List<ItensSinteticoCarga> ItensCargaConferencia(string codPlaca, string datFaturamento, long ? codigoCliente)
        {
            try
            {
                String sql = null;
                if (codigoCliente != null)
                {
                    sql = "  SELECT DISTINCT REG.NUMREG, IPV.NUMNFV, CLI.NOMCLI, REG.SITREG,  TO_CHAR(REG.DATGER,'DD/MM/RRRR') DATGER, PFA.NUMANE,TO_CHAR(ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV), 2), 'FM999G999G999D90', 'nls_numeric_characters='',.''') VALORLIQUIDO" +
                        "    FROM NWMS_PRODUCAO.N0203REG REG" +
                        "   INNER JOIN NWMS_PRODUCAO.N0203IPV IPV" +
                        "      ON IPV.NUMREG = REG.NUMREG" +
                        "    LEFT JOIN SAPIENS.E135PFA PFA" +
                        "        ON IPV.CODEMP = PFA.CODEMP" +
                        "           AND IPV.CODFIL = PFA.FILNFV" +
                        "           AND IPV.CODSNF = PFA.SNFNFV" +
                        "           AND IPV.NUMNFV = PFA.NUMNFV" +
                        "     INNER JOIN SAPIENS.E085CLI CLI" +
                        "        ON (REG.CODCLI = CLI.CODCLI)" +
                        "     INNER JOIN SAPIENS.E140NFV NFV" +
                        "       ON IPV.CODEMP = NFV.CODEMP" +
                        "          AND IPV.CODFIL = NFV.CODFIL" +
                        "          AND IPV.CODSNF = NFV.CODSNF" +
                        "          AND IPV.NUMNFV = NFV.NUMNFV" +
                        "   WHERE (NFV.PLAVEI = '" + codPlaca.ToUpper() + "'" + "OR NFV.PLAVEI =' ')" + 
                        "         AND IPV.DATEMI >= TO_DATE('" + datFaturamento + "', 'DD/MM/YYYY')" +
                        "         AND IPV.DATEMI <= TO_DATE('" + datFaturamento + "','DD/MM/YYYY')" +
                        "         AND REG.CODCLI = " + codigoCliente +  
                        "         AND REG.TIPATE = 1" +
                        "        GROUP BY REG.NUMREG," +
                        "                 IPV.NUMNFV," +
                        "                 CLI.NOMCLI," +
                        "                 REG.SITREG," +
                        "                 REG.DATGER," +
                        "                 PFA.NUMANE";
                }
                else { 

                    sql = "  SELECT DISTINCT REG.NUMREG, IPV.NUMNFV, CLI.NOMCLI, REG.SITREG,  TO_CHAR(REG.DATGER,'DD/MM/RRRR') DATGER, PFA.NUMANE,TO_CHAR(ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV), 2), 'FM999G999G999D90', 'nls_numeric_characters='',.''') VALORLIQUIDO" +
                        "    FROM NWMS_PRODUCAO.N0203REG REG" +
                        "   INNER JOIN NWMS_PRODUCAO.N0203IPV IPV" +
                        "      ON IPV.NUMREG = REG.NUMREG" +
                        "    LEFT JOIN SAPIENS.E135PFA PFA" +
                        "        ON IPV.CODEMP = PFA.CODEMP" +
                        "           AND IPV.CODFIL = PFA.FILNFV" +
                        "           AND IPV.CODSNF = PFA.SNFNFV" +
                        "           AND IPV.NUMNFV = PFA.NUMNFV" +
                        "     INNER JOIN SAPIENS.E085CLI CLI" +
                        "        ON (REG.CODCLI = CLI.CODCLI)" +
                        "     INNER JOIN SAPIENS.E140NFV NFV" +
                        "       ON IPV.CODEMP = NFV.CODEMP" +
                        "          AND IPV.CODFIL = NFV.CODFIL" +
                        "          AND IPV.CODSNF = NFV.CODSNF" +
                        "          AND IPV.NUMNFV = NFV.NUMNFV" +
                        "   WHERE NFV.PLAVEI = '" + codPlaca.ToUpper() + "'" +
                        "         AND IPV.DATEMI >= TO_DATE('" + datFaturamento + "', 'DD/MM/YYYY')" +
                        "         AND IPV.DATEMI <= TO_DATE('" + datFaturamento + "','DD/MM/YYYY')" +
                        "         AND REG.TIPATE = 1" +
                        "        GROUP BY REG.NUMREG," +
                        "                 IPV.NUMNFV," +
                        "                 CLI.NOMCLI," +
                        "                 REG.SITREG," +
                        "                 REG.DATGER," +
                        "                 PFA.NUMANE";

                }

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<ItensSinteticoCarga> listaItensCarga = new List<ItensSinteticoCarga>();
                ItensSinteticoCarga itensCarga = new ItensSinteticoCarga();
                decimal somaValorTotal = 0;
                
                while (dr.Read())
                {
                    itensCarga = new ItensSinteticoCarga();
                    itensCarga.codigoProtocolo = Convert.ToInt64(dr["NUMREG"]);
                    itensCarga.notaFiscal = Convert.ToInt64(dr["NUMNFV"]);
                    itensCarga.nomeCliente = dr["NOMCLI"].ToString();
                    itensCarga.status = dr["SITREG"].ToString() == "1" ? "Pendente" :
                        dr["SITREG"].ToString() == "2" ? "Aguardando Aprovação" :
                        dr["SITREG"].ToString() == "3" ? "Integrado" :
                        dr["SITREG"].ToString() == "4" ? "Aprovado" :
                        dr["SITREG"].ToString() == "5" ? "Reprovado" :
                        dr["SITREG"].ToString() == "6" ? "Reabilitado":
                        dr["SITREG"].ToString() == "8" ? "Coleta" :
                        dr["SITREG"].ToString() == "9" ? "Conferido" :
                        dr["SITREG"].ToString() == "10" ? "Faturado" :
                        dr["SITREG"].ToString() == "11" ? "Indenizado" :
                        dr["SITREG"].ToString() == "7" ? "Cancelado" : "";
                    itensCarga.dataGeracao = dr["DATGER"].ToString();
                    itensCarga.embarque = dr["NUMANE"].ToString();
                    //itensCarga.valorTotal = Convert.ToDouble(dr["VALORLIQUIDO"]).ToString("###,###,##0.00");
                    itensCarga.valorTotal = (dr["VALORLIQUIDO"].ToString().Replace(".", "@"));
                    itensCarga.PlacaCaminhao = codPlaca.ToUpper().ToString().Substring(0, 3) + "-" + codPlaca.ToUpper().ToString().Substring(3, 4);
                    itensCarga.DataFaturamento = datFaturamento.ToString();
                    somaValorTotal += (decimal)Convert.ToDouble(dr["VALORLIQUIDO"]);
                    itensCarga.quantidade = somaValorTotal.ToString("###,###,##0.00");
                    listaItensCarga.Add(itensCarga);
                }
                dr.Close();
                conn.Close();
                return listaItensCarga;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca informações dos Itens de Coleta para Conferência
        /// </summary>
        /// <param name="codPlaca">Placa</param>
        /// <param name="datFaturamento">Data de Faturamento</param>
        /// <returns></returns>
        public List<ItensSinteticoColeta> ItensColetaConferencia(string codPlaca, string datFaturamento)
        {
            try
            {
                string sql = "  SELECT DISTINCT REG.NUMREG, IPV.NUMNFV, CLI.NOMCLI, REG.SITREG,  TO_CHAR(REG.DATGER,'DD/MM/RRRR') DATGER, PFA.NUMANE,TO_CHAR(ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV),2), 'FM999G999G999D90',   'nls_numeric_characters='',.''') VALORLIQUIDO" +
                             "    FROM NWMS_PRODUCAO.N0203REG REG" +
                             "   INNER JOIN NWMS_PRODUCAO.N0203IPV IPV" +
                             "      ON IPV.NUMREG = REG.NUMREG" +
                             "    LEFT JOIN SAPIENS.E135PFA PFA" +
                             "        ON IPV.CODEMP = PFA.CODEMP" +
                             "           AND IPV.CODFIL = PFA.FILNFV" +
                             "           AND IPV.CODSNF = PFA.SNFNFV" +
                             "           AND IPV.NUMNFV = PFA.NUMNFV" +
                             "     INNER JOIN SAPIENS.E085CLI CLI" +
                             "        ON (REG.CODCLI = CLI.CODCLI)" +
                              "     INNER JOIN NWMS_PRODUCAO.N0204POC POC" +
                             "        ON (POC.NUMREG = REG.NUMREG)" +
                             "   WHERE POC.PLACA = '" + codPlaca.ToUpper() + "' AND POC.SITPOC = 1" +
                             "         AND REG.SITREG IN (8)" +
                             "        GROUP BY REG.NUMREG," +
                             "                 IPV.NUMNFV," +
                             "                 CLI.NOMCLI," +
                             "                 REG.SITREG," +
                             "                 REG.DATGER," +
                             "                 PFA.NUMANE";
                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<ItensSinteticoColeta> listaItensColeta = new List<ItensSinteticoColeta>();
                ItensSinteticoColeta itensColeta = new ItensSinteticoColeta();
                decimal somaTotalColeta = 0;

                while (dr.Read())
                {
                    itensColeta = new ItensSinteticoColeta();
                    itensColeta.codigoProtocolo = Convert.ToInt64(dr["NUMREG"]);
                    itensColeta.notaFiscal = Convert.ToInt64(dr["NUMNFV"]);
                    itensColeta.nomeCliente = dr["NOMCLI"].ToString();
                    itensColeta.status = dr["SITREG"].ToString() == "1" ? "Pendente" :
                        dr["SITREG"].ToString() == "2" ? "Aguardando Aprovação" :
                        dr["SITREG"].ToString() == "3" ? "Integrado" :
                        dr["SITREG"].ToString() == "4" ? "Aprovado" :
                        dr["SITREG"].ToString() == "5" ? "Reprovado" :
                        dr["SITREG"].ToString() == "6" ? "Reabilitado":
                        dr["SITREG"].ToString() == "8" ? "Coleta" :
                        dr["SITREG"].ToString() == "9" ? "Conferido" :
                        dr["SITREG"].ToString() == "10" ? "Faturado" :
                        dr["SITREG"].ToString() == "11" ? "Indenizado" :
                        dr["SITREG"].ToString() == "7" ? "Cancelado" : "";
                    itensColeta.dataGeracao = dr["DATGER"].ToString();
                    somaTotalColeta += (decimal)Convert.ToDouble(dr["VALORLIQUIDO"]);
                    itensColeta.quantidade = somaTotalColeta.ToString("###,###,##0.00");
                    itensColeta.embarque = dr["NUMANE"].ToString();
                    itensColeta.valorTotal = dr["VALORLIQUIDO"].ToString().Replace(".", "@");
                    listaItensColeta.Add(itensColeta);
                }
                dr.Close();
                conn.Close();
                return listaItensColeta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca informações dos itens da ocorrência para conferência
        /// </summary>
        /// <param name="codPlaca">Placa</param>
        /// <param name="datFaturamento">Data de Faturamento</param>
        /// <returns></returns>
        public List<N0203REG> ItensCargaConferenciaN0203REG(string codPlaca, string datFaturamento)
        {
            try
            {
                string sql = "  SELECT DISTINCT REG.NUMREG, IPV.NUMNFV, CLI.NOMCLI, REG.SITREG,  TO_CHAR(REG.DATGER,'DD/MM/RRRR') DATGER, PFA.NUMANE,TO_CHAR(ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV),2), 'FM999G999G999D90',   'nls_numeric_characters='',.''') VALORLIQUIDO" +
                             "    FROM NWMS_PRODUCAO.N0203REG REG" +
                             "   INNER JOIN NWMS_PRODUCAO.N0203IPV IPV" +
                             "      ON IPV.NUMREG = REG.NUMREG" +
                             "    LEFT JOIN SAPIENS.E135PFA PFA" +
                             "        ON IPV.CODEMP = PFA.CODEMP" +
                             "           AND IPV.CODFIL = PFA.FILNFV" +
                             "           AND IPV.CODSNF = PFA.SNFNFV" +
                             "           AND IPV.NUMNFV = PFA.NUMNFV" +
                             "     INNER JOIN SAPIENS.E085CLI CLI" +
                             "        ON (REG.CODCLI = CLI.CODCLI)" +
                             "     INNER JOIN SAPIENS.E140NFV NFV" +
                             "       ON IPV.CODEMP = NFV.CODEMP" +
                             "          AND IPV.CODFIL = NFV.CODFIL" +
                             "          AND IPV.CODSNF = NFV.CODSNF" +
                             "          AND IPV.NUMNFV = NFV.NUMNFV" +
                             "   WHERE NFV.PLAVEI = '" + codPlaca.ToUpper() + "'" +
                             "         AND IPV.DATEMI >= TO_DATE('" + datFaturamento + "', 'DD/MM/YYYY')" +
                             "         AND IPV.DATEMI <= TO_DATE('" + datFaturamento + "','DD/MM/YYYY')" +
                             "         AND REG.SITREG IN (4)" +
                             "        GROUP BY REG.NUMREG," +
                             "                 IPV.NUMNFV," +
                             "                 CLI.NOMCLI," +
                             "                 REG.SITREG," +
                             "                 REG.DATGER," +
                             "                 PFA.NUMANE";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<N0203REG> listaItensCarga = new List<N0203REG>();
                N0203REG itensCarga = new N0203REG();

                while (dr.Read())
                {
                    itensCarga = new N0203REG();
                    itensCarga.NUMREG = Convert.ToInt64(dr["NUMREG"]);
                    //itensCarga.N0203IPV = Convert.ToInt64(dr["NUMNFV"]);
                    //itensCarga.C = dr["NOMCLI"].ToString();
                    itensCarga.SITREG = Convert.ToInt64(dr["SITREG"]);
                    itensCarga.DATGER = Convert.ToDateTime(dr["DATGER"]);
                    //itensCarga.NUMANE = dr["NUMANE"].ToString();
                    //  itensCarga.VAL = dr["VALORLIQUIDO"].ToString();
                    listaItensCarga.Add(itensCarga);
                }
                dr.Close();
                conn.Close();
                return listaItensCarga;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cria o Relatório sintetico
        /// </summary>
        /// <param name="codFilial">Código da Filial</param>
        /// <param name="numAneEmbarque">Número da Embalagem</param>
        /// <param name="codPlaca">Placa</param>
        /// <param name="dataInicial">Data Inicial</param>
        /// <param name="dataFinal">Data Final</param>
        /// <param name="tipoPesquisa">Tipo Pesquisa</param>
        /// <param name="dataFaturamento">Data Faturamento</param>
        /// <returns></returns>
        public List<RelatorioSintetico> RelatorioSintetico(long? codFilial, long? numAneEmbarque, string codPlaca, DateTime? dataInicial, DateTime? dataFinal, int tipoPesquisa, DateTime? dataFaturamento, long? codigoCliente)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var situacaoFechado = (long)Enums.SituacaoRegistroOcorrencia.Fechado;
                    var situacaoAprovado = (long)Enums.SituacaoRegistroOcorrencia.Aprovado;
                    var situacaoPreAprovado = (long)Enums.SituacaoRegistroOcorrencia.PreAprovado;
                    string analiseEmbarqueTexto = string.Empty;

                    // Seta Lista
                    var listaRegistros = (from r in contexto.N0203REG
                                          join c in contexto.N0203IPV on new { r.NUMREG } equals new { c.NUMREG }
                                          //join o in contexto.N0204MDO on new { c.ORIOCO } equals new { o.CODORI }
                                          //join m in contexto.N0204MDV on new { c.CODMDV } equals new { m.CODMDV }
                                          where r.NUMREG == null
                                          select new RelatorioSintetico { }).ToList();

                    List<int> listaTiposPesquisa = new List<int>();
                    listaTiposPesquisa.Add((int)Enums.TipoPesquisaRegistroOcorrencia.AnaliseEmbarque);
                    listaTiposPesquisa.Add((int)Enums.TipoPesquisaRegistroOcorrencia.Placa_Periodo);
                    listaTiposPesquisa.Add((int)Enums.TipoPesquisaRegistroOcorrencia.Placa_Data_Faturamento);

                    if (listaTiposPesquisa.Contains(tipoPesquisa))
                    {
                        // Seta Lista
                        var listaProtocolos = (from a in contexto.N0203REG
                                               join b in contexto.N0203IPV on new { a.NUMREG } equals new { b.NUMREG }
                                               where a.NUMREG == 0
                                               select new { a.NUMREG, a.SITREG }).ToList();
                        
                        if (tipoPesquisa == (int)Enums.TipoPesquisaRegistroOcorrencia.AnaliseEmbarque)
                        {
                            // Recupera os protocolos da filial e analise de embarque informados
                            if (codFilial == 1)
                            {
                                listaProtocolos = (from a in contexto.N0203REG
                                                   join b in contexto.N0203IPV on new { a.NUMREG } equals new { b.NUMREG }
                                                   where (b.CODFIL == codFilial && b.NUMANE == numAneEmbarque) || (b.CODFIL == 101 && b.NUMANE_REL == numAneEmbarque) && (a.SITREG == situacaoFechado || a.SITREG == situacaoAprovado || a.SITREG == situacaoPreAprovado)
                                                   group new { a } by new { a.NUMREG, a.SITREG } into grupo
                                                   orderby grupo.Key.NUMREG
                                                   select new { grupo.Key.NUMREG, grupo.Key.SITREG }).ToList();
                            }
                            else
                            {
                                // Se filial == 101 
                                // Se houver algum protocolo somente com notas da 101 e outro somente com notas da filial 1 e ambas as analises de embarque estiverem relacionadas (Sapiens), recupera a analise de embarque relacionada da filial 1 para pesquisar os protocolos de ambas
                                var codAneRel = contexto.N0203IPV.Where(c => c.CODFIL == codFilial && c.NUMANE == numAneEmbarque).Select(c => c.NUMANE_REL).FirstOrDefault();
                                listaProtocolos = (from a in contexto.N0203REG
                                                   join b in contexto.N0203IPV on new { a.NUMREG } equals new { b.NUMREG }
                                                   where (b.CODFIL == codFilial && b.NUMANE == numAneEmbarque) || (b.CODFIL == 1 && b.NUMANE == codAneRel) && (a.SITREG == situacaoFechado || a.SITREG == situacaoAprovado || a.SITREG == situacaoPreAprovado)
                                                   group new { a } by new { a.NUMREG, a.SITREG } into grupo
                                                   orderby grupo.Key.NUMREG
                                                   select new { grupo.Key.NUMREG, grupo.Key.SITREG }).ToList();
                            }
                        }
                        else if (tipoPesquisa == (int)Enums.TipoPesquisaRegistroOcorrencia.Placa_Data_Faturamento)
                        {
                            // Recupera os protocolos da placa e período informados
                            listaProtocolos = (from a in contexto.N0203REG
                                               join b in contexto.N0203IPV on new { a.NUMREG } equals new { b.NUMREG }
                                               where a.PLACA == codPlaca && a.DATGER >= dataInicial && a.DATGER <= dataFinal && (a.SITREG == situacaoFechado || a.SITREG == situacaoAprovado || a.SITREG == situacaoPreAprovado)
                                               group new { a } by new { a.NUMREG, a.SITREG } into grupo
                                               orderby grupo.Key.NUMREG
                                               select new { grupo.Key.NUMREG, grupo.Key.SITREG }).ToList();
                        }
                        else if (tipoPesquisa == (int)Enums.TipoPesquisaRegistroOcorrencia.Placa_Periodo)
                        {
                            DateTime dataFatIni = DateTime.Parse(dataFaturamento.ToString()).AddDays(-1);
                            DateTime dataFatFim = DateTime.Parse(dataFaturamento.ToString()).AddDays(1);

                            // Recupera os protocolos da placa e data de faturamento informados
                            if (codPlaca == "AAA2222")
                            {
                                listaProtocolos = (from a in contexto.N0203REG
                                                   join b in contexto.N0203IPV on new { a.NUMREG } equals new { b.NUMREG }
                                                   where a.PLACA == null && b.DATEMI >= dataFatIni && b.DATEMI <= dataFatFim && (a.SITREG == situacaoFechado || a.SITREG == situacaoAprovado || a.SITREG == situacaoPreAprovado)
                                                   group new { a } by new { a.NUMREG, a.SITREG } into grupo
                                                   orderby grupo.Key.NUMREG
                                                   select new { grupo.Key.NUMREG, grupo.Key.SITREG }).ToList();
                            }
                            else {
                                
                                listaProtocolos = (from a in contexto.N0203REG
                                                   join b in contexto.N0203IPV on new { a.NUMREG } equals new { b.NUMREG }
                                                   where a.PLACA == codPlaca && b.DATEMI >= dataFatIni && b.DATEMI <= dataFatFim && (a.SITREG == situacaoFechado || a.SITREG == situacaoAprovado || a.SITREG == situacaoPreAprovado)
                                                   group new { a } by new { a.NUMREG, a.SITREG } into grupo
                                                   orderby grupo.Key.NUMREG
                                                   select new { grupo.Key.NUMREG, grupo.Key.SITREG }).ToList();
                            }
                        }

                        if (listaProtocolos.Count > 0)
                        {
                            var auxlistaProtocolos = (from a in listaProtocolos
                                                      select a.NUMREG).ToList();

                            // Seleciona todas as analises de embarque dos protocolos acima
                            var listaAnalises = (from b in contexto.N0203IPV
                                                 where auxlistaProtocolos.Contains(b.NUMREG)
                                                 group b by new { b.CODFIL, NUMANE = b.NUMANE } into grupo
                                                 orderby grupo.Key.CODFIL, grupo.Key.NUMANE
                                                 select new { grupo.Key.CODFIL, grupo.Key.NUMANE }).ToList();

                            foreach (var itemAnalise in listaAnalises)
                            {
                                // Busca itens de cada filial e analise 
                                var listaItemReg = (from a in contexto.N0203REG
                                                    join b in contexto.N0203IPV on a.NUMREG equals b.NUMREG
                                                    join c in contexto.N0204MDV on b.CODMOT equals c.CODMDV
                                                    where b.CODFIL == itemAnalise.CODFIL && b.NUMANE == itemAnalise.NUMANE &&  //c.CODMDV == b.CODMOT &&
                                                    (a.SITREG == situacaoFechado || a.SITREG == situacaoAprovado || a.SITREG == situacaoPreAprovado)
                                                    group new { m = b } by new { b.CODPRO, b.CODDER, b.CPLIPV, b.CODMOT, b.QTDFAT, c.DESCMDV } into grupo
                                                    select new RelatorioSintetico
                                                    {
                                                        NumAnaliseEmb = string.Empty,
                                                        CodPro = grupo.Key.CODPRO,
                                                        CodDer = grupo.Key.CODDER,
                                                        DescPro = grupo.Key.CPLIPV,
                                                        CodMotivoDevolucao = grupo.Key.CODMOT,
                                                        DescMotivoDevolucao = grupo.Key.DESCMDV,
                                                        quantidade = grupo.Key.QTDFAT,
                                                        QtdeDevolucao = grupo.Sum(d => d.m.QTDDEV),

                                                    }).ToList();
                                if (codigoCliente != null) { 
                                     listaItemReg = (from a in contexto.N0203REG
                                         join b in contexto.N0203IPV on a.NUMREG equals b.NUMREG
                                         join c in contexto.N0204MDV on b.CODMOT equals c.CODMDV
                                       where b.CODFIL == itemAnalise.CODFIL && b.NUMANE == itemAnalise.NUMANE && a.CODCLI == codigoCliente  && //c.CODMDV == b.CODMOT &&
                                         (a.SITREG == situacaoFechado || a.SITREG == situacaoAprovado || a.SITREG == situacaoPreAprovado)
                                         group new { m = b } by new { b.CODPRO, b.CODDER, b.CPLIPV, b.CODMOT, b.QTDFAT, c.DESCMDV } into grupo
                                        select new RelatorioSintetico
                                         {
                                             NumAnaliseEmb = string.Empty,
                                             CodPro = grupo.Key.CODPRO,
                                             CodDer = grupo.Key.CODDER,
                                            DescPro = grupo.Key.CPLIPV,
                                             CodMotivoDevolucao = grupo.Key.CODMOT,
                                             DescMotivoDevolucao = grupo.Key.DESCMDV,
                                             quantidade = grupo.Key.QTDFAT,
                                             QtdeDevolucao = grupo.Sum(d => d.m.QTDDEV),
 
                                         }).ToList();
                                }
                                
                                if (listaItemReg.Count > 0)
                                {
                                    analiseEmbarqueTexto = analiseEmbarqueTexto + itemAnalise.CODFIL.ToString() + " - ( " + itemAnalise.NUMANE.ToString() + " ) ";
                                    listaRegistros.AddRange(listaItemReg);
                                }
                            }

                            listaRegistros = (from m in listaRegistros
                                              group m by new { m.CodPro, m.CodDer, m.DescPro, m.CodMotivoDevolucao, m.DescMotivoDevolucao } into grupo
                                              orderby grupo.Key.CodPro, grupo.Key.CodDer, grupo.Key.CodMotivoDevolucao
                                              select new RelatorioSintetico
                                              {
                                                  NumAnaliseEmb = string.Empty,
                                                  CodPro = grupo.Key.CodPro,
                                                  CodDer = grupo.Key.CodDer,
                                                  DescPro = grupo.Key.DescPro,
                                                  CodMotivoDevolucao = grupo.Key.CodMotivoDevolucao,
                                                  DescMotivoDevolucao = grupo.Key.DescMotivoDevolucao,
                                                  QtdeDevolucao = grupo.Sum(d => d.QtdeDevolucao)

                                              }).ToList();

                            var codReg = listaProtocolos[0].NUMREG;
                            // Busca Data de Faturamento dos protocolos
                            var dadosPro = (from a in contexto.N0203REG
                                            join b in contexto.N0203IPV on new { a.NUMREG } equals new { b.NUMREG }
                                            where a.NUMREG == codReg
                                            select new { a.CODMOT, a.PLACA, b.DATEMI }).FirstOrDefault();

                            listaRegistros[0].NumAnaliseEmb = analiseEmbarqueTexto;
                            listaRegistros[0].DataFaturamento = dadosPro.DATEMI.ToShortDateString();

                            if (listaProtocolos.Where(c => c.SITREG == situacaoFechado).Count() > 0)
                            {
                                listaRegistros[0].ExisteProtocoloFechado = true;
                            }

                            // Dados Motorista
                            if (dadosPro.CODMOT != 0)
                            {
                                listaRegistros[0].PlacaCaminhao = dadosPro.PLACA.Substring(0, 3) + "-" + dadosPro.PLACA.Substring(3, 4);
                                E073MOTDataAccess E073MOTDataAccess = new E073MOTDataAccess();
                                var nomeMotorista = E073MOTDataAccess.PesquisasMotoristas(dadosPro.CODMOT).FirstOrDefault().Nome;
                                listaRegistros[0].CodMotorista = dadosPro.CODMOT;
                                listaRegistros[0].NomeMotorista = nomeMotorista;
                            }
                        }
                    }

                    return listaRegistros;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Consulta placas das ocorrências para situação diferente de Recebido(9)
        /// </summary>
        /// <param name="numeroRegistro">Código de Ocorrência</param>
        /// <returns>true/false</returns>
        public String consultarPlacasFaturamento(long numeroRegistro)
        {
            try
            {
                string sql = "SELECT WM_CONCAT(NUMREG) NUMREG" +
                             "  FROM NWMS_PRODUCAO.N0203REG REG1" +
                             " WHERE REG1.PLACA IN (SELECT REG.PLACA" +
                             "  FROM NWMS_PRODUCAO.N0203REG REG" +
                             "  LEFT JOIN NWMS_PRODUCAO.N0204POC POC" +
                             "    ON POC.NUMREG = REG.NUMREG" +
                             " WHERE REG.NUMREG = " + numeroRegistro + ")" +
                             "   AND REG1.SITREG <> 9 ";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return dr["NUMREG"].ToString();
                }
                dr.Close();
                conn.Close();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Verifica as ocorrências Agrupadas
        /// </summary>
        /// <param name="codigoAgrupador">Código do agrupador</param>
        /// <returns></returns>
        public List<int> verificarOcorrenciaAgrupada(int codigoAgrupador)
        {
            try
            {
                string sql = "   SELECT COALESCE(AGR1.NUMREG ,0) AGRREG" +
                             "     FROM NWMS_PRODUCAO.N0203AGR AGR1" +
                             "    INNER JOIN NWMS_PRODUCAO.N0203REG REG1" +
                             "       ON REG1.NUMREG = AGR1.NUMREG AND AGRREG = " + codigoAgrupador + "";


                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr2 = cmd.ExecuteReader();
                List<int> itens = new List<int>();
                while (dr2.Read())
                {
                    itens.Add(Convert.ToInt32(dr2["AGRREG"]));
                }

                dr2.Close();
                conn.Close();
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Verifica as ocorrências
        /// </summary>
        /// <param name="codigoAgrupador">Código do Agrupador</param>
        /// <returns>itens</returns>
        public List<int> verificarOcorrencia(int codigoAgrupador)
        {
            try
            {
                string sql = "   SELECT COALESCE(AGR1.NUMREG ,0) AGRREG" +
                             "     FROM NWMS_PRODUCAO.N0203AGR AGR1" +
                             "    INNER JOIN NWMS_PRODUCAO.N0203REG REG1" +
                             "       ON REG1.NUMREG = AGR1.NUMREG AND AGR1.NUMREG = " + codigoAgrupador + "";


                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr2 = cmd.ExecuteReader();
                List<int> itens = new List<int>();
                while (dr2.Read())
                {
                    itens.Add(Convert.ToInt32(dr2["AGRREG"]));
                }

                dr2.Close();
                conn.Close();
                return itens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ConsultarOrigem(int Ocorrencia)
        {
            try
            {
                string sql = "SELECT ORIOCO FROM N0203IPV WHERE NUMREG = " + Ocorrencia + "";
                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                bool Motivo = false;
                OracleDataReader dr2 = cmd.ExecuteReader();
                
                while(dr2.Read())
                {
                    //Motivo = (Convert.ToInt32(dr2["ORIOCO"]));
                    if (Convert.ToInt32(dr2["ORIOCO"]) == 8)
                    {
                        Motivo = true;
                    }
                }

                dr2.Close();
                conn.Close();
                return Motivo;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Altera o Status do agrupamento
        /// </summary>
        /// <param name="codigoAgrupamento">Código do agrupador</param>
        public void alterarStatusAgrupamento(long codigoAgrupamento)
        {
            try
            {
                string sql = "UPDATE NWMS_PRODUCAO.N0203AGR SET STAGR = 'I' WHERE AGRREG = " + codigoAgrupamento + "";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                OracleDataReader dr2 = cmd.ExecuteReader();
                dr2.Close();
                conn.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Verifica todas as ocorrências agrupadas com status de Recebido(9)
        /// Aprova todas as ocorrências agrupadas
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        /// <param name="usuarioTramite">Usuário do Tramite</param>
        /// <param name="observacao">Observação</param>
        /// <param name="situacaoRegistro">Situação</param>
        /// <returns></returns>
        public string AprovarRegistrosOcorrenciaAgrupada(long codigoRegistro, long usuarioTramite, string observacao, int situacaoRegistro)
        {


            var itensAgrupados = verificarOcorrenciaAgrupada(Convert.ToInt16(codigoRegistro));

            //VERIFICAR SE TODAS AS OCORRÊNCIAS AGRUPADAS ESTÃO COM O STATUS 9
            using (Context contexto = new Context())
            {
                foreach (var item in itensAgrupados)
                {
                    var original = contexto.N0203REG.Where(c => c.NUMREG == item && c.SITREG == 9).SingleOrDefault();
                    if (original == null)
                    {
                        return "O registro de ocorrência " + item + " não está com a situação Conferido!";
                    }
                }
            }

            //APROVAR TODAS AS OCORRÊNCIAS AGRUPADAS
            foreach (var item in itensAgrupados)
            {
                using (Context contexto = new Context())
                {
                    int situacaoPreAprovado = (int)Enums.SituacaoRegistroOcorrencia.Recebido;
                    var original = contexto.N0203REG.Where(c => c.NUMREG == item && c.SITREG == situacaoPreAprovado).SingleOrDefault();

                    if (original != null)
                    {
                        N0203TRA itemTramites = new N0203TRA();
                        itemTramites.NUMREG = original.NUMREG;
                        itemTramites.SEQTRA = original.N0203TRA.OrderBy(c => c.SEQTRA).Last().SEQTRA + 1;

                        if (situacaoRegistro == (int)Enums.OperacaoAprovacaoFaturamento.Aprovar)
                        {
                            itemTramites.DESTRA = "REGISTRO DE OCORRENCIA APROVADO";
                        }

                        itemTramites.OBSTRA = observacao;
                        itemTramites.USUTRA = usuarioTramite;
                        itemTramites.DATTRA = DateTime.Now;
                        original.N0203TRA.Add(itemTramites);


                        original.SITREG = (long)Enums.SituacaoRegistroOcorrencia.Aprovado;

                        contexto.SaveChanges();
                    }
                }
            } alterarStatusAgrupamento(codigoRegistro);
            return "Registros de ocorrências agrupadas foram integradas com sucesso!";
        }

        /// <summary>
        /// SITUAÇÃO DE APROVAÇÃO E CANCELAMENTO DE OCORRENCIAS AGRUPADAS
        /// SITUAÇÃO DE APROVAÇÃO E CANCELAMENTO DE OCORRENCIAS NÂO AGRUPADAS
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        /// <param name="usuarioTramite">Usuário do Tramite</param>
        /// <param name="observacao">Observação</param>
        /// <param name="situacaoRegistro">Situação de Registro</param>
        /// <param name="tipoOperacao">Tipo de Operação</param>
        /// <returns></returns>
        public string AprovarRegistrosOcorrencia(long codigoRegistro, long usuarioTramite, string observacao, int situacaoRegistro, string tipoOperacao)
        {
            //SITUAÇÃO DE APROVAÇÃO E CANCELAMENTO DE OCORRENCIAS AGRUPADAS
            if (tipoOperacao == "2" && verificarOcorrenciaAgrupada(Convert.ToInt16(codigoRegistro)).Count > 0)
            {
                return AprovarRegistrosOcorrenciaAgrupada(codigoRegistro, usuarioTramite, observacao, situacaoRegistro);
            }

            //SITUAÇÃO DE APROVAÇÃO E CANCELAMENTO DE OCORRENCIAS NÂO AGRUPADAS
            if (situacaoRegistro == 2)
            {
                using (Context contexto = new Context())
                {
                    var original = contexto.N0203REG.Where(c => c.NUMREG == codigoRegistro && (c.SITREG != 3 && c.SITREG != 10)).SingleOrDefault();
                    if (original == null)
                    {
                        return "Operação não permitida, verifique se a ocorrência está com a situação integrado ou faturada.";
                    }

                    if (consultaString("NWMS_PRODUCAO.N0203AGR", "NUMREG", "NUMREG = " + codigoRegistro + "") == "VAZIO")
                    {
                        N0203TRA itemTramites = new N0203TRA();
                        itemTramites.NUMREG = original.NUMREG;

                        itemTramites.SEQTRA = original.N0203TRA.OrderBy(c => c.SEQTRA).Last().SEQTRA + 1;
                        itemTramites.USUTRA = usuarioTramite;
                        itemTramites.DATTRA = DateTime.Today;
                        itemTramites.DESTRA = "REGISTRO DE OCORRENCIA CANCELADO";
                        itemTramites.OBSTRA = observacao;
                        original.N0203TRA.Add(itemTramites);
                        original.SITREG = (long)Enums.SituacaoRegistroOcorrencia.Cancelado;

                        contexto.SaveChanges();
                        return "Registro de Ocorrencia cancelado com Sucesso!";
                    }
                    else
                    {
                        return "Registro de Ocorrência está vinculada a um agrupamento.";
                    }
                }
            }
            else if (situacaoRegistro == 1)
            {
                using (Context contexto = new Context())
                {
                    int situacaoRecebido = (int)Enums.SituacaoRegistroOcorrencia.Recebido;
                    var original = contexto.N0203REG.Where(c => c.NUMREG == codigoRegistro && c.SITREG == situacaoRecebido).SingleOrDefault();
                    if (original == null)
                    {
                        return "Operação não permitida, verifique se a ocorrência está com a situação Conferido.";
                    }
                    if (original != null)
                    {
                        N0203TRA itemTramites = new N0203TRA();
                        itemTramites.NUMREG = original.NUMREG;
                        itemTramites.SEQTRA = original.N0203TRA.OrderBy(c => c.SEQTRA).Last().SEQTRA + 1;

                        if (situacaoRegistro == (int)Enums.OperacaoAprovacaoFaturamento.Aprovar)
                        {
                            itemTramites.DESTRA = "REGISTRO DE OCORRENCIA APROVADO";
                        }

                        itemTramites.OBSTRA = observacao;
                        itemTramites.USUTRA = usuarioTramite;
                        itemTramites.DATTRA = DateTime.Now;
                        original.N0203TRA.Add(itemTramites);


                        original.SITREG = (long)Enums.SituacaoRegistroOcorrencia.Aprovado;

                        contexto.SaveChanges();
                        return "Registro de ocorrência aprovada com Sucesso.";
                    }
                }
            }
            else
            {
                using (Context contexto = new Context())
                {
                    int situacaoRecebido = (int)Enums.SituacaoRegistroOcorrencia.Recebido;
                    var original = contexto.N0203REG.Where(c => c.NUMREG == codigoRegistro && c.SITREG == situacaoRecebido).SingleOrDefault();
                    if (original == null)
                    {
                        return "Operação não permitida, verifique se a ocorrência está com situação Conferido.";
                    }
                    if (original != null)
                    {
                        N0203TRA itemTramites = new N0203TRA();
                        itemTramites.NUMREG = original.NUMREG;
                        itemTramites.SEQTRA = original.N0203TRA.OrderBy(c => c.SEQTRA).Last().SEQTRA + 1;

                        if (situacaoRegistro == (int)Enums.OperacaoAprovacaoFaturamento.Reaprovar)
                        {
                            itemTramites.DESTRA = "REGISTRO DE OCORRENCIA REABILITADO";
                        }

                        itemTramites.OBSTRA = observacao;
                        itemTramites.USUTRA = usuarioTramite;
                        itemTramites.DATTRA = DateTime.Now;
                        original.N0203TRA.Add(itemTramites);

                        original.SITREG = (long)Enums.SituacaoRegistroOcorrencia.Reaprovar;

                        contexto.SaveChanges();
                        return "Registro de Ocorrência ficou como reabilitado";

                    }
                }
            }
            return "";
        }

        /// <summary>
        /// Faz aprovação dos registros de ocorrências
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        /// <param name="codUsuarioLogado">Código Usuário Logado</param>
        /// <param name="observacaoAprovacao">Observação de Aprovado</param>
        /// <returns></returns>
        public bool AprovarRegistrosOcorrenciaNivel1(long codigoRegistro, long codUsuarioLogado, string observacaoAprovacao)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var original = contexto.N0203REG.Where(c => c.NUMREG == codigoRegistro).FirstOrDefault();
                    List<N0203IPV> verificarAreasParaAprovacao = contexto.N0203IPV.Where(c => c.NUMREG == codigoRegistro).ToList();
                    List<N0203TRA> verificarAreasAprovadas = contexto.N0203TRA.Where(c => c.NUMREG == codigoRegistro && c.CODORI != null).ToList();
                    List<N0203UAP> codigoOrigem = contexto.N0203UAP.Where(c => c.CODUSU == codUsuarioLogado && c.CODATD == original.TIPATE).ToList();
                    List<N0203TRA> TemRegristroRepetido = contexto.N0203TRA.Where(c => c.NUMREG == codigoRegistro && c.DESTRA.Contains("REGISTRO DE OCORRENCIA PRÉ APROVADO")).ToList();

                    foreach (var codigoOrigemAp in codigoOrigem)
                    {
                        foreach (var item in verificarAreasParaAprovacao)
                        {
                            if (codigoOrigemAp.CODORI == item.ORIOCO)
                            {
                                #region
                                if (original != null)
                                {
                                    N0203TRA itemTramites = new N0203TRA();
                                    itemTramites.NUMREG = original.NUMREG;
                                    itemTramites.SEQTRA = original.N0203TRA.OrderBy(c => c.SEQTRA).Last().SEQTRA + 1;
                                    itemTramites.DESTRA = "REGISTRO DE OCORRENCIA PRÉ APROVADO";
                                    itemTramites.OBSTRA = observacaoAprovacao;
                                    itemTramites.USUTRA = codUsuarioLogado;
                                    itemTramites.DATTRA = DateTime.Now;
                                    itemTramites.CODORI = item.ORIOCO;
                                    original.N0203TRA.Add(itemTramites);
                                    contexto.SaveChanges();

                                    if (verificarAreasAprovadas.Count == verificarAreasParaAprovacao.Count)
                                    {
                                        original.SITREG = (long)Enums.SituacaoRegistroOcorrencia.PreAprovado;
                                        original.APREAP = "N";
                                    }
                                    else
                                    {
                                        original.SITREG = (long)Enums.SituacaoRegistroOcorrencia.Fechado;
                                        original.APREAP = "S";
                                    }
                                    contexto.SaveChanges();

                                }
                                #endregion
                            }
                        }
                    }
                    List<N0203IPV> verificarAreasParaAprovacaoAtualizada = contexto.N0203IPV.Where(c => c.NUMREG == codigoRegistro).ToList();
                    List<N0203TRA> verificarAreasAprovadasAtualizada = contexto.N0203TRA.Where(c => c.NUMREG == codigoRegistro && c.CODORI != null).ToList();

                    if (verificarAreasParaAprovacaoAtualizada.Count <= verificarAreasAprovadasAtualizada.Count)
                    {
                        original.SITREG = (long)Enums.SituacaoRegistroOcorrencia.PreAprovado;
                        original.APREAP = "N";
                    }
                    else
                    {
                        original.SITREG = (long)Enums.SituacaoRegistroOcorrencia.Fechado;
                        original.APREAP = "S";
                    }
                    contexto.SaveChanges();
                    return original != null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Reprova os registros de ocorrências
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        /// <param name="codUsuarioLogado">Código Usuário Logado</param>
        /// <param name="observacaoReprovacao">Observação de Reprovação</param>
        /// <returns>true/false</returns>
        public bool ReprovarRegistrosOcorrenciaNivel1(long codigoRegistro, long codUsuarioLogado, string observacaoReprovacao)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var original = contexto.N0203REG.Where(c => c.NUMREG == codigoRegistro).FirstOrDefault();

                    if (original != null)
                    {
                        N0203TRA itemTramites = new N0203TRA();
                        itemTramites.NUMREG = original.NUMREG;
                        itemTramites.SEQTRA = original.N0203TRA.OrderBy(c => c.SEQTRA).Last().SEQTRA + 1;
                        itemTramites.DESTRA = "REGISTRO DE OCORRENCIA REPROVADO";
                        itemTramites.OBSTRA = observacaoReprovacao;
                        itemTramites.USUTRA = codUsuarioLogado;
                        itemTramites.DATTRA = DateTime.Now;
                        original.N0203TRA.Add(itemTramites);

                        original.SITREG = (long)Enums.SituacaoRegistroOcorrencia.Reprovado;
                        original.APREAP = "";
                        contexto.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lista os protocolos de titulos do cliente
        /// </summary>
        /// <param name="codigoRegistro">Código de Ocorrência</param>
        /// <returns></returns>
        public List<DadosNotasServicoModel> ListaProtocolosTituloCliente(long codigoRegistro)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var obsAprovacao = ". OBS APROVAÇÃO: " + contexto.N0203TRA.Where(c => c.NUMREG == codigoRegistro).OrderByDescending(c => c.SEQTRA).FirstOrDefault().OBSTRA;
                    var listaRegistros = (from c in contexto.N0203REG
                                          join m in contexto.N0203IPV on new { c.NUMREG } equals new { m.NUMREG }
                                          from b in contexto.N0204ORI
                                          where m.NUMREG == codigoRegistro && c.ORIOCO == b.CODORI
                                          group new { c, m, b } by new { m.NUMREG, c.ORIOCO, b.CODORI_SAPIENS, b.DESCORI, m.CODEMP, m.CODFIL, m.CODSNF, m.NUMNFV, c.OBSREG } into grupo
                                          select new DadosNotasServicoModel
                                          {
                                              NumeroProtocolo = grupo.Key.NUMREG,
                                              CodDepartamentoOrigem = grupo.Key.ORIOCO,
                                              CodDepartamentoSapiens = grupo.Key.CODORI_SAPIENS,
                                              DescDepartamentoOrigem = grupo.Key.DESCORI,
                                              CodEmpresa = grupo.Key.CODEMP,
                                              CodFilialNota = grupo.Key.CODFIL,
                                              NumeroNota = grupo.Key.NUMNFV,
                                              SerieNota = grupo.Key.CODSNF,
                                              Observacao = grupo.Key.OBSREG + obsAprovacao

                                          }).ToList();

                    return listaRegistros;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa protocolos pendentes de aprovação
        /// </summary>
        /// <param name="codUsuarioLogado">Código Usuário Logado</param>
        /// <returns>Lista de Protocolos</returns>
        public List<N0203REG> PesquisaProtocolosPendentesAprovacao(long codUsuarioLogado)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var situacaoFechado = (int)Enums.SituacaoRegistroOcorrencia.Fechado;
                    //var situacaoPreAprovado = (int)Enums.SituacaoRegistroOcorrencia.PreAprovado;
                    var tipoAtendimento = (int)Enums.TipoAtendimento.DevolucaoMercadorias;
                    var listaN0203REG = new List<N0203REG>();

                    var centroCusto = contexto.N0203UAP.Where(c => c.CODATD == tipoAtendimento && c.CODUSU == codUsuarioLogado).Select(t => t.CODORI).ToList();

                    var listaRegistros = (from c in contexto.N0203REG
                                          join m in contexto.N0203IPV on new { c.NUMREG } equals new { m.NUMREG }
                                          where (c.SITREG == situacaoFechado) && (c.APREAP == null || c.APREAP == "S") && centroCusto.Contains(m.ORIOCO)
                                          group new { c } by new { c.NUMREG } into grupo
                                          orderby grupo.Key
                                          select grupo.Key).ToList();
                    foreach (var reg in listaRegistros)
                    {

                         var verificaPreAprovacao = (from c in contexto.N0203REG
                                                    join d in contexto.N0203TRA on new { c.NUMREG } equals new { d.NUMREG }
                                                    where c.NUMREG == reg.NUMREG && d.USUTRA == codUsuarioLogado && c.SITREG == 2 && d.DESTRA == "REGISTRO DE OCORRENCIA PRÉ APROVADO"
                                                    select c).Count();

                        if (verificaPreAprovacao == 0)
                        {
                            var itemListaReg = contexto.N0203REG.Include("N0203IPV").Where(c => c.NUMREG == reg.NUMREG).FirstOrDefault();
                            if (itemListaReg != null)
                            {
                                listaN0203REG.Add(itemListaReg);
                            }
                        }
                    }
                    return listaN0203REG;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa protocolos pendentes de aprovação DashBoard
        /// </summary>
        /// <param name="codigoUsuario">Códgio Usuário</param>
        /// <returns></returns>
        public List<ProtocolosAprovacaoModel> PesquisarProtocolosPendentesAprovacaoDashBoard(long codigoUsuario)
        {
            List<string> itensOrigens = PesquisaLiberacoesDashBoard(codigoUsuario);
            string itensOrigensConcat = String.Join(",", itensOrigens);

            try
            {

                string sql = "   SELECT REG.NUMREG, TO_DATE(TO_CHAR(REG.DATGER, 'DD/MM/YYYY')) DATGER, MDV.DESCMDV, ORI.DESCORI,  IPV.NUMNFV, TO_CHAR(ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV),2), 'FM999G999G999D90',   'nls_numeric_characters='',.''') VALORLIQUIDO, ATD.DESCATD" +
                             "     FROM NWMS_PRODUCAO.N0203REG REG" +
                             "    INNER JOIN NWMS_PRODUCAO.N0203IPV IPV" +
                             "       ON (REG.NUMREG = IPV.NUMREG)" +
                             "    INNER JOIN NWMS_PRODUCAO.N0204DORI DORI" +
                             "       ON (IPV.ORIOCO = DORI.CODORI)" +
                             "    INNER JOIN NWMS_PRODUCAO.N0204ORI ORI" +
                             "       ON (IPV.ORIOCO = ORI.CODORI)" +
                             "    INNER JOIN NWMS_PRODUCAO.N0204ATD ATD" +
                             "    ON(REG.TIPATE = ATD.CODATD)" +
                             "    INNER JOIN NWMS_PRODUCAO.N0204MDV MDV" +
                             "    ON(MDV.CODMDV = IPV.CODMOT)" +
                             "    WHERE REG.SITREG = 2" +
                             "      AND DORI.CODUSU = " + codigoUsuario + "" +
                             "      AND NOT EXISTS" +
                             "    (SELECT 1" +
                             "             FROM NWMS_PRODUCAO.N0203TRA TRA" +
                             "            WHERE TRA.NUMREG = REG.NUMREG" +
                             "              AND TRA.DESTRA = 'REGISTRO DE OCORRENCIA PRÉ APROVADO'" +
                             "              AND TRA.CODORI IN (SELECT DORI.CODORI" +
                             "                                   FROM NWMS_PRODUCAO.N0204DORI DORI1" +
                             "                                  WHERE DORI1.CODUSU = " + codigoUsuario + "))" +
                             "                                   GROUP BY REG.NUMREG," +
                             "                               REG.DATGER," +
                             "                               MDV.DESCMDV," +
                             "                               ORI.DESCORI," +
                             "                               ATD.DESCATD," +
                             "                               IPV.NUMNFV ORDER BY DATGER ASC";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<ProtocolosAprovacaoModel> listaProtocolosPendentes = new List<ProtocolosAprovacaoModel>();
                ProtocolosAprovacaoModel itemProtocolo = new ProtocolosAprovacaoModel();
                decimal valorTotal = 0;
                while (dr.Read())
                {
                    itemProtocolo = new ProtocolosAprovacaoModel();
                    itemProtocolo.CodigoRegistro = Convert.ToInt64(dr["NUMREG"]);
                    itemProtocolo.DataFechamento = dr["DATGER"].ToString();
                    itemProtocolo.descMotivo = dr["DESCMDV"].ToString();
                    itemProtocolo.DescOrigemOcorrencia = dr["DESCORI"].ToString();
                    itemProtocolo.numeroNotaFiscal = dr["NUMNFV"].ToString();
                    itemProtocolo.valorLiquido = dr["VALORLIQUIDO"].ToString();
                    itemProtocolo.DescTipoAtendimento = dr["DESCATD"].ToString();
                    valorTotal += Convert.ToDecimal(itemProtocolo.valorLiquido);
                    itemProtocolo.valorTotal = valorTotal;
                    listaProtocolosPendentes.Add(itemProtocolo);
                }

                dr.Close();
                conn.Close();
                return listaProtocolosPendentes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="numeroOCorrencia">Código de Ocorrência</param>
        /// <returns></returns>
        public List<TimeLine> timeLine(long numeroOCorrencia)
        {

            try
            {

                string sql = @" SELECT
                                            TRA.NUMREG,
                                            TRA.DESTRA,
                                            USU.LOGIN,
                                            TRA.DATTRA,
                                            TRA.OBSTRA,
                                            CASE
                                                WHEN TRA.OBSTRA IS NULL
                                                THEN REG.OBSREG
                                                ELSE ' '
                                            END            OBSREG,
                                            ORIIPV.DESCORI ORIGEMAPROVACAO,
                                            ORIREG.DESCORI ORIGEMOCORRENCIA
                                    FROM
                                        NWMS_PRODUCAO.N0203TRA TRA
                                    INNER JOIN
                                        NWMS_PRODUCAO.N9999USU USU
                                    ON
                                        TRA.USUTRA = USU.CODUSU
                                    INNER JOIN
                                        NWMS_PRODUCAO.N0203REG REG
                                    ON
                                        TRA.NUMREG = REG.NUMREG
                                    INNER JOIN
                                        NWMS_PRODUCAO.N0203IPV IPV
                                    ON
                                        IPV.NUMREG = REG.NUMREG
                                    INNER JOIN
                                        NWMS_PRODUCAO.N0204ORI ORIIPV
                                    ON
                                        ORIIPV.CODORI = IPV.ORIOCO
                                    INNER JOIN
                                        NWMS_PRODUCAO.N0204ORI ORIREG
                                    ON
                                        REG.ORIOCO = ORIREG.CODORI";
                sql += " WHERE ";
                sql += " REG.NUMREG = " + numeroOCorrencia + " ";
                sql += @"GROUP BY TRA.NUMREG,
                                                TRA.DESTRA,
                                                USU.LOGIN,
                                                TRA.DATTRA,
                                                TRA.OBSTRA,
                                                REG.OBSREG,
                                                ORIIPV.DESCORI,
                                                ORIREG.DESCORI
                                          ORDER BY TRA.DATTRA";


                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<TimeLine> listaTramite = new List<TimeLine>();
                TimeLine itemTramite = new TimeLine();

                while (dr.Read())
                {
                    itemTramite = new TimeLine();
                    itemTramite.NUMREG = Convert.ToInt64(dr["NUMREG"]);
                    itemTramite.DESTRA = dr["DESTRA"].ToString();
                    itemTramite.DESCORITRA = dr["ORIGEMAPROVACAO"].ToString();
                    itemTramite.DESCORIREG = dr["ORIGEMOCORRENCIA"].ToString();
                    itemTramite.DATTRA = dr["DATTRA"].ToString();
                    itemTramite.OBSTRA = dr["OBSTRA"].ToString();
                    itemTramite.OBSREG = dr["OBSREG"].ToString();
                    itemTramite.USUTRA = dr["LOGIN"].ToString();
                    listaTramite.Add(itemTramite);
                }

                dr.Close();
                conn.Close();
                return listaTramite;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca a quantidades de protocolos por Area
        /// </summary>
        /// <param name="dias">Dias</param>
        /// <param name="situacao">Situação</param>
        /// <returns></returns>
        public List<N0204ORI> quantidadeProtocolosPorArea(int dias, int situacao)
        {
            try
            {
                string sql = @"SELECT ORI.DESCORI, ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV), 2) VALOR
                                  FROM NWMS_PRODUCAO.N0203REG REG
                                INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                                   ON (IPV.NUMREG = REG.NUMREG)
                                INNER JOIN NWMS_PRODUCAO.N0204MDV MDV
                                   ON (MDV.CODMDV = IPV.CODMOT)
                                INNER JOIN NWMS_PRODUCAO.N0204ORI ORI
                                   ON (ORI.CODORI = IPV.ORIOCO)
                                INNER JOIN NWMS_PRODUCAO.N0204ATD ATD
                                   ON (REG.TIPATE = ATD.CODATD)
                                INNER JOIN SAPIENS.E135PFA PFA
                                   ON IPV.CODEMP = PFA.CODEMP
                                  AND IPV.CODFIL = PFA.FILNFV
                                  AND IPV.CODSNF = PFA.SNFNFV
                                  AND IPV.NUMNFV = PFA.NUMNFV
                                INNER JOIN SAPIENS.E085CLI CLI
                                   ON (REG.CODCLI = CLI.CODCLI)
                                WHERE 1 = 1
                                  AND  REG.DATGER BETWEEN TO_DATE(TO_CHAR(SYSDATE - " + dias + ")) AND TO_DATE(TO_CHAR(SYSDATE))";
                if (situacao == 2)
                {
                    sql += @" AND NOT EXISTS
                          (SELECT 1
                             FROM NWMS_PRODUCAO.N0203TRA TRA
                            WHERE TRA.NUMREG = REG.NUMREG
                              AND TRA.DESTRA = 'REGISTRO DE OCORRENCIA PRÉ APROVADO')";
                }

                sql += situacao == 0 ? " GROUP BY ORI.DESCORI" : " AND REG.SITREG = " + situacao + "  GROUP BY ORI.DESCORI";
                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<N0204ORI> listaItensOrigens = new List<N0204ORI>();
                N0204ORI itensOrigens = new N0204ORI();
                while (dr.Read())
                {
                    itensOrigens = new N0204ORI();
                    itensOrigens.DESCORI = dr["DESCORI"].ToString();
                    itensOrigens.QTDORIVALOR = Convert.ToDecimal(dr["VALOR"]);
                    listaItensOrigens.Add(itensOrigens);
                }
                dr.Close();
                conn.Close();
                return listaItensOrigens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca a quantidade de protocolos por area mes
        /// </summary>
        /// <param name="dias">Dias</param>
        /// <param name="situacao">Situação</param>
        /// <returns></returns>
        public List<N0204ORI> quantidadeProtocolosPorAreaMeses(int dias, int situacao)
        {
            try
            {
                string sql = @"SELECT ORI.DESCORI, COUNT(DISTINCT IPV.NUMREG) AS QUANTIDADE
                                 FROM NWMS_PRODUCAO.N0203REG REG
                                INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                                   ON (IPV.NUMREG = REG.NUMREG)
                                INNER JOIN NWMS_PRODUCAO.N0204MDV MDV
                                   ON (MDV.CODMDV = IPV.CODMOT)
                                INNER JOIN NWMS_PRODUCAO.N0204ORI ORI
                                   ON (ORI.CODORI = IPV.ORIOCO)
                                INNER JOIN NWMS_PRODUCAO.N0204ATD ATD
                                   ON (REG.TIPATE = ATD.CODATD)
                                INNER JOIN SAPIENS.E135PFA PFA
                                   ON IPV.CODEMP = PFA.CODEMP
                                  AND IPV.CODFIL = PFA.FILNFV
                                  AND IPV.CODSNF = PFA.SNFNFV
                                  AND IPV.NUMNFV = PFA.NUMNFV
                                INNER JOIN SAPIENS.E085CLI CLI
                                   ON (REG.CODCLI = CLI.CODCLI)
                                WHERE 1 = 1
                                  AND  TO_DATE(TO_CHAR(REG.DATGER , 'DD-MM-RRRR')) BETWEEN TO_DATE(TO_CHAR(SYSDATE - " + dias + ")) AND TO_DATE(TO_CHAR(SYSDATE))";
                if (situacao == 2)
                {
                    sql += @" AND NOT EXISTS
                             (SELECT 1
                                      FROM NWMS_PRODUCAO.N0203TRA TRA
                                     WHERE TRA.NUMREG = REG.NUMREG
                                       AND TRA.DESTRA = 'REGISTRO DE OCORRENCIA PRÉ APROVADO'
                                       AND TRA.CODORI IN
                                           (SELECT DORI1.CODORI FROM NWMS_PRODUCAO.N0204DORI DORI1))";
                }
                sql += situacao == 0 ? " GROUP BY ORI.DESCORI" : " AND REG.SITREG = " + situacao + "  GROUP BY ORI.DESCORI";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<N0204ORI> listaItensOrigens = new List<N0204ORI>();
                N0204ORI itensOrigens = new N0204ORI();
                while (dr.Read())
                {
                    itensOrigens = new N0204ORI();
                    itensOrigens.DESCORI = dr["DESCORI"].ToString();
                    itensOrigens.QTDORI = Convert.ToInt32(dr["QUANTIDADE"]);
                    listaItensOrigens.Add(itensOrigens);
                }
                dr.Close();
                conn.Close();
                return listaItensOrigens;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa protocolos pendentes de aprovação para o DashBoard
        /// </summary>
        /// <param name="codUsuarioLogado">Código Usuário Logado</param>
        /// <returns>listaProtocolosPendentes</returns>

        public List<ProtocolosAprovacaoModel> PesquisaProtocolosPendentesAprovacaoDashBoard(long codUsuarioLogado)
        {
            List<string> itensOrigens = PesquisaLiberacoesDashBoard(codUsuarioLogado);
            string itensOrigensConcat = String.Join(",", itensOrigens);
            

            try
            {
                string sql = "   SELECT *" +
                             "    FROM (SELECT REG.NUMREG," +
                             "                           TO_DATE(TO_CHAR(REG.DATGER, 'DD/MM/YYYY')) DATGER," +
                             "                           MDV.DESCMDV," +
                             "                           ORI.DESCORI," +
                             "                           ATD.DESCATD," +
                             "                           IPV.NUMNFV," +
                             "                          TO_CHAR(ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV),2), 'FM999G999G999D90',   'nls_numeric_characters='',.''') VALORLIQUIDO," +
                             "                           (SELECT COUNT(DISTINCT TRA.CODORI)" +
                             "                              FROM NWMS_PRODUCAO.N0203TRA TRA" +
                             "                             WHERE TRA.NUMREG = REG.NUMREG" +
                             "                               AND TRA.DESTRA =" +
                             "                                   'REGISTRO DE OCORRENCIA FECHADO') AS AREASAP," +
                             "                           (SELECT COUNT(DISTINCT IPV.ORIOCO)" +
                             "                              FROM NWMS_PRODUCAO.N0203IPV IPV" +
                             "                             WHERE IPV.NUMREG = REG.NUMREG) AREASPAP" +
                             "             FROM NWMS_PRODUCAO.N0203REG REG" +
                             "             LEFT JOIN NWMS_PRODUCAO.N0203IPV IPV" +
                             "               ON IPV.NUMREG = REG.NUMREG" +
                             "            INNER JOIN NWMS_PRODUCAO.N0204ATD ATD" +
                             "               ON ATD.CODATD = REG.TIPATE" +
                             "            INNER JOIN NWMS_PRODUCAO.N0204MDV MDV" +
                             "               ON MDV.CODMDV = IPV.CODMOT" +
                             "            INNER JOIN NWMS_PRODUCAO.N0204ORI ORI" +
                             "               ON ORI.CODORI = REG.ORIOCO" +
                             "            WHERE 1 = 1";
                sql += itensOrigensConcat != "" ? " AND REG.SITREG IN (" + itensOrigensConcat + ")" : "";
                sql += "        AND REG.SITREG IN (2)" +
                "              AND REG.USUGER = " + codUsuarioLogado + "" +
                "         GROUP BY  REG.NUMREG," +
                                  " REG.DATGER," +
                                  " MDV.DESCMDV," +
                                  " ORI.DESCORI," +
                                  " ATD.DESCATD," +
                                  " IPV.NUMNFV)" +
                "    ORDER BY DATGER ASC";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<ProtocolosAprovacaoModel> listaProtocolosPendentes = new List<ProtocolosAprovacaoModel>();
                ProtocolosAprovacaoModel itemProtocolo = new ProtocolosAprovacaoModel();
                decimal valorTotal = 0;
                while (dr.Read())
                {
                    itemProtocolo = new ProtocolosAprovacaoModel();
                    itemProtocolo.CodigoRegistro = Convert.ToInt64(dr["NUMREG"]);
                    itemProtocolo.DataFechamento = dr["DATGER"].ToString();
                    itemProtocolo.descMotivo = dr["DESCMDV"].ToString();
                    itemProtocolo.DescOrigemOcorrencia = dr["DESCORI"].ToString();
                    itemProtocolo.numeroNotaFiscal = dr["NUMNFV"].ToString();
                    itemProtocolo.AREASAP = dr["AREASAP"].ToString();
                    itemProtocolo.AREASPAP = dr["AREASPAP"].ToString();
                    itemProtocolo.valorLiquido = dr["VALORLIQUIDO"].ToString();
                    valorTotal += Convert.ToDecimal(itemProtocolo.valorLiquido);
                    itemProtocolo.valorTotal = valorTotal;
                    itemProtocolo.DescTipoAtendimento = dr["DESCATD"].ToString();
                    listaProtocolosPendentes.Add(itemProtocolo);
                }
                dr.Close();
                conn.Close();
                return listaProtocolosPendentes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisas as liberações para apresentar no Dashboard
        /// </summary>
        /// <param name="codigoUsuario">Código do Usuário</param>
        /// <returns>itensOrigem</returns>
        public List<string> PesquisaLiberacoesDashBoard(long codigoUsuario)
        {
            try
            {
                string sql = "SELECT CODORI FROM NWMS_PRODUCAO.N0204DORI WHERE CODUSU = " + codigoUsuario + "";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<string> itensOrigem = new List<string>();

                while (dr.Read())
                {
                    itensOrigem.Add(dr["CODORI"].ToString());
                }

                dr.Close();
                conn.Close();
                return itensOrigem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa todos os protolos aprovador para apresentrar no dashboard
        /// </summary>
        /// <param name="codUsuarioLogado">Código Usuário Logado</param>
        /// <returns></returns>
        public List<ProtocolosAprovacaoModel> PesquisaProtocolosAprovadosDashBoard(long codUsuarioLogado)
        {
            List<string> itensOrigens = PesquisaLiberacoesDashBoard(codUsuarioLogado);
            string itensOrigensConcat = String.Join(",", itensOrigens);
            try
            {
                string sql = "   SELECT NUMREG, DATGER, DESCMDV, DESCORI, NUMNFV, AREASAP, AREASPAP, VALORLIQUIDO, DESCATD" +
                          "     FROM (SELECT DISTINCT NUMREG, DATGER, DESCMDV, DESCORI, NUMNFV, AREASAP, AREASPAP, VALORLIQUIDO, DESCATD" +
                          "             FROM (SELECT REG.NUMREG," +
                          "                                 TO_DATE(TO_CHAR(REG.DATGER, 'DD/MM/YYYY')) DATGER," +
                          "                                   MOT.DESCMDV," +
                          "                                   ORI.DESCORI," +
                          "                                   IPV.NUMNFV," +
                          "                                  TRA.DATTRA," +
                          "                                  ATD.DESCATD," +
                          "                                    TO_CHAR(ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV),2), 'FM999G999G999D90',   'nls_numeric_characters='',.''') VALORLIQUIDO," +
                          "                                   (SELECT COUNT(DISTINCT TRA.CODORI)" +
                          "                                      FROM NWMS_PRODUCAO.N0203TRA TRA" +
                          "                                     WHERE TRA.NUMREG = REG.NUMREG" +
                          "                                       AND TRA.DESTRA = 'REGISTRO DE OCORRENCIA PRÉ APROVADO') AS AREASAP," +
                          "                                    (SELECT COUNT(DISTINCT IPV.ORIOCO)" +
                          "                                       FROM NWMS_PRODUCAO.N0203IPV IPV" +
                          "                                      WHERE IPV.NUMREG = REG.NUMREG) AREASPAP" +
                          "                     FROM NWMS_PRODUCAO.N0203REG REG" +
                          "                    INNER JOIN NWMS_PRODUCAO.N0203IPV IPV" +
                          "                       ON IPV.NUMREG = REG.NUMREG" +
                          "                    INNER JOIN NWMS_PRODUCAO.N0204ATD ATD" +
                          "                       ON ATD.CODATD = REG.TIPATE" +
                          "                    INNER JOIN NWMS_PRODUCAO.N0204MDV MOT" +
                          "                       ON MOT.CODMDV = IPV.CODMOT" +
                          "                    INNER JOIN NWMS_PRODUCAO.N0204ORI ORI" +
                          "                       ON ORI.CODORI = REG.ORIOCO" +
                          "                    INNER JOIN NWMS_PRODUCAO.N0203TRA TRA" +
                          "                       ON TRA.NUMREG = REG.NUMREG" +
                          "                    WHERE 1 = 1" +
                          "                      AND REG.SITREG IN (4)";
                sql += itensOrigensConcat != "" ? "AND REG.ORIOCO IN (" + itensOrigensConcat + ")" : "";
                sql += "                AND REG.USUGER = " + codUsuarioLogado + " GROUP BY REG.NUMREG," +
                                          " REG.DATGER," +
                                          " MOT.DESCMDV," +
                                          " ORI.DESCORI," +
                                          " IPV.NUMNFV," +
                                          " ATD.DESCATD," +
                                          " TRA.DATTRA))" +
                "                    ORDER BY DATGER ASC";
                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<ProtocolosAprovacaoModel> listaProtocolosPendentes = new List<ProtocolosAprovacaoModel>();
                ProtocolosAprovacaoModel itemProtocolo = new ProtocolosAprovacaoModel();
                decimal valorTotal = 0;
                while (dr.Read())
                {
                    itemProtocolo = new ProtocolosAprovacaoModel();
                    itemProtocolo.CodigoRegistro = Convert.ToInt64(dr["NUMREG"]);
                    itemProtocolo.DataFechamento = dr["DATGER"].ToString();
                    itemProtocolo.descMotivo = dr["DESCMDV"].ToString();
                    itemProtocolo.DescOrigemOcorrencia = dr["DESCORI"].ToString();
                    itemProtocolo.numeroNotaFiscal = dr["NUMNFV"].ToString();
                    itemProtocolo.AREASAP = dr["AREASAP"].ToString();
                    itemProtocolo.AREASPAP = dr["AREASPAP"].ToString();
                    itemProtocolo.valorLiquido = dr["VALORLIQUIDO"].ToString();
                    itemProtocolo.DescTipoAtendimento = dr["DESCATD"].ToString();
                    valorTotal += Convert.ToDecimal(itemProtocolo.valorLiquido);
                    itemProtocolo.valorTotal = valorTotal;
                    listaProtocolosPendentes.Add(itemProtocolo);
                }

                dr.Close();
                conn.Close();
                return listaProtocolosPendentes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Pesquisa Protocolos Atrasados
        /// </summary>
        /// <param name="codUsuarioLogado">Código Usuário Logado</param>
        /// <returns>cont</returns>
        public int PesquisaProtocolosAtrasados(long codUsuarioLogado)
        {
            try
            {
                string sql = "SELECT DISTINCT REG.NUMREG REG" +
                            "      FROM NWMS_PRODUCAO.N0203REG REG" +
                            "     INNER JOIN NWMS_PRODUCAO.N0203IPV IPV" +
                            "        ON (IPV.NUMREG = REG.NUMREG)" +
                            "     INNER JOIN NWMS_PRODUCAO.N0204MDV MDV" +
                            "        ON (MDV.CODMDV = IPV.CODMOT)" +
                            "     INNER JOIN NWMS_PRODUCAO.N0204ORI ORI" +
                            "        ON (ORI.CODORI = IPV.ORIOCO)" +
                            "     INNER JOIN NWMS_PRODUCAO.N0204ATD ATD" +
                            "        ON (REG.TIPATE = ATD.CODATD)" +
                            "     INNER JOIN SAPIENS.E135PFA PFA" +
                            "        ON IPV.CODEMP = PFA.CODEMP" +
                            "           AND IPV.CODFIL = PFA.FILNFV" +
                            "           AND IPV.CODSNF = PFA.SNFNFV" +
                            "           AND IPV.NUMNFV = PFA.NUMNFV" +
                            "     INNER JOIN SAPIENS.E085CLI CLI" +
                            "        ON (REG.CODCLI = CLI.CODCLI)" +
                            "     WHERE 1 = 1" +
                            "           AND ORI.CODORI IN (SELECT DORI2.CODORI" +
                            "                                FROM NWMS_PRODUCAO.N0204DORI DORI2" +
                            "                               WHERE DORI2.CODUSU = " + codUsuarioLogado + ")" +
                            "           AND REG.SITREG IN (2," +
                            "                              4)" +
                            "  AND REG.DATGER BETWEEN TO_DATE(TO_CHAR(SYSDATE - 30)) AND TO_DATE(TO_CHAR(SYSDATE))" +
                            "     GROUP BY REG.NUMREG," +
                            "              REG.SITREG," +
                            "              REG.DATGER," +
                            "              IPV.NUMNFV," +
                            "              IPV.CODFIL," +
                            "              CLI.CODCLI," +
                            "              CLI.NOMCLI," +
                            "              PFA.NUMANE," +
                            "              ORI.DESCORI," +
                            "              PFA.CODFIL," +
                            "              ATD.DESCATD," +
                            "              MDV.DESCMDV" + "";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<N0203REG> listaProtocolosPendentes = new List<N0203REG>();
                N0203REG itemProtocolo = new N0203REG();
                int cont = 0;
                while (dr.Read())
                {
                    cont = cont + 1;

                }

                dr.Close();
                conn.Close();
                return cont;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Pesquisa protocolos aprovados pendentes de aprovação
        /// </summary>
        /// <param name="codUsuarioLogado">Código Usuário Logado</param>
        /// <returns></returns>
        public List<long> PesquisaProtocolosAprovadosXPendentesAprovacao(long codUsuarioLogado)
        {
            long quantidadePendentes = 0;
            long quantidadeAprovado = 0;
            List<long> listaQuantidade = new List<long>();
            try
            {
                string sql = "   SELECT COUNT(1) AS LINHA" +
                             "    FROM (SELECT DISTINCT REG.NUMREG," +
                             "                           REG.DATGER," +
                             "                           ATD.DESCATD," +
                             "                           ORI.DESCORI," +
                             "                           IPV.NUMNFV," +
                             "                           TRA.DATTRA" +
                             "             FROM NWMS_PRODUCAO.N0203REG REG" +
                             "            INNER JOIN NWMS_PRODUCAO.N0203IPV IPV" +
                             "               ON IPV.NUMREG = REG.NUMREG" +
                             "            INNER JOIN NWMS_PRODUCAO.N0204ATD ATD" +
                             "               ON ATD.CODATD = REG.TIPATE" +
                             "            INNER JOIN NWMS_PRODUCAO.N0204ORI ORI" +
                             "               ON ORI.CODORI = REG.ORIOCO" +
                             "            INNER JOIN NWMS_PRODUCAO.N0203TRA TRA" +
                             "               ON TRA.NUMREG = REG.NUMREG" +
                             "            WHERE 1 = 1" +
                             "              AND REG.SITREG IN (4)" +
                             "              AND REG.USUGER = " + codUsuarioLogado + "" +
                             "            ORDER BY TRA.DATTRA ASC)";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    quantidadeAprovado = Convert.ToInt64(dr["LINHA"]);
                }

                dr.Close();
                conn.Close();


                string sql1 = "  SELECT COUNT(1) AS LINHA FROM (SELECT DISTINCT REG.NUMREG," +
                       "                     REG.DATGER," +
                       "                     ATD.DESCATD, " +
                       "                         ORI.DESCORI," +
                       "                         IPV.NUMNFV " +
                       "           FROM NWMS_PRODUCAO.N0203REG REG " +
                       "          INNER JOIN NWMS_PRODUCAO.N0203IPV IPV " +
                       "             ON IPV.NUMREG = REG.NUMREG " +
                       "          INNER JOIN NWMS_PRODUCAO.N0203UAP UAP " +
                       "             ON UAP.CODORI = IPV.ORIOCO " +
                       "            AND UAP.CODATD = REG.TIPATE " +
                       "          INNER JOIN NWMS_PRODUCAO.N0204ATD ATD " +
                       "             ON ATD.CODATD = UAP.CODATD " +
                       "          INNER JOIN NWMS_PRODUCAO.N0204ORI ORI " +
                       "             ON ORI.CODORI = UAP.CODORI " +
                       "          WHERE 1 = 1 " +
                       "            AND REG.SITREG IN (2) " +
                       "            AND UAP.CODUSU =   " + codUsuarioLogado + "   " +
                       "            AND NOT EXISTS (SELECT 1 " +
                       "                   FROM NWMS_PRODUCAO.N0203TRA TRA " +
                       "                  WHERE TRA.NUMREG = REG.NUMREG " +
                       "                    AND TRA.CODORI = IPV.ORIOCO))";

                OracleConnection con = new OracleConnection(OracleStringConnection);
                OracleCommand cmd1 = new OracleCommand(sql1, con);
                cmd1.CommandType = CommandType.Text;
                con.Open();

                OracleDataReader dr1 = cmd1.ExecuteReader();

                while (dr1.Read())
                {
                    quantidadePendentes = Convert.ToInt64(dr1["LINHA"]);
                }

                dr1.Close();
                con.Close();

                listaQuantidade.Add(quantidadeAprovado);
                listaQuantidade.Add(quantidadePendentes);

                return listaQuantidade;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa as orbservações lançadas pelo SAC
        /// </summary>
        /// <param name="numreg">Código de Ocorrência</param>
        /// <returns></returns>
        public string pesquisarObservacaoSAC(long numreg)
        {
            try
            {
                string sql = "SELECT N0203REG.OBSREG FROM NWMS_PRODUCAO.N0203REG WHERE NUMREG = " + numreg + "";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                OracleDataReader dr2 = cmd.ExecuteReader();

                if (dr2.Read())
                {
                    return dr2["OBSREG"].ToString();
                }

                dr2.Close();
                conn.Close();
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa protocolos que foram aprovados e estão esperando faturamento
        /// </summary>
        /// <param name="codUsuarioLogado">Código Usuário Logado</param>
        /// <returns>listaProtocolosPendentes</returns>
        public List<ProtocolosAprovacaoModel> PesquisaProtocolosForamAprovadosEsperandoFaturamento(long codUsuarioLogado)
        {
            try
            {
                string sql = "SELECT REG.NUMREG, " +
                 "                TO_DATE(TO_CHAR(REG.DATGER, 'DD/MM/YYYY')) DATGER," +
                 "                 MDV.DESCMDV," +
                 "                 ORI.DESCORI," +
                 "                IPV.NUMNFV," +
                 "                ATD.DESCATD," +
                 "                 TO_CHAR(ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV),2), 'FM999G999G999D90',   'nls_numeric_characters='',.''') VALORLIQUIDO" +
                 "            FROM NWMS_PRODUCAO.N0203REG REG" +
                 "           INNER JOIN NWMS_PRODUCAO.N0203IPV IPV" +
                 "               ON (REG.NUMREG = IPV.NUMREG)" +
                 "            INNER JOIN NWMS_PRODUCAO.N0204ORI ORI" +
                 "               ON (IPV.ORIOCO = ORI.CODORI)" +
                 "            INNER JOIN NWMS_PRODUCAO.N0204ATD ATD" +
                 "               ON (REG.TIPATE = ATD.CODATD)" +
                 "            INNER JOIN NWMS_PRODUCAO.N0204MDV MDV" +
                 "               ON (MDV.CODMDV = IPV.CODMOT)" +
                 "            WHERE IPV.ORIOCO IN (SELECT DORI.CODORI" +
                 "                     FROM NWMS_PRODUCAO.N0204DORI DORI" +
                 "                    WHERE DORI.CODUSU = " + codUsuarioLogado + "" +
                 "                      AND DORI.CODORI = ORI.CODORI)" +
                 "              AND REG.SITREG = 4 GROUP BY REG.NUMREG, REG.DATGER, MDV.DESCMDV, ORI.DESCORI, IPV.NUMNFV, ATD.DESCATD ORDER BY DATGER ASC";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr2 = cmd.ExecuteReader();

                List<ProtocolosAprovacaoModel> listaProtocolosPendentes = new List<ProtocolosAprovacaoModel>();
                ProtocolosAprovacaoModel itemProtocolo = new ProtocolosAprovacaoModel();
                decimal valorTotal = 0;
                while (dr2.Read())
                {
                    itemProtocolo = new ProtocolosAprovacaoModel();
                    itemProtocolo.CodigoRegistro = Convert.ToInt64(dr2["NUMREG"]);
                    itemProtocolo.DataFechamento = dr2["DATGER"].ToString();
                    itemProtocolo.descMotivo = dr2["DESCMDV"].ToString();
                    itemProtocolo.DescOrigemOcorrencia = dr2["DESCORI"].ToString();
                    itemProtocolo.numeroNotaFiscal = dr2["NUMNFV"].ToString();
                    itemProtocolo.valorLiquido = dr2["VALORLIQUIDO"].ToString();
                    itemProtocolo.DescTipoAtendimento = dr2["DESCATD"].ToString();
                    valorTotal += Convert.ToDecimal(itemProtocolo.valorLiquido);
                    itemProtocolo.valorTotal = valorTotal;
                    listaProtocolosPendentes.Add(itemProtocolo);
                }

                dr2.Close();
                conn.Close();
                return listaProtocolosPendentes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Valida todas as notas de outros protocolos
        /// </summary>
        /// <param name="codProtocolo">Código de Ocorrência</param>
        /// <param name="lista">Lista</param>
        /// <param name="tipAtend">Tipo de Atendimento</param>
        /// <param name="msgRetorno">Mensagem de Retorno</param>
        /// <returns>true/false</returns>
        public bool ValidaNotasOutroProtocolo(long? codProtocolo, List<Tuple<long, long>> lista, string tipAtend, out string msgRetorno, string usuarioLogado)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    msgRetorno = string.Empty;
                    var listaSituacao = new List<long>();
                    var tipoAtendimento = Convert.ToInt16(tipAtend);
                    
                    listaSituacao.Add((long)Enums.SituacaoRegistroOcorrencia.Pendente);
                    listaSituacao.Add((long)Enums.SituacaoRegistroOcorrencia.Fechado);
                    listaSituacao.Add((long)Enums.SituacaoRegistroOcorrencia.PreAprovado);
                    listaSituacao.Add((long)Enums.SituacaoRegistroOcorrencia.Aprovado);
                    listaSituacao.Add((long)Enums.SituacaoRegistroOcorrencia.Coleta);
                    listaSituacao.Add((long)Enums.SituacaoRegistroOcorrencia.Recebido);
                    listaSituacao.Add((long)Enums.SituacaoRegistroOcorrencia.Faturado);
                    listaSituacao.Add((long)Enums.SituacaoRegistroOcorrencia.Indenizado);

                    var listaItemNota = (from a in contexto.N0203REG
                                         join b in contexto.N0203IPV on new { a.NUMREG } equals new { b.NUMREG }
                                         where a.NUMREG == null
                                         select new { b.NUMREG, b.CODFIL, b.NUMNFV, b.CODPRO, b.CODDER }).FirstOrDefault();

                    foreach (var item in lista)
                    {
                        if (codProtocolo != null)
                        {
                            listaItemNota = (from a in contexto.N0203REG
                                             join b in contexto.N0203IPV on new { a.NUMREG } equals new { b.NUMREG }
                                             where a.NUMREG != codProtocolo && listaSituacao.Contains(a.SITREG) && b.CODFIL == item.Item1 && b.NUMNFV == item.Item2 && a.TIPATE == tipoAtendimento
                                             select new { b.NUMREG, b.CODFIL, b.NUMNFV, b.CODPRO, b.CODDER }).FirstOrDefault();
                        }
                        else
                        {
                            listaItemNota = (from a in contexto.N0203REG
                                             join b in contexto.N0203IPV on new { a.NUMREG } equals new { b.NUMREG }
                                             where listaSituacao.Contains(a.SITREG) && b.CODFIL == item.Item1 && b.NUMNFV == item.Item2 && a.TIPATE == tipoAtendimento
                                             select new { b.NUMREG, b.CODFIL, b.NUMNFV, b.CODPRO, b.CODDER }).FirstOrDefault();
                        }

                        if (listaItemNota != null && usuarioLogado != "luciana.vieira") 
                        {
                            //16/01/2017 - Rafael Baccin
                            //msgRetorno = msgRetorno + "O Registro de Ocorrência Nº " + listaItemNota.NUMREG.ToString() + " contém a nota ( " + item.Item1.ToString() + " ) " + item.Item2.ToString() + ".<br/>";
                            msgRetorno = msgRetorno + "O Registro de Ocorrência Nº " + listaItemNota.NUMREG.ToString() + " contém a nota ( " + item.Item1.ToString() + " ) " + item.Item2.ToString() + ".<br/>" +
                                "Inicio da Lista: { NUMREG = " + listaItemNota.NUMREG.ToString() + ", CODFIL = " + listaItemNota.CODFIL.ToString() + ", NUMNFV = " + listaItemNota.NUMNFV.ToString() +
                                ", CODPRO = " + listaItemNota.CODPRO + ", CODDER = " + listaItemNota.CODDER + "}";
                        }
                    }

                    if (msgRetorno != string.Empty)
                    {
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Valida notas de protocolos Reprovados
        /// </summary>
        /// <param name="codProtocolo">Código de Ocorrência</param>
        /// <param name="lista">Lista</param>
        /// <returns>True/False</returns>
        public bool ValidaNotasProtocoloReprovado(long codProtocolo, List<Tuple<long, long>> lista)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    var situacao = (long)Enums.SituacaoRegistroOcorrencia.Reprovado;
                    var listaNotasProcolo = (from a in contexto.N0203REG
                                             join b in contexto.N0203IPV on new { a.NUMREG } equals new { b.NUMREG }
                                             where b.NUMREG == codProtocolo && a.SITREG == situacao
                                             group b by new { b.NUMREG, b.CODFIL, b.NUMNFV } into grupo
                                             select new { grupo.Key.NUMREG, grupo.Key.CODFIL, grupo.Key.NUMNFV }).ToList();

                    if (listaNotasProcolo.Count > 0)
                    {
                        foreach (var item in lista)
                        {
                            var valida = (from a in listaNotasProcolo
                                          where a.CODFIL == item.Item1 && a.NUMNFV == item.Item2
                                          select a).FirstOrDefault();

                            if (valida == null)
                            {
                                return false;
                            }
                        }

                        return true;
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca informações para o relatório de Mês X Ocorrência
        /// </summary>
        /// <returns>Lista de Ocorrência</returns>
        public List<MesXOcorrencia> mesXOcorrencia()
        {
            try
            {
                string sql = @" SELECT COUNT(DISTINCT IPV.NUMREG || IPV.ORIOCO) QUANTIDADE,
                                       TO_CHAR(REG.DATGER, 'MONTH') MES,
                                       TO_CHAR(REG.DATGER, 'MM') MES,
                                       TO_CHAR(REG.DATGER, 'RRRR') ANO
                                  FROM NWMS_PRODUCAO.N0203REG REG
                                 INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                                    ON (IPV.NUMREG = REG.NUMREG)
                                 INNER JOIN NWMS_PRODUCAO.N0204MDV MDV
                                    ON (MDV.CODMDV = IPV.CODMOT)
                                 INNER JOIN NWMS_PRODUCAO.N0204ORI ORI
                                    ON (ORI.CODORI = IPV.ORIOCO)
                                 INNER JOIN NWMS_PRODUCAO.N0204ATD ATD
                                    ON (REG.TIPATE = ATD.CODATD)
                                 INNER JOIN SAPIENS.E135PFA PFA
                                    ON IPV.CODEMP = PFA.CODEMP
                                   AND IPV.CODFIL = PFA.FILNFV
                                   AND IPV.CODSNF = PFA.SNFNFV
                                   AND IPV.NUMNFV = PFA.NUMNFV
                                 INNER JOIN SAPIENS.E085CLI CLI
                                    ON (REG.CODCLI = CLI.CODCLI)
                                 WHERE REG.SITREG NOT IN (1,7,5) AND TO_DATE(TO_CHAR(REG.DATGER, 'DD/MM/RRRR')) BETWEEN TO_DATE(CONCAT('01/',TO_CHAR(ADD_MONTHS(SYSDATE, -6), 'MM/RRRR')), 'DD/MM/RRRR') AND TO_DATE(TO_CHAR(SYSDATE, 'DD/MM/RRRR'))
                                 GROUP BY --ORI.DESCORI,
                                          TO_CHAR(REG.DATGER, 'MONTH'),
                                          TO_CHAR(REG.DATGER, 'MM'),
                                          TO_CHAR(REG.DATGER, 'RRRR')
                                 ORDER BY TO_CHAR(REG.DATGER, 'RRRR'), TO_CHAR(REG.DATGER, 'MM') ASC";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<MesXOcorrencia> lista = new List<MesXOcorrencia>();
                MesXOcorrencia itens = new MesXOcorrencia();
                while (dr.Read())
                {
                    itens = new MesXOcorrencia();
                    itens.quantidade = Convert.ToInt32(dr["QUANTIDADE"]);
                    itens.mes = dr["MES"].ToString();
                    lista.Add(itens);
                }

                dr.Close();
                conn.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca informações para o relatório Mês X Origem
        /// </summary>
        /// <returns>Lista de Ocorrências</returns>
        public List<MesXOrigem> mesXOrigem()
        {
            try
            {
                string sql = @"     SELECT ORI.DESCORI,
                                           COUNT(DISTINCT IPV.NUMREG) AS QUANTIDADE,
                                           TO_CHAR(REG.DATGER, 'MONTH') MES,
                                           TO_CHAR(REG.DATGER, 'MM') MES,
                                           TO_CHAR(REG.DATGER, 'RRRR') ANO
                                      FROM NWMS_PRODUCAO.N0203REG REG
                                     INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                                        ON (IPV.NUMREG = REG.NUMREG)
                                     INNER JOIN NWMS_PRODUCAO.N0204MDV MDV
                                        ON (MDV.CODMDV = IPV.CODMOT)
                                     INNER JOIN NWMS_PRODUCAO.N0204ORI ORI
                                        ON (ORI.CODORI = IPV.ORIOCO)
                                     INNER JOIN NWMS_PRODUCAO.N0204ATD ATD
                                        ON (REG.TIPATE = ATD.CODATD)
                                     INNER JOIN SAPIENS.E135PFA PFA
                                        ON IPV.CODEMP = PFA.CODEMP
                                       AND IPV.CODFIL = PFA.FILNFV
                                       AND IPV.CODSNF = PFA.SNFNFV
                                       AND IPV.NUMNFV = PFA.NUMNFV
                                     INNER JOIN SAPIENS.E085CLI CLI
                                        ON (REG.CODCLI = CLI.CODCLI)
                                  WHERE REG.SITREG NOT IN (1,7,5) AND TO_DATE(TO_CHAR(REG.DATGER, 'DD/MM/RRRR')) BETWEEN TO_DATE(CONCAT('01/',TO_CHAR(ADD_MONTHS(SYSDATE, -6), 'MM/RRRR')), 'DD/MM/RRRR') AND TO_DATE(TO_CHAR(SYSDATE, 'DD/MM/RRRR'))
                                     GROUP BY ORI.DESCORI,
                                              TO_CHAR(REG.DATGER, 'MONTH'),
                                              TO_CHAR(REG.DATGER, 'MM'),
                                              TO_CHAR(REG.DATGER, 'RRRR')
                                              ORDER BY TO_CHAR(REG.DATGER, 'RRRR'), TO_CHAR(REG.DATGER, 'MM') ASC";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<MesXOrigem> lista = new List<MesXOrigem>();
                MesXOrigem itens = new MesXOrigem();

                while (dr.Read())
                {
                    itens = new MesXOrigem();
                    itens.quantidade = Convert.ToInt32(dr["QUANTIDADE"]);
                    itens.mes = dr["MES"].ToString();
                    itens.origem = dr["DESCORI"].ToString();
                    itens.ano = dr["ANO"].ToString();
                    lista.Add(itens);
                }

                dr.Close();
                conn.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Retorna a Média de pre aprovados
        /// </summary>
        /// <returns></returns>
        public int mediaPreAprovado()
        {
            int media = 0;
            try
            {
                string sql = @"SELECT NVL(ROUND(((SUM(H) + SUM(M / 60) + SUM(S / 36000)) / COUNT(*)), 0),0) AS MEDIAHORAPREPAROVADO
                                  FROM (SELECT DISTINCT REG.NUMREG,
                                                        REG.DATGER,
                                                        TRA.DATTRA,
                                                        TRUNC(((TRA.DATTRA - REG.DATGER) * 86400 / 3600)) H,
                                                        TRUNC(MOD((TRA.DATTRA - REG.DATGER) * 86400, 3600) / 60) M,
                                                        TRUNC(MOD(MOD((TRA.DATTRA - REG.DATGER) * 86400,
                                                                      3600),
                                                                  60)) S
                                          FROM NWMS_PRODUCAO.N0203REG REG
                                         INNER JOIN NWMS_PRODUCAO.N0203TRA TRA
                                            ON (REG.NUMREG = TRA.NUMREG)
                                         WHERE TRA.DESTRA = 'REGISTRO DE OCORRENCIA PRÉ APROVADO'
                                         GROUP BY REG.NUMREG, REG.DATGER, TRA.DATTRA)";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<MediaPreAprovado> listaProtocolosPendentes = new List<MediaPreAprovado>();
                MediaPreAprovado itemProtocolo = new MediaPreAprovado();

                if (dr.Read())
                {
                    media = Convert.ToInt32(dr["MEDIAHORAPREPAROVADO"]);
                }

                dr.Close();
                conn.Close();
                return media;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Retorna a média aprovado
        /// </summary>
        /// <returns>media</returns>
        public int mediaAprovado()
        {
            int media = 0;
            try
            {
                string sql = @"SELECT NVL(ROUND(((SUM(H) + SUM(M / 60) + SUM(S / 36000)) / COUNT(*)), 0),0) AS MEDIAHORAAPROVADO
                                      FROM (SELECT DISTINCT REG.NUMREG,
                                                            REG.DATGER,
                                                            TRA.DATTRA,
                                                            TRUNC(((TRA.DATTRA - REG.DATGER) * 86400 / 3600)) H,
                                                            TRUNC(MOD((TRA.DATTRA - REG.DATGER) * 86400, 3600) / 60) M,
                                                            TRUNC(MOD(MOD((TRA.DATTRA - REG.DATGER) * 86400,
                                                                          3600),
                                                                      60)) S
                                              FROM NWMS_PRODUCAO.N0203REG REG
                                             INNER JOIN NWMS_PRODUCAO.N0203TRA TRA
                                                ON (REG.NUMREG = TRA.NUMREG)
                                             WHERE TRA.DESTRA = 'REGISTRO DE OCORRENCIA APROVADO'
                                             GROUP BY REG.NUMREG, REG.DATGER, TRA.DATTRA)";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    media = Convert.ToInt32(dr["MEDIAHORAAPROVADO"]);
                }

                dr.Close();
                conn.Close();
                return media;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Busca informações das ocorrências para o relatório analitico
        /// </summary>
        /// <param name="campoNumeroRegistro">Código de Ocorrência</param>
        /// <param name="campoFilial">Código Filial</param>
        /// <param name="campoEmbarque">Código de Embarque</param>
        /// <param name="campoPlaca">Placa</param>
        /// <param name="campoPeriodoInicial">Periodo Inicial</param>
        /// <param name="campoPeriodoFinal">Periodo Final</param>
        /// <param name="campoCliente">Cliente</param>
        /// <param name="campoSituacao">Situação</param>
        /// <param name="campoDataFaturamento">Data Faturamento</param>
        /// <returns></returns>
        public List<RelatorioAnalitico> imprimirRelatorioAnaliticoRegistroOcorrencia(string campoNumeroRegistro, string campoFilial, string campoEmbarque, string campoPlaca, string campoPeriodoInicial, string campoPeriodoFinal, string campoCliente, string campoSituacao, string campoDataFaturamento)
        {
            var campoPlacaFormatado = campoPlaca.Replace("-", "").ToUpper();
            decimal SomaTotalValorLiquido = 0;

            try
            {
                string sql = @" SELECT REG.NUMREG,
                                       NFV.PLAVEI,
                                       REG.SITREG,
                                       REG.DATGER,
                                       REG.DATULT,
                                       REG.OBSREG,
                                       ATD.CODATD,
                                       ATD.DESCATD,
                                       ATDIPV.CODATD AS IPVCODATD,
                                       ATDIPV.DESCATD AS IPVDESCATD,
                                       COALESCE(UPPER(MOT.NOMMOT),'TRANSPORTADORA')   MOTORISTA,
                                       COALESCE(MOT.CODMTR, 0) CODMOT,
                                       MOTIVO.DESCMDV MOTIVO,
                                       NOMEULT.NOMCOM,
                                       NOMEGER.NOMCOM,
                                       USUGER.CODUSU AS CODUSUGER,
                                       USUULT.CODUSU AS CODUSUULT,
                                       PFA.CODFIL,
                                       PFA.NUMANE,
                                       CLI.CODCLI,
                                       CLI.NOMCLI,
                                       ORI.CODORI,
                                       ORI.DESCORI,
                                       PFA.NUMANE,
                                       PFA.CODFIL,
                                       IPV.CODSNF,
                                       IPV.NUMNFV,
                                       IPV.SEQIPV,
                                       IPV.CODDER,
                                       IPV.CPLIPV,
                                       IPV.CODDEP,
                                       IPV.PREUNI,
                                       IPV.QTDFAT,
                                       IPV.ORIOCO AS IPVCODORI,
                                       ORIIPV.DESCORI AS IPVDESC,
                                       IPV.CODMOT,
                                       IPV.QTDDEV,
                                       IPV.TNSPRO,
                                       IPV.USUULT,
                                       IPV.PEROFE,
                                       IPV.PERIPI,
                                       (IPV.QTDDEV * IPV.PREUNI) VALORBRUTO,
                                       IPV.PEROFE,
                                       IPV.CODEMP,
                                       IPV.CODFIL,
                                       IPV.CODPRO,
                                       IPV.VLRIPI,
                                       IPV.PREUNI,
                                       IPV.VLRST,
                                       (EV.VLRDZF + EV.VLRPIT + EV.VLRCRT) AS SUFRAMA,
                                       COALESCE(IPV.VLRFRE,0) VLRFRE,
                                       IPV.TNSPRO
                                  FROM NWMS_PRODUCAO.N0203REG REG

                                 INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                                    ON (IPV.NUMREG = REG.NUMREG)

                                 INNER JOIN NWMS_PRODUCAO.N0204ORI ORI
                                    ON (REG.ORIOCO = ORI.CODORI)

                                 INNER JOIN NWMS_PRODUCAO.N0204ORI ORIIPV
                                    ON (IPV.ORIOCO = ORIIPV.CODORI)

                                 INNER JOIN NWMS_PRODUCAO.N0204ATD ATD
                                    ON (REG.TIPATE = ATD.CODATD)

                                 INNER JOIN NWMS_PRODUCAO.N0204ATD ATDIPV
                                    ON (REG.TIPATE = ATDIPV.CODATD)

                                 INNER JOIN SAPIENS.E085CLI CLI
                                    ON (REG.CODCLI = CLI.CODCLI)

                                 INNER JOIN SAPIENS.E140NFV NFV
                                    ON NFV.CODEMP = IPV.CODEMP
                                   AND NFV.CODFIL = IPV.CODFIL
                                   AND NFV.NUMNFV = IPV.NUMNFV
                                   AND NFV.CODSNF = IPV.CODSNF

                                LEFT JOIN SAPIENS.E073MOT MOT
                                  ON (NFV.CODMTR = MOT.CODMTR AND NFV.CODTRA = MOT.CODTRA)

                                 LEFT JOIN SAPIENS.E135PFA PFA
                                    ON IPV.CODEMP = PFA.CODEMP
                                   AND IPV.CODFIL = PFA.FILNFV
                                   AND IPV.CODSNF = PFA.SNFNFV
                                   AND IPV.NUMNFV = PFA.NUMNFV

                                 INNER JOIN NWMS_PRODUCAO.N9999USU USUGER
                                    ON (REG.USUGER = USUGER.CODUSU)

                                 INNER JOIN NWMS_PRODUCAO.N9999USU USUULT
                                    ON (REG.USUULT = USUULT.CODUSU)

                                 INNER JOIN SAPIENS.R999USU LOGIGER
                                    ON (USUGER.LOGIN = LOGIGER.NOMUSU)

                                 INNER JOIN SAPIENS.R910USU NOMEGER
                                    ON (LOGIGER.CODUSU = NOMEGER.CODENT)

                                 INNER JOIN SAPIENS.R999USU LOGULT
                                    ON (USUULT.LOGIN = LOGULT.NOMUSU)

                                 INNER JOIN SAPIENS.R910USU NOMEULT
                                    ON (LOGULT.CODUSU = NOMEULT.CODENT)

                                 INNER JOIN SAPIENS.E075PRO PRO
                                    ON (IPV.CODPRO = PRO.CODPRO)

                                 INNER JOIN NWMS_PRODUCAO.N0204MDV MOTIVO 
                                    ON (IPV.CODMOT = MOTIVO.CODMDV)

                                 INNER JOIN SAPIENS.E075DER DER
                                    ON (IPV.CODDER = DER.CODDER AND IPV.CODPRO = DER.CODPRO) 
                                INNER JOIN SAPIENS.E140IPV EV
                                    ON (EV.CODEMP = IPV.CODEMP
                                    AND EV.CODFIL = IPV.CODFIL
                                    AND EV.CODSNF = IPV.CODSNF
                                    AND EV.NUMNFV = IPV.NUMNFV
                                    AND EV.SEQIPV = IPV.SEQIPV
                                    AND EV.CODPRO = IPV.CODPRO
                                    AND EV.CODDER = IPV.CODDER)
                                WHERE 1 = 1";

                sql += campoNumeroRegistro != "" ? " AND REG.NUMREG =" + campoNumeroRegistro : "";
                sql += campoFilial != "" && campoEmbarque != "" ? " AND PFA.CODFIL =" + campoFilial + " AND PFA.NUMANE =" + campoEmbarque : "";
                sql += campoPlaca != "" ? " AND REG.PLACA ='" + campoPlacaFormatado + "'" : "";
                sql += campoPeriodoInicial != "" && campoPeriodoFinal != "" ? " AND  TO_DATE(TO_CHAR(REG.DATGER , 'DD-MM-RRRR')) BETWEEN '" + DateTime.Parse(campoPeriodoInicial).ToShortDateString() + "' AND '" + DateTime.Parse(campoPeriodoFinal).ToShortDateString() + "'" : "";
                sql += campoCliente != "" ? " AND CLI.CODCLI =" + campoCliente : "";
                sql += campoSituacao != "0" ? "AND REG.SITREG =" + campoSituacao : "";
                sql += campoDataFaturamento != "" ? "AND IPV.DATEMI ='" + DateTime.Parse(campoDataFaturamento).ToShortDateString() + "'" : "";
                sql += @" GROUP BY  REG.NUMREG, NFV.PLAVEI,
                                       REG.SITREG,
                                       REG.DATGER,
                                       REG.DATULT,
                                       REG.OBSREG,
                                       ATD.CODATD,
                                       ATD.DESCATD,
                                       ATDIPV.CODATD,
                                       ATDIPV.DESCATD,
                                       UPPER(MOT.NOMMOT),
                                       MOT.CODMOT,
                                       NOMEULT.NOMCOM,
                                       NOMEGER.NOMCOM,
                                       USUGER.CODUSU,
                                       USUULT.CODUSU,
                                       PFA.CODFIL,
                                       PFA.NUMANE,
                                       CLI.CODCLI,
                                       CLI.NOMCLI,
                                       ORI.CODORI,
                                       ORI.DESCORI,
                                       PFA.NUMANE,
                                       PFA.CODFIL,
                                       IPV.CODSNF,
                                       MOT.CODMTR,
                                       IPV.NUMNFV,
                                       IPV.SEQIPV,
                                       IPV.CODDER,
                                       IPV.CPLIPV,
                                       IPV.CODDEP,
                                       IPV.PREUNI,
                                       IPV.QTDFAT,
                                       IPV.ORIOCO,
                                       ORIIPV.DESCORI,
                                       IPV.CODMOT,
                                       IPV.QTDDEV,
                                       MOTIVO.DESCMDV,
                                       IPV.TNSPRO,
                                       IPV.USUULT,
                                       IPV.PEROFE,
                                       IPV.PERIPI,
                                       IPV.PEROFE,
                                       IPV.CODEMP,
                                       IPV.CODFIL,
                                       IPV.CODPRO,
                                       IPV.VLRIPI,
                                       IPV.PREUNI,
                                       IPV.VLRST,
                                       IPV.VLRFRE,
                                       IPV.TNSPRO,
                                       (EV.VLRDZF + EV.VLRPIT + EV.VLRCRT)
                                       ORDER BY IPV.SEQIPV";
                DebugEmail email = new DebugEmail();
                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<RelatorioAnalitico> lista = new List<RelatorioAnalitico>();
                RelatorioAnalitico itens = new RelatorioAnalitico();
                int contado = 0;

                while (dr.Read())
                {
                    itens = new RelatorioAnalitico();
                     itens.CodigoRegistro = Convert.ToInt64(dr["NUMREG"]);
                    itens.CodTipoAtendimento = dr["CODATD"].ToString();
                    itens.DescTipoAtendimento = dr["DESCATD"].ToString();
                    itens.CodOrigemOcorrencia = dr["CODORI"].ToString();
                    itens.DescOrigemOcorrencia =  dr["DESCORI"].ToString();
                    itens.CodCliente = dr["CODCLI"].ToString(); ;
                    itens.NomeCliente = dr["NOMCLI"].ToString();
                    itens.CodMotorista = dr["CODMOT"].ToString();
                    itens.CodPlaca = dr["PLAVEI"].ToString();
                    itens.NomeMotorista = dr["MOTORISTA"].ToString();
                    itens.DataHrGeracao = dr["DATGER"].ToString();
                    itens.NomeUsuarioGeracao = dr["NOMCOM"].ToString();
                    itens.UsuarioGeracao = dr["CODUSUGER"].ToString();
                    itens.CodSituacaoRegistro = dr["SITREG"].ToString();
                    
                    switch (itens.CodSituacaoRegistro)
                    {
                        case "1":
                            itens.DescSituacaoRegistro = "Rascunho";
                            break;
                        case "2":
                            itens.DescSituacaoRegistro = "Aguardando Aprovação";
                            break;
                        case "3":
                            itens.DescSituacaoRegistro = "Integrado";
                            break;
                        case "4":
                            itens.DescSituacaoRegistro = "Aprovado";
                            break;
                        case "5":
                            itens.DescSituacaoRegistro = "Reprovado";
                            break;
                        case "6":
                            itens.DescSituacaoRegistro = "Reabilitado";
                            break;
                        case "8":
                            itens.DescSituacaoRegistro = "Coleta";
                            break;
                        case "9":
                            itens.DescSituacaoRegistro = "Conferido";
                            break;
                        case "10":
                            itens.DescSituacaoRegistro = "Faturado";
                            break;
                        case "7":
                            itens.DescSituacaoRegistro = "Cancelado";
                            break;
                        case "11":
                            itens.DescSituacaoRegistro = "Indenizado";
                            break;
                    }
                    
                    itens.UltimaAlteracao = dr["DATULT"].ToString(); ;
                    itens.NomeUsuarioUltimaAlteracao = dr["NOMCOM"].ToString();
                    itens.UsuarioUltimaAlteracao = dr["CODUSUULT"].ToString();
                    itens.Observacao = dr["OBSREG"].ToString();
                    // Itens Devolução
                    itens.Empresa = dr["CODEMP"].ToString();
                    itens.Filial = 1;

                    // Add item na Lista de analises de embarque

                    itens.SerieNota = dr["CODSNF"].ToString();
                    itens.NumeroNota = dr["NUMNFV"].ToString();
                    itens.SeqNota = Convert.ToInt64(dr["SEQIPV"]);
                    itens.CodPro = dr["CODPRO"].ToString();
                    itens.CodDer = dr["CODDER"].ToString();
                    itens.DescPro = dr["CPLIPV"].ToString();
                    itens.CodDepartamento = dr["CODDEP"].ToString();
                    itens.QtdeFat = Convert.ToInt64(dr["QTDFAT"]);

                    itens.PrecoUnitario = dr["PREUNI"].ToString();
                    itens.CodOrigemOcorrenciaItemDev = dr["IPVCODORI"].ToString();
                    itens.DescOrigemOcorrenciaItemDev = dr["IPVDESC"].ToString();
                    itens.CodMotivoDevolucao = dr["IPVCODATD"].ToString();
                    itens.DescMotivoDevolucao = dr["MOTIVO"].ToString();
                    itens.QtdeDevolucao = Convert.ToInt64(dr["QTDDEV"]);
                    itens.PercDesconto = dr["PEROFE"].ToString();
                    itens.PercIpi = dr["PERIPI"].ToString();

                    decimal PercIpiDecimal = Convert.ToInt64(dr["PERIPI"]);

                    itens.ValorBruto = decimal.Parse((itens.QtdeDevolucao * Convert.ToDecimal(itens.PrecoUnitario)).ToString());
                    itens.ValorBrutoS = itens.ValorBruto.ToString("###,###,##0.00");
                    itens.ValorIpi = Convert.ToDecimal(dr["VLRIPI"]);
                    itens.ValorSt = Convert.ToInt64(dr["VLRST"]);
                    itens.TipoTransacao = dr["TNSPRO"].ToString();
                    itens.AnaliseEmbarque = dr["NUMANE"].ToString();
                    itens.Suframa = Convert.ToDecimal(dr["SUFRAMA"]);
                    itens.Filial = 1;
                    itens.valorFrete = Convert.ToDecimal(dr["VLRFRE"]);

                    itens.ValorIpi = (itens.ValorBruto * PercIpiDecimal) / 100; //itens.QtdeDevolucao * (itens.ValorIpi / itens.QtdeFat);
                    itens.ValorIpiS = itens.ValorIpi.ToString("###,###,##0.00");
                    itens.ValorSt = itens.QtdeDevolucao * (itens.ValorSt / itens.QtdeFat);
                    itens.ValorStS = itens.ValorSt.ToString("###,###,##0.00");
                    itens.Suframa = itens.QtdeDevolucao * (itens.Suframa / itens.QtdeFat);
                    itens.SuframaS = itens.Suframa.ToString("###,###,##0.##");
                    itens.ValorLiquido = (itens.QtdeDevolucao * decimal.Parse(itens.PrecoUnitario.ToString())) + itens.ValorIpi + itens.ValorSt + itens.valorFrete - itens.Suframa;
                    itens.ValorLiquidoS = itens.ValorLiquido.ToString("###,###,##0.00");

                    SomaTotalValorLiquido = SomaTotalValorLiquido + itens.ValorLiquido;
                    itens.TotalValorLiquido = SomaTotalValorLiquido;
                    contado++;
                    lista.Add(itens);
                }
                
                dr.Close();
                conn.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Pesquisa as Ocorrências
        /// </summary>
        /// <param name="campoNumeroRegistro">Código de Ocorrência</param>
        /// <param name="campoFilial">Código Filial</param>
        /// <param name="campoEmbarque">Código de embarque</param>
        /// <param name="campoPlaca">Placa</param>
        /// <param name="campoPeriodoInicial">Periodo Inicial</param>
        /// <param name="campoPeriodoFinal">Periodo Final</param>
        /// <param name="campoCliente">Código Cliente</param>
        /// <param name="campoSituacao">Situação</param>
        /// <param name="campoDataFaturamento">Data Faturamento</param>
        /// <param name="codigoUsuario">Código Usuário</param>
        /// <param name="tipo">Tipo</param>
        /// <param name="operacao">Operação</param>
        /// <returns></returns>
        public List<Ocorrencia> pesquisaOcorrencia(string campoNumeroRegistro, string campoFilial, string campoEmbarque, string campoPlaca, string campoPeriodoInicial, string campoPeriodoFinal, string campoCliente, string campoSituacao, string campoDataFaturamento, long codigoUsuario, string tipo, string operacao)
        {
            double SomaTotalValorLiquido = 0;
            var campoPlacaFormatado = campoPlaca.Replace("-", "").ToUpper();
            string sql = @" SELECT REG.NUMREG,
                                   REG.SITREG,
                                   TO_CHAR(REG.DATGER, 'DD/MM/RRRR') DATGER,
                                   IPV.NUMNFV,
                                   ORI.DESCORI,
                                   CLI.CODCLI,
                                   PFA.CODFIL,
                                   PFA.NUMANE,
                                   CLI.NOMCLI,
                                   ATD.DESCATD,
                                   MDV.DESCMDV,
                                   SUM(IPV.VLRLIQ) VLRLIQ,
                                   ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV), 2) VALORLIQUIDO
                              FROM NWMS_PRODUCAO.N0203REG REG
                             INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                                ON (IPV.NUMREG = REG.NUMREG)
                             INNER JOIN NWMS_PRODUCAO.N0204MDV MDV
                                ON (MDV.CODMDV = IPV.CODMOT)
                             INNER JOIN NWMS_PRODUCAO.N0204ORI ORI
                                ON (ORI.CODORI = IPV.ORIOCO)
                             INNER JOIN NWMS_PRODUCAO.N0204ATD ATD
                                ON (REG.TIPATE = ATD.CODATD)
                             INNER JOIN SAPIENS.E135PFA PFA
                                ON IPV.CODEMP = PFA.CODEMP
                               AND IPV.CODFIL = PFA.FILNFV
                               AND IPV.CODSNF = PFA.SNFNFV
                               AND IPV.NUMNFV = PFA.NUMNFV
                             INNER JOIN SAPIENS.E085CLI CLI
                                ON (REG.CODCLI = CLI.CODCLI)
                             WHERE 1 = 1 ";
            if (operacao == "U")
            {
                sql += "AND REG.USUGER = " + codigoUsuario + "";
            }
            if (campoSituacao == "2" && tipo == "D")
            {
                sql += "AND NOT EXISTS (SELECT 1 " +
                 "           FROM NWMS_PRODUCAO.N0203TRA TRA" +
                 "          WHERE TRA.NUMREG = REG.NUMREG" +
                 "            AND TRA.DESTRA = 'REGISTRO DE OCORRENCIA PRÉ APROVADO'" +
                 "            AND TRA.CODORI IN (SELECT DORI1.CODORI" +
                 "          FROM NWMS_PRODUCAO.N0204DORI DORI1" +
                 "         WHERE DORI1.CODUSU = " + codigoUsuario + ")) ";
            }
            if (tipo == "D" && campoSituacao != "2")
            {
                sql += " AND ORI.CODORI IN (SELECT DORI2.CODORI FROM NWMS_PRODUCAO.N0204DORI DORI2 WHERE DORI2.CODUSU = " + codigoUsuario + ") ";
            }
            sql += campoNumeroRegistro != "" ? "AND REG.NUMREG =" + campoNumeroRegistro : "";
            sql += campoFilial != "" && campoEmbarque != "" ? "AND PFA.CODFIL =" + campoFilial + " AND PFA.NUMANE =" + campoEmbarque : "";
            sql += campoPlaca != "" ? "AND REG.PLACA ='" + campoPlacaFormatado + "'" : "";
            sql += campoPeriodoInicial != "" && campoPeriodoFinal != "" ? "AND REG.DATGER BETWEEN '" + campoPeriodoInicial + "' AND '" + campoPeriodoFinal + "'" : "";
            sql += campoCliente != "" ? "AND CLI.CODCLI =" + campoCliente : "";
            sql += campoSituacao != "0" && campoSituacao != "" ? "AND REG.SITREG IN(" + campoSituacao + ")" : "";
            sql += campoDataFaturamento != "" ? "AND IPV.DATEMI ='" + campoDataFaturamento + "'" : "";

            sql += @"GROUP BY REG.NUMREG,
                                      REG.SITREG,
                                      REG.DATGER,
                                      IPV.NUMNFV,
                                      IPV.CODFIL,
                                      CLI.CODCLI,
                                      CLI.NOMCLI,
                                      PFA.NUMANE,
                                      ORI.DESCORI,
                                      PFA.CODFIL,
                                      ATD.DESCATD,
                                      MDV.DESCMDV
                             ORDER BY REG.DATGER ASC";

            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();

            List<Ocorrencia> lista = new List<Ocorrencia>();
            Ocorrencia itens = new Ocorrencia();
            
            DateTime dateForButton = DateTime.Now.AddDays(-30);

            while (dr.Read())
            {
                itens = new Ocorrencia();
                itens.CodigoRegistro = Convert.ToInt64(dr["NUMREG"]); //Ocorrência
                itens.DescTipoAtendimento = dr["DESCATD"].ToString(); //Atendimento
                itens.DescOrigemOcorrencia = dr["DESCORI"].ToString(); //Origem
                itens.CodCliente = dr["CODCLI"].ToString();
                itens.NomeCliente = dr["NOMCLI"].ToString();
                itens.ValorLiquido = decimal.Parse(dr["VLRLIQ"].ToString());
                itens.DataHrGeracao = dr["DATGER"].ToString(); //Emissão
                itens.CodSituacaoRegistro = dr["SITREG"].ToString();
                itens.DescMotivoDevolucao = dr["DESCMDV"].ToString(); //Motivo
                itens.NumeroNota = dr["NUMNFV"].ToString(); //NF

                if (Convert.ToDateTime(itens.DataHrGeracao) < dateForButton)
                {
                    itens.MenorqueTrintaDias = "S";
                    itens.MaiorqueTrintaDias = "N";
                }
                else
                {
                    itens.MenorqueTrintaDias = "N";
                    itens.MaiorqueTrintaDias = "S";
                }

                itens.ValorLiquido = decimal.Parse(dr["VALORLIQUIDO"].ToString()); //Valor Liquido
                switch (dr["SITREG"].ToString())
                {
                    case "1":
                        itens.DescSituacaoRegistro = "Rascunho";
                        break;
                    case "2":
                        itens.DescSituacaoRegistro = "Aguardando Aprovação";
                        break;
                    case "3":
                        itens.DescSituacaoRegistro = "Integrado";
                        break;
                    case "4":
                        itens.DescSituacaoRegistro = "Aprovado";
                        break;
                    case "5":
                        itens.DescSituacaoRegistro = "Reprovado";
                        break;
                    case "6":
                        itens.DescSituacaoRegistro = "Reabilitado";
                        break;
                    case "8":
                        itens.DescSituacaoRegistro = "Coleta";
                        break;
                    case "9":
                        itens.DescSituacaoRegistro = "Conferido";
                        break;
                    case "10":
                        itens.DescSituacaoRegistro = "Faturado";
                        break;
                    case "7":
                        itens.DescSituacaoRegistro = "Reprovado";
                        break;
                    case "11":
                        itens.DescSituacaoRegistro = "Indenizado";
                        break;
                }
                
                SomaTotalValorLiquido += Convert.ToDouble(dr["VALORLIQUIDO"].ToString());
                itens.TotalValorLiquidoD = SomaTotalValorLiquido.ToString("###,###,##0.00"); ;
                lista.Add(itens);
            }

            dr.Close();
            conn.Close();
            return lista;
        }

        public int pedidosFaturarIndenizacao()
        {
            string sql = "SELECT COUNT(DISTINCT(REG.NUMREG)) AS CONTADOR FROM N0203REG REG, N0203IPV IPV WHERE REG.NUMREG = IPV.NUMREG AND REG.SITREG = 11 AND IPV.ORIOCO = 8";

            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();
            int contador = 0;
            while (dr.Read())
            {
                contador = Convert.ToInt32(dr["CONTADOR"].ToString());
            }
            return contador;
        }

        public List<RelatorioGraficoOcorrencia> relatorioGraficoOcorrencias(string mes, string ano, string indicador)
        {
            string origem = tratarOrigem(indicador);
            string sql = "SELECT REG.NUMREG," +
                      "                 ATD.DESCATD," +
                      "                 to_char(IPV.DATEMI, 'dd/mm/yyyy') AS DATEMI," +
                      "                 ORI.DESCORI," +
                      "                 MDV.DESCMDV," +
                      "                 IPV.NUMNFV," +
                      "                  ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV),2) VLRLIQ" +
                      "            FROM NWMS_PRODUCAO.N0203REG REG" +
                      "           INNER JOIN NWMS_PRODUCAO.N0203IPV IPV" +
                      "              ON (IPV.NUMREG = REG.NUMREG)" +
                      "           INNER JOIN NWMS_PRODUCAO.N0204MDV MDV" +
                      "              ON (MDV.CODMDV = IPV.CODMOT)" +
                      "           INNER JOIN NWMS_PRODUCAO.N0204ORI ORI" +
                      "              ON (ORI.CODORI = IPV.ORIOCO)" +
                      "           INNER JOIN NWMS_PRODUCAO.N0204ATD ATD" +
                      "              ON (REG.TIPATE = ATD.CODATD)" +
                      "           INNER JOIN SAPIENS.E135PFA PFA" +
                      "              ON IPV.CODEMP = PFA.CODEMP" +
                      "                 AND IPV.CODFIL = PFA.FILNFV" +
                      "                 AND IPV.CODSNF = PFA.SNFNFV" +
                      "                 AND IPV.NUMNFV = PFA.NUMNFV" +
                      "           INNER JOIN SAPIENS.E085CLI CLI" +
                      "              ON (REG.CODCLI = CLI.CODCLI)" +
                      "           WHERE REG.SITREG NOT IN (1," +
                      "                                7," +
                      "                                5)" +
                      "                 AND TO_CHAR(REG.DATGER," +
                      "                             'MM') = " + mes + "" +
                      "                 AND TO_CHAR(REG.DATGER," +
                      "                             'YYYY') = " + ano + "" +
                      "                 AND ORI.CODORI = " + origem + "" +
                      "           GROUP BY REG.NUMREG," +
                      "                    ATD.DESCATD," +
                      "                    IPV.DATEMI," +
                      "                    ORI.DESCORI," +
                      "                    MDV.DESCMDV," +
                      "                    IPV.NUMNFV";

            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();

            List<RelatorioGraficoOcorrencia> lista = new List<RelatorioGraficoOcorrencia>();
            RelatorioGraficoOcorrencia itens = new RelatorioGraficoOcorrencia();

            while (dr.Read())
            {
                itens = new RelatorioGraficoOcorrencia();
                itens.NUMREG = Convert.ToInt32(dr["NUMREG"]);
                itens.DESCATD = dr["DESCATD"].ToString();
                itens.DESCORI = dr["DESCORI"].ToString();
                itens.DATGER = dr["DATEMI"].ToString();
                itens.DESCMDV = dr["DESCMDV"].ToString();
                itens.NUMNFV = dr["NUMNFV"].ToString();
                itens.VALORLIQUIDO = decimal.Parse(dr["VLRLIQ"].ToString());
                lista.Add(itens);
            }

            dr.Close();
            conn.Close();
            return lista;
        }

        /// <summary>
        /// busca informações dos indicadores por setor
        /// </summary>
        /// <param name="status">Status</param>
        /// <param name="mes">Mês</param>
        /// <param name="indicador">Indicador</param>
        /// <param name="ano">Ano</param>
        /// <returns></returns>
        public List<Ocorrencia> CarregarIndicadorSetores(string status, string mes, string indicador, string ano)
        {
            double SomaTotalValorLiquido = 0;
            string origem = tratarOrigem(indicador);

            string sql = @" SELECT DISTINCT AGP.DESAGP, ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV), 2) VALORLIQUIDO
                              FROM NWMS_PRODUCAO.N0203REG REG
                             INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                                ON (IPV.NUMREG = REG.NUMREG)
                             INNER JOIN NWMS_PRODUCAO.N0204ORI ORI
                                ON (ORI.CODORI = IPV.ORIOCO)
                             INNER JOIN SAPIENS.E085CLI CLI
                                ON (REG.CODCLI = CLI.CODCLI)
                             INNER JOIN SAPIENS.E075PRO PRO
                                ON PRO.CODPRO = IPV.CODPRO
                             INNER JOIN SAPIENS.E013AGP AGP
                                ON AGP.CODAGP = PRO.CODAGP
                             INNER JOIN NWMS_PRODUCAO.N0204MDV MDV
                                ON (MDV.CODMDV = IPV.CODMOT)
                             WHERE 1 = 1 ";
            sql += " AND EXTRACT(MONTH FROM REG.DATGER) = " + mes + " AND EXTRACT(YEAR FROM REG.DATGER) = " + ano + "";
            sql += status != "0" && status != null && status != "" ? status == "Integrado" ? "AND REG.SITREG = 3 " : "AND REG.SITREG = 4 " + " " : " AND REG.SITREG NOT IN (1,7,5) ";
            sql += " AND IPV.ORIOCO = " + origem + "" +
                   "          GROUP BY AGP.DESAGP ORDER BY VALORLIQUIDO DESC";

            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();

            List<Ocorrencia> lista = new List<Ocorrencia>();
            Ocorrencia itens = new Ocorrencia();

            while (dr.Read())
            {
                itens = new Ocorrencia();
                itens.DescAgrupamento = dr["DESAGP"].ToString();
                itens.TotalValorLiquidoD = dr["VALORLIQUIDO"].ToString();
                SomaTotalValorLiquido += Convert.ToDouble(dr["VALORLIQUIDO"].ToString());
                itens.TotalValorLiquidoS = SomaTotalValorLiquido.ToString("###,###,##0.00"); ;
                lista.Add(itens);
            }

            dr.Close();
            conn.Close();
            return lista;
        }

        /// <summary>
        /// busca as informações das ocorrências
        /// </summary>
        /// <param name="numreg">Código de Ocorrência</param>
        /// <returns></returns>
        public List<String> listarObservacoes(string numreg)
        {
            string sql = @"SELECT DISTINCT REG.OBSREG SAC, TRA.OBSTRA APROVADOR
                             FROM NWMS_PRODUCAO.N0203REG REG
                            INNER JOIN NWMS_PRODUCAO.N0203TRA TRA
                               ON TRA.NUMREG = REG.NUMREG";
            sql += "  WHERE REG.NUMREG = " + numreg + " AND CODORI IS NOT NULL " +
                 "   GROUP BY REG.OBSREG,TRA.OBSTRA";

            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();

            List<String> lista = new List<String>();
            var SAC = "";
            var APROVADOR = "";

            while (dr.Read())
            {
                SAC = dr["SAC"].ToString();
                APROVADOR = dr["APROVADOR"].ToString();
                lista.Add(SAC);
                lista.Add(APROVADOR);
            }

            dr.Close();
            conn.Close();
            return lista;
        }

        /// <summary>
        /// Faz o tratamento das origens dos indicadores
        /// </summary>
        /// <param name="indicador">Indicador</param>
        /// <returns></returns>
        public String tratarOrigem(string indicador)
        {
            switch (indicador)
            {
                case "myBarChartContabilidade":
                    return "3";
                case "myBarChartTransporte":
                    return "7";
                case "myBarChartCliente":
                    return "2";
                case "myBarChartComercial":
                    return "1";
                case "myBarChartIndustrial":
                    return "5";
                case "myBarChartExpedicao":
                    return "4";
                case "myBarChartRepresentante":
                    return "6";
                case "myBarChartIndenizado":
                    return "8";
            }
            return "";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">Status</param>
        /// <param name="mes">Mês</param>
        /// <param name="filtroAgrup">Filtro AGrupamento</param>
        /// <param name="indicador">Indicador</param>
        /// <param name="ano">Ano</param>
        /// <returns></returns>
        public List<Ocorrencia> ocorrenciaDrill(string status, string mes, string filtroAgrup, string indicador, string ano)
        {
            string origem = tratarOrigem(indicador);
            try
            {
                string sql = "SELECT REG.NUMREG," +
                      "                 ATD.DESCATD," +
                      "                 IPV.DATEMI," +
                      "                 ORI.DESCORI," +
                      "                 MDV.DESCMDV," +
                      "                 IPV.NUMNFV," +
                      "                  ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV),2) VLRLIQ" +
                      "            FROM NWMS_PRODUCAO.N0203REG REG" +
                      "           INNER JOIN NWMS_PRODUCAO.N0203IPV IPV" +
                      "              ON (IPV.NUMREG = REG.NUMREG)" +
                      "           INNER JOIN NWMS_PRODUCAO.N0204MDV MDV" +
                      "              ON (MDV.CODMDV = IPV.CODMOT)" +
                      "           INNER JOIN NWMS_PRODUCAO.N0204ORI ORI" +
                      "              ON (ORI.CODORI = IPV.ORIOCO)" +
                      "           INNER JOIN NWMS_PRODUCAO.N0204ATD ATD" +
                      "              ON (REG.TIPATE = ATD.CODATD)" +
                      "           INNER JOIN SAPIENS.E135PFA PFA" +
                      "              ON IPV.CODEMP = PFA.CODEMP" +
                      "                 AND IPV.CODFIL = PFA.FILNFV" +
                      "                 AND IPV.CODSNF = PFA.SNFNFV" +
                      "                 AND IPV.NUMNFV = PFA.NUMNFV" +
                      "           INNER JOIN SAPIENS.E085CLI CLI" +
                      "              ON (REG.CODCLI = CLI.CODCLI)" +
                      "           WHERE REG.SITREG NOT IN (1," +
                      "                                7," +
                      "                                5)" +
                      "                 AND TO_CHAR(REG.DATGER," +
                      "                             'MM') = " + mes + "" +
                      "                 AND TO_CHAR(REG.DATGER," +
                      "                             'YYYY') = " + ano + "" +
                      "                 AND ORI.CODORI = " + origem + "" +
                      "           GROUP BY REG.NUMREG," +
                      "                    ATD.DESCATD," +
                      "                    IPV.DATEMI," +
                      "                    ORI.DESCORI," +
                      "                    MDV.DESCMDV," +
                      "                    IPV.NUMNFV";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<Ocorrencia> lista = new List<Ocorrencia>();
                Ocorrencia itens = new Ocorrencia();
                while (dr.Read())
                {
                    itens = new Ocorrencia();
                    itens.CodigoRegistro = Convert.ToInt64(dr["NUMREG"]);
                    itens.DescTipoAtendimento = dr["DESCATD"].ToString();
                    itens.DATAEMISSAO = dr["DATEMI"].ToString();
                    itens.DescOrigemOcorrencia = dr["DESCORI"].ToString();
                    itens.DescMotivoDevolucao = dr["DESCMDV"].ToString();
                    itens.NumeroNota = dr["NUMNFV"].ToString();
                    itens.ValorLiquido = Convert.ToInt64(dr["VLRLIQ"]);
                    itens.ValorLiquidoS = itens.ValorLiquido.ToString("###,###,##0.00");
                    lista.Add(itens);
                }

                dr.Close();
                conn.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca as ocorrências com faturamento atrasado
        /// </summary>
        /// <returns>Lista</returns>
        public List<Ocorrencia> carregarOcorrenciaFaturamentoAtrasado()
        {
            try
            {
                string sql = @"SELECT DISTINCT REG.NUMREG,
                                        ATD.DESCATD,
                                        IPV.DATEMI,
                                        TRA.DATTRA,
                                        ORI.DESCORI,
                                        MDV.DESCMDV,
                                        IPV.NUMNFV,
                                        ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV), 2) VLRLIQ,
                                        TRA.DATTRA DATASIT
                          FROM NWMS_PRODUCAO.N0203REG REG
                         INNER JOIN NWMS_PRODUCAO.N0203TRA TRA
                            ON REG.NUMREG = TRA.NUMREG
                         INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                            ON (IPV.NUMREG = REG.NUMREG)
                         INNER JOIN NWMS_PRODUCAO.N0204MDV MDV
                            ON (MDV.CODMDV = IPV.CODMOT)
                         INNER JOIN NWMS_PRODUCAO.N0204ORI ORI
                            ON (ORI.CODORI = IPV.ORIOCO)
                         INNER JOIN NWMS_PRODUCAO.N0204ATD ATD
                            ON (REG.TIPATE = ATD.CODATD)
                         INNER JOIN SAPIENS.E135PFA PFA
                            ON IPV.CODEMP = PFA.CODEMP
                               AND IPV.CODFIL = PFA.FILNFV
                               AND IPV.CODSNF = PFA.SNFNFV
                               AND IPV.NUMNFV = PFA.NUMNFV
                         INNER JOIN SAPIENS.E085CLI CLI
                            ON (REG.CODCLI = CLI.CODCLI)
                         WHERE ((TRA.DESTRA = 'OCORRENCIA RECEBIDA' OR TRA.DESTRA = 'OCORRENCIA CONFERIDO') AND REG.SITREG = 9 AND
                               SYSDATE - TRA.DATTRA > 1 AND REG.NUMREG NOT IN (SELECT NUMREG FROM NWMS_PRODUCAO.N0203TRA 
                                               WHERE SYSDATE - DATTRA < 1 AND (DESTRA Like 'OCORR%NCIA RECEBIDA' OR DESTRA Like 'OCORR%NCIA CONFERIDO')))
                               OR (TRA.DESTRA Like 'REGISTRO DE OCORR%NCIA APROVADO' AND
                               REG.SITREG = 3 AND SYSDATE - TRA.DATTRA > 1)
                               AND SYSDATE - TRA.DATTRA > 1
                         GROUP BY REG.NUMREG,
                                  ATD.DESCATD,
                                  IPV.DATEMI,
                                  TRA.DATTRA,
                                  ORI.DESCORI,
                                  MDV.DESCMDV,
                                  IPV.NUMNFV
                         ORDER BY REG.NUMREG DESC";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<Ocorrencia> lista = new List<Ocorrencia>();
                Ocorrencia itens = new Ocorrencia();
                while (dr.Read())
                {
                    itens = new Ocorrencia();
                    itens.CodigoRegistro = Convert.ToInt64(dr["NUMREG"]);
                    itens.DescTipoAtendimento = dr["DESCATD"].ToString();
                    itens.DATAEMISSAO = dr["DATEMI"].ToString();
                    itens.DescOrigemOcorrencia = dr["DESCORI"].ToString();
                    itens.DescMotivoDevolucao = dr["DESCMDV"].ToString();
                    itens.NumeroNota = dr["NUMNFV"].ToString();
                    itens.ValorLiquido = Convert.ToDecimal(dr["VLRLIQ"]);
                    itens.DataSituacao = dr["DATASIT"].ToString();
                    itens.ValorLiquidoS = itens.ValorLiquido.ToString("###,###,##0.00");
                    lista.Add(itens);
                }

                dr.Close();
                conn.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Carrega as Ocorrências com faturameto em dia 
        /// </summary>
        /// <returns></returns>
        public List<Ocorrencia> carregarOcorrenciaFaturamentoEmDia()
        {
            try
            {
                string sql = @"  SELECT DISTINCT REG.NUMREG,
                                        ATD.DESCATD,
                                        IPV.DATEMI,
                                        TRA.DATTRA,
                                        ORI.DESCORI,
                                        MDV.DESCMDV,
                                        IPV.NUMNFV,
                                         ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV), 2) VLRLIQ,
                                        TRA.DATTRA DATASIT
                          FROM NWMS_PRODUCAO.N0203REG REG
                         INNER JOIN NWMS_PRODUCAO.N0203TRA TRA
                            ON REG.NUMREG = TRA.NUMREG
                         INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                            ON (IPV.NUMREG = REG.NUMREG)
                         INNER JOIN NWMS_PRODUCAO.N0204MDV MDV
                            ON (MDV.CODMDV = IPV.CODMOT)
                         INNER JOIN NWMS_PRODUCAO.N0204ORI ORI
                            ON (ORI.CODORI = IPV.ORIOCO)
                         INNER JOIN NWMS_PRODUCAO.N0204ATD ATD
                            ON (REG.TIPATE = ATD.CODATD)
                         INNER JOIN SAPIENS.E135PFA PFA
                            ON IPV.CODEMP = PFA.CODEMP
                               AND IPV.CODFIL = PFA.FILNFV
                               AND IPV.CODSNF = PFA.SNFNFV
                               AND IPV.NUMNFV = PFA.NUMNFV
                         INNER JOIN SAPIENS.E085CLI CLI
                            ON (REG.CODCLI = CLI.CODCLI)
                         WHERE ((TRA.DESTRA Like 'OCORR%NCIA RECEBIDA' OR TRA.DESTRA Like 'OCORR%NCIA CONFERIDO') AND REG.SITREG = 9 AND
                               SYSDATE - TRA.DATTRA < 1)
                               OR (TRA.DESTRA like 'REGISTRO DE OCORR%NCIA APROVADO' AND
                               REG.SITREG = 3 AND SYSDATE - TRA.DATTRA < 1)
                               AND SYSDATE - TRA.DATTRA < 1
                         GROUP BY REG.NUMREG,
                                  ATD.DESCATD,
                                  IPV.DATEMI,
                                  TRA.DATTRA,
                                  ORI.DESCORI,
                                  MDV.DESCMDV,
                                  IPV.NUMNFV
                         ORDER BY REG.NUMREG DESC";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<Ocorrencia> lista = new List<Ocorrencia>();
                Ocorrencia itens = new Ocorrencia();
                while (dr.Read())
                {
                    itens = new Ocorrencia();
                    itens.CodigoRegistro = Convert.ToInt64(dr["NUMREG"]);
                    itens.DescTipoAtendimento = dr["DESCATD"].ToString();
                    itens.DATAEMISSAO = dr["DATEMI"].ToString();
                    itens.DescOrigemOcorrencia = dr["DESCORI"].ToString();
                    itens.DescMotivoDevolucao = dr["DESCMDV"].ToString();
                    itens.NumeroNota = dr["NUMNFV"].ToString();
                    itens.ValorLiquido = Convert.ToDecimal(dr["VLRLIQ"]);
                    itens.DataSituacao = dr["DATASIT"].ToString();
                    itens.ValorLiquidoS = itens.ValorLiquido.ToString("###,###,##0.00");
                    lista.Add(itens);
                }

                dr.Close();
                conn.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca as informações dos indicadores da industria
        /// </summary>
        /// <param name="status">Status</param>
        /// <param name="mes">Mês</param>
        /// <param name="filtroAgrup">Filtro Agrupamento</param>
        /// <param name="indicador">Indicador</param>
        /// <param name="ano">Ano</param>
        /// <returns></returns>
        public List<Ocorrencia> CarregarIndicadorIndustria(string status, string mes, string filtroAgrup, string indicador, string ano)
        {
            double SomaTotalValorLiquido = 0;
            double SomaTotalValorLiquido1 = 0;
            string origem = tratarOrigem(indicador);

            string sql = @" SELECT DISTINCT REG.NUMREG,
                                            AGP.DESAGP,
                                            PRO.CODPRO,
                                            PRO.DESPRO,
                                            DER.DESDER,
                                            CLI.CODCLI,
                                            ORI.DESCORI,
                                            MDV.DESCMDV,
                                            SUM(IPV.QTDDEV) AS QTDDEV,
                                            ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV), 2) VALORLIQUIDO
                              FROM NWMS_PRODUCAO.N0203REG REG
                             INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                                ON (IPV.NUMREG = REG.NUMREG)
                             INNER JOIN NWMS_PRODUCAO.N0204ORI ORI
                                ON (ORI.CODORI = IPV.ORIOCO)
                             INNER JOIN SAPIENS.E085CLI CLI
                                ON (REG.CODCLI = CLI.CODCLI)
                             INNER JOIN SAPIENS.E075PRO PRO
                                ON PRO.CODPRO = IPV.CODPRO
                             INNER JOIN SAPIENS.E013AGP AGP
                                ON AGP.CODAGP = PRO.CODAGP
                             INNER JOIN NWMS_PRODUCAO.N0204MDV MDV
                                ON (MDV.CODMDV = IPV.CODMOT) 
                             INNER JOIN SAPIENS.E075DER DER
                                ON (DER.CODEMP = IPV.CODEMP AND DER.CODPRO = IPV.CODPRO AND DER.CODDER = IPV.CODDER)";
            sql += "WHERE 1 = 1 AND REG.SITREG NOT IN (1,7,5) ";
            sql += "AND EXTRACT(MONTH FROM REG.DATGER) = " + mes + " AND EXTRACT(YEAR FROM REG.DATGER) = " + ano + " AND IPV.ORIOCO = " + origem + "";
            sql += filtroAgrup != "" && filtroAgrup != null && filtroAgrup != "Selecione.." ? " AND AGP.DESAGP = '" + filtroAgrup + "'" : "";
            sql += @" GROUP BY REG.NUMREG,
                                      AGP.DESAGP,
                                      PRO.CODPRO,
                                      PRO.DESPRO,
                                      CLI.CODCLI,
                                      ORI.DESCORI,
                                      DER.DESDER,
                                      MDV.DESCMDV ORDER BY NUMREG";

            
            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();

            List<Ocorrencia> lista = new List<Ocorrencia>();
            Ocorrencia itens = new Ocorrencia();

            while (dr.Read())
            {
                itens = new Ocorrencia();
                itens.CodigoRegistro = Convert.ToInt64(dr["NUMREG"]);
                itens.DescAgrupamento = dr["DESAGP"].ToString();
                itens.CodPro = dr["CODPRO"].ToString();
                itens.DescPro = dr["DESPRO"].ToString();
                itens.DesDer = dr["DESDER"].ToString();
                itens.CodCliente = dr["CODCLI"].ToString();
                itens.DescOrigemOcorrencia = dr["DESCORI"].ToString();
                itens.DescMotivoDevolucao = dr["DESCMDV"].ToString();
                itens.QtdeDevolucao = Convert.ToInt64(dr["QTDDEV"]);
                SomaTotalValorLiquido1 = Convert.ToDouble(dr["VALORLIQUIDO"].ToString());
                itens.TotalValorLiquidoD = SomaTotalValorLiquido1.ToString("###,###,##0.00");

                SomaTotalValorLiquido += Convert.ToDouble(dr["VALORLIQUIDO"].ToString());
                itens.TotalValorLiquidoS = SomaTotalValorLiquido.ToString("###,###,##0.00");

                lista.Add(itens);
            }

            dr.Close();
            conn.Close();
            return lista;
        }

        public List<RelatorioGraficoItens> RelatorioGraficoItens(string status, string mes, string filtroAgrup, string indicador, string ano)
        {
            double SomaTotalValorLiquido = 0;
            double SomaTotalValorLiquido1 = 0;
            string origem = tratarOrigem(indicador);

            string sql = @" SELECT DISTINCT REG.NUMREG,
                                            AGP.DESAGP,
                                            PRO.CODPRO,
                                            PRO.DESPRO,
                                            DER.DESDER,
                                            CLI.CODCLI,
                                            ORI.DESCORI,
                                            MDV.DESCMDV,
                                            SUM(IPV.QTDDEV) AS QTDDEV,
                                            ROUND(SUM((IPV.VLRLIQ / IPV.QTDFAT) * IPV.QTDDEV), 2) VALORLIQUIDO
                              FROM NWMS_PRODUCAO.N0203REG REG
                             INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                                ON (IPV.NUMREG = REG.NUMREG)
                             INNER JOIN NWMS_PRODUCAO.N0204ORI ORI
                                ON (ORI.CODORI = IPV.ORIOCO)
                             INNER JOIN SAPIENS.E085CLI CLI
                                ON (REG.CODCLI = CLI.CODCLI)
                             INNER JOIN SAPIENS.E075PRO PRO
                                ON PRO.CODPRO = IPV.CODPRO
                             INNER JOIN SAPIENS.E013AGP AGP
                                ON AGP.CODAGP = PRO.CODAGP
                             INNER JOIN NWMS_PRODUCAO.N0204MDV MDV
                                ON (MDV.CODMDV = IPV.CODMOT) 
                             INNER JOIN SAPIENS.E075DER DER
                                ON (DER.CODEMP = IPV.CODEMP AND DER.CODPRO = IPV.CODPRO AND DER.CODDER = IPV.CODDER)";
            sql += "WHERE 1 = 1 AND REG.SITREG NOT IN (1,7,5) ";
            sql += "AND EXTRACT(MONTH FROM REG.DATGER) = " + mes + " AND EXTRACT(YEAR FROM REG.DATGER) = " + ano + " AND IPV.ORIOCO = " + origem + "";
            sql += filtroAgrup != "" && filtroAgrup != null && filtroAgrup != "Selecione.." ? " AND AGP.DESAGP = '" + filtroAgrup + "'" : "";
            sql += @" GROUP BY REG.NUMREG,
                                      AGP.DESAGP,
                                      PRO.CODPRO,
                                      PRO.DESPRO,
                                      CLI.CODCLI,
                                      ORI.DESCORI,
                                      DER.DESDER,
                                      MDV.DESCMDV ORDER BY NUMREG";
            
            OracleConnection conn = new OracleConnection(OracleStringConnection);
            OracleCommand cmd = new OracleCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();

            List<RelatorioGraficoItens> lista = new List<RelatorioGraficoItens>();
            RelatorioGraficoItens itens = new RelatorioGraficoItens();

            while (dr.Read())
            {
                itens = new RelatorioGraficoItens();
                itens.NUMREG = Convert.ToInt32(dr["NUMREG"]);
                itens.DESAGP = dr["DESAGP"].ToString();
                itens.CODPRO = dr["CODPRO"].ToString();
                itens.DESPRO = dr["DESPRO"].ToString();
                itens.DESDER = dr["DESDER"].ToString();
                itens.CODCLI = dr["CODCLI"].ToString();
                itens.DESCORI = dr["DESCORI"].ToString();
                itens.DESCMDV = dr["DESCMDV"].ToString();
                itens.QTDDEV = Convert.ToInt32(dr["QTDDEV"]);
                SomaTotalValorLiquido1 = Convert.ToDouble(dr["VALORLIQUIDO"].ToString());
                itens.VALORLIQUIDO = SomaTotalValorLiquido1.ToString("###,###,##0.00");

                SomaTotalValorLiquido = Convert.ToDouble(dr["VALORLIQUIDO"].ToString());
                itens.TotalValorLiquidoS = SomaTotalValorLiquido.ToString("###,###,##0.00");

                lista.Add(itens);
            }

            dr.Close();
            conn.Close();
            return lista;
        }

        /// <summary>
        /// Realiza a impressão dos Relatório Sintetico das ocorrências
        /// </summary>
        /// <param name="campoNumeroRegistro">Código de Ocorrência</param>
        /// <param name="campoFilial">Código Filial</param>
        /// <param name="campoEmbarque">Código Embarque</param>
        /// <param name="campoPlaca">Placa</param>
        /// <param name="campoPeriodoInicial">Periodo Inicial</param>
        /// <param name="campoPeriodoFinal">Periodo Final</param>
        /// <param name="campoCliente">Código Cliente</param>
        /// <param name="campoSituacao">Situação</param>
        /// <param name="campoDataFaturamento">Data Faturamento</param>
        /// <returns></returns>
        public List<RelatorioAnalitico> imprimirRelatorioSinteticoRegistroOcorrencia(string campoNumeroRegistro, string campoFilial, string campoEmbarque, string campoPlaca, string campoPeriodoInicial, string campoPeriodoFinal, string campoCliente, string campoSituacao, string campoDataFaturamento)
        {
            var campoPlacaFormatado = campoPlaca.Replace("-", "").ToUpper();
            decimal SomaTotalValorLiquido = 0;
            try
            {
                string sql = @" SELECT REG.NUMREG,
                                       REG.PLACA,
                                       REG.SITREG,
                                       REG.DATGER,
                                       REG.DATULT,
                                       REG.OBSREG,
                                       ATD.CODATD,
                                       ATD.DESCATD,
                                       ATDIPV.CODATD AS IPVCODATD,
                                       ATDIPV.DESCATD AS IPVDESCATD,
                                       MOT.NOMMOT,
                                       MOT.CODMOT,
                                       NOMEULT.NOMCOM,
                                       NOMEGER.NOMCOM,
                                       USUGER.CODUSU AS CODUSUGER,
                                       USUULT.CODUSU AS CODUSUULT,
                                       PFA.CODFIL,
                                       PFA.NUMANE,
                                       CLI.CODCLI,
                                       CLI.NOMCLI,
                                       ORI.CODORI,
                                       ORI.DESCORI,
                                       PFA.NUMANE,
                                       PFA.CODFIL,
                                       IPV.CODSNF,
                                       IPV.NUMNFV,
                                       IPV.SEQIPV,
                                       IPV.CODDER,
                                       IPV.CPLIPV,
                                       IPV.CODDEP,
                                       IPV.PREUNI,
                                       IPV.QTDFAT,
                                       IPV.ORIOCO AS IPVCODORI,
                                       ORIIPV.DESCORI AS IPVDESC,
                                       IPV.CODMOT,
                                       IPV.QTDDEV,
                                       IPV.TNSPRO,
                                       IPV.USUULT,
                                       IPV.PEROFE,
                                       IPV.PERIPI,
                                       (IPV.QTDDEV * IPV.PREUNI) VALORBRUTO,
                                       IPV.PEROFE,
                                       IPV.CODEMP,
                                       IPV.CODFIL,
                                       IPV.CODPRO,
                                       IPV.VLRIPI,
                                       IPV.PREUNI,
                                       IPV.VLRST,
                                       IPV.TNSPRO
                                  FROM NWMS_PRODUCAO.N0203REG REG

                                 INNER JOIN NWMS_PRODUCAO.N0203IPV IPV
                                    ON (IPV.NUMREG = REG.NUMREG)

                                 INNER JOIN NWMS_PRODUCAO.N0204ORI ORI
                                    ON (REG.ORIOCO = ORI.CODORI)

                                 INNER JOIN NWMS_PRODUCAO.N0204ORI ORIIPV
                                    ON (IPV.ORIOCO = ORIIPV.CODORI)

                                 INNER JOIN NWMS_PRODUCAO.N0204ATD ATD
                                    ON (REG.TIPATE = ATD.CODATD)

                                 INNER JOIN NWMS_PRODUCAO.N0204ATD ATDIPV
                                    ON (IPV.CODMOT = ATDIPV.CODATD)

                                 INNER JOIN SAPIENS.E085CLI CLI
                                    ON (REG.CODCLI = CLI.CODCLI)

                                 INNER JOIN NWMS_PRODUCAO.N0012MOT MOT
                                    ON (REG.CODMOT = MOT.CODMOT)

                                 INNER JOIN SAPIENS.E135PFA PFA
                                    ON IPV.CODEMP = PFA.CODEMP
                                   AND IPV.CODFIL = PFA.FILNFV
                                   AND IPV.CODSNF = PFA.SNFNFV
                                   AND IPV.NUMNFV = PFA.NUMNFV

                                 INNER JOIN NWMS_PRODUCAO.N9999USU USUGER
                                    ON (REG.USUGER = USUGER.CODUSU)

                                 INNER JOIN NWMS_PRODUCAO.N9999USU USUULT
                                    ON (REG.USUULT = USUULT.CODUSU)

                                 INNER JOIN SAPIENS.R999USU LOGIGER
                                    ON (USUGER.LOGIN = LOGIGER.NOMUSU)

                                 INNER JOIN SAPIENS.R910USU NOMEGER
                                    ON (LOGIGER.CODUSU = NOMEGER.CODENT)

                                 INNER JOIN SAPIENS.R999USU LOGULT
                                    ON (USUULT.LOGIN = LOGULT.NOMUSU)

                                 INNER JOIN SAPIENS.R910USU NOMEULT
                                    ON (LOGULT.CODUSU = NOMEULT.CODENT)

                                 INNER JOIN SAPIENS.E075PRO PRO
                                    ON (IPV.CODPRO = PRO.CODPRO)

                                 INNER JOIN SAPIENS.E075DER DER
                                    ON (IPV.CODDER = DER.CODDER AND IPV.CODPRO = DER.CODPRO)";

                sql += campoNumeroRegistro != "" ? "AND REG.NUMREG =" + campoNumeroRegistro : "";
                sql += campoFilial != "" && campoEmbarque != "" ? "AND PFA.CODFIL =" + campoFilial + " AND PFA.NUMANE =" + campoEmbarque : "";
                sql += campoPlaca != "" ? "AND REG.PLACA ='" + campoPlacaFormatado + "'" : "";
                sql += campoPeriodoInicial != "" && campoPeriodoFinal != "" ? "AND REG.DATGER BETWEEN '" + campoPeriodoInicial + "' AND '" + campoPeriodoFinal + "'" : "";
                sql += campoCliente != "" ? "AND CLI.CODCLI =" + campoCliente : "";
                sql += campoSituacao != "0" ? "AND REG.SITREG =" + campoSituacao : "";
                sql += campoDataFaturamento != "" ? "AND IPV.DATEMI ='" + campoDataFaturamento + "'" : "";

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();

                List<RelatorioAnalitico> lista = new List<RelatorioAnalitico>();
                RelatorioAnalitico itens = new RelatorioAnalitico();

                while (dr.Read())
                {
                    itens = new RelatorioAnalitico();
                    itens.CodigoRegistro = Convert.ToInt64(dr["NUMREG"]);
                    itens.CodTipoAtendimento = dr["CODATD"].ToString();
                    itens.DescTipoAtendimento = dr["DESCATD"].ToString();
                    itens.CodOrigemOcorrencia = dr["CODORI"].ToString();
                    itens.DescOrigemOcorrencia = dr["DESCORI"].ToString();
                    itens.CodCliente = dr["CODCLI"].ToString();
                    itens.NomeCliente = dr["NOMCLI"].ToString();
                    itens.CodMotorista = dr["CODMOT"].ToString();
                    itens.CodPlaca = dr["PLACA"].ToString();
                    itens.NomeMotorista = dr["NOMMOT"].ToString();
                    itens.DataHrGeracao = dr["DATGER"].ToString();
                    itens.NomeUsuarioGeracao = dr["NOMCOM"].ToString();
                    itens.UsuarioGeracao = dr["CODUSUGER"].ToString();
                    itens.CodSituacaoRegistro = dr["SITREG"].ToString();

                    switch (itens.CodSituacaoRegistro)
                    {
                        case "1":
                            itens.DescSituacaoRegistro = "Rascunho";
                            break;
                        case "2":
                            itens.DescSituacaoRegistro = "Aguardando Aprovação";
                            break;
                        case "3":
                            itens.DescSituacaoRegistro = "Integrado";
                            break;
                        case "4":
                            itens.DescSituacaoRegistro = "Aprovado";
                            break;
                        case "5":
                            itens.DescSituacaoRegistro = "Reprovado";
                            break;
                        case "6":
                            itens.DescSituacaoRegistro = "Reabilitado";
                            break;
                        case "8":
                            itens.DescSituacaoRegistro = "Coleta";
                            break;
                        case "9":
                            itens.DescSituacaoRegistro = "Conferido";
                            break;
                        case "10":
                            itens.DescSituacaoRegistro = "Faturado";
                            break;
                        case "7":
                            itens.DescSituacaoRegistro = "Reprovado";
                            break;
                        case "11":
                            itens.DescSituacaoRegistro = "Indenizado";
                            break;
                    }

                    itens.UltimaAlteracao = dr["DATULT"].ToString(); ;
                    itens.NomeUsuarioUltimaAlteracao = dr["NOMCOM"].ToString();
                    itens.UsuarioUltimaAlteracao = dr["CODUSUULT"].ToString();
                    itens.Observacao = dr["OBSREG"].ToString();

                    // Itens Devolução
                    itens.Empresa = dr["CODEMP"].ToString();
                    itens.Filial = Convert.ToInt64(dr["CODFIL"]);

                    // Add item na Lista de analises de embarque

                    itens.SerieNota = dr["CODSNF"].ToString();
                    itens.NumeroNota = dr["NUMNFV"].ToString();
                    itens.SeqNota = Convert.ToInt64(dr["SEQIPV"]);
                    itens.CodPro = dr["CODPRO"].ToString();
                    itens.CodDer = dr["CODDER"].ToString();
                    itens.DescPro = dr["CPLIPV"].ToString();
                    itens.CodDepartamento = dr["CODDEP"].ToString();
                    itens.QtdeFat = Convert.ToInt64(dr["QTDFAT"]);

                    itens.PrecoUnitario = dr["PREUNI"].ToString();
                    itens.CodOrigemOcorrenciaItemDev = dr["IPVCODORI"].ToString();
                    itens.DescOrigemOcorrenciaItemDev = dr["IPVDESC"].ToString();
                    itens.CodMotivoDevolucao = dr["IPVCODATD"].ToString();
                    itens.DescMotivoDevolucao = dr["IPVDESCATD"].ToString();
                    itens.QtdeDevolucao = Convert.ToInt64(dr["QTDDEV"]);
                    itens.PercDesconto = dr["PEROFE"].ToString();
                    itens.PercIpi = dr["PERIPI"].ToString();

                    itens.ValorBruto = decimal.Parse((itens.QtdeDevolucao * Convert.ToDecimal(itens.PrecoUnitario)).ToString());
                    itens.ValorBrutoS = itens.ValorBruto.ToString("###,###,##0.00");
                    itens.ValorIpi = Convert.ToDecimal(dr["VLRIPI"]);
                    itens.ValorSt = Convert.ToInt64(dr["VLRST"]);
                    itens.TipoTransacao = dr["TNSPRO"].ToString();
                    itens.AnaliseEmbarque = dr["NUMANE"].ToString();
                    itens.Filial = Convert.ToInt64(dr["CODFIL"]);

                    itens.ValorIpi = itens.QtdeDevolucao * (itens.ValorIpi / itens.QtdeFat);
                    itens.ValorIpiS = itens.ValorIpi.ToString("###,###,##0.00");
                    itens.ValorSt = itens.QtdeDevolucao * (itens.ValorSt / itens.QtdeFat);
                    itens.ValorStS = itens.ValorSt.ToString("###,###,##0.00");
                    itens.ValorLiquido = (itens.QtdeDevolucao * decimal.Parse(itens.PrecoUnitario.ToString())) + itens.ValorIpi + itens.ValorSt;
                    itens.ValorLiquidoS = itens.ValorLiquido.ToString("###,###,##0.00");

                    SomaTotalValorLiquido = SomaTotalValorLiquido + itens.ValorLiquido;
                    itens.TotalValorLiquido = SomaTotalValorLiquido;

                    lista.Add(itens);
                }

                dr.Close();
                conn.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

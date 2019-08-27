using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using NUTRIPLAN_WEB.MVC_4_BS.Model;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class N0204MDODataAccess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();
        /// <summary>
        /// Pesquisa as Origens por códigos de motivo
        /// </summary>
        /// <param name="codigoMotivo">Código do Motivo</param>
        /// <returns>Lista de Origens</returns>
        public List<N0204MDO> PesquisaOrigensPorMotivo(long codigoMotivo)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    string situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString();
                    var listaOrigens = contexto.N0204MDO.Include("N0204ORI").Where(c => c.CODMDV == codigoMotivo && c.SITREL == situacao).OrderBy(c => c.CODORI).ToList();
                    return listaOrigens;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ListaMotivoporOrigem> PesquisaMotivoporOrigem(long codigoOrigem)
        {
            try
            {
                string sql = @"SELECT MDV.CODMDV, MDV.DESCMDV
                              FROM N0204MDO MDO, N0204ORI ORI, N0204MDV MDV
                             WHERE MDO.CODORI = ORI.CODORI
                               AND MDO.CODMDV = MDV.CODMDV
                               AND ORI.CODORI = " + codigoOrigem + 
                               " AND MDV.SITMDV = 'A'";
                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                ListaMotivoporOrigem motivo = new ListaMotivoporOrigem();
                List<ListaMotivoporOrigem> itens = new List<ListaMotivoporOrigem>();
                OracleDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    motivo = new ListaMotivoporOrigem();
                    motivo.codigoMotivo = dr["CODMDV"].ToString();
                    motivo.descriçãoMotivo = dr["DESCMDV"].ToString();
                    itens.Add(motivo);
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
        /// Pesquisa os motivos nas origens 
        /// </summary>
        /// <param name="codigoMotivo">Código do Motivo</param>
        /// <returns>Lista de Motivos</returns>
        public List<RelacionamentoMotivoOrigemModel> PesquisaMotivoOrigem(long codigoMotivo)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    List<RelacionamentoMotivoOrigemModel> listaMotivoOrigem = new List<RelacionamentoMotivoOrigemModel>();
                    RelacionamentoMotivoOrigemModel itemMotivoOrigem = new RelacionamentoMotivoOrigemModel();

                    string situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString();

                    var listaOrigens = contexto.N0204ORI.Where(c => c.SITORI == situacao).OrderBy(c => c.CODORI).ToList();
                    var listaMotOrigens = contexto.N0204MDO.Where(c => c.CODMDV == codigoMotivo && c.SITREL == situacao).OrderBy(c => c.CODORI).ToList();

                    if (listaOrigens != null)
                    {
                        foreach (N0204ORI itemOrigem in listaOrigens)
                        {
                            itemMotivoOrigem = new RelacionamentoMotivoOrigemModel();
                            itemMotivoOrigem.Codigo = itemOrigem.CODORI;
                            itemMotivoOrigem.Descricao = itemOrigem.DESCORI;

                            var marcarOrigem = listaMotOrigens.Where(c => c.CODORI == itemOrigem.CODORI).FirstOrDefault();

                            if (marcarOrigem != null)
                            {
                                itemMotivoOrigem.Marcar = true;
                            }

                            listaMotivoOrigem.Add(itemMotivoOrigem);
                        }
                    }

                    return listaMotivoOrigem;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Grava os motivos de devolução nas Origens das ocorrências
        /// </summary>
        /// <param name="codigoMotivo">Código do Motivo</param>
        /// <param name="listaMotivosOrigens">Lista de Motivos por Origem</param>
        /// <returns>true/false</returns>
        public bool GravarMotivoDevXOrigemOcorrencia(long codigoMotivo, List<N0204MDO> listaMotivosOrigens)
        {
            try
            {
                using (Context contexto = new Context())
                {
                    string situacao = ((char)Enums.SituacaoRegistro.Ativo).ToString();

                    var listaMotOri = contexto.N0204MDO.Where(c => c.CODMDV == codigoMotivo).OrderBy(c => c.CODORI).ToList();

                    if (listaMotOri != null)
                    {
                        foreach (N0204MDO itemMotOriCad in listaMotOri)
                        {
                            contexto.N0204MDO.Remove(itemMotOriCad);
                            contexto.SaveChanges();
                        }
                    }

                    foreach (N0204MDO itemCad in listaMotivosOrigens)
                    {
                        if (contexto.N0204MDO.Count() == 0)
                        {
                            itemCad.IDROW = 1;
                        }
                        else
                        {
                            itemCad.IDROW = contexto.N0204MDO.Max(p => p.IDROW + 1);
                        }

                        contexto.N0204MDO.Add(itemCad);
                        contexto.SaveChanges();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

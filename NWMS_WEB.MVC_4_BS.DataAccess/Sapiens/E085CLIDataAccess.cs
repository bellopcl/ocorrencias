using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;
using System.Data;
using NUTRIPLAN_WEB.MVC_4_BS.Model;
using System.Collections;

namespace NUTRIPLAN_WEB.MVC_4_BS.DataAccess
{
    /// <summary>
    /// Classe de acesso ao banco de dados
    /// </summary>
    public class E085CLIDataAccess
    {
        public string OracleStringConnection = Attributes.KeyValueAttribute.GetFirst("Descricao", Enums.OracleStringConnection.Sapiens).GetValue<string>();
        /// <summary>
        /// Retorna uma lista de clientes
        /// </summary>
        /// <param name="codigoCliente">Código do Cliente</param>
        /// <returns>listaClientes</returns>
        public List<E085CLIModel> PesquisasClientes(long? codigoCliente)
        {
            try
            {
                string sql = @"SELECT e085cli.CODCLI AS Codigo,                      
                             e085cli.NOMCLI AS NomeFantasia,                       
                             e085cli.APECLI AS RazaoSocial,                        
                             e085cli.CGCCPF as CNPJCPF,                            
                             e085cli.TIPCLI AS TipoCliente,                        
                             e085cli.CODPAI AS CodigoPais,                         
                             e085cli.INSEST AS InscricaoEstadual,                  
                             e085cli.ENDCLI || ' ' || e085cli.CPLEND AS Endereco,  
                             e085cli.CIDCLI as Cidade,                             
                             e085cli.SIGUFS as Estado                              
                        FROM SAPIENS.E085CLI, SAPIENS.e140nfv, SAPIENS.E001TNS     
                       where  
                         E085CLI.CLIFOR IN ('A', 'C')                                          
                         AND E085CLI.CODCLI = E140NFV.CODCLI                       
                         AND E140NFV.CODEMP = E001TNS.CODEMP                       
                         AND E140NFV.TNSPRO = E001TNS.CODTNS                       
                         AND E001TNS.VENFAT = 'S'                                  
                         and e140nfv.datemi >= (to_date(current_date) - 180)       
                         and e140nfv.sitnfv = 2                                    
                       group by e085cli.CODCLI,                                    
                                e085cli.NOMCLI,                                    
                                e085cli.APECLI,                                    
                                e085cli.CGCCPF,                                    
                                e085cli.TIPCLI,                                    
                                e085cli.CODPAI,                                    
                                e085cli.INSEST,                                    
                                e085cli.ENDCLI,                                    
                                e085cli.CplEnd,                                    
                                e085cli.CIDCLI,                                    
                                e085cli.SIGUFS                                     
                       order by e085cli.CODCLI"; 
                
                if (codigoCliente != null)
                {
                    sql = @"SELECT CODCLI AS Codigo,
                                 NOMCLI AS NomeFantasia,                    
                                 APECLI AS RazaoSocial,                     
                                 CGCCPF as CNPJCPF,                         
                                 TIPCLI AS TipoCliente,                     
                                 CODPAI AS CodigoPais,                      
                                 INSEST AS InscricaoEstadual,               
                                 ENDCLI || ' ' || CPLEND AS Endereco,       
                                 CIDCLI as Cidade,                          
                                 SIGUFS as Estado                           
                            FROM SAPIENS.E085CLI                            
                           where SITCLI = 'A' and CODCLI = " + codigoCliente;
                }

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();
                OracleDataReader dr = cmd.ExecuteReader();

                List<E085CLIModel> listaClientes = new List<E085CLIModel>();
                E085CLIModel itemCliente = new E085CLIModel();
                string tipoPessoa = ((char)Enums.TipoPessoa.Fisica).ToString();

                while (dr.Read())
                {
                    itemCliente = new E085CLIModel();
                    itemCliente.CodigoCliente = dr.GetInt64(0);
                    if (dr.GetString(1) != null) 
                    { 
                        itemCliente.NomeFantasia = dr.GetString(1);
                    }
                    else
                    {
                        itemCliente.NomeFantasia = "Nome Fantasia ";
                    }
                    itemCliente.RazaoSocial = dr.GetString(2);

                    string formatado = dr.GetInt64(3).ToString();

                    // Código do País -- Brasil
                    if (dr.GetString(5) == "1058")
                    {
                        // Pessoa Física
                        if (dr.GetString(4) == tipoPessoa)
                        {
                            formatado = dr.GetInt64(3).ToString(@"000\.000\.000\-00");
                        }
                        else
                        {
                            formatado = dr.GetInt64(3).ToString(@"00\.000\.000\/0000\-00");
                        }
                    }

                    itemCliente.CnpjCpf = formatado;
                    itemCliente.InscricaoEstadual = dr.GetString(6);
                    itemCliente.Endereco = dr.GetString(7);
                    itemCliente.Cidade = dr.GetString(8);
                    itemCliente.Estado = dr.GetString(9);
                    listaClientes.Add(itemCliente);
                }

                dr.Close();
                conn.Close();
                return listaClientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Retorna uma lista de clientes
        /// </summary>
        /// <param name="codigoCliente">Código Cliente</param>
        /// <returns>lista</returns>
        public ArrayList PesquisaClientesArray(long? codigoCliente)
        {
            try
            {
                string sql = string.Empty;
                sql = "SELECT CODCLI AS Codigo, NOMCLI AS NomeFantasia, APECLI AS RazaoSocial, CGCCPF as CNPJCPF,INSEST AS InscricaoEstadual,ENDCLI ||' '|| CPLEND AS Endereco,CIDCLI as Cidade, SIGUFS as Estado FROM SAPIENS.E085CLI where SITCLI = 'A' order by CODCLI";
                if (codigoCliente != null)
                {
                    sql = "SELECT CODCLI AS Codigo, NOMCLI AS NomeFantasia, APECLI AS RazaoSocial, CGCCPF as CNPJCPF,INSEST AS InscricaoEstadual,ENDCLI ||' '|| CPLEND AS Endereco,CIDCLI as Cidade, SIGUFS as Estado FROM SAPIENS.E085CLI where SITCLI = 'A' and CODCLI = " + codigoCliente;
                }

                OracleConnection conn = new OracleConnection(OracleStringConnection);
                OracleCommand cmd = new OracleCommand(sql, conn);
                cmd.CommandType = CommandType.Text;
                conn.Open();

                OracleDataReader dr = cmd.ExecuteReader();
                ArrayList lista = new ArrayList();
                string tipoPessoa = ((char)Enums.TipoPessoa.Fisica).ToString();

                while (dr.Read())
                {
                    ArrayList itemLista = new ArrayList();
                    itemLista.Add(dr.GetInt64(0));
                    itemLista.Add(dr.GetString(1));
                    itemLista.Add(dr.GetString(2));

                    string formatado = dr.GetInt64(3).ToString();

                    // Código do País -- Brasil
                    if (dr.GetString(5) == "1058")
                    {
                        // Pessoa Física
                        if (dr.GetString(4) == tipoPessoa)
                        {
                            formatado = dr.GetInt64(3).ToString(@"000\.000\.000\-00");
                        }
                        else
                        {
                            formatado = dr.GetInt64(3).ToString(@"00\.000\.000\/0000\-00");
                        }
                    }

                    itemLista.Add(formatado);

                    itemLista.Add(dr.GetString(6));
                    itemLista.Add(dr.GetString(7));
                    itemLista.Add(dr.GetString(8));
                    itemLista.Add(dr.GetString(9));
                    lista.Add(itemLista);
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

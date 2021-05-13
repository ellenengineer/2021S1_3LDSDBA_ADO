using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using System.Linq;

namespace DAO
{
    public class ClienteDAO : DAO
    {
        public List<Cliente> RetornarListaCliente()
        {
            List<Cliente> lstCliente = new List<Cliente>();
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.AppendLine("SELECT cli.Cod_Cli");
                sbQuery.AppendLine(",tp.Nome_TipoCli");
                sbQuery.AppendLine(",cli.Nome_Cli");
                sbQuery.AppendLine(",cli.Data_CadCli");
                sbQuery.AppendLine(",cli.Renda_Cli");
                sbQuery.AppendLine(",cli.Sexo_Cli");
                sbQuery.AppendLine(",cli.Cod_TipoCli");
                sbQuery.AppendLine(" FROM Cliente cli");
                sbQuery.AppendLine(" INNER JOIN TipoCli tp on tp.Cod_TipoCli = cli.Cod_TipoCli");

                SqlCommand objCmd = new SqlCommand(sbQuery.ToString(),conn);
                objCmd.CommandType = CommandType.Text;

                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();

                        SqlDataReader sdReader = objCmd.ExecuteReader();

                        while (sdReader.Read())
                        {
                            Cliente cli = new Cliente();
                            cli.TipoCli = new TipoCliente();

                            int iConvert = 0;
                            double dConvert = 0;
                            DateTime dtConvert = DateTime.MinValue;

                            if (sdReader["Cod_Cli"]!= null)
                            {
                                cli.Cod_Cli = int.TryParse(sdReader["Cod_Cli"].ToString(), out iConvert) ? iConvert : 0;
                            }
                            if (sdReader["Nome_TipoCli"]!= null )
                            {
                                cli.TipoCli.Nome_TipoCli = sdReader["Nome_TipoCli"].ToString();
                            }
                            if(sdReader["Data_CadCli"] != null)
                            {
                                cli.Data_CadCli = DateTime.TryParse(sdReader["Data_CadCli"].ToString(), out dtConvert) ? dtConvert : DateTime.MaxValue;
                            }
                            if(sdReader["Renda_Cli"] != null)
                            {
                                cli.Renda_Cli = double.TryParse(sdReader["Renda_Cli"].ToString(), out dConvert) ? dConvert : 0;
                            }
                            if (sdReader["Sexo_Cli"] != null)
                            {
                                cli.Sexo_Cli = sdReader["Sexo_Cli"].ToString();
                            }
                            if (sdReader["Cod_TipoCli"] != null)
                            {
                                cli.TipoCli.Cod_TipoCli = int.TryParse(sdReader["Cod_TipoCli"].ToString(), out iConvert) ? iConvert: 0;
                            }
                            lstCliente.Add(cli);
                        }
                    }

                }
                catch(SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex1)
                {

                    throw ex1;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return lstCliente;
        }

        public Cliente RetornarCliente(int CodCli)
        {
            List<Cliente> lstCli = RetornarListaCliente();

            Cliente cli = lstCli.Where(c => c.Cod_Cli == CodCli).FirstOrDefault();
            return cli;
        }

       public int CadastrarCliente(Cliente cli)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);

            int retorno = 0;

            StringBuilder sbComando = new StringBuilder();
            sbComando.AppendLine("INSERT INTO CLIENTE (");
            sbComando.AppendLine("                       Cod_Cli");
            sbComando.AppendLine("                      ,Nome_Cli");
            sbComando.AppendLine("                      ,Data_CadCli");
            sbComando.AppendLine("                      ,Renda_Cli");
            sbComando.AppendLine("                      ,Sexo_Cli");
            sbComando.AppendLine("                      ,Cod_TipoCli");
            sbComando.AppendLine("                     )");
            sbComando.AppendLine("VALUES (");
            sbComando.AppendLine("        @Cod_Cli");
            sbComando.AppendLine("       ,@Nome_Cli");
            sbComando.AppendLine("       ,@Data_CadCli");
            sbComando.AppendLine("       ,@Renda_Cli");
            sbComando.AppendLine("       ,@Sexo_Cli");
            sbComando.AppendLine("       ,@Cod_TipoCli");
            sbComando.AppendLine("       )");

            SqlCommand cmd = new SqlCommand(sbComando.ToString(),conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Cod_Cli",cli.Cod_Cli);
            cmd.Parameters.AddWithValue("@Nome_Cli",cli.Nome_Cli);
            cmd.Parameters.AddWithValue("@Data_CadCli",cli.Data_CadCli);
            cmd.Parameters.AddWithValue("@Renda_Cli",cli.Renda_Cli);
            cmd.Parameters.AddWithValue("@Sexo_Cli",cli.Sexo_Cli);
            cmd.Parameters.AddWithValue("@Cod_TipoCli", cli.TipoCli.Cod_TipoCli);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch(SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if(conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return retorno;
        }

        public int AtualizarCliente(Cliente cli)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);

            int retorno = 0;

            StringBuilder sbComando = new StringBuilder();
            sbComando.AppendLine("UPDATE CLIENTE ");
            sbComando.AppendLine(" SET ");
            sbComando.AppendLine("    Nome_Cli = @Nome_Cli");
            sbComando.AppendLine("    ,Data_CadCli = @Data_CadCli");
            sbComando.AppendLine("    ,Renda_Cli = @Renda_Cli");
            sbComando.AppendLine("    ,Sexo_Cli = @Sexo_Cli");
            sbComando.AppendLine("    ,Cod_TipoCli = @Cod_TipoCli");
            sbComando.AppendLine("WHERE Cod_Cli = @Cod_Cli ");

            SqlCommand cmd = new SqlCommand(sbComando.ToString(), conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Cod_Cli", cli.Cod_Cli);
            cmd.Parameters.AddWithValue("@Nome_Cli", cli.Nome_Cli);
            cmd.Parameters.AddWithValue("@Data_CadCli", cli.Data_CadCli);
            cmd.Parameters.AddWithValue("@Renda_Cli", cli.Renda_Cli);
            cmd.Parameters.AddWithValue("@Sexo_Cli", cli.Sexo_Cli);
            cmd.Parameters.AddWithValue("@Cod_TipoCli", cli.TipoCli.Cod_TipoCli);

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return retorno;
        }

        public int ApagarCliente(Cliente cli)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);

            int retorno = 0;

            StringBuilder sbComando = new StringBuilder();
            sbComando.AppendLine("DELETE FROM CLIENTE ");
            sbComando.AppendLine("WHERE Cod_Cli = @Cod_Cli ");

            SqlCommand cmd = new SqlCommand(sbComando.ToString(),conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Cod_Cli", cli.Cod_Cli);
      
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return retorno;
        }


    }
}


using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Model;
using System.Linq;

namespace DAO
{
    public class LoginDAO : DAO
    {
        public Login Logar(Login lg)
        {
            Login objLoginRetorno = new Login();

            List<Login> lslLogin = new List<Login>();

            SqlConnection conn = new SqlConnection(this.connectionString);

            StringBuilder sbComando = new StringBuilder();
            sbComando.AppendLine("SELECT lg.login");
            sbComando.AppendLine("      ,lg.senha");
            sbComando.AppendLine("      ,cli.Cod_Cli");
            sbComando.AppendLine("      ,cli.Nome_Cli ");
            sbComando.AppendLine("      ,cli.Data_CadCli");
            sbComando.AppendLine("      ,cli.Renda_Cli");
            sbComando.AppendLine("      ,cli.Sexo_Cli");
            sbComando.AppendLine("      ,cli.Cod_TipoCli");
            sbComando.AppendLine("      ,tp.Nome_TipoCli");
            sbComando.AppendLine("FROM Login lg");
            sbComando.AppendLine("INNER JOIN Cliente cli on cli.Cod_Cli = lg.Cod_Cli");
            sbComando.AppendLine("INNER JOIN TipoCli tp on tp.Cod_TipoCli = cli.Cod_TipoCli");
            sbComando.AppendLine("WHERE login = '" + lg.strLogin + "'");
            sbComando.AppendLine("AND senha = '" + lg.Senha + "'");

            SqlCommand objCmd = new SqlCommand(sbComando.ToString(), conn);
            objCmd.CommandType = CommandType.Text;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    SqlDataReader sr = objCmd.ExecuteReader();

                    while (sr.Read())
                    {
                        Login objLogin = new Login();
                        objLogin.Cliente = new Cliente();
                        objLogin.Cliente.TipoCli = new TipoCliente();
                        int iConvert = 0;
                        double dConvert = 0;
                        DateTime dtConvert = DateTime.MinValue;


                        if (sr["login"] != null)
                        {
                            objLogin.strLogin = sr["login"].ToString();
                        }

                        if (sr["senha"] != null)
                        {
                            objLogin.Senha = sr["senha"].ToString();
                        }

                        if (sr["Cod_Cli"] != null)
                        {
                            objLogin.Cliente.Cod_Cli = int.TryParse(sr["Cod_Cli"].ToString(), out iConvert) ? iConvert : 0;
                        }
                        if (sr["Nome_TipoCli"] != null)
                        {
                            objLogin.Cliente.TipoCli.Nome_TipoCli = sr["Nome_TipoCli"].ToString();
                        }
                        if (sr["Data_CadCli"] != null)
                        {
                            objLogin.Cliente.Data_CadCli = DateTime.TryParse(sr["Data_CadCli"].ToString(), out dtConvert) ? dtConvert : DateTime.MaxValue;
                        }
                        if (sr["Renda_Cli"] != null)
                        {
                            objLogin.Cliente.Renda_Cli = double.TryParse(sr["Renda_Cli"].ToString(), out dConvert) ? dConvert : 0;
                        }
                        if (sr["Sexo_Cli"] != null)
                        {
                            objLogin.Cliente.Sexo_Cli = sr["Sexo_Cli"].ToString();
                        }
                        if (sr["Cod_TipoCli"] != null)
                        {
                            objLogin.Cliente.TipoCli.Cod_TipoCli = int.TryParse(sr["Cod_TipoCli"].ToString(), out iConvert) ? iConvert : 0;
                        }

                        lslLogin.Add(objLogin);
                    }

                    objLoginRetorno = lslLogin.FirstOrDefault();
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
            return objLoginRetorno;
        }


        public Login RetornarLoginPorCliente(Login lg)
        {
            Login objLoginRetorno = new Login();

            List<Login> lslLogin = new List<Login>();

            SqlConnection conn = new SqlConnection(this.connectionString);

            StringBuilder sbComando = new StringBuilder();
            sbComando.AppendLine("SELECT lg.login");
            sbComando.AppendLine("      ,lg.senha");
            sbComando.AppendLine("      ,cli.Cod_Cli");
            sbComando.AppendLine("      ,cli.Nome_Cli ");
            sbComando.AppendLine("      ,cli.Data_CadCli");
            sbComando.AppendLine("      ,cli.Renda_Cli");
            sbComando.AppendLine("      ,cli.Sexo_Cli");
            sbComando.AppendLine("      ,cli.Cod_TipoCli");
            sbComando.AppendLine("      ,tp.Nome_TipoCli");
            sbComando.AppendLine("FROM Login lg");
            sbComando.AppendLine("INNER JOIN Cliente cli on cli.Cod_Cli = lg.Cod_Cli");
            sbComando.AppendLine("INNER JOIN TipoCli tp on tp.Cod_TipoCli = cli.Cod_TipoCli");
            sbComando.AppendLine("WHERE lg.Cod_Cli = " + lg.Cliente.Cod_Cli );

            SqlCommand objCmd = new SqlCommand(sbComando.ToString(), conn);
            objCmd.CommandType = CommandType.Text;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    SqlDataReader sr = objCmd.ExecuteReader();

                    while (sr.Read())
                    {
                        Login objLogin = new Login();
                        objLogin.Cliente = new Cliente();
                        objLogin.Cliente.TipoCli = new TipoCliente();
                        int iConvert = 0;
                        double dConvert = 0;
                        DateTime dtConvert = DateTime.MinValue;


                        if (sr["login"] != null)
                        {
                            objLogin.strLogin = sr["login"].ToString();
                        }

                        if (sr["senha"] != null)
                        {
                            objLogin.Senha = sr["senha"].ToString();
                        }

                        if (sr["Cod_Cli"] != null)
                        {
                            objLogin.Cliente.Cod_Cli = int.TryParse(sr["Cod_Cli"].ToString(), out iConvert) ? iConvert : 0;
                        }
                        if (sr["Nome_TipoCli"] != null)
                        {
                            objLogin.Cliente.TipoCli.Nome_TipoCli = sr["Nome_TipoCli"].ToString();
                        }
                        if (sr["Data_CadCli"] != null)
                        {
                            objLogin.Cliente.Data_CadCli = DateTime.TryParse(sr["Data_CadCli"].ToString(), out dtConvert) ? dtConvert : DateTime.MaxValue;
                        }
                        if (sr["Renda_Cli"] != null)
                        {
                            objLogin.Cliente.Renda_Cli = double.TryParse(sr["Renda_Cli"].ToString(), out dConvert) ? dConvert : 0;
                        }
                        if (sr["Sexo_Cli"] != null)
                        {
                            objLogin.Cliente.Sexo_Cli = sr["Sexo_Cli"].ToString();
                        }
                        if (sr["Cod_TipoCli"] != null)
                        {
                            objLogin.Cliente.TipoCli.Cod_TipoCli = int.TryParse(sr["Cod_TipoCli"].ToString(), out iConvert) ? iConvert : 0;
                        }

                        lslLogin.Add(objLogin);
                    }

                    objLoginRetorno = lslLogin.FirstOrDefault();
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
            return objLoginRetorno;
        }


        public Login LogarSemSenha(Login lg)
        {
            Login objLoginRetorno = new Login();

            List<Login> lslLogin = new List<Login>();

            SqlConnection conn = new SqlConnection(this.connectionString);

            StringBuilder sbComando = new StringBuilder();
            sbComando.AppendLine("SELECT lg.login");
            sbComando.AppendLine("      ,lg.senha");
            sbComando.AppendLine("      ,cli.Cod_Cli");
            sbComando.AppendLine("      ,cli.Nome_Cli ");
            sbComando.AppendLine("      ,cli.Data_CadCli");
            sbComando.AppendLine("      ,cli.Renda_Cli");
            sbComando.AppendLine("      ,cli.Sexo_Cli");
            sbComando.AppendLine("      ,cli.Cod_TipoCli");
            sbComando.AppendLine("      ,tp.Nome_TipoCli");
            sbComando.AppendLine("FROM Login lg");
            sbComando.AppendLine("INNER JOIN Cliente cli on cli.Cod_Cli = lg.Cod_Cli");
            sbComando.AppendLine("INNER JOIN TipoCli tp on tp.Cod_TipoCli = cli.Cod_TipoCli");
            sbComando.AppendLine("WHERE login = '" + lg.strLogin + "'");

            SqlCommand objCmd = new SqlCommand(sbComando.ToString(), conn);
            objCmd.CommandType = CommandType.Text;

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    SqlDataReader sr = objCmd.ExecuteReader();

                    while (sr.Read())
                    {
                        Login objLogin = new Login();
                        objLogin.Cliente = new Cliente();
                        objLogin.Cliente.TipoCli = new TipoCliente();
                        int iConvert = 0;
                        double dConvert = 0;
                        DateTime dtConvert = DateTime.MinValue;


                        if (sr["login"] != null)
                        {
                            objLogin.strLogin = sr["login"].ToString();
                        }

                        if (sr["senha"] != null)
                        {
                            objLogin.Senha = sr["senha"].ToString();
                        }

                        if (sr["Cod_Cli"] != null)
                        {
                            objLogin.Cliente.Cod_Cli = int.TryParse(sr["Cod_Cli"].ToString(), out iConvert) ? iConvert : 0;
                        }
                        if (sr["Nome_TipoCli"] != null)
                        {
                            objLogin.Cliente.TipoCli.Nome_TipoCli = sr["Nome_TipoCli"].ToString();
                        }
                        if (sr["Data_CadCli"] != null)
                        {
                            objLogin.Cliente.Data_CadCli = DateTime.TryParse(sr["Data_CadCli"].ToString(), out dtConvert) ? dtConvert : DateTime.MaxValue;
                        }
                        if (sr["Renda_Cli"] != null)
                        {
                            objLogin.Cliente.Renda_Cli = double.TryParse(sr["Renda_Cli"].ToString(), out dConvert) ? dConvert : 0;
                        }
                        if (sr["Sexo_Cli"] != null)
                        {
                            objLogin.Cliente.Sexo_Cli = sr["Sexo_Cli"].ToString();
                        }
                        if (sr["Cod_TipoCli"] != null)
                        {
                            objLogin.Cliente.TipoCli.Cod_TipoCli = int.TryParse(sr["Cod_TipoCli"].ToString(), out iConvert) ? iConvert : 0;
                        }

                        lslLogin.Add(objLogin);
                    }

                    objLoginRetorno = lslLogin.FirstOrDefault();
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
            return objLoginRetorno;
        }

        public int CadastrarLogin(Login lg)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);

            int retorno = 0;

            StringBuilder sbComando = new StringBuilder();
            sbComando.AppendLine("INSERT INTO LOGIN (");
            sbComando.AppendLine("                       Cod_Cli");
            sbComando.AppendLine("                      ,login");
            sbComando.AppendLine("                      ,senha");
            sbComando.AppendLine("                     )");
            sbComando.AppendLine("VALUES (");
            sbComando.AppendLine("        @Cod_Cli");
            sbComando.AppendLine("       ,@login");
            sbComando.AppendLine("       ,@login");
            sbComando.AppendLine("       )");

            SqlCommand cmd = new SqlCommand(sbComando.ToString(), conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Cod_Cli", lg.Cliente.Cod_Cli);
            cmd.Parameters.AddWithValue("@login", lg.strLogin);
            cmd.Parameters.AddWithValue("@login", lg.Senha);
     
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


        public int AtualizarSenha(Login lg)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);

            int retorno = 0;

            StringBuilder sbComando = new StringBuilder();
            sbComando.AppendLine("UPDATE Login ");
            sbComando.AppendLine(" SET ");
            sbComando.AppendLine("    senha = @senha");
                sbComando.AppendLine("WHERE login = @login ");

            SqlCommand cmd = new SqlCommand(sbComando.ToString(), conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@login", lg.strLogin);
            cmd.Parameters.AddWithValue("@senha", lg.Senha);
    
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

        public int ExcluirLogin(Login lg)
        {
            SqlConnection conn = new SqlConnection(this.connectionString);

            int retorno = 0;

            StringBuilder sbComando = new StringBuilder();
            sbComando.AppendLine("DELETE FROM LOGIN ");
            sbComando.AppendLine("WHERE login = @login ");

            SqlCommand cmd = new SqlCommand(sbComando.ToString(), conn);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@login",  lg.strLogin);

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

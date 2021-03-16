using System;
using System.Collections.Generic;
using Model;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAO
{
    public class ProdutoDAO
    {
        const string connectionString = @"Data Source=LAPTOP-B54P23KG;Initial Catalog=INFONEW;Integrated Security=True;";

        public List<Produto> GetProdutos()
        {
            List<Produto> lstProduto = new List<Produto>();
            using (SqlConnection conexao = new SqlConnection(connectionString))
            {
                StringBuilder sbComando = new StringBuilder();
                sbComando.AppendLine("SELECT Cod_Prod");
                sbComando.AppendLine(",Cod_TipoProd");
                sbComando.AppendLine(",Nome_Prod");
                sbComando.AppendLine(",Qtd_EstqProd");
                sbComando.AppendLine(",Val_UnitProd");
                sbComando.AppendLine(",Val_Total");
                sbComando.AppendLine("FROM Produto");

                SqlCommand cmd = new SqlCommand(sbComando.ToString(), conexao);
                cmd.CommandType = CommandType.Text;

                try
                {
                    conexao.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Produto prd = new Produto();

                        int iConvert = 0;
                        double dConvert = 0;
                        if (reader["Cod_Prod"] != null)
                        {
                            prd.Cod_Prod = int.TryParse(reader["Cod_Prod"].ToString(), out iConvert) ? Convert.ToInt32(reader["Cod_Prod"]) : 0;
                        }
                        if (reader["Cod_TipoProd"] != null)
                        {
                            prd.Cod_TipoProd = int.TryParse(reader["Cod_TipoProd"].ToString(), out iConvert) ? Convert.ToInt32(reader["Cod_TipoProd"]) : 0;
                        }

                        prd.Nome_Prod = reader["Nome_Prod"] != null ? reader["Nome_Prod"].ToString() : "Produto inexistente";

                        if (reader["Qtd_EstqProd"] != null)
                        {
                            prd.Qtd_EstqProd = int.TryParse(reader["Qtd_EstqProd"].ToString(), out iConvert) ? Convert.ToInt32(reader["Qtd_EstqProd"]) : 0;
                        }
                        if (reader["Val_UnitProd"] != null)
                        {
                            prd.Val_UnitProd = double.TryParse(reader["Val_UnitProd"].ToString(), out dConvert) ? Convert.ToDouble(reader["Val_UnitProd"]) : 0;
                        }
                        if (reader["Val_Total"] != null)
                        {
                            prd.Val_Total = double.TryParse(reader["Val_Total"].ToString(), out dConvert) ? Convert.ToDouble(reader["Val_Total"]) : 0;
                        }

                        lstProduto.Add(prd);
                    }

                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return lstProduto;
        }

        public int InserirProduto(Produto prd)
        {
            int retorno = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                StringBuilder sbComandoSql = new StringBuilder();
                sbComandoSql.AppendLine(" INSERT INTO Produto(");
                sbComandoSql.AppendLine("Cod_TipoProd");
                sbComandoSql.AppendLine(",Nome_Prod");
                sbComandoSql.AppendLine(",Qtd_EstqProd");
                sbComandoSql.AppendLine(",Val_UnitProd");
                sbComandoSql.AppendLine(")");
                sbComandoSql.AppendLine("VALUES(");
                sbComandoSql.AppendLine("  @Cod_TipoProd");
                sbComandoSql.AppendLine(" ,@Nome_Prod");
                sbComandoSql.AppendLine(" ,@Qtd_EstqProd");
                sbComandoSql.AppendLine(" ,@Val_UnitProd ");
                sbComandoSql.AppendLine(")");


                SqlCommand cmd = new SqlCommand(sbComandoSql.ToString(), con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Cod_TipoProd", prd.Cod_TipoProd);
                cmd.Parameters.AddWithValue("@Nome_Prod", prd.Nome_Prod);
                cmd.Parameters.AddWithValue("@Qtd_EstqProd", prd.Qtd_EstqProd);
                cmd.Parameters.AddWithValue("@Val_UnitProd", prd.Val_UnitProd);

                try
                {
                    con.Open();
                    retorno = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            return retorno;
        }

        public int AtualizarProduto(Produto prd)
        {
            int retorno = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                StringBuilder sbComandoSql = new StringBuilder();
                sbComandoSql.AppendLine(" UPDATE Produto SET");
                sbComandoSql.AppendLine("Cod_TipoProd = @Cod_TipoProd");
                sbComandoSql.AppendLine(",Nome_Prod = @Nome_Prod");
                sbComandoSql.AppendLine(",Qtd_EstqProd = @Qtd_EstqProd");
                sbComandoSql.AppendLine(",Val_UnitProd = @Val_UnitProd");
                sbComandoSql.AppendLine(" WHERE Cod_Prod = @Cod_Prod");
    

                SqlCommand cmd = new SqlCommand(sbComandoSql.ToString(), con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Cod_TipoProd", prd.Cod_TipoProd);
                cmd.Parameters.AddWithValue("@Nome_Prod", prd.Nome_Prod);
                cmd.Parameters.AddWithValue("@Qtd_EstqProd", prd.Qtd_EstqProd);
                cmd.Parameters.AddWithValue("@Val_UnitProd", prd.Val_UnitProd);
                cmd.Parameters.AddWithValue("@Cod_Prod", prd.Cod_Prod);

                try
                {
                    con.Open();
                    retorno = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            return retorno;

        }


        public int ExcluirProduto(int Cod_Produto)
        {
            int retorno = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                StringBuilder sbComandoSql = new StringBuilder();
                sbComandoSql.AppendLine(" DELETE FROM Produto");
                sbComandoSql.AppendLine(" WHERE Cod_Prod = @Cod_Prod");


                SqlCommand cmd = new SqlCommand(sbComandoSql.ToString(), con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Cod_Prod", Cod_Produto);

                try
                {
                    con.Open();
                    retorno = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }
            }
            return retorno;

        }


    }
}

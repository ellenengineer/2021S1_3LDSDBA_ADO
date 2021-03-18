using System;
using Model;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Net.Http.Formatting;

namespace TesteAPIBanco
{
    class Program
    {
        static void Main(string[] args)
        {
            //InserirProduto();
            // AtualizarProduto();
            ExcluirProduto();
            Console.ReadLine();
        }

        static void InserirProduto()
        {
            using (var client = new HttpClient())
            {
                Produto prd = new Produto { Cod_TipoProd = 1, Nome_Prod = "Microondas", Qtd_EstqProd = 4, Val_UnitProd = 500 };
                client.BaseAddress = new Uri("https://localhost:44348/fapen/");
                var response = client.PutAsJsonAsync("produto", prd).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Sucesso ! " + response.ToString());
                }
                else
                    Console.Write("Erro ao inserir produto: " + response.ToString());
            }
        }

        static void AtualizarProduto()
        {
            using (var client = new HttpClient())
            {
                Produto prd = new Produto { Cod_Prod = 11, Cod_TipoProd = 1, Nome_Prod = "Microondas Moderno", Qtd_EstqProd = 10, Val_UnitProd = 550 };
                client.BaseAddress = new Uri("https://localhost:44348/fapen/");
                var response = client.PutAsJsonAsync("produto", prd).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Sucesso ao atualizar produto! " + response.ToString());
                }
                else
                    Console.Write("Erro ao atualizar produto: " + response.ToString());
            }
        }

        static void ExcluirProduto()
        {
            int idProduto = 11;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44348/fapen/");
                var response = client.DeleteAsync("produto/" + idProduto).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Sucesso ao excluir produto! " + response.ToString());
                }
                else
                    Console.Write("Erro ao excluir produto: " + response.ToString());
            }
        }
    }
}

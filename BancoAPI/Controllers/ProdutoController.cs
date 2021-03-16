using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using DAO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BancoAPI.Controllers
{
    [Route("fapen/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        // GET: api/<ProdutoController>
        [HttpGet]
        public List<Produto> Get()
        {
            ProdutoDAO objProdDAO = new ProdutoDAO();

            List<Produto> lstProduto = objProdDAO.GetProdutos();

            return lstProduto.OrderBy(p => p.Nome_Prod).ToList();
        }

        [HttpGet]
        [Route("QtdeProduto")]
        public double QtdeProduto()
        {
            ProdutoDAO objProdDAO = new ProdutoDAO();

            List<Produto> lstProduto = objProdDAO.GetProdutos();

            return lstProduto.Count();
        }

        [HttpGet]
        [Route("MediaValorProduto")]
        public double MediaValorProduto()
        {
            ProdutoDAO objProdDAO = new ProdutoDAO();

            List<Produto> lstProduto = objProdDAO.GetProdutos();

            return lstProduto.Average(p=>p.Val_Total);
        }

        [HttpGet]
        [Route("ProdutoCaro")]
        public double ProdutoCaro()
        {
            ProdutoDAO objProdDAO = new ProdutoDAO();

            List<Produto> lstProduto = objProdDAO.GetProdutos();

            return lstProduto.Max(p => p.Val_UnitProd);
        }

        [HttpGet]
        [Route("ProdutoBarato")]
        public double ProdutoBarato()
        {
            ProdutoDAO objProdDAO = new ProdutoDAO();

            List<Produto> lstProduto = objProdDAO.GetProdutos();

            return lstProduto.Min(p => p.Val_UnitProd);
        }

        // GET api/<ProdutoController>/5
        [HttpGet("{codProd}")]
       
        public Produto Get(int codProd)
        {
            ProdutoDAO objProdDAO = new ProdutoDAO();

            List<Produto> lstProduto = objProdDAO.GetProdutos();

            List<Produto> lstPRodWhere = lstProduto.Where(p => p.Cod_Prod == codProd).ToList();

            Produto prd = lstPRodWhere.FirstOrDefault();
            return prd;
        }

        // POST api/<ProdutoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProdutoController>/5
        [HttpPut]
        public ActionResult<IEnumerable<string>> Put([FromBody] Produto prd)
        {
            int retorno = 0;
            if (prd.Cod_Prod < 1)
            {
                ProdutoDAO objDAO = new ProdutoDAO();
                retorno = objDAO.InserirProduto(prd);
            }
            else
            {
                ProdutoDAO objDAO = new ProdutoDAO();
                retorno = objDAO.AtualizarProduto(prd);
            }
            if (retorno == 1)
            {
                return new string[] { "Produto Inserido ou atualizado com sucesso!" };
            }
            return new string[] { "Produto Não inserido ou atualizado!" };
        }

        [HttpDelete("{codProduto}")]
        public ActionResult<IEnumerable<string>> Delete(int codProduto)
        {
            int retorno = 0;
            if (codProduto == 0)
            { return new string[] { "Produto invalido!" }; }
            else
            {
                ProdutoDAO prdDao = new ProdutoDAO();
                retorno = prdDao.ExcluirProduto(codProduto);
                if (retorno == 1)
                {
                    return new string[] { "Produto excluido!" };
                }

            }
            return new string[] { string.Empty };
        }
    }
}

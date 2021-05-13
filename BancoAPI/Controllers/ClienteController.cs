using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using DAO;

namespace BancoAPI.Controllers
{
    [Route("fapen/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
       [HttpGet]
        public List<Cliente> RetornarClientes()
        {
            ClienteDAO objDao = new ClienteDAO();
           return objDao.RetornarListaCliente();
        }

        [HttpGet("{codCli}")]
        public Cliente Details(int codCli)
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            return clienteDAO.RetornarCliente(codCli);
        }

        [HttpPut]
        public ActionResult<IEnumerable<string>> Put([FromBody] Cliente cli)
        {
            int retorno = 0;
            ClienteDAO clienteDAO = new ClienteDAO();
            if (cli.Cod_Cli < 1)
            {
                retorno = clienteDAO.CadastrarCliente(cli);
            }
            else
            {
                retorno = clienteDAO.AtualizarCliente(cli);
            }
            if (retorno == 1)
            {
                return new string[] { "Cliente Inserido ou atualizado com sucesso!" };
            }
            return new string[] { "Cliente Não inserido ou não atualizado!" };
        }

        [HttpDelete("{codCli}")]
        public ActionResult<IEnumerable<string>> Delete(int codCli)
        {
            int retorno = 0;
            if (codCli == 0)
            { return new string[] { "Cliente invalido!" }; }
            else
            {
                ClienteDAO clienteDAO = new ClienteDAO();
                Cliente cli = new Cliente();
                cli.Cod_Cli = codCli;

                retorno = clienteDAO.ApagarCliente(cli);
                if (retorno == 1)
                {
                    return new string[] { "Cliente excluido com sucesso!" };
                }

            }
            return new string[] { string.Empty };
        }




    }
}

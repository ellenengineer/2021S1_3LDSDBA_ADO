using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAO;
using Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BancoAPI.Controllers
{
    [Route("fapen/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        [HttpGet("{login}")]
        public Login Get(string login)
        {
            LoginDAO objDao = new LoginDAO();

            Login lg = new Login();
            lg.strLogin = login;
            return objDao.LogarSemSenha(lg); ;
        }



        // POST api/<LoginController>
        [HttpPost]
        public Login Post([FromBody] Login value)
        {
            Login retorno = null;
            if(value != null)
            {
                LoginDAO objDao = new LoginDAO();

                retorno = objDao.Logar(value);

            }

            return retorno;

        }

        [HttpPut]
        public ActionResult<IEnumerable<string>> Put([FromBody] Login value)
        {
            int retorno = 0;

            LoginDAO objDao = new LoginDAO();
            Login lg = objDao.RetornarLoginPorCliente(value);


            if (lg == null)
            {
                retorno = objDao.CadastrarLogin(value);
            }
            else
            {
                retorno = objDao.AtualizarSenha(value);
            }
            if (retorno == 1)
            {
                return new string[] { "Login Inserido ou atualizado com sucesso!" };
            }
            return new string[] { "Login Não inserido ou atualizado!" };
        }

        [HttpDelete("{login}")]
        public ActionResult<IEnumerable<string>> Delete(string login)
        {
            int retorno = 0;
            if (string.IsNullOrEmpty(login))
            { return new string[] { "Login invalido!" }; }
            else
            {
                LoginDAO objDAO = new LoginDAO();
                Login lg = new Login();
                lg.strLogin = login;

                retorno = objDAO.ExcluirLogin(lg);
                if (retorno == 1)
                {
                    return new string[] { "Login excluido com sucesso!" };
                }

            }
            return new string[] { string.Empty };
        }
    }
}

using Newtonsoft.Json.Linq;
using IaziServerWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IaziServerWeb.Controllers
{
    public class ClienteServicoController : ApiController
    {
        [Route("agenda/addcliser")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage CadastroEmpresa([FromBody]JObject model)
        {
            try
            {
                dynamic json = model["Horario"];
                var idUsuario = 0;
                var idCliente = 0;
                string dataServico = json.dataServico;
                idUsuario = json.idUsuario;

                DBContext db = new DBContext();
                var query = from user in db.Usuario
                            where user.idUsuario == idUsuario
                            select user.idCliente;
                foreach (var x in query)
                {
                    idCliente = x;
                }
                ClienteServico cs = new ClienteServico()
                {
                    dataServico = Convert.ToDateTime(json.dataServico),
                    idEmpresaClienteServico = json.idEmpresaCliente,
                    valorServico = json.valorServico,
                    idCliente = idCliente
                };
                db.Database.CreateIfNotExists();
                db.ClienteServico.Add(cs);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Cadastro realizado.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }
    }
}

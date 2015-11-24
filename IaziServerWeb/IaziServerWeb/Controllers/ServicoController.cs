using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IaziServerWeb.Models;

namespace IaziServerWeb.Controllers
{
    public class ServicoController : ApiController
    {
        private DBContext db = new DBContext();
        [Route("api/addservico")]
        [HttpPost]
        public HttpResponseMessage CadastroServico(Servico c)
        {
            try
            {
                db.Database.CreateIfNotExists();
                db.Servico.Add(c);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Cadastro do servico " + c.nomeServico + " realizado.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }
    }
}

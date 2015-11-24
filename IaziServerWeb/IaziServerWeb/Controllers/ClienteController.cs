using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IaziServerWeb.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace IaziServerWeb.Controllers
{
    //[Authorize]
    //public class ValuesController : ApiController
    //{
    //    public HttpResponseMessage Get(int id) { ... }
    //    public HttpResponseMessage Post() { ... }
    //}

    public class ClienteController : ApiController
    {

        // GET: api/values
        [Route("api/values")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
            return new string[] { "value1", "value2" };
        }

        [Route("api/addclient")]
        [HttpPost]
        public HttpResponseMessage CadastroCliente(JObject model)
        {
            try
            {
                //System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                //var data = js.Deserialize<Cliente>(dataCliente);
                var cliente = new Cliente();
                var usuario = new Usuario();
                DBContext db = new DBContext();

                db.Database.CreateIfNotExists();

                dynamic json = model;
                cliente = json.Cliente.ToObject<Cliente>();
                db.Cliente.Add(cliente);
                db.SaveChanges();

                usuario.cliente = cliente;
                usuario.cliente.idCliente = cliente.idCliente;
                usuario.roleUsuario = "user";
                usuario.senhaUsuario = json.Password.ToString();
                db.Usuario.Add(usuario);
                db.SaveChanges();


                var passRetorno = Simple.Encrypt(usuario.senhaUsuario);

                return Request.CreateResponse(HttpStatusCode.OK, new { usuario.idUsuario, usuario.roleUsuario, passRetorno });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }


        [Route("api/getclient")]
        public HttpResponseMessage GetCliente()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }

    }
}
/*
exemplo de envio: (RAW)

{
    "password":"minhasenhamalucacriptografada",
    "cliente": {
        "nomeCliente": "nomeCliente",
        "sobrenomeCliente": "sobrenomeCliente",
        "telefoneCliente": "telefoneCliente",
        "emailCliente": "emailCliente",
        "cidadeCliente": "cidadeCliente",
        "estadoCliente": "estadoCliente",
        "geoLatitudeCliente": "geoLatitudeCliente",
        "geoLongitudeCliente": "geoLongitudeCliente"
    }
}

*/

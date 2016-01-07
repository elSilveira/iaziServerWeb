using IaziServerWeb.Bomo;
using IaziServerWeb.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IaziServerWeb.Controllers
{
    public class BomoController : ApiController
    {
        [Route("bomoApi/addClient")]
        [HttpPost]
        public HttpResponseMessage CadastroCliente([FromBody]JObject model)
        {
            try
            {
                //System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                //var data = js.Deserialize<Cliente>(dataCliente);
                var cliente = new BomoCliente();
                DBContext db = new DBContext();
                dynamic json = model;
                cliente = json.BomoCliente.ToObject<BomoCliente>();
                if (cliente.cnh == null) cliente.cnh = "";
                db.BomoCliente.Add(cliente);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new { cliente.idBomoCliente });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("bomoApi/addEmpresa")]
        [HttpPost]
        public HttpResponseMessage CadastroEmpresa([FromBody]JObject model)
        {
            try
            {
                //System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                //var data = js.Deserialize<Cliente>(dataCliente);
                BomoEmpresa empresa = new BomoEmpresa();
                DBContext db = new DBContext();
                dynamic json = model;
                empresa = json.BomoEmpresa.ToObject<BomoEmpresa>();
                db.BomoEmpresa.Add(empresa);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new { empresa.idBomoEmpresa });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("bomoApi/addUsuario")]
        [HttpPost]
        public HttpResponseMessage CadastroUsuario([FromBody]JObject model)
        {
            try
            {
                //System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                //var data = js.Deserialize<Cliente>(dataCliente);
                dynamic json = model;

                var usuario = new BomoUsuario();
                var cliente = new BomoCliente();
                var bomoEmpresa = new BomoEmpresa();
                int idEmpresa = 0;
                idEmpresa = json["idEmpresa"];
                string email = json["email"];
                string senha = json["senha"]; 
                DBContext db = new DBContext();
                cliente = (from cli in db.BomoCliente where cli.email == email select cli).First();
                usuario.senha = Simple.Encrypt(senha);
                usuario.bomoCliente = cliente;
                if (idEmpresa > 0)
                {
                   bomoEmpresa = (from emp in db.BomoEmpresa where emp.idBomoEmpresa == idEmpresa select emp).First();
                
                    usuario.bomoEmpresa = bomoEmpresa;
                }
                db.BomoUsuario.Add(usuario);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new { usuario.idBomoUsuario });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("bomoApi/getUsuario")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage GetUsuario([FromBody]JObject modelUser)
        {
            try
            {
                //System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                //var data = js.Deserialize<Cliente>(dataCliente);
                dynamic json = modelUser;

                BomoUsuario usuario = new BomoUsuario();
                string email = "";
                email = json["emailUsuario"];
                DBContext db = new DBContext();
                usuario = (from usu in db.BomoUsuario where usu.bomoCliente.email == email select usu).First();

                return Request.CreateResponse(HttpStatusCode.OK, new { usuario });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("bomoApi/sendContato")]
        [HttpPost]
        public HttpResponseMessage SendContato([FromBody]JObject modelContato)
        {
            try
            {
                //System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                //var data = js.Deserialize<Cliente>(dataCliente);
                dynamic json = modelContato;

                BomoContato contato = new BomoContato();
                contato = json.BomoContato.ToObject<BomoContato>();
                DBContext db = new DBContext();
                db.BomoContato.Add(contato);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("bomoApi/addLancamento")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage AddLancamento([FromBody]JObject model)
        {
            try
            {
                dynamic json = model["Lancamento"];
                DBContext db = new DBContext();
                int idEmpresa = 0; idEmpresa = json.idEmpresa;
                var dataVenda = DateTime.Now;
                double valorVenda = json.valorVenda;
                var cnh = "";
                cnh = json.cnh;
                BomoCliente cliente = new BomoCliente();

                var query = from cli in db.BomoCliente
                               where cli.cnh == cnh
                               select cli;
                foreach(BomoCliente bc in query)
                {
                    cliente = bc;
                }
                if (cliente.idBomoCliente < 1)
                {
                    cliente = new BomoCliente();
                    cliente.cnh = cnh;
                    db.BomoCliente.Add(cliente);
                    db.SaveChanges();
                }

                BomoLancamentos lanc = new BomoLancamentos();
                lanc.BomoCliente = db.BomoCliente.Single(x => x.idBomoCliente == cliente.idBomoCliente);
                lanc.BomoEmpresa = db.BomoEmpresa.Single(x => x.idBomoEmpresa == idEmpresa);
                lanc.dataVenda = dataVenda;
                lanc.valorVenda = valorVenda;
                db.BomoLancamentos.Add(lanc);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }
    }
}

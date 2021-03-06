﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IaziServerWeb.Models;
using Newtonsoft.Json.Linq;

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
        int cont = 0;
        // GET: api/values
        [Route("api/values")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            DBContext db = new DBContext();
            return new string[] {cont++ + ""};
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

        [Route("login")]
        public HttpResponseMessage Login([FromBody]JObject model)
        {
            try
            {
                dynamic json = model;
                var usuario = "";
                var senha = "";
                usuario = json.emailCliente;
                senha = json.senhaUsuario;
                DBContext db = new DBContext();
                var query = from u in db.Usuario where u.cliente.emailCliente == usuario
                            && u.senhaUsuario == senha
                            select u;
                Usuario usu = new Usuario();
                foreach(Usuario u in query)
                {
                    usu = u;
                    usu.senhaUsuario = Simple.Encrypt(u.senhaUsuario);
                }
                return Request.CreateResponse(HttpStatusCode.OK, new { usu });
            }
            catch(Exception er)
            {
               return Request.CreateResponse(HttpStatusCode.BadRequest, er.Message);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IaziServerWeb.Models;
using Newtonsoft.Json.Linq;
using System.Timers;
using System.Net.NetworkInformation;

namespace IaziServerWeb.Controllers
{
    public class CategoriaController : ApiController
    {
        System.Timers.Timer aTimer;
        // Specify what you want to happen when the Elapsed event is raised.
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Uri url = new Uri("http://iazi-com-br.umbler.net");
            string pingurl = string.Format("{0}", url.Host);
            string host = pingurl;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 10000);
                if (reply.Status == IPStatus.Success)
                    System.Console.WriteLine("Ping True.");
            }
            catch (Exception err) { };
        }

        [Route("api/pingOff")]
        [HttpGet]
        public HttpResponseMessage StopPing()
        {
            aTimer.Enabled = false;

            System.Console.WriteLine("Ping Stop.");

            return Request.CreateResponse(HttpStatusCode.OK);

        }

        [Route("api/pingOn")]
        [HttpGet]
        public HttpResponseMessage StartPing()
        {
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 10000;
            aTimer.Enabled = true;

            System.Console.WriteLine("Ping Start.");

            Uri url = new Uri("http://iazi-com-br.umbler.net");
            string pingurl = string.Format("{0}", url.Host);
            string host = pingurl;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 10000);
                if (reply.Status == IPStatus.Success)
                    System.Console.WriteLine("Ping True.");
            }
            catch (Exception err) { };
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("servicos/listCategoria")]
        [HttpGet]
        [Authorize]
        public HttpResponseMessage ListarCategorias([FromBody]JObject model)
        {
            try
            {
                List<Categoria> categorias = new List<Categoria>();
                using(DBContext db = new DBContext())
                {
                    db.Database.CreateIfNotExists();
                    var query = from c in db.Categoria select c;
                                
                    foreach (Categoria cat in query)
                    {
                        categorias.Add(cat);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, categorias);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [Route("addcategoria")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public HttpResponseMessage CadastroCategoria(Categoria c)
        {
            try
            {
                using (DBContext db = new DBContext())
                {
                    db.Database.CreateIfNotExists();
                    db.Categoria.Add(c);
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Cadastro da categoria " + c.nomeCategoria + " realizado.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);

            }

        }
    }
}

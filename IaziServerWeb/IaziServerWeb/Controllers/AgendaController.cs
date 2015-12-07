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
    public class AgendaController : ApiController
    {
        [Route("agenda/listAgenda")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage ListarAgenda([FromBody]JObject model)
        {
            try
            {
                dynamic json = model["Funcionario"];
                int idCliente = 0;
                int ano, mes, dia;
                if (json != null)
                {
                    List<Agenda> agendaProfissional = new List<Agenda>();
                    idCliente = json.idUsuario;
                    DBContext db = new DBContext();
                    ano = json.ano; mes = json.mes; dia = json.dia;

                    var query = from a in db.Agenda
                                where a.empresaCliente.idEmpresaCliente == idCliente
                                && a.horarioAgenda.Year == ano
                                && a.horarioAgenda.Month == mes + 1
                                && a.horarioAgenda.Day == dia
                                select a;
                    foreach (Agenda a in query)
                    {
                        agendaProfissional.Add(a);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, agendaProfissional);
                }
                else
                {

                    List<ClienteServico> agendaProfissional = new List<ClienteServico>();
                    json = model["Cliente"];
                    idCliente = json.idUsuario;
                    DBContext db = new DBContext();
                    ano = json.ano; mes = json.mes; dia = json.dia;

                    var query = from cs in db.ClienteServico
                                join u in db.Usuario on cs.cliente.idCliente equals idCliente
                                where cs.cliente.idCliente == idCliente
                                && cs.statusServico == 1
                                && cs.dataServico.Year == ano
                                && cs.dataServico.Month == mes + 1
                                && cs.dataServico.Day == dia
                                select cs;
                    foreach (ClienteServico a in query)
                    {
                        agendaProfissional.Add(a);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, agendaProfissional);

                }            
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}

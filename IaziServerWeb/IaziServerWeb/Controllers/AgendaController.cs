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
                int idFuncionario = 0;
                int ano = json.ano, mes = json.mes, dia = json.dia;
                //DateTime data = new DateTime(ano, mes+1, dia);
                idFuncionario = json.idEmpresaCliente;
                List<Agenda> agendaProfissional = new List<Agenda>();
                DBContext db = new DBContext();

                var query = from a in db.Agenda
                            where a.empresaCliente.idEmpresaCliente == idFuncionario
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
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}

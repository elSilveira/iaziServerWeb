using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IaziServerWeb.Models;
using Newtonsoft.Json.Linq;
using System.Data.Common;
using Newtonsoft.Json;
using System.IO;

namespace IaziServerWeb.Controllers
{

    public class EmpresaController : ApiController
    {
        public struct Profissional
        {
            public string nomeCliente;
            public string sobrenomeCliente;
            public string especializacaoCliente;
            public string especializacao2Cliente;
            public string especializacao3Cliente;
            public int idEmpresaCliente;

            public Profissional(string x, string y, string z, string e, string s, int p)
            {
                this.nomeCliente = x;
                this.sobrenomeCliente = y;
                this.especializacaoCliente = z;
                this.especializacao2Cliente = e;
                this.especializacao3Cliente = s;
                this.idEmpresaCliente = p;
            }
        }


        [Route("empresas/listFuncionarios")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage ListarFuncionariosEmpresa([FromBody]JObject model)
        {

            try
            {
                dynamic json = model["servicos"];
                //List<EmpresaServico> listaServicos = json;
                List<Profissional> listProfissional = new List<Profissional>();
                //List<int> listaId = json.ListServicosSelecionados;
                DBContext db = new DBContext();
                string empresas = json;
                List<string> lista = empresas.Split(',').ToList();

                foreach (string id in lista)
                {
                    var idEmpresa = Convert.ToInt32(id);
                    var query = from ecs in db.EmpresaClienteServico
                                join ec in db.EmpresaCliente on ecs.idEmpresaCliente equals ec.idEmpresaCliente
                                join c in db.Cliente on ec.idCliente equals c.idCliente
                                where ecs.idEmpresaServico == idEmpresa
                                select new
                                {
                                    nomeCliente = c.nomeCliente,
                                    sobrenomeCliente = c.sobrenomeCliente,
                                    especializacaoCliente = ec.especializacaoCliente,
                                    especializacao2Cliente = ec.especializacao2Cliente,
                                    especializacao3Cliente = ec.especializacao3Cliente,
                                    idEmpresaCliente = ecs.idEmpresaClienteServico

                                };
                    IEnumerable<Profissional> cart = from ca in query.AsEnumerable()
                                                     select new Profissional(
                                                         ca.nomeCliente,
                                                         ca.sobrenomeCliente,
                                                         ca.especializacaoCliente,
                                                         ca.especializacao2Cliente,
                                                         ca.especializacao3Cliente,
                                                         ca.idEmpresaCliente
                                                     );

                    foreach (Profissional x in cart)
                    {
                        var exist = false;
                        foreach (Profissional p in listProfissional)
                            if (x.nomeCliente == p.nomeCliente) exist = true;
                        if(!exist) listProfissional.Add(x);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, listProfissional);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("empresas/listServicos")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage ListarServicosEmpresa([FromBody]JObject model)
        {
            try
            {
                List<EmpresaServico> servicos = new List<EmpresaServico>();
                dynamic json = model;
                int id = 0;
                id = json.idEmpresaServico;
                DBContext db = new DBContext();
                var query = from es in db.EmpresaServico
                            join s in db.Servico on es.idServico equals s.idServico
                            orderby s.idCategoria, s.nomeServico
                            where es.idEmpresa == id
                            select es;

                foreach (EmpresaServico es in query)
                {
                    servicos.Add(es);
                }

                return Request.CreateResponse(HttpStatusCode.OK, servicos);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [Route("empresas/listEmpresas")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage ListarCategorias([FromBody]JObject model)
        {
            try
            {
                List<Empresa> empresas = new List<Empresa>();
                dynamic json = model;
                using (DBContext db = new DBContext())
                {

                    var query = db.Empresa.SqlQuery("select e.* from Empresas e " +
                        " where e.idEmpresa in ( " +
                        "     select es.idEmpresa from EmpresaServico es where " +
                        "     es.idServico in (select s.idServico from Servicos s where idCategoria = " + json.idCategoria + ")) " +
                        "     and e.cidadeEmpresa = (Select cli.cidadeCliente from Clientes cli " +
                        "         where cli.idCliente = (select u.idCliente from Usuarios u where idUsuario = " + json.idUsuario + ")) " +
                        "     and e.estadoEmpresa = (Select cli.estadoCliente from Clientes cli " +
                        "         where cli.idCliente = (select u.idCliente from Usuarios u where idUsuario = " + json.idUsuario + ")) " +
                        "     order by e.bairroEmpresa ");
                    foreach (Empresa e in query)
                    {
                        empresas.Add(e);
                    }
                    foreach (Empresa e in empresas)
                    {
                        db.SaveChanges();
                        var query2 = (from s in db.Servico
                                      join es in db.EmpresaServico on e.idEmpresa equals es.idEmpresa
                                      where s.idServico == es.idServico
                                      select s.tipoServico).Distinct();
                        string tipoServico = "";
                        foreach (string tipo in query2)
                        {
                            tipoServico += tipo + " ";
                        }
                        e.tipoServico = tipoServico;

                    }


                }
                return Request.CreateResponse(HttpStatusCode.OK, empresas);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/addempresa")]
        [HttpPost]
        [Authorize]
        public HttpResponseMessage CadastroEmpresa(Empresa c)
        {
            try
            {
                using (DBContext db = new DBContext())
                {

                    db.Database.CreateIfNotExists();
                    db.Empresa.Add(c);
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Cadastro da empresa " + c.nomeEmpresa + " realizado.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }

        [Route("api/empaddservico")]
        [HttpPost]
        public HttpResponseMessage AddCategoria(EmpresaServico c)
        {
            try
            {
                using (DBContext db = new DBContext())
                {
                    db.Database.CreateIfNotExists();
                    db.EmpresaServico.Add(c);
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Cadastro do servico " + c.servico.nomeServico + " na empresa " + c.empresa.nomeEmpresa + " realizado.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }

        [Route("api/empaddempregado")]
        [HttpPost]
        public HttpResponseMessage AddEmpregado(EmpresaCliente c)
        {
            try
            {
                using (DBContext db = new DBContext())
                {
                    db.Database.CreateIfNotExists();
                    db.EmpresaCliente.Add(c);
                    db.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.OK, "Cadastro do servico " + c.cliente.nomeCliente + " na empresa " + c.empresa.nomeEmpresa + " realizado.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);

            }
        }
    }
}

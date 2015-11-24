using IaziServerWeb.Migrations;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IaziServerWeb.Models
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class DBContext : DbContext
    {

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Servico> Servico { get; set; }
        public virtual DbSet<Agenda> Agenda { get; set; }
        public virtual DbSet<EmpresaServico> EmpresaServico { get; set; }
        public virtual DbSet<EmpresaCliente> EmpresaCliente { get; set; }
        public virtual DbSet<ClienteServico> ClienteServico { get; set; }
        public virtual DbSet<EmpresaClienteServico> EmpresaClienteServico { get; set; }

        public DBContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DBContext, Configuration>());
        }
    }
}

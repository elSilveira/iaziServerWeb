namespace IaziServerWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agenda",
                c => new
                    {
                        idAgenda = c.Int(nullable: false, identity: true),
                        horarioAgenda = c.DateTime(nullable: false, precision: 0),
                        infoHorario = c.Int(nullable: false),
                        empresaCliente_idEmpresaCliente = c.Int(),
                    })
                .PrimaryKey(t => t.idAgenda)
                .ForeignKey("dbo.EmpresaCliente", t => t.empresaCliente_idEmpresaCliente)
                .Index(t => t.empresaCliente_idEmpresaCliente);
            
            CreateTable(
                "dbo.EmpresaCliente",
                c => new
                    {
                        idEmpresaCliente = c.Int(nullable: false, identity: true),
                        tipoCliente = c.Int(nullable: false),
                        especializacaoCliente = c.String(maxLength: 150, storeType: "nvarchar"),
                        especializacao2Cliente = c.String(maxLength: 150, storeType: "nvarchar"),
                        especializacao3Cliente = c.String(maxLength: 150, storeType: "nvarchar"),
                        cliente_idCliente = c.Int(),
                        empresa_idEmpresa = c.Int(),
                    })
                .PrimaryKey(t => t.idEmpresaCliente)
                .ForeignKey("dbo.Clientes", t => t.cliente_idCliente)
                .ForeignKey("dbo.Empresas", t => t.empresa_idEmpresa)
                .Index(t => t.cliente_idCliente)
                .Index(t => t.empresa_idEmpresa);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        idCliente = c.Int(nullable: false, identity: true),
                        nomeCliente = c.String(maxLength: 150, storeType: "nvarchar"),
                        sobrenomeCliente = c.String(maxLength: 150, storeType: "nvarchar"),
                        telefoneCliente = c.String(maxLength: 150, storeType: "nvarchar"),
                        emailCliente = c.String(maxLength: 150, storeType: "nvarchar"),
                        cidadeCliente = c.String(maxLength: 150, storeType: "nvarchar"),
                        estadoCliente = c.String(maxLength: 150, storeType: "nvarchar"),
                        geoLatitudeCliente = c.String(maxLength: 150, storeType: "nvarchar"),
                        geoLongitudeCliente = c.String(maxLength: 150, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.idCliente)
                .Index(t => t.emailCliente, unique: true);
            
            CreateTable(
                "dbo.Empresas",
                c => new
                    {
                        idEmpresa = c.Int(nullable: false, identity: true),
                        nomeEmpresa = c.String(maxLength: 150, storeType: "nvarchar"),
                        cidadeEmpresa = c.String(maxLength: 150, storeType: "nvarchar"),
                        estadoEmpresa = c.String(maxLength: 150, storeType: "nvarchar"),
                        ruaEmpresa = c.String(maxLength: 150, storeType: "nvarchar"),
                        bairroEmpresa = c.String(maxLength: 150, storeType: "nvarchar"),
                        geoLatitudeEmpresa = c.String(maxLength: 150, storeType: "nvarchar"),
                        geoLongitudeEmpresa = c.String(maxLength: 150, storeType: "nvarchar"),
                        cepEmpresa = c.String(maxLength: 150, storeType: "nvarchar"),
                        numeroEmpresa = c.Int(nullable: false),
                        imagemEmpresa = c.String(maxLength: 150, storeType: "nvarchar"),
                        infoEmpresa = c.String(maxLength: 150, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.idEmpresa);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        idCategoria = c.Int(nullable: false, identity: true),
                        nomeCategoria = c.String(maxLength: 150, storeType: "nvarchar"),
                        iconeCategoria = c.String(maxLength: 150, storeType: "nvarchar"),
                        infoCategoria = c.String(maxLength: 150, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.idCategoria);
            
            CreateTable(
                "dbo.ClienteServico",
                c => new
                    {
                        idCliServ = c.Int(nullable: false, identity: true),
                        statusServico = c.Int(nullable: false),
                        valorServico = c.Double(nullable: false),
                        dataServico = c.DateTime(precision: 0),
                        dataAlternativa = c.DateTime(precision: 0),
                        dataResposta = c.DateTime(precision: 0),
                        cliente_idCliente = c.Int(),
                        empresaCliServ_idEmpresaCliServ = c.Int(),
                    })
                .PrimaryKey(t => t.idCliServ)
                .ForeignKey("dbo.Clientes", t => t.cliente_idCliente)
                .ForeignKey("dbo.EmpresaCliServ", t => t.empresaCliServ_idEmpresaCliServ)
                .Index(t => t.cliente_idCliente)
                .Index(t => t.empresaCliServ_idEmpresaCliServ);
            
            CreateTable(
                "dbo.EmpresaCliServ",
                c => new
                    {
                        idEmpresaCliServ = c.Int(nullable: false, identity: true),
                        empresaCliente_idEmpresaCliente = c.Int(),
                        empresaServico_idEmpresaServico = c.Int(),
                    })
                .PrimaryKey(t => t.idEmpresaCliServ)
                .ForeignKey("dbo.EmpresaCliente", t => t.empresaCliente_idEmpresaCliente)
                .ForeignKey("dbo.EmpresaServico", t => t.empresaServico_idEmpresaServico)
                .Index(t => t.empresaCliente_idEmpresaCliente)
                .Index(t => t.empresaServico_idEmpresaServico);
            
            CreateTable(
                "dbo.EmpresaServico",
                c => new
                    {
                        idEmpresaServico = c.Int(nullable: false, identity: true),
                        tempoServico = c.Int(nullable: false),
                        valorServico = c.Double(nullable: false),
                        empresa_idEmpresa = c.Int(),
                        servico_idServico = c.Int(),
                    })
                .PrimaryKey(t => t.idEmpresaServico)
                .ForeignKey("dbo.Empresas", t => t.empresa_idEmpresa)
                .ForeignKey("dbo.Servicos", t => t.servico_idServico)
                .Index(t => t.empresa_idEmpresa)
                .Index(t => t.servico_idServico);
            
            CreateTable(
                "dbo.Servicos",
                c => new
                    {
                        idServico = c.Int(nullable: false, identity: true),
                        nomeServico = c.String(maxLength: 150, storeType: "nvarchar"),
                        tipoServico = c.String(maxLength: 150, storeType: "nvarchar"),
                        categoria_idCategoria = c.Int(),
                    })
                .PrimaryKey(t => t.idServico)
                .ForeignKey("dbo.Categorias", t => t.categoria_idCategoria)
                .Index(t => t.categoria_idCategoria);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        idUsuario = c.Int(nullable: false, identity: true),
                        senhaUsuario = c.String(maxLength: 200, storeType: "nvarchar"),
                        roleUsuario = c.String(maxLength: 200, storeType: "nvarchar"),
                        cliente_idCliente = c.Int(),
                    })
                .PrimaryKey(t => t.idUsuario)
                .ForeignKey("dbo.Clientes", t => t.cliente_idCliente)
                .Index(t => t.cliente_idCliente);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "cliente_idCliente", "dbo.Clientes");
            DropForeignKey("dbo.ClienteServico", "empresaCliServ_idEmpresaCliServ", "dbo.EmpresaCliServ");
            DropForeignKey("dbo.EmpresaCliServ", "empresaServico_idEmpresaServico", "dbo.EmpresaServico");
            DropForeignKey("dbo.EmpresaServico", "servico_idServico", "dbo.Servicos");
            DropForeignKey("dbo.Servicos", "categoria_idCategoria", "dbo.Categorias");
            DropForeignKey("dbo.EmpresaServico", "empresa_idEmpresa", "dbo.Empresas");
            DropForeignKey("dbo.EmpresaCliServ", "empresaCliente_idEmpresaCliente", "dbo.EmpresaCliente");
            DropForeignKey("dbo.ClienteServico", "cliente_idCliente", "dbo.Clientes");
            DropForeignKey("dbo.Agenda", "empresaCliente_idEmpresaCliente", "dbo.EmpresaCliente");
            DropForeignKey("dbo.EmpresaCliente", "empresa_idEmpresa", "dbo.Empresas");
            DropForeignKey("dbo.EmpresaCliente", "cliente_idCliente", "dbo.Clientes");
            DropIndex("dbo.Usuarios", new[] { "cliente_idCliente" });
            DropIndex("dbo.Servicos", new[] { "categoria_idCategoria" });
            DropIndex("dbo.EmpresaServico", new[] { "servico_idServico" });
            DropIndex("dbo.EmpresaServico", new[] { "empresa_idEmpresa" });
            DropIndex("dbo.EmpresaCliServ", new[] { "empresaServico_idEmpresaServico" });
            DropIndex("dbo.EmpresaCliServ", new[] { "empresaCliente_idEmpresaCliente" });
            DropIndex("dbo.ClienteServico", new[] { "empresaCliServ_idEmpresaCliServ" });
            DropIndex("dbo.ClienteServico", new[] { "cliente_idCliente" });
            DropIndex("dbo.Clientes", new[] { "emailCliente" });
            DropIndex("dbo.EmpresaCliente", new[] { "empresa_idEmpresa" });
            DropIndex("dbo.EmpresaCliente", new[] { "cliente_idCliente" });
            DropIndex("dbo.Agenda", new[] { "empresaCliente_idEmpresaCliente" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Servicos");
            DropTable("dbo.EmpresaServico");
            DropTable("dbo.EmpresaCliServ");
            DropTable("dbo.ClienteServico");
            DropTable("dbo.Categorias");
            DropTable("dbo.Empresas");
            DropTable("dbo.Clientes");
            DropTable("dbo.EmpresaCliente");
            DropTable("dbo.Agenda");
        }
    }
}

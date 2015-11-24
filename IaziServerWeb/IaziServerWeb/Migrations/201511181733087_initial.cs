namespace ServerTeste2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.EmpresaServicos", newName: "EmpresaServico");
            DropForeignKey("dbo.Agenda", "idCliente", "dbo.Clientes");
            DropForeignKey("dbo.Agenda", "idEmpresa", "dbo.Empresas");
            DropIndex("dbo.Agenda", new[] { "idEmpresa" });
            DropIndex("dbo.Agenda", new[] { "idCliente" });
            DropIndex("dbo.Clientes", new[] { "emailCliente" });
            CreateTable(
                "dbo.ClienteServico",
                c => new
                    {
                        idClienteServico = c.Int(nullable: false, identity: true),
                        idCliente = c.Int(nullable: false),
                        idEmpresaClienteServico = c.Int(nullable: false),
                        statusServico = c.Int(nullable: false),
                        valorServico = c.Double(nullable: false),
                        dataServico = c.DateTime(precision: 0),
                        dataAlternativa = c.DateTime(precision: 0),
                        dataResposta = c.DateTime(precision: 0),
                    })
                .PrimaryKey(t => t.idClienteServico)
                .ForeignKey("dbo.Clientes", t => t.idCliente, cascadeDelete: true)
                .ForeignKey("dbo.EmpresaClienteServico", t => t.idEmpresaClienteServico, cascadeDelete: true)
                .Index(t => t.idCliente)
                .Index(t => t.idEmpresaClienteServico);
            
            CreateTable(
                "dbo.EmpresaClienteServico",
                c => new
                    {
                        idEmpresaClienteServico = c.Int(nullable: false, identity: true),
                        idEmpresaCliente = c.Int(nullable: false),
                        idEmpresaServico = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idEmpresaClienteServico)
                .ForeignKey("dbo.EmpresaCliente", t => t.idEmpresaCliente, cascadeDelete: true)
                .ForeignKey("dbo.EmpresaServico", t => t.idEmpresaServico, cascadeDelete: true)
                .Index(t => t.idEmpresaCliente)
                .Index(t => t.idEmpresaServico);
            
            AddColumn("dbo.Agenda", "idEmpresaCliente", c => c.Int(nullable: false));
            AddColumn("dbo.Agenda", "infoHorario", c => c.Int(nullable: false));
            AddColumn("dbo.Empresas", "imagemEmpresa", c => c.String(unicode: false));
            AddColumn("dbo.Empresas", "infoEmpresa", c => c.String(unicode: false));
            AddColumn("dbo.Categorias", "iconeCategoria", c => c.String(unicode: false));
            AddColumn("dbo.Categorias", "infoCategoria", c => c.String(unicode: false));
            AddColumn("dbo.EmpresaCliente", "especializacaoCliente", c => c.String(unicode: false));
            AddColumn("dbo.EmpresaCliente", "especializacao2Cliente", c => c.String(unicode: false));
            AddColumn("dbo.EmpresaCliente", "especializacao3Cliente", c => c.String(unicode: false));
            AddColumn("dbo.Servicos", "tipoServico", c => c.String(unicode: false));
            AlterColumn("dbo.Agenda", "horarioAgenda", c => c.DateTime(nullable: false, precision: 0));
            AlterColumn("dbo.Clientes", "nomeCliente", c => c.String(unicode: false));
            AlterColumn("dbo.Clientes", "sobrenomeCliente", c => c.String(unicode: false));
            AlterColumn("dbo.Clientes", "telefoneCliente", c => c.String(unicode: false));
            AlterColumn("dbo.Clientes", "emailCliente", c => c.String(unicode: false));
            AlterColumn("dbo.Clientes", "cidadeCliente", c => c.String(unicode: false));
            AlterColumn("dbo.Clientes", "estadoCliente", c => c.String(unicode: false));
            AlterColumn("dbo.Clientes", "geoLatitudeCliente", c => c.String(unicode: false));
            AlterColumn("dbo.Clientes", "geoLongitudeCliente", c => c.String(unicode: false));
            AlterColumn("dbo.Empresas", "nomeEmpresa", c => c.String(unicode: false));
            AlterColumn("dbo.Empresas", "cidadeEmpresa", c => c.String(unicode: false));
            AlterColumn("dbo.Empresas", "estadoEmpresa", c => c.String(unicode: false));
            AlterColumn("dbo.Empresas", "ruaEmpresa", c => c.String(unicode: false));
            AlterColumn("dbo.Empresas", "bairroEmpresa", c => c.String(unicode: false));
            AlterColumn("dbo.Empresas", "geoLatitudeEmpresa", c => c.String(unicode: false));
            AlterColumn("dbo.Empresas", "geoLongitudeEmpresa", c => c.String(unicode: false));
            AlterColumn("dbo.Empresas", "cepEmpresa", c => c.String(unicode: false));
            AlterColumn("dbo.Categorias", "nomeCategoria", c => c.String(unicode: false));
            AlterColumn("dbo.Servicos", "nomeServico", c => c.String(unicode: false));
            CreateIndex("dbo.Agenda", "idEmpresaCliente");
            CreateIndex("dbo.Clientes", "emailCliente", unique: true);
            AddForeignKey("dbo.Agenda", "idEmpresaCliente", "dbo.EmpresaCliente", "idEmpresaCliente", cascadeDelete: true);
            DropColumn("dbo.Agenda", "idEmpresa");
            DropColumn("dbo.Agenda", "idCliente");
            DropColumn("dbo.Agenda", "idFuncionario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Agenda", "idFuncionario", c => c.Int(nullable: false));
            AddColumn("dbo.Agenda", "idCliente", c => c.Int(nullable: false));
            AddColumn("dbo.Agenda", "idEmpresa", c => c.Int(nullable: false));
            DropForeignKey("dbo.ClienteServico", "idEmpresaClienteServico", "dbo.EmpresaClienteServico");
            DropForeignKey("dbo.EmpresaClienteServico", "idEmpresaServico", "dbo.EmpresaServico");
            DropForeignKey("dbo.EmpresaClienteServico", "idEmpresaCliente", "dbo.EmpresaCliente");
            DropForeignKey("dbo.ClienteServico", "idCliente", "dbo.Clientes");
            DropForeignKey("dbo.Agenda", "idEmpresaCliente", "dbo.EmpresaCliente");
            DropIndex("dbo.EmpresaClienteServico", new[] { "idEmpresaServico" });
            DropIndex("dbo.EmpresaClienteServico", new[] { "idEmpresaCliente" });
            DropIndex("dbo.ClienteServico", new[] { "idEmpresaClienteServico" });
            DropIndex("dbo.ClienteServico", new[] { "idCliente" });
            DropIndex("dbo.Clientes", new[] { "emailCliente" });
            DropIndex("dbo.Agenda", new[] { "idEmpresaCliente" });
            AlterColumn("dbo.Servicos", "nomeServico", c => c.String(nullable: false, maxLength: 150, storeType: "nvarchar"));
            AlterColumn("dbo.Categorias", "nomeCategoria", c => c.String(nullable: false, maxLength: 150, storeType: "nvarchar"));
            AlterColumn("dbo.Empresas", "cepEmpresa", c => c.String(maxLength: 10, storeType: "nvarchar"));
            AlterColumn("dbo.Empresas", "geoLongitudeEmpresa", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.Empresas", "geoLatitudeEmpresa", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.Empresas", "bairroEmpresa", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.Empresas", "ruaEmpresa", c => c.String(maxLength: 150, storeType: "nvarchar"));
            AlterColumn("dbo.Empresas", "estadoEmpresa", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.Empresas", "cidadeEmpresa", c => c.String(maxLength: 100, storeType: "nvarchar"));
            AlterColumn("dbo.Empresas", "nomeEmpresa", c => c.String(nullable: false, maxLength: 150, storeType: "nvarchar"));
            AlterColumn("dbo.Clientes", "geoLongitudeCliente", c => c.String(maxLength: 150, storeType: "nvarchar"));
            AlterColumn("dbo.Clientes", "geoLatitudeCliente", c => c.String(maxLength: 150, storeType: "nvarchar"));
            AlterColumn("dbo.Clientes", "estadoCliente", c => c.String(maxLength: 150, storeType: "nvarchar"));
            AlterColumn("dbo.Clientes", "cidadeCliente", c => c.String(maxLength: 150, storeType: "nvarchar"));
            AlterColumn("dbo.Clientes", "emailCliente", c => c.String(nullable: false, maxLength: 150, storeType: "nvarchar"));
            AlterColumn("dbo.Clientes", "telefoneCliente", c => c.String(nullable: false, maxLength: 25, storeType: "nvarchar"));
            AlterColumn("dbo.Clientes", "sobrenomeCliente", c => c.String(nullable: false, maxLength: 150, storeType: "nvarchar"));
            AlterColumn("dbo.Clientes", "nomeCliente", c => c.String(nullable: false, maxLength: 150, storeType: "nvarchar"));
            AlterColumn("dbo.Agenda", "horarioAgenda", c => c.DateTime(nullable: false));
            DropColumn("dbo.Servicos", "tipoServico");
            DropColumn("dbo.EmpresaCliente", "especializacao3Cliente");
            DropColumn("dbo.EmpresaCliente", "especializacao2Cliente");
            DropColumn("dbo.EmpresaCliente", "especializacaoCliente");
            DropColumn("dbo.Categorias", "infoCategoria");
            DropColumn("dbo.Categorias", "iconeCategoria");
            DropColumn("dbo.Empresas", "infoEmpresa");
            DropColumn("dbo.Empresas", "imagemEmpresa");
            DropColumn("dbo.Agenda", "infoHorario");
            DropColumn("dbo.Agenda", "idEmpresaCliente");
            DropTable("dbo.EmpresaClienteServico");
            DropTable("dbo.ClienteServico");
            CreateIndex("dbo.Clientes", "emailCliente", unique: true);
            CreateIndex("dbo.Agenda", "idCliente");
            CreateIndex("dbo.Agenda", "idEmpresa");
            AddForeignKey("dbo.Agenda", "idEmpresa", "dbo.Empresas", "idEmpresa", cascadeDelete: true);
            AddForeignKey("dbo.Agenda", "idCliente", "dbo.Clientes", "idCliente", cascadeDelete: true);
            RenameTable(name: "dbo.EmpresaServico", newName: "EmpresaServicos");
        }
    }
}

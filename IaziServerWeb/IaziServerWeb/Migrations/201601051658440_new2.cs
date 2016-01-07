namespace IaziServerWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BomoUsuario",
                c => new
                    {
                        idBomoUsuario = c.Int(nullable: false, identity: true),
                        senha = c.String(maxLength: 150, storeType: "nvarchar"),
                        bomoCliente_idBomoCliente = c.Int(),
                        bomoEmpresa_idBomoEmpresa = c.Int(),
                    })
                .PrimaryKey(t => t.idBomoUsuario)
                .ForeignKey("dbo.BomoCliente", t => t.bomoCliente_idBomoCliente)
                .ForeignKey("dbo.BomoEmpresa", t => t.bomoEmpresa_idBomoEmpresa)
                .Index(t => t.bomoCliente_idBomoCliente)
                .Index(t => t.bomoEmpresa_idBomoEmpresa);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BomoUsuario", "bomoEmpresa_idBomoEmpresa", "dbo.BomoEmpresa");
            DropForeignKey("dbo.BomoUsuario", "bomoCliente_idBomoCliente", "dbo.BomoCliente");
            DropIndex("dbo.BomoUsuario", new[] { "bomoEmpresa_idBomoEmpresa" });
            DropIndex("dbo.BomoUsuario", new[] { "bomoCliente_idBomoCliente" });
            DropTable("dbo.BomoUsuario");
        }
    }
}

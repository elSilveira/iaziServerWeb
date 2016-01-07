namespace IaziServerWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BomoUsuario",
                c => new
                    {
                        idBomoUsuario = c.Int(nullable: false, identity: true),
                        usuario = c.String(maxLength: 150, storeType: "nvarchar"),
                        senha = c.String(maxLength: 150, storeType: "nvarchar"),
                        bomoCliente_idBomoCliente = c.Int(),
                        bomoEmpresa_idBomoEmpresa = c.Int(),
                    })
                .PrimaryKey(t => t.idBomoUsuario);
            
            CreateIndex("dbo.BomoUsuario", "bomoEmpresa_idBomoEmpresa");
            CreateIndex("dbo.BomoUsuario", "bomoCliente_idBomoCliente");
            AddForeignKey("dbo.BomoUsuario", "bomoEmpresa_idBomoEmpresa", "dbo.BomoEmpresa", "idBomoEmpresa");
            AddForeignKey("dbo.BomoUsuario", "bomoCliente_idBomoCliente", "dbo.BomoCliente", "idBomoCliente");
        }
    }
}

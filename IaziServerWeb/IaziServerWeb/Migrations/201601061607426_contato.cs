namespace IaziServerWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contato : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BomoContato",
                c => new
                    {
                        idBomoContato = c.Int(nullable: false, identity: true),
                        email = c.String(maxLength: 150, storeType: "nvarchar"),
                        nome = c.String(maxLength: 150, storeType: "nvarchar"),
                        assunto = c.String(maxLength: 150, storeType: "nvarchar"),
                        mensagem = c.String(maxLength: 150, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.idBomoContato);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BomoContato");
        }
    }
}
